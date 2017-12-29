using System;

namespace Destiny_Clan_Manager.Controllers
{
    internal class GetMembershipsResponse
    {
        public Response response { get; set; }
        public int ErrorCode { get; set; }
        public int ThrottleSeconds { get; set; }
        public string ErrorStatus { get; set; }
        public string Message { get; set; }
        public Messagedata MessageData { get; set; }


        public class Response
        {
            public Destinymembership[] destinyMemberships { get; set; }
            public Bungienetuser bungieNetUser { get; set; }
        }

        public class Bungienetuser
        {
            public string membershipId { get; set; }
            public string uniqueName { get; set; }
            public string displayName { get; set; }
            public int profilePicture { get; set; }
            public int profileTheme { get; set; }
            public int userTitle { get; set; }
            public string successMessageFlags { get; set; }
            public bool isDeleted { get; set; }
            public string about { get; set; }
            public DateTime firstAccess { get; set; }
            public DateTime lastUpdate { get; set; }
            public Context context { get; set; }
            public bool showActivity { get; set; }
            public string locale { get; set; }
            public bool localeInheritDefault { get; set; }
            public bool showGroupMessaging { get; set; }
            public string profilePicturePath { get; set; }
            public string profileThemeName { get; set; }
            public string userTitleDisplay { get; set; }
            public string statusText { get; set; }
            public DateTime statusDate { get; set; }
            public string blizzardDisplayName { get; set; }
        }

        public class Context
        {
            public bool isFollowing { get; set; }
            public Ignorestatus ignoreStatus { get; set; }
        }

        public class Ignorestatus
        {
            public bool isIgnored { get; set; }
            public int ignoreFlags { get; set; }
        }

        public class Destinymembership
        {
            public string iconPath { get; set; }
            public int membershipType { get; set; }
            public string membershipId { get; set; }
            public string displayName { get; set; }
        }

        public class Messagedata
        {
        }

    }
}