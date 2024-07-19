using System;
using System.Collections.Generic;
using System.Text;

namespace Comtrade.FinanceTrack.Helper.Error
{
    public static class ErrorHelper
    {
        public static string GetAllMessages(Exception ex)
        {
            string message = ex.Message;
            while (!object.ReferenceEquals(ex.InnerException, (null)))
            {
                message += " ###InnerEx### " + ex.InnerException.Message;
                ex = ex.InnerException;
            }
            return message;
        }
    }
}
