using Microsoft.EntityFrameworkCore;

namespace api.Models;

public class Nutrients
{
    public int Id { get; set; }
    public string? UserId { get; set; }
    public int Calories { get; set; }

    public int Carbohidrates { get; set; }
    public int Fats { get; set; }

    public int Proteins { get; set; }

    public int Fiber {get; set;}

    public int CurrentBodyWeight { get; set; }
    public DateOnly CreationDate { get; set; }
}
