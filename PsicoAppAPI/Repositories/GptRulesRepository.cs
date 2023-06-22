using System.Data;
using Microsoft.EntityFrameworkCore;
using PsicoAppAPI.Data;
using PsicoAppAPI.Models;
using PsicoAppAPI.Repositories.Interfaces;

namespace PsicoAppAPI.Repositories;

public class GptRulesRepository : IGptRulesRepository
{
    private readonly DataContext _context;

    public GptRulesRepository(DataContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }


    public async Task<string?> GetRules()
    {
        var rule = await _context.FindAsync<GptRules>(1);
        return rule?.Rules;
    }

    public async Task<bool> SetRulesAndSaveChanges(string newRules)
    {
        _context.Update(new GptRules()
        {
            Id = 1,
            Rules = newRules
        });
        return await _context.SaveChangesAsync() > 0;
    }
}