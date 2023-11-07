using System;
using System.Text.RegularExpressions;

namespace DataWorker
{
    public static class Validator
    {
        static readonly string customerNamePattern = @"^[A-Za-z\s'-]+$";
        public static bool ValidatingCreateData(ModelWithoutIDRequest requestWithoutID)
        {
            if(requestWithoutID == null) return false;
            if (string.IsNullOrEmpty(requestWithoutID.FirstName) || requestWithoutID.FirstName.Length > 20) return false;
            else if (string.IsNullOrEmpty (requestWithoutID.LastName) || requestWithoutID.LastName.Length > 20) return false;
            else if (requestWithoutID.Age <= 0 || requestWithoutID.Age > 120) return false;
            else if (string.IsNullOrEmpty(requestWithoutID.Address) || requestWithoutID.Address.Length > 50) return false;
            else if (!Regex.IsMatch(requestWithoutID.FirstName, customerNamePattern) || !Regex.IsMatch(requestWithoutID.LastName, customerNamePattern)) return false;
            return true;
        }

        public static bool ValidatingUpdateData(ModelRequest request)
        {
            if (request == null) return false;
            if (request.CustomerId <= 0) return false; //Autoset starts at 1
            else if (string.IsNullOrEmpty(request.FirstName) || request.FirstName.Length > 20) return false;
            else if (string.IsNullOrEmpty(request.LastName) || request.LastName.Length > 20) return false;
            else if (request.Age <= 0 || request.Age > 120) return false;
            else if (string.IsNullOrEmpty(request.Address) || request.Address.Length > 50) return false;
            else if (!Regex.IsMatch(request.FirstName, customerNamePattern) || !Regex.IsMatch(request.LastName, customerNamePattern)) return false;
            return true;
        }
    }
}