using SHARED.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;

namespace SHARED.GenericModels
{
    public static class Generics
    {
        public static ClaimsPrincipal SetClaimPrincipal(UserSession model)
        {
            return new ClaimsPrincipal(new ClaimsIdentity(
                new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, model.Id!),
                    new Claim(ClaimTypes.Name, model.UserName!),
                    new Claim(ClaimTypes.Role, model.Role!),
                }, "JwtAuth"));
        }

        public static UserSession GetClaimsFromToken(string jwtToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwtToken);
            var claims = token.Claims;

            string Id = claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value!;
            string Name = claims.First(c => c.Type == ClaimTypes.Name).Value!;
            string Role = claims.First(c => c.Type == ClaimTypes.Role).Value!;

            return new UserSession(Id, Name, Role);
        }

        public static JsonSerializerOptions JsonOptions()
        {
            return new JsonSerializerOptions
            {
                AllowTrailingCommas = true,
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                UnmappedMemberHandling = JsonUnmappedMemberHandling.Skip
            };
        }

        public static string SerializeObj<T>(T modelObject) => JsonSerializer.Serialize(modelObject, JsonOptions());
        public static T DeserializeJsonString<T>(string jsonString) => JsonSerializer.Deserialize<T>(jsonString, JsonOptions())!;
        public static IList<T> DeserializeJsonStringList<T>(string jsonString) => JsonSerializer.Deserialize<IList<T>>(jsonString, JsonOptions())!;
        public static StringContent GenerateStringContent(string serializedObj) => new(serializedObj, Encoding.UTF8, "application/json");
    }
}
