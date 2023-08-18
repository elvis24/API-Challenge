﻿using System.ComponentModel.DataAnnotations;

namespace API_Challenge.Modelos
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
