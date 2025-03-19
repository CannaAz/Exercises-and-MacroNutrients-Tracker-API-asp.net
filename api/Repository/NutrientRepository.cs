using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;
using api.Dtos.Nutrients;
using Microsoft.AspNetCore.Http.HttpResults;

namespace api.Repository;

public class NutrientRepository : INutrientsRepository
{
    private readonly AppDbContext _context;

    public NutrientRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Nutrients>> GetAllNutrientsAsync(string email)
    {
        var userId = await GetUserId(email);
        var nutrients = await _context.Nutrients.Where(item => item.UserId == userId && item.UserId == userId).ToListAsync();
        return nutrients;
    }
    public async Task<Nutrients> GetByIdAsync(int id, string email)
    {
        var userId = await GetUserId(email);
        return await _context.Nutrients.FirstOrDefaultAsync(item => item.Id == id && item.UserId == userId);
    }
    public async Task<Nutrients> CreateNutrientAsync(Nutrients NutrientsModel)
    {
        await _context.Nutrients.AddAsync(NutrientsModel);
        _context.SaveChanges();
        return NutrientsModel;
    }
    public async Task<Nutrients> UpdateAsync(int id,string email, UpdateNutrientDto NutrientsDto)
    {
        var userId = await GetUserId(email);
        var currentNutrient = await _context.Nutrients.FirstOrDefaultAsync(item => item.Id == id && item.UserId == userId);
        if(currentNutrient == null) return null;

        currentNutrient.CurrentBodyWeight = NutrientsDto.CurrentBodyWeight;
        currentNutrient.Calories = NutrientsDto.Calories;
        currentNutrient.Carbohidrates = NutrientsDto.carbohidrates;
        currentNutrient.Fats = NutrientsDto.fats;
        currentNutrient.Proteins = NutrientsDto.proteins;

        await _context.SaveChangesAsync();
        throw new NotImplementedException();
    }
    public async Task<Nutrients> DeleteAsync(int id, string email)
    {
        var userId = await GetUserId(email);
        var nutrient = await _context.Nutrients.FirstOrDefaultAsync(item => item.Id == id && item.UserId == userId);
        if(nutrient == null) return null;
        
        _context.Remove(nutrient);
        _context.SaveChanges();

        return nutrient;
    }

    private async Task<string> GetUserId(string email)
    {
        return (await _context.Users.FirstOrDefaultAsync(item => item.Email == email)).Id;
    }
}
