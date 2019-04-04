using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transport.Model
{
    public interface IValidator<in T> where T : class
    {
        void Validate(T element);
    }

    public class ValidatorException : Exception
    {
        public ValidatorException(string message) : base(message)
        { }
    }
}
