namespace BrewLab.Common.DTOs;
public static class ResultDTO
{
    public class Auth
    {
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; } = [];
        public string? Token { get; set; }

        public static readonly Auth LoginOuSenhaIncorretos = new()
        {
            Success = false,
            Errors = ["Login ou senha incorretos."]
        };
    }
}
