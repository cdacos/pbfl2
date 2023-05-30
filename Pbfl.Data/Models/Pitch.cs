using Pbfl.Data.Helpers;

namespace Pbfl.Data.Models;

public class Pitch
{
    public int PitchId { get; set; }

    public string Name { get; set; } = default!;
    
    public override string ToString()
    {
        return StringHelper.GetString(this);
    }
}