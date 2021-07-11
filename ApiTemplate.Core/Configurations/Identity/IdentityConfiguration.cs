using ApiTemplate.Common.Enums.DateTimes;
using ApiTemplate.Common.Markers.Configurations;

namespace ApiTemplate.Core.Configurations.Identity
{
    public class IdentityConfiguration : IAppSetting
    {
        public bool PasswordRequireDigit { get; set; }
        public bool PasswordRequireLowercase { get; set; }
        public bool PasswordRequireNonAlphanumeric { get; set; }
        public bool PasswordRequireUppercase { get; set; }
        public bool LockoutAllowedForNewUsers { get; set; }
        public bool UserRequireUniqueEmail { get; set; }
        public int PasswordRequiredUniqueChars { get; set; }
        public int PasswordMinLength { get; set; }
        public int LockoutMaxFailedAccessAttempts { get; set; }
        public string UserAllowedUserNameCharacters { get; set; }
        public int LockoutDefaultLockoutTimeSpan { get; set; }
        public Period LockoutType { get; set; }
    }
}
