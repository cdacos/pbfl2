using Pbfl.Models.Helpers;

namespace Pbfl.Models;

public class Pitch
{
    public int PitchId { get; set; }

    public string Name { get; set; } = default!;
    
    public override string ToString()
    {
        return StringHelper.GetString(this);
    }
}