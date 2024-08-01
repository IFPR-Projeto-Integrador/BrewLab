namespace BrewLab.Common.DTOs;

public static class ExperimenterDTO
{
    public class Register
    {
        public string UserName { get; set; } = "";
        public string Password { get; set; } = "";
        public string RepeatPassword { get; set; } = "";
        public string Email { get; set; } = "";
        public string Name { get; set; } = "";

        public bool Validate()
        {
            if (ValidateName(Name).Any()) return false;
            if (ValidateUserName(UserName).Any()) return false;
            if (ValidatePassword(Password).Any()) return false;
            if (ValidateRepeatPassword(Password, RepeatPassword).Any()) return false;
            if (ValidateEmail(Email).Any()) return false;

            return true;
        }
    }

    public class Login
    {
        public string UserName { get; set; } = "";
        public string Password { get; set; } = "";

        public bool Validate()
        {
            if (ValidatePassword(Password).Any()) return false;
            if (ValidateUserName(UserName).Any()) return false;

            return true;
        }
    }

    public class NameAndId
    {
        public int Id { get; set; } = 0;
        public string UserName { get; set; } = "";

        public bool Validate()
        {
            if (ValidateUserName(UserName).Any()) return false;

            return true;
        }
    }

    public static IEnumerable<string> ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) yield return "O nome é obrigatório.";
    }

    public static IEnumerable<string> ValidateUserName(string userName)
    {
        if (string.IsNullOrWhiteSpace(userName)) yield return "O nome de usuário é obrigatório.";
    }

    public static IEnumerable<string> ValidatePassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password)) yield return "A senha é obrigatória.";

        if (password.Length < 8)
            yield return "A senha precisa conter no mínimo 8 caractéres.";
    }

    public static IEnumerable<string> ValidateRepeatPassword(string password, string repeatPassword)
    {
        if (string.IsNullOrWhiteSpace(repeatPassword)) yield return "Repita a senha.";

        if (!password.Equals(repeatPassword))
            yield return "As senhas não são iguais.";
    }

    public static IEnumerable<string> ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email)) yield return "O email é obrigatório.";

        if (!email.Contains('@'))
            yield return "O email é inválido.";
    }
}