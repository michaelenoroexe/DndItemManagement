using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Administration.Repository.Contracts;
using Administration.Service.Contracts;
using Administration.Shared.DataTransferObjects.DM;
using Administration.Shared.DataTransferObjects.Room;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;

namespace Administration.Service;

internal sealed class AuthenticationService : IAuthenticationService
{
    private readonly IRepositoryManager repositoryManager;
    private readonly IConfiguration configuration;
    private readonly IMapper mapper;
    private readonly IHasher hasher;

    private static SigningCredentials GetSigningCredentials()
    {
        var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET")!);
        var secret = new SymmetricSecurityKey(key);

        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    private static List<Claim> GetClaims(int? dmId, int? roomId)
    {
        var claims = new List<Claim>();
        if (dmId.HasValue) claims.Add(new Claim(ClaimTypes.Name, dmId.Value.ToString()));
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
        IRepositoryManager repositoryManager, 
        IHasher hasher, IMapper mapper)
    {
        this.configuration = configuration;
        this.repositoryManager = repositoryManager;
        this.hasher = hasher;
        this.mapper = mapper;
    }

    public async Task<DMDto?> ValidateDM(DMForAuthenticationDto dMForAuthentication)
    {
        var dm = await repositoryManager.DM.GetDmByNameAsync(dMForAuthentication.Login!, false);

        var result = (dm is not null && hasher.VerifyPassword(dm.Password, dMForAuthentication.Password!));

        return result? mapper.Map<DMDto>(dm) : null;
    }

    public async Task<RoomDto?> ValidateRoom(RoomForAuthenticationDto roomInfo)
    {
        var room = await repositoryManager.Room.GetRoomAsync(roomInfo.Id!.Value, false);
        if (room is null) return null;
        
        if (!hasher.VerifyPassword(room.Password, roomInfo.Password!)) return null;

        return mapper.Map<RoomDto>(room);
    }

    public string CreateToken(int? dmId, int? roomId)
    {
        if (dmId is null && roomId is null) throw new ArgumentNullException("Both arguments can't be null");
        var signingCredentials = GetSigningCredentials();
        var claims = GetClaims(dmId, roomId);
        var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }
}