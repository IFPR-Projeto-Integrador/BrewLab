namespace BrewLab.Common.Configuration;
public class EmailConfig
{
    public string ApiKeyPublic { get; set; } = "";
    public string ApiKeyPrivate { get; set; } = "";
    public string Email { get; set; } = "";
    public string NomeEmail { get; set; } = "";

    public void Verify()
    {
        if (string.IsNullOrWhiteSpace(ApiKeyPublic)) throw new ArgumentNullException(nameof(ApiKeyPublic));
        if (string.IsNullOrWhiteSpace(ApiKeyPrivate)) throw new ArgumentNullException(nameof(ApiKeyPrivate));
        if (string.IsNullOrWhiteSpace(Email)) throw new ArgumentNullException(nameof(Email));
        if (string.IsNullOrWhiteSpace(NomeEmail)) throw new ArgumentNullException(nameof(NomeEmail));
    }
}
