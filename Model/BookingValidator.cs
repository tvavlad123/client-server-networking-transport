using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transport.Model
{
    public class BookingValidator : IValidator<Booking>
    {
        public void Validate(Booking element)
        {
            StringBuilder errors = new StringBuilder();
            if (element.Id < 0)
                errors.Append("ID is negative");
            if (element.ClientId < 0)
                errors.Append("Invalid ClientId");
            if (element.RideId < 0)
                errors.Append("Invalid RideId");
            if (element.SeatNo < 0)
                errors.Append("Invalid seat number: incorrect value");
            var errorMessage = errors.ToString();
            if (errorMessage.Length > 0)
                throw new ValidatorException(errorMessage);
        }
    }
}
