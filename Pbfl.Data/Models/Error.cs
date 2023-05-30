using Pbfl.Data.Helpers;

namespace Pbfl.Data.Models;

public class Error
{
    public int ErrorId { get; set; }
    
    public required string Details { get; set; }

    public override string ToString()
    {
        return StringHelper.GetString(this);
    }
}