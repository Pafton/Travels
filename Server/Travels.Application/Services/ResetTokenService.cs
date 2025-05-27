using AutoMapper;
using Travels.Application.Interfaces;
using Travels.Domain.Entities;
using Travels.Domain.Interfaces;

public class ResetTokenService : IResetTokenService
{
    private readonly IPasswordResetTokenRepository _passwordResetTokenRepository;
    private readonly IMapper _mapper;

    public ResetTokenService(IPasswordResetTokenRepository passwordResetTokenRepository, IMapper mapper)
    {
        _passwordResetTokenRepository = passwordResetTokenRepository;
        _mapper = mapper;
    }

    public async Task AddToken(string token, string email)
    {
        var resetToken = new PasswordResetToken
        {
            Token = token,
            ExpirationDate = DateTime.UtcNow.AddMinutes(30),
            CreatedAt = DateTime.UtcNow,
            UserId = 1 
        };

        await _passwordResetTokenRepository.AddToken(resetToken);
    }

    public async Task<string?> GetEmailByToken(string token)
    {
        var resetToken = await _passwordResetTokenRepository.GetToken(token);

        return resetToken?.User?.Email;
    }

    public async Task RemoveToken(string token)
    {
        var resetToken = await _passwordResetTokenRepository.GetToken(token);
        if (resetToken != null)
            await _passwordResetTokenRepository.RemoveToken(resetToken);
    }
}
