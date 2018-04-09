using System;
using System.Net.Mail;

namespace Financeasy.Api.Utils.Validations
{
    public static class Validation
    {
        public static bool CheckEmail(string email)
        {
            try
            {
                var mailAddress = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        
    }
}