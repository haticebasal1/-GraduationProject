using System;

namespace PhoneCase.Shared.Configurations.Auth;

public class JwtConfig
{
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string Secret { get; set; } = string.Empty;
    public double AccessTokenExpiration { get; set; }
    public double RefreshTokenExpiration { get; set; }
}
