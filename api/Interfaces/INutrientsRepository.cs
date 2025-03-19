using api.Dtos.Nutrients;
using api.Models;

namespace api.Interfaces;

public interface INutrientsRepository
{
    public Task<List<Nutrients>> GetAllNutrientsAsync(string email) ;
    public Task<Nutrients> GetByIdAsync(int id, string email);
    public Task<Nutrients> CreateNutrientAsync(Nutrients NutrientsModel);
    public Task<Nutrients> UpdateAsync(int id, string email, UpdateNutrientDto NutrientsDto);
    public Task<Nutrients> DeleteAsync(int id, string email);
}
