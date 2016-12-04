using System;

namespace BikeSharing.Clients.Core.Validations
{
    public class MonthRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }
            
            var month = Convert.ToInt32(value);

            if(month < 0 || month > 12)
            {
                return false;
            }

            return true;
        }
    }
}
