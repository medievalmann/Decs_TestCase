﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationManager.Core.Models
{
    public class Configuration
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte Type { get; set; }
        public string Value { get; set; }
        public bool IsActive { get; set; }
        public string ApplicationId { get; set; }
        public Application Application { get; set; }
    }
}
