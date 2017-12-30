using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Destiny_Clan_Manager.Controllers
{
    public class ManagerController : Controller
    {
        string APIBaseURL = ConfigurationManager.AppSettings["APIBaseURL"];
        string APIKey = ConfigurationManager.AppSettings["API_KEY"];
        public ActionResult Index()
        {
            if (Request.Form.Count > 0)
            {
                foreach (string item in Request.Form)
                {
                    if (item.IndexOf("kick") == 0)
                    {
                        string kickId = item.Remove(0, "kick".Length);
                        try
                        {
                            GetGroupMembersResponse.Result[] groupMembers = (GetGroupMembersResponse.Result[])Session["GroupMembers"];
                            KickPlayer(kickId, (string)Session["groupId"], (string)Session["OAUTHToken"], (int)Session["membershipType"], ref groupMembers);
                            Session["GroupMembers"] = groupMembers;
                        }
                        catch (Exception ex)
                        {
                            Trace.TraceError("Exception reached", ex);
                            continue;
                        }
                    }
                }
            }
            if (Session["GroupMembers"] != null)
                return View();
            if (Session["OAUTHToken"] == null)
                return RedirectToAction("Login", "Home");
            WebClient wc = new WebClient();

            string OAUTHCode = Session["OAUTHToken"]?.ToString();
            string membershipId = Session["MembershipID"]?.ToString();
            string pathGetMembershipsForUser = "/User/GetMembershipsForCurrentUser/";
            wc.Headers["X-API-Key"] = APIKey;
            wc.Headers["Authorization"] = "Bearer " + OAUTHCode;
            var response = wc.DownloadString(APIBaseURL + pathGetMembershipsForUser);
            GetMembershipsResponse jResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<GetMembershipsResponse>(response);
            Session["destinyMembershipId"] = jResponse.response.destinyMemberships[0]?.membershipId;
            Trace.TraceInformation("Found user " + jResponse.response.destinyMemberships[0]?.displayName + " logging in.");
            Session["DisplayName"] = jResponse.response.destinyMemberships[0]?.displayName;
            Session["membershipType"] = jResponse.response.destinyMemberships[0]?.membershipType;
            string destinyMembershipId = Session["destinyMembershipId"].ToString();
            string GetGroupPath = "/GroupV2/User/" + Session["membershipType"] + "/" + destinyMembershipId + "/All/Clan/";
            response = wc.DownloadString(APIBaseURL + GetGroupPath);
            GetGroupResponse groupResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<GetGroupResponse>(response);
            //TODO: Set up for multiple platforms.
            Trace.TraceInformation("Got group id " + groupResponse.response.results[0]?.group.groupId + " named " + groupResponse.response.results[0]?.group.name);
            string groupId;
            Session["GroupID"] = groupId = groupResponse.response.results[0]?.group.groupId;
            string getGroupMembersPath = "/GroupV2/" + groupId + "/members/";
            response = wc.DownloadString(APIBaseURL + getGroupMembersPath);
            GetGroupMembersResponse groupMembersResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<GetGroupMembersResponse>(response);
            Session["GroupMembers"] = groupMembersResponse.response.results;
            return View();
        }

        public static DateTime getLastPlayed(HttpSessionStateBase Session, string destinyMembershipId, int membershipType)
        {
            Trace.TraceInformation("Getting last played for Destiny Member: " + destinyMembershipId);
            string APIBaseURL = ConfigurationManager.AppSettings["APIBaseURL"];
            string APIKey = ConfigurationManager.AppSettings["API_KEY"];

            string ProfilePath = "/Destiny2/" + membershipType + "/Profile/" + destinyMembershipId + "/?components=profiles";
            string OAUTHCode = Session["OAUTHToken"]?.ToString();
            string membershipId = Session["MembershipID"]?.ToString();
            WebClient wc = new WebClient();
            wc.Headers["X-API-Key"] = APIKey;
            wc.Headers["Authorization"] = "Bearer " + OAUTHCode;

            var response = wc.DownloadString(APIBaseURL + ProfilePath);
            GetProfileResponse ProfileResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<GetProfileResponse>(response);
            try
            {
                return ProfileResponse.response.profile.data.dateLastPlayed;
            }
            catch
            {
                return DateTime.MinValue;
            }
        }
        // GET: Manager/Authorize
        public ActionResult Authorize()
        {
            if (Request.QueryString == null)
            {
                Response.Redirect(Url.Content("~/Manager/Index"));
                return View();
            }
            else
            {
                string authCode;
                if (string.IsNullOrWhiteSpace(authCode = Request.QueryString.Get("code")))
                {
                    Response.Redirect(Url.Content("~/"));
                    return View();
                }
                else
                {
                    string baseTokenUrl = "https://www.bungie.net/platform/app/oauth/token/";
                    WebClient wc = new WebClient();
                    wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    wc.Headers["X-API-Key"] = ConfigurationManager.AppSettings["API_KEY"];
                    string urlEncodedBody = "client_id=" + ConfigurationManager.AppSettings["client_id"] + "&grant_type=authorization_code&code=" + authCode;
                    string response = string.Empty;
                    try
                    {
                        response = wc.UploadString(baseTokenUrl, urlEncodedBody);
                    }
                    catch (HttpException ex)
                    {
                        Trace.TraceError("Exception thrown: " + ex);
                        return RedirectToAction("Error", "Shared");
                    }
                    catch (WebException ex)
                    {
                        Trace.TraceError("Exception thrown: " + ex);
                        return RedirectToAction("Error", "Shared");
                    }
                    TokenResponse parsedResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<TokenResponse>(response);
                    if (string.IsNullOrWhiteSpace(parsedResponse.membership_id) || string.IsNullOrWhiteSpace(parsedResponse.access_token))
                    {
                        return RedirectToAction("Error", "Shared");
                    }

                    Session["OAUTHToken"] = parsedResponse.access_token;
                    Session["MembershipID"] = parsedResponse.membership_id;
                    Response.Redirect("~/Manager/Index");
                }
            }
            return View();
        }

        public static void KickPlayer(string kickId, string groupId, string OauthToken, int membershipType, ref GetGroupMembersResponse.Result[] GroupMembers)
        {
            WebClient wc = new WebClient();
            wc.Headers["X-API-Key"] = ConfigurationManager.AppSettings["API_KEY"];
            wc.Headers["Authorization"] = "Bearer " + OauthToken;

            string base_url = ConfigurationManager.AppSettings["APIBaseURL"];
            string KickPath = "/GroupV2/" + groupId + "/Members/" + membershipType + "/" + kickId + "/Kick/";
            try
            {
                wc.DownloadString(base_url + KickPath);
            } catch
            {
                return;
            }
            List<GetGroupMembersResponse.Result> listofMembers = new List<GetGroupMembersResponse.Result>(GroupMembers);
            foreach (var member in listofMembers)
            {
                if (member.destinyUserInfo.membershipId == kickId)
                {
                    listofMembers.Remove(member);
                    break;
                }
            }

            GroupMembers = listofMembers.ToArray();
        }
    }
}