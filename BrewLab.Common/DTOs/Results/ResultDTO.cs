﻿namespace BrewLab.Common.DTOs;

public static class ResultDTO
{
    public class Result
    {
        public bool Success { get; set; } = false;
        public IEnumerable<string> Errors { get; set; } = [];

        public static readonly Result Succeeded = new() { Success = true };
        public static readonly Result UnknownError = new() { Success = false, Errors = ["Erro desconhecido."] };
        public static readonly Result InvalidDTO = new() { Success = false, Errors = ["Modelo inválido."] };
        public static readonly Result InvalidIdentification = new() { Success = false, Errors = ["Identificador incorreto."] };
        public static readonly Result ParsingFail = new() { Success = false, Errors = ["Falha ao tentar realizar a combinação."] };
        public static readonly Result IncorrectPassword = new() { Success = false, Errors = ["Senha incorreta."] };
        public static readonly Result ExperimenterDoesNotExist = new() { Success = false, Errors = ["Experimentador não existe."] };
        public static readonly Result RepeatPasswordDoesNotMatch = new() { Success = false, Errors = ["As senhas não são as mesmas."] };
        public static readonly Result CouldNotSendEmail = new() { Success = false, Errors = ["Não foi possível enviar o email no momento. Tente novamente mais tarde."] };
    }

    public class Auth : Result
    {
        public string? Token { get; set; }
        public ExperimenterDTO.NameAndId? Experimenter { get; set; }

        public static readonly Auth IncorrectLoginOrPassword = new() { Success = false, Errors = ["Login ou senha incorretos."] };
    }
}

