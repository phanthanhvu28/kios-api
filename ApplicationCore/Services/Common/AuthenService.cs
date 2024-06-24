using ApplicationCore.UseCases.AuthenUser.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ApplicationCore.Services.Common;
public class AuthenService : IAuthenService
{
    private readonly IConfiguration _config;
    public AuthenService(IConfiguration config)
    {
        _config = config;
    }

    public string GenerateToken(UserModel user)
    {
        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha256);
        Claim[] claims = new[]
        {
                new Claim("name",user.Username),
                new Claim("email",user.Username),
                new Claim("username",user.Username),
                new Claim("fullname",user.Username),
                new Claim("role","admin")
            };
        JwtSecurityToken token = new(_config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credentials);


        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string Encrypt(string plainText)
    {
        using (Aes aes = Aes.Create())
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(_config["Jwt:Key"]!);
            Array.Resize(ref keyBytes, aes.KeySize / 8);
            aes.Key = keyBytes;
            aes.GenerateIV();

            using (ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
            using (MemoryStream ms = new())
            {
                ms.Write(aes.IV, 0, aes.IV.Length);
                using (CryptoStream cs = new(ms, encryptor, CryptoStreamMode.Write))
                using (StreamWriter writer = new(cs))
                {
                    writer.Write(plainText);
                }
                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }
    public string Decrypt(string cipherText)
    {
        byte[] fullCipher = Convert.FromBase64String(cipherText);
        using (Aes aes = Aes.Create())
        {
            byte[] iv = new byte[aes.BlockSize / 8];
            byte[] cipher = new byte[fullCipher.Length - iv.Length];

            Array.Copy(fullCipher, iv, iv.Length);
            Array.Copy(fullCipher, iv.Length, cipher, 0, cipher.Length);

            byte[] keyBytes = Encoding.UTF8.GetBytes(_config["Jwt:Key"]!);
            Array.Resize(ref keyBytes, aes.KeySize / 8);
            aes.Key = keyBytes;
            aes.IV = iv;

            using (ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
            using (MemoryStream ms = new(cipher))
            using (CryptoStream cs = new(ms, decryptor, CryptoStreamMode.Read))
            using (StreamReader reader = new(cs))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
public interface IAuthenService
{
    public string GenerateToken(UserModel user);
    public string Encrypt(string plainText);
    public string Decrypt(string cipherText);
}
