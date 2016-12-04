using System;

namespace BikeSharing.Clients.Core.Validations
{
    public class YearRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }


            var year = Convert.ToInt32(value);

            if(year < 0 || year > 99)
            {
                return false;
            }

            return true;
        }
    }
}
