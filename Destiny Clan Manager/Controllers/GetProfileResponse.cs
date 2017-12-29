using System;

namespace Destiny_Clan_Manager.Controllers
{
    internal class GetProfileResponse
    {

        public Response response { get; set; }
        public int ErrorCode { get; set; }
        public int ThrottleSeconds { get; set; }
        public string ErrorStatus { get; set; }
        public string Message { get; set; }
        public Messagedata MessageData { get; set; }

        public class Response
        {
            public Profile profile { get; set; }
            public Itemcomponents itemComponents { get; set; }
        }

        public class Profile
        {
            public Data data { get; set; }
            public int privacy { get; set; }
        }

        public class Data
        {
            public Userinfo userInfo { get; set; }
            public DateTime dateLastPlayed { get; set; }
            public int versionsOwned { get; set; }
            public string[] characterIds { get; set; }
        }

        public class Userinfo
        {
            public int membershipType { get; set; }
            public string membershipId { get; set; }
            public string displayName { get; set; }
        }

        public class Itemcomponents
        {
        }

        public class Messagedata
        {
        }

    }
}