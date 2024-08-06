namespace BrewLab.Common.DTOs;
public static class ExperimentalPlanningDTO
{
    public class Create
    {
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string ExperimentalMatrix { get; set; } = "";
        public int IdExperimentalModel { get; set; } = 0;

        public virtual bool Validate()
        {
            if (ValidateName(Name).Any()) return false;
            if (ValidateDescription(Description).Any()) return false;
            if (ValidateExperimentalMatrix(ExperimentalMatrix).Any()) return false;
            if (ValidateIdExperimentalModel(IdExperimentalModel).Any()) return false;

            return true;
        }
    }

    public class View
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string ExperimentalMatrix { get; set; } = "";
        public int? IdExperimentalModel { get; set; } = null;
    }

    public class Edit : Create
    {
        public int Id { get; set; }

        public override bool Validate()
        {
            if (ValidateId(Id).Any()) return false;

            return base.Validate();
        }
    }

    public static IEnumerable<string> ValidateId(int id)
    {
        if (id <= 0) yield return "Identificador é obrigatório.";
    }

    public static IEnumerable<string> ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) yield return "Nome é obrigatório.";
    }

    public static IEnumerable<string> ValidateDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description)) yield return "Descrição é obrigatória.";
    }

    public static IEnumerable<string> ValidateExperimentalMatrix(string experimentalMatrix)
    {
        if (string.IsNullOrWhiteSpace(experimentalMatrix)) yield return "Matriz experimental é obrigatória.";
    }

    public static IEnumerable<string> ValidateIdExperimentalModel(int idExperimentalModel)
    {
        if (idExperimentalModel <= 0) yield return "Modelo experimental é obrigatório.";
    }
}
