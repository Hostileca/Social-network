using System.Text.Json.Serialization;

namespace SharedResources.Dtos.User;

public class UserUpdateDto
{
    [JsonIgnore]
    public string Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}