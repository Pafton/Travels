using Microsoft.EntityFrameworkCore;
using Travels.Domain.Entities;
using Travels.Domain.Interfaces;
using Travels.Infrastructure.Presistance;

public class PasswordResetTokenRepository : IPasswordResetTokenRepository
{
    private readonly AppDbContext _context;

    public PasswordResetTokenRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddToken(PasswordResetToken token)
    {
        await _context.PasswordResetTokens.AddAsync(token);
        await _context.SaveChangesAsync();
    }

    public async Task<PasswordResetToken> GetToken(string token)
    {
        return await _context.PasswordResetTokens.FirstOrDefaultAsync(t => t.Token == token && !t.IsUsed);
    }

    public async Task<bool> IsTokenExpired(string token)
    {
        var resetToken = await GetToken(token);
        return resetToken != null && resetToken.ExpirationDate < DateTime.UtcNow;
    }

    public async Task MarkTokenAsUsed(string token)
    {
        var resetToken = await _context.PasswordResetTokens.FirstOrDefaultAsync(t => t.Token == token);
        if (resetToken != null)
        {
            resetToken.IsUsed = true;
            await _context.SaveChangesAsync();
        }
    }

    public async Task RemoveToken(PasswordResetToken token)
    {
        _context.PasswordResetTokens.Remove(token);
        await _context.SaveChangesAsync();
    }
}
