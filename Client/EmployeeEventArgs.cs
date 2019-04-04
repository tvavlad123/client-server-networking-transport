using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public enum EmployeeEvent
    {
        BookingAdded
    }

    public class EmployeeEventArgs : EventArgs
    {
        public EmployeeEvent EmployeeEvent { get; }
        public object Data { get; }

        public EmployeeEventArgs(object data, EmployeeEvent employeeEvent)
        {
            EmployeeEvent = employeeEvent;
            Data = data;
        }
    }
}
