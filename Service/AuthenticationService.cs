using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository.Contracts;
using Service.Contracts;
using Shared.DataTransferObjects.DM;
using Shared.DataTransferObjects.Room;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Service;

internal sealed class AuthenticationService : IAuthenticationService
{
    private readonly IRepositoryManager repositoryManager;
    private readonly IConfiguration configuration;
    private readonly IHasher hasher;

    private static SigningCredentials GetSigningCredentials()
    {
        var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET")!);
        var secret = new SymmetricSecurityKey(key);

        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    private static List<Claim> GetClaims(string? dmLogin, int? roomId)
    {
        var claims = new List<Claim>();
        if (dmLogin is not null) claims.Add(new Claim(ClaimTypes.Name, dmLogin));
        if (roomId.HasValue) claims.Add(new Claim(ClaimTypes.Actor, roomId.Value.ToString()));
        return claims;
    }

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
    {
        var jwtSettings = configuration.GetSection("JwtSettings");

        var tokenOptions = new JwtSecurityToken
        (
            issuer: jwtSettings["validIssuer"],
            audience: jwtSettings["validAudience"],
            claims: claims,
            expires: DateTime.Now.AddDays(Convert.ToDouble(jwtSettings["expires"])),
            signingCredentials: signingCredentials
        );

        return tokenOptions;
    }

    public AuthenticationService(IConfiguration configuration, 
        IRepositoryManager repositoryManager, IHasher hasher)
    {
        this.configuration = configuration;
        this.repositoryManager = repositoryManager;
        this.hasher = hasher;
    }

    public async Task<bool> ValidateDM(DMForAuthenticationDto dMForAuthentication)
    {
        var dm = await repositoryManager.DM.GetDmByNameAsync(dMForAuthentication.Login!, false);

        var result = (dm is not null && hasher.VerifyPassword(dm.Password, dMForAuthentication.Password!));

        return result;
    }

    public async Task<bool> ValidateRoom(RoomForAuthenticationDto roomInfo)
    {
        var room = await repositoryManager.Room.GetRoomAsync(roomInfo.Id!.Value, false);

        var result = (room is not null && hasher.VerifyPassword(room.Password, roomInfo.Password!));

        return result;
    }

    public string CreateToken(string? dmLogin, int? roomId)
    {
        if (dmLogin is null && roomId is null) throw new ArgumentNullException("Both arguments can't be null");
        var signingCredentials = GetSigningCredentials();
        var claims = GetClaims(dmLogin, roomId);
        var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }
}