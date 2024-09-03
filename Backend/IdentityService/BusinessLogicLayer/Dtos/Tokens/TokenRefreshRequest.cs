using Newtonsoft.Json;

namespace BusinessLogicLayer.Dtos.Tokens;

public class TokenRefreshRequest
{
    public string RefreshToken { get; set; }
}