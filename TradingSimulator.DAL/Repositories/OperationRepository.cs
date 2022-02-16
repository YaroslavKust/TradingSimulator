﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSimulator.DAL.Models;

namespace TradingSimulator.DAL.Repositories
{
    public class OperationRepository: Repository<Operation>, IOperationRepository
    {
        public OperationRepository(TradingContext context) : base(context) { }
    }
}
