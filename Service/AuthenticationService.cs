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

    private static List<Claim> GetClaims(string? dmLogin, int? characterId)
    {
        var claims = new List<Claim>();
        if (dmLogin is not null) claims.Add(new Claim(ClaimTypes.Name, dmLogin));
        if (characterId.HasValue) claims.Add(new Claim(ClaimTypes.Actor, characterId.Value.ToString()));
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
        if (room is null) return false;

        var character = await repositoryManager.Character.GetCharacterAsync(roomInfo.CharacterId!.Value, false);
        if (character is null || !character.RoomId.Equals(room.Id)) return false;

        return hasher.VerifyPassword(room.Password, roomInfo.Password!);
    }

    public string CreateToken(string? dmLogin, int? characterId)
    {
        if (dmLogin is null && characterId is null) throw new ArgumentNullException("Both arguments can't be null");
        var signingCredentials = GetSigningCredentials();
        var claims = GetClaims(dmLogin, characterId);
        var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }
}