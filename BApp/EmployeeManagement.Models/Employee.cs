﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Models
{
    public class Employee
    {
        public int EmployeetId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public Department Department { get; set; }
        public string PhotoPath { get; set; }
    }
}
