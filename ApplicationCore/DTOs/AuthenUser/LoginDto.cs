namespace ApplicationCore.DTOs.AuthenUser;
public record LoginDto
{
    public string access_token { get; set; }
    public string token_type { get; set; }
    public string id_token { get; set; }
    public string scope { get; set; }
    public long expires_in { get; set; }
}
