﻿using System.ComponentModel.DataAnnotations;

namespace LearningManagementSystemAPI.DTOs
{
    public class AccountDTO
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public Boolean Gender { get; set; }
        public string Avatar { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime Birthday { get; set; }
        public int Status { get; set; }
        public string RoleName { get; set; }
    }
}
