namespace BrewLab.Common.DTOs;

public static class ResultDTO
{
    public class Result
    {
        public bool Success { get; set; } = false;
        public IEnumerable<string> Errors { get; set; } = [];

        public static readonly Result Succeeded = new() { Success = true };
        public static readonly Result InvalidDTO = new() { Success = false, Errors = ["Modelo inválido."] };
        public static readonly Result InvalidIdentification = new() { Success = false, Errors = ["Identificador incorreto."] };
        public static readonly Result ParsingFail = new() { Success = false, Errors = ["Falha ao tentar realizar o parsing."] };
    }

    public class Auth : Result
    {
        public string? Token { get; set; }
        public ExperimenterDTO.NameAndId? Experimenter { get; set; }

        public static readonly Auth IncorrectLoginOrPassword = new() { Success = false, Errors = ["Login ou senha incorretos."] };
    }
}

