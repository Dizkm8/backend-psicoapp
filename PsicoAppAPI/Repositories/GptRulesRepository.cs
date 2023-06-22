using Microsoft.EntityFrameworkCore;
using PsicoAppAPI.Data;
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
        var rules = await _context.GPTRules?.ToListAsync()!;
        return rules[0]?.Rules;
    }
}