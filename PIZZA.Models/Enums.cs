namespace PIZZA.Enums
{
    public enum DatabaseConnectionType
    {
        WindowsAuthentication,
        SQLServerAuthentication,
        Advanced,
    }

    public enum TaskPriority
    {
        LowPriority,
        NormalPriority,
        HighPriority,
        VeryHighPriority,
        PeopleAreDyingPriority
    }

    public enum TaskState
    {
        Created,
        Opened,
        Finished
    }

    public enum TaskRole
    {
        Creator,
        Editor,
        Visitor
    }

    public static class EnumToString
    {
        public static string ToFriendlyString(this TaskPriority taskPriority)
        {
            return taskPriority switch
            {
                TaskPriority.LowPriority => "Niski",
                TaskPriority.NormalPriority => "Normalny",
                TaskPriority.HighPriority => "Wysoki",
                TaskPriority.VeryHighPriority => "Powyżej wysokiego",
                TaskPriority.PeopleAreDyingPriority => "Ludzie tutaj umierają",
                _ => "",
            };
        }

        public static string ToFriendlyString(this TaskState taskState)
        {
            return taskState switch
            {
                TaskState.Created => "Utworzon",
                TaskState.Opened => "Otworzon",
                TaskState.Finished => "Ukończon",
                _ => ""
            };
        }

        public static string ToFriendlyString(this TaskRole taskRole)
        {
            return taskRole switch
            {
                TaskRole.Creator => "Stwórca",
                TaskRole.Editor => "Edytor",
                TaskRole.Visitor => "Wizytator",
                _ => ""
            };
        }
    }

}
