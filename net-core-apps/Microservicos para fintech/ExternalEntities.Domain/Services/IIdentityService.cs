namespace ExternalEntities.Domain.Services
{
    public interface IIdentityService
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string UserTenant { get; set; }
        public string ReqIpAddress { get; set; }
        public bool? IsTempUser { get; set; }
        public bool IsAdmin { get; set; }
        public string AccessToken { get; set; }
        string GetCurrentUserId();
        string GetCurrentUserEmail();
        string GetCurrentUserName();
    }
}
