using Pbfl.Models.Helpers;

namespace Pbfl.Models;

public class Login
{
    public int LoginId { get; set; }

    public string Email { get; set; } = default!;

    public string PasswordHash { get; set; } = default!;

    public override string ToString()
    {
        return StringHelper.GetString(this);
    }
}