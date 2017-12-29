using System;

namespace Destiny_Clan_Manager.Controllers
{
    public class GetGroupMembersResponse
    {
        public Response response { get; set; }
        public int ErrorCode { get; set; }
        public int ThrottleSeconds { get; set; }
        public string ErrorStatus { get; set; }
        public string Message { get; set; }
        public Messagedata MessageData { get; set; }

        public class Response
        {
            public Result[] results { get; set; }
            public int totalResults { get; set; }
            public bool hasMore { get; set; }
            public Query query { get; set; }
            public bool useTotalResults { get; set; }
        }

        public class Query
        {
            public int itemsPerPage { get; set; }
            public int currentPage { get; set; }
        }

        public class Result
        {
            public int memberType { get; set; }
            public bool isOnline { get; set; }
            public string groupId { get; set; }
            public Destinyuserinfo destinyUserInfo { get; set; }
            public Bungienetuserinfo bungieNetUserInfo { get; set; }
            public DateTime joinDate { get; set; }
        }

        public class Destinyuserinfo
        {
            public string iconPath { get; set; }
            public int membershipType { get; set; }
            public string membershipId { get; set; }
            public string displayName { get; set; }
        }

        public class Bungienetuserinfo
        {
            public string supplementalDisplayName { get; set; }
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