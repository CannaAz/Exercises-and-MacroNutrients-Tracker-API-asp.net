using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Nutrients;

public record class UpdateNutrientDto
{
    [Required]
    public int Calories { get; set; }
    [Required]
    public int carbohidrates { get; set; }
    [Required]
    public int fats { get; set; }
    [Required]
    public int proteins { get; set; }
    [Required]
    public int CurrentBodyWeight { get; set; }
}
