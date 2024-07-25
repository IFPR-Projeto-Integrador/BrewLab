namespace BrewLab.Common.Configuration;
public class Database
{
    public string Host { get; set; } = "";
    public string DatabaseName { get; set; } = "";
    public string User { get; set; } = "";
    public string Password { get; set; } = "";

    public void Verify()
    {
        if (string.IsNullOrWhiteSpace(Host)) throw new ArgumentNullException("Variável 'host' não definida.");
        if (string.IsNullOrWhiteSpace(DatabaseName)) throw new ArgumentNullException("Variável 'DatabaseName' não definida.");
        if (string.IsNullOrWhiteSpace(User)) throw new ArgumentNullException("Variável 'User' não definida.");
        if (string.IsNullOrWhiteSpace(Password)) throw new ArgumentNullException("Variável 'Password' não definida.");
    }
}
