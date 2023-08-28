using LogisticAPI.Entities;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.RegularExpressions;

namespace LogisticAPI.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class ConveyanceIdAttribute : ValidationAttribute
    {
        // Internal field to hold the mask value.



        public override bool IsValid(object value)
        {
            var phoneNumber = (Conveyance)value;
            bool result = true;
            {
                result = MatchesMask(phoneNumber);
                return result;
            }
        }

        // Checks if the entered phone number matches the mask.
        bool MatchesMask(Conveyance actcual)
        {
            string GROUND_TRANSPORT = @"^[A-Za-z]{3}\d{3}[A-Za-z]$";
            string MARINE_TRANSPORT = @"^[A-Za-z]{3}\d{4}[A-Za-z]$";

            if (actcual.TransportType == Enums.TransportEnum.MARINE_TRANSPORT)
            {

                if (Regex.IsMatch(actcual.Id, MARINE_TRANSPORT))
                {
                    return true;
                }
            }
            else if (actcual.TransportType == Enums.TransportEnum.GROUND_TRANSPORT)
            {
                if (Regex.IsMatch(actcual.Id, GROUND_TRANSPORT))
                {
                    return true;
                }
            }

            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentCulture,
              ErrorMessageString, name);
        }
    }
}
