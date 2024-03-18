using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using Notifications.Domain.Services;

namespace Notifications.Infraestructure.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public int UserId { get; set; }
        public string Name { get; set; }
        public string UserTenant { get; set; }
        public bool? IsTempUser { get; set; }
        public bool IsAdmin { get; set; }
        public string ReqIpAddress { get; set; }
        public string AccessToken { get; set; }

        public IdentityService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            UserId = Convert.ToInt32(GetCurrentUserId());
            Name = GetCurrentUserName();
            IsTempUser = _httpContextAccessor?.HttpContext?
                .User?.IsInRole("Temporary");
            IsAdmin = _httpContextAccessor?.HttpContext?
                .User?.IsInRole("SysAdmin") ?? false;
            ReqIpAddress = httpContextAccessor?.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? string.Empty;
            AccessToken = httpContextAccessor?.HttpContext?.Request?.Headers["Authorization"].FirstOrDefault()?.Replace("Bearer", "");
        }

        public string GetCurrentUserId() =>
            _httpContextAccessor?
                .HttpContext?
                .User?
                .Identities?
                .FirstOrDefault()?.Claims?.FirstOrDefault(e => e.Type == "sub")?.Value;
        public string GetCurrentUserEmail() =>
            _httpContextAccessor?
                .HttpContext?
                .User?
                .Identities?
                .FirstOrDefault()?.Claims?.FirstOrDefault(e => e.Type == "email")?.Value;
        public string GetCurrentUserName() =>
            _httpContextAccessor?
                .HttpContext?
                .User?
                .Identities?
                .FirstOrDefault()?.Claims.FirstOrDefault(e => e.Type == "name")?.Value;
    }
}
