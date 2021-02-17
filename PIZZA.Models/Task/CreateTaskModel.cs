using PIZZA.Enums;
using PIZZA.Models.Validator;
using System;
using System.ComponentModel.DataAnnotations;

namespace PIZZA.Models.Task
{
    public class CreateTaskModel
    {
        [Required(ErrorMessage ="Pole nazwa zadania jest wymagane.")]
        [MaxLength(256, ErrorMessage = "Pole nazwa zadania nie może zawierać więcej niż 256 znaków.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Pole priorytet jest wymagane.")]
        public TaskPriority Priority { get; set; }

        [Required(ErrorMessage = "Pole deadline jest wymagane.")]
        [CheckDateRange]
        public DateTime Deadline { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string Description { get; set; } = "";

        public string Note { get; set; }
    }
}
