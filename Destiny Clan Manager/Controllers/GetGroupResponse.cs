using System;

namespace Destiny_Clan_Manager.Controllers
{
    internal class GetGroupResponse
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
            public Member member { get; set; }
            public Group group { get; set; }
        }

        public class Member
        {
            public int memberType { get; set; }
            public bool isOnline { get; set; }
            public string groupId { get; set; }
            public Destinyuserinfo destinyUserInfo { get; set; }
            public DateTime joinDate { get; set; }
        }

        public class Destinyuserinfo
        {
            public string iconPath { get; set; }
            public int membershipType { get; set; }
            public string membershipId { get; set; }
            public string displayName { get; set; }
        }

        public class Group
        {
            public string groupId { get; set; }
            public string name { get; set; }
            public int groupType { get; set; }
            public string membershipIdCreated { get; set; }
            public DateTime creationDate { get; set; }
            public DateTime modificationDate { get; set; }
            public string about { get; set; }
            public object[] tags { get; set; }
            public int memberCount { get; set; }
            public bool isPublic { get; set; }
            public bool isPublicTopicAdminOnly { get; set; }
            public string primaryAlliedGroupId { get; set; }
            public string motto { get; set; }
            public bool allowChat { get; set; }
            public bool isDefaultPostPublic { get; set; }
            public int chatSecurity { get; set; }
            public string locale { get; set; }
            public int avatarImageIndex { get; set; }
            public int homepage { get; set; }
            public int membershipOption { get; set; }
            public int defaultPublicity { get; set; }
            public string theme { get; set; }
            public string bannerPath { get; set; }
            public string avatarPath { get; set; }
            public bool isAllianceOwner { get; set; }
            public string conversationId { get; set; }
            public bool enableInvitationMessagingForAdmins { get; set; }
            public DateTime banExpireDate { get; set; }
            public Features features { get; set; }
            public Claninfo clanInfo { get; set; }
        }

        public class Features
        {
            public int maximumMembers { get; set; }
            public int maximumMembershipsOfGroupType { get; set; }
            public int capabilities { get; set; }
            public int[] membershipTypes { get; set; }
            public bool invitePermissionOverride { get; set; }
            public bool updateCulturePermissionOverride { get; set; }
            public int hostGuidedGamePermissionOverride { get; set; }
            public bool updateBannerPermissionOverride { get; set; }
            public int joinLevel { get; set; }
        }

        public class Claninfo
        {
            public D2clanprogressions d2ClanProgressions { get; set; }
            public string clanCallsign { get; set; }
            public Clanbannerdata clanBannerData { get; set; }
        }

        public class D2clanprogressions
        {
            public _584850370 _584850370 { get; set; }
            public _1273404180 _1273404180 { get; set; }
            public _3759191272 _3759191272 { get; set; }
            public _3381682691 _3381682691 { get; set; }
        }

        public class _584850370
        {
            public int progressionHash { get; set; }
            public int dailyProgress { get; set; }
            public int dailyLimit { get; set; }
            public int weeklyProgress { get; set; }
            public int weeklyLimit { get; set; }
            public int currentProgress { get; set; }
            public int level { get; set; }
            public int levelCap { get; set; }
            public int stepIndex { get; set; }
            public int progressToNextLevel { get; set; }
            public int nextLevelAt { get; set; }
        }

        public class _1273404180
        {
            public int progressionHash { get; set; }
            public int dailyProgress { get; set; }
            public int dailyLimit { get; set; }
            public int weeklyProgress { get; set; }
            public int weeklyLimit { get; set; }
            public int currentProgress { get; set; }
            public int level { get; set; }
            public int levelCap { get; set; }
            public int stepIndex { get; set; }
            public int progressToNextLevel { get; set; }
            public int nextLevelAt { get; set; }
        }

        public class _3759191272
        {
            public long progressionHash { get; set; }
            public int dailyProgress { get; set; }
            public int dailyLimit { get; set; }
            public int weeklyProgress { get; set; }
            public int weeklyLimit { get; set; }
            public int currentProgress { get; set; }
            public int level { get; set; }
            public int levelCap { get; set; }
            public int stepIndex { get; set; }
            public int progressToNextLevel { get; set; }
            public int nextLevelAt { get; set; }
        }

        public class _3381682691
        {
            public long progressionHash { get; set; }
            public int dailyProgress { get; set; }
            public int dailyLimit { get; set; }
            public int weeklyProgress { get; set; }
            public int weeklyLimit { get; set; }
            public int currentProgress { get; set; }
            public int level { get; set; }
            public int levelCap { get; set; }
            public int stepIndex { get; set; }
            public int progressToNextLevel { get; set; }
            public int nextLevelAt { get; set; }
        }

        public class Clanbannerdata
        {
            public long decalId { get; set; }
            public long decalColorId { get; set; }
            public long decalBackgroundColorId { get; set; }
            public int gonfalonId { get; set; }
            public long gonfalonColorId { get; set; }
            public int gonfalonDetailId { get; set; }
            public long gonfalonDetailColorId { get; set; }
        }

        public class Messagedata
        {
        }

    }
}