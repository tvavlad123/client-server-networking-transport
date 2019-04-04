using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transport.Model
{
    public class RideValidator : IValidator<Ride>
    {
        public void Validate(Ride element)
        {
            StringBuilder errors = new StringBuilder();
            if (element.Id < 0)
                errors.Append("ID is negative");
            if (element.Destination.Length == 0)
                errors.Append("Invalid destination: length zero");
            var errorMessage = errors.ToString();
            if (errorMessage.Length > 0)
                throw new ValidatorException(errorMessage);
        }
    }
}
