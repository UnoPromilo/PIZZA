﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PIZZA.Models.User
{
    public class RegistrationModelComplete:RegistrationModel
    {
        [Required(ErrorMessage = "Pole imię jest wymagane.")]
        [StringLength(256, ErrorMessage = "Imię nie może być dłuższe niż 256 znaków.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Pole nazwisko jest wymagane.")]
        [StringLength(256, ErrorMessage = "Nazwisko nie może być dłuższe niż 256 znaków.")]

        public string LastName { get; set; }

        [StringLength(256, ErrorMessage = "Numer telefonu nie może być krótszy niż 7 znaków oraz dłuższy niż 50 znaków.", MinimumLength =7)]

        public string PhoneNumber { get; set; }

        public bool ForcePasswordChangeWhileNextLogin { get; set; } = true;

        [StringLength(256, ErrorMessage = "Adres nie może być dłuższy niż 256 znaków")]
        public string AddressLine { get; set; }

        [StringLength(10, ErrorMessage = "Kod pocztowy nie może być dłuższy niż 10 znaków")]
        public string PostalCode { get; set; }

        [StringLength(50, ErrorMessage = "Miasto nie może być dłuższe niż 50 znaków.")]
        public string Town { get; set; }
    }
}
