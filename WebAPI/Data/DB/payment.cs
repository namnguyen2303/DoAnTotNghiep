﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DB
{
    public class payment
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public bool active { get; set; }
    }
}
