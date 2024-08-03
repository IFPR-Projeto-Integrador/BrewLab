namespace BrewLab.Web.Utils;

public static class ActionTypeExtensions
{
    public static ActionType ToActionType(this string action)
    {
        return action switch
        {
            "view" => ActionType.View,
            "edit" => ActionType.Edit,
            "delete" => ActionType.Delete,
            "create" => ActionType.Create,
            _ => ActionType.View
        };
    }

    public static string ToStringE(this ActionType action)
    {
        return action switch
        {
            ActionType.View => "view",
            ActionType.Edit => "edit",
            ActionType.Delete => "delete",
            ActionType.Create => "create",
            _ => "view"
        };
    }

    public static bool IsVisualizationOnly(this ActionType action)
    {
        return action switch
        {
            ActionType.View => true,
            ActionType.Delete => true,
            _ => false
        };
    }
}

public enum ActionType { View, Edit, Delete, Create }