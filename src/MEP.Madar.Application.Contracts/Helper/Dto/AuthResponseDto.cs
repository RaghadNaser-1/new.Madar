using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace MEP.Madar.Helper.Dto
{
    public class AuthResponseDto
    {
        public string? Message { get; set; }
        public bool IsAuthenticated { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public List<string>? Roles { get; set; }
        public string? Token { get; set; }

        public int ExpiresOn { get; set; }
        [JsonIgnore]
        public string? RefreshToken { get; set; }
        public int RefreshTokenExpiration { get; set; }
    }
}
