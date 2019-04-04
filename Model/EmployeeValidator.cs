using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transport.Model
{
    public class EmployeeValidator : IValidator<Employee>
    {
        public void Validate(Employee element)
        {
            StringBuilder errors = new StringBuilder();
            if (element.Id < 0)
                errors.Append("ID is negative");
            if (element.FirstName.Length == 0)
                errors.Append("Invalid first name: length zero");
            if (element.LastName.Length == 0)
                errors.Append("Invalid last name: length zero");
            if (element.UserName.Length == 0)
                errors.Append("Invalid username: length zero");
            if (element.Password.Length == 0)
                errors.Append("Invalid password: length zero");
            if (element.Office.Length == 0)
                errors.Append("Invalid office: length zero");
            var errorMessage = errors.ToString();
            if (errorMessage.Length > 0)
                throw new ValidatorException(errorMessage);
        }
    }
}
