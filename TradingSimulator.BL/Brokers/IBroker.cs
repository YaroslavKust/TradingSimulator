﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSimulator.BL.Services
{
    public interface IBroker
    {
        void Update(ObserveParameters parameters);
    }
}
