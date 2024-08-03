using System.ComponentModel.DataAnnotations;

namespace BrewLab.Common.DTOs;
public static class ExperimentalModelDTO
{
    public class Create
    {
        public string Name { get; set; } = "";
        public string Model { get; set; } = "";
        public string Description { get; set; } = "";
        public int ExperimenterId { get; set; } = 0;

        public virtual bool Validate()
        {
            if (ValidateName(Name).Any()) return false;
            if (ValidateModel(Model).Any()) return false;
            if (ValidateDescription(Description).Any()) return false;
            if (ValidateExperimenterId(ExperimenterId).Any()) return false;

            return true;
        }
    }

    public class View
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = "";
        public string Model { get; set; } = "";
        public string Description { get; set; } = "";
    }

    public class Edit : Create
    {
        public int Id { get; set; } = 0;

        public override bool Validate()
        {
            if (ValidateId(Id).Any()) return false;

            return base.Validate();
        }
    }

    public static IEnumerable<string> ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) yield return "O nome é obrigatório.";
    }

    public static IEnumerable<string> ValidateModel(string model)
    {
        if (string.IsNullOrWhiteSpace(model)) yield return "O modelo é obrigatório.";
    }

    public static IEnumerable<string> ValidateDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description)) yield return "A descrição é obrigatória.";
    }

    public static IEnumerable<string> ValidateExperimenterId(int experimenterId)
    {
        if (experimenterId <= 0) yield return "Experimentador inválido.";
    }

    public static IEnumerable<string> ValidateId(int id)
    {
        if (id <= 0) yield return "Identificador de modelo experimental inválido.";
    }
}
