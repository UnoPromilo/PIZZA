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
}
