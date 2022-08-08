﻿using ConfigurationManager.Core.Models;
using ConfigurationManager.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationManager.Data.Repositories
{
    public class ConfigurationRepository : Repository<Configuration>, IConfigurationRepository
    {
        public ConfigurationRepository(ConfigurationManagerDbContext context)
            : base(context)
        { }
    }
}
