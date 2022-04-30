using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingSimulator.BL.Services
{
    public interface IEmailService
    {
        void SendNotification(string from, string to, string message);
        void SendConfirmation(string from, string to, string message, string linkToConfirm);
    }
}
