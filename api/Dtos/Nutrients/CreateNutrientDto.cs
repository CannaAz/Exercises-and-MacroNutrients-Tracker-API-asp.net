using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Nutrients;

public record class CreateNutrientDto
{
    [Required]
    public int Calories { get; set; }
    [Required]
    public int Carbohidrates { get; set; }
    [Required]
    public int Fats { get; set; }
    [Required]
    public int Proteins { get; set; }
    [Required]
    public int Fiber {get; set;}
    [Required]
    public int CurrentBodyWeight { get; set; }
}
