using Pbfl.Models.Helpers;

namespace Pbfl.Models;

public class League
{
    public int LeagueId { get; set; }
    
    public string Name { get; set; } = default!;
    
    public string Description { get; set; } = default!;
    
    public DateTime? BirthDateMinimum { get; set; }
    
    public DateTime? BirthDateMaximum { get; set; }
    
    public override string ToString()
    {
        return StringHelper.GetString(this);
    }
}