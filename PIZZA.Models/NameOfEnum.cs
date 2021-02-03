using PIZZA.Enums;

namespace PIZZA.Models
{
    public static class NameOfEnum
    {
        public static string Name(this TaskPriority taskPriority) => taskPriority switch
        {
            TaskPriority.LowPriority => "Niski",
            TaskPriority.NormalPriority => "Średni",
            TaskPriority.HighPriority => "Wysoki",
            _ => "",
        };
        
    }
}
