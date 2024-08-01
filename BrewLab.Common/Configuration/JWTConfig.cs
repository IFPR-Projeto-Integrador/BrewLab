namespace BrewLab.Common.Configuration;
public class JWTConfig
{
    public string SymmetricSecurityKey { get; set; } = "";
    public int JWTTokenDuration { get; set; } = -1;
    public string JWTAudience { get; set; } = "";
    public string JWTIssuer { get; set; } = "";

    public void Verify()
    {
        if (string.IsNullOrWhiteSpace(SymmetricSecurityKey)) throw new ArgumentNullException("Variável 'SymmetricSecurityKey' não definida.");
        if (string.IsNullOrWhiteSpace(JWTAudience)) throw new ArgumentNullException("Variável 'JWTAudience' não definida.");
        if (string.IsNullOrWhiteSpace(JWTIssuer)) throw new ArgumentNullException("Variável 'JWTIssuer' não definida.");
        if (JWTTokenDuration < 0) throw new ArgumentNullException("Variável 'JWTTokenDuration' não definida.");
    }
}
