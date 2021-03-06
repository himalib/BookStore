﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AuthnAuthz.Model
{
    public class EmpModel
    {
        public int EmployeeId { get; set; }
        [Required(ErrorMessage = "First name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; }
        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }
    }
}