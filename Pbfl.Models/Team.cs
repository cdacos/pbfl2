using Pbfl.Models.Helpers;

namespace Pbfl.Models;

public class Team
{
    public int TeamId { get; set; }
    
    public string Name { get; set; } = default!;
    
    public int LeagueId { get; set; }
    public League League { get; set; } = default!;
    
    public string? KitColourPrimary { get; set; }
    
    public string? KitColourSecondary { get; set; }
    
    public override string ToString()
    {
        return StringHelper.GetString(this);
    }
}