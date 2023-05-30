using Pbfl.Models.Helpers;

namespace Pbfl.Models;

public class Error
{
    public int ErrorId { get; set; }
    
    public required string Details { get; set; }

    public override string ToString()
    {
        return StringHelper.GetString(this);
    }
}