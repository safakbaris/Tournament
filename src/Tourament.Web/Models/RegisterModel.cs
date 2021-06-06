﻿using System.ComponentModel.DataAnnotations;

namespace Tourament.Web.Models
{
    public class RegisterModel
    {
        [Required, MaxLength(256)]
        public string Username { get; set; }

        [Required, MaxLength(256)]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
