using ApplicationCore.DTOs.AuthenUser;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ApplicationCore.Services.Common;
public class AuthenService : IAuthenService
{
    private readonly IConfiguration _config;

    private const int Keysize = 256;
    // This constant determines the number of iterations for the password bytes generation function.
    private const int DerivationIterations = 1000;
    public AuthenService(IConfiguration config)
    {
        _config = config;
    }

    public string GenerateToken(CreateUserDto user)
    {
        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha256);
        Claim[] claims = new[]
        {
                new Claim("address",user.Address),
                new Claim("phone",user.Phone),
                new Claim("email",user.Email),
                new Claim("username",user.Username),
                new Claim("fullname",user.Fullname),
                new Claim("role","admin"),
                new Claim("menus", JsonConvert.SerializeObject(user.Menus)),
                new Claim("storecode","123456789"),
            };
        JwtSecurityToken token = new(_config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMicroseconds(43200),
            signingCredentials: credentials);


        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    public string Encrypt(string plainText)
    {
        string privateKey = _config["Authen:Secretkey"]!;

        byte[] iv = new byte[16];
        byte[] array;

        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(privateKey);
            aes.IV = iv;

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using (MemoryStream memoryStream = new())
            {
                using (CryptoStream cryptoStream = new((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter streamWriter = new((Stream)cryptoStream))
                    {
                        streamWriter.Write(plainText);
                    }

                    array = memoryStream.ToArray();
                }
            }
        }

        return Convert.ToBase64String(array);
    }
    public string Decrypt(string cipherText)
    {
        string privateKey = _config["Authen:Secretkey"]!;
        byte[] iv = new byte[16];
        byte[] buffer = Convert.FromBase64String(cipherText);

        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(privateKey);
            aes.IV = iv;
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using (MemoryStream memoryStream = new(buffer))
            {
                using (CryptoStream cryptoStream = new((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader streamReader = new((Stream)cryptoStream))
                    {
                        return streamReader.ReadToEnd();
                    }
                }
            }
        }
    }
}
public interface IAuthenService
{
    public string GenerateToken(CreateUserDto user);
    public string Encrypt(string plainText);
    public string Decrypt(string cipherText);
}
