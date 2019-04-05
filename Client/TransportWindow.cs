using Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Transport.Model;
using Transport.Repository;
using Transport.Service;

namespace Transport.Controller
{
    public partial class TransportWindow : Form, Util.IObserver<Ride>, Util.IObserver<Booking>
    {
        private readonly ClientController _clientController;
        private List<Ride> _rides;

        public TransportWindow(ClientController clientController)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            ridesView.ReadOnly = true;
            bookingsView.ReadOnly = true;
            bookingsView.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
            _clientController = clientController;
           
            _clientController.UpdateEvent += _clientController_UpdateEvent;
            FormClosed += (a, b) => {
                _clientController.LogOut();
                _clientController.UpdateEvent -= _clientController_UpdateEvent;
            };
            NotifyOnEvent();
        }

        public delegate void UpdateDataCallback(DataGridView ridesView, DataGridView bookingsView,
            List<Ride> list1, List<Ride> list2);

        public void NotifyOnEvent()
        {
            ridesView.Rows.Clear();
            bookingsView.Rows.Clear();
            searchByDestination.Clear();
            searchByDate.Clear();
            searchByHour.Clear();
            foreach (Ride ride in _clientController.GetAllRides())
            {
                int seatCounter = 0;
                foreach (Booking booking in _clientController.GetAllBookings())
                {
                    if (ride.Id == booking.RideId)
                    {
                        seatCounter++;
                    }
                }
                int seatNo = 18 - seatCounter;
                if (seatNo <= 0) seatNo = 0;
                ridesView.Rows.Add(ride.Destination, ride.Date.ToString("yyyy-MM-dd"), ride.Hour.ToString(@"HH\:mm"), seatNo);
            }
        }

        private void _clientController_UpdateEvent(object sender, EmployeeEventArgs e)
        {
            if (e.EmployeeEvent == EmployeeEvent.BookingAdded)
            {
                var allRides = (List<Ride>)e.Data;
                var customRides = searchByDestination.Text.Trim().Length > 0 ? allRides
                    .Where(x => x.Destination.ToLower().Contains(searchByDestination.Text.Trim().ToLower())
                                 && x.Date.ToString("yyyy-MM-dd").Contains(searchByDate.Text.Trim()) &&
                                                     x.Hour.ToString(@"HH\:mm").Contains(searchByHour.Text.Trim())).OrderBy(x => x.Id).ToList() : new List<Ride>();
                BeginInvoke(new UpdateDataCallback(Target), ridesView, bookingsView, allRides, customRides);
            }
        }

        private void Target(DataGridView ridesView, DataGridView bookingsView, List<Ride> list1, List<Ride> list2)
        {
            _rides = new List<Ride>();
            ridesView.Rows.Clear();
            bookingsView.Rows.Clear();
            foreach (var ride in list1)
            {
                int seatCounter = 0;
                foreach (Booking booking in _clientController.GetAllBookings())
                {
                    if (ride.Id == booking.RideId)
                    {
                        seatCounter++;
                    }
                }
                int seatNo = 18 - seatCounter;
                if (seatNo <= 0) seatNo = 0;
                ridesView.Rows.Add(ride.Destination, ride.Date.ToString("yyyy-MM-dd"), ride.Hour.ToString(@"HH\:mm"), seatNo);
            }
                
            foreach (var ride in list2)
            {
                List<int> seats = new List<int>();
                foreach (Booking booking in _clientController.GetAllBookings())
                {
                    if (booking.RideId == ride.Id)
                    {
                        foreach (Transport.Model.Client client in _clientController.GetAllClients())
                        {
                            if (client.Id == booking.ClientId)
                            {
                                seats.Add(booking.SeatNo);
                                bookingsView.Rows.Add($"{client.FirstName} {client.LastName}", booking.SeatNo, "Already booked");
                            }
                        }

                    }
                }

                for (int index = 1; index <= 18; index++)
                {
                    if (seats.IndexOf(index) == -1)
                    {
                        bookingsView.Rows.Add("-", index, "Booking available");
                    }
                }
                bookingsView.Sort(bookingsView.Columns["SeatNo"], ListSortDirection.Ascending);
            }
        }

       

        private void button1_Click(object sender, System.EventArgs e)
        {
            string destination = searchByDestination.Text.Trim();
            if (destination.Length == 0)
            {
                MessageBox.Show("Destination cannot be empty");
                return;
            }

            if (!DateTime.TryParse(searchByDate.Text, out DateTime date))
            {
                MessageBox.Show("Not a valid date.");
                return;
            }

            if (!DateTime.TryParse(searchByHour.Text, out DateTime hour))
            {
                MessageBox.Show("Not a valid hour.");
                return;
            }

            bookingsView.Rows.Clear();
            _rides = new List<Ride>();
            Ride ride = _clientController.GetCustomRides(destination, date.ToString("yyyy-MM-dd"), hour.ToString(@"HH\:mm"))[0];
            _rides.Add(ride);
            List<int> seats = new List<int>();
            foreach(Booking booking in _clientController.GetAllBookings())
            {
                if (booking.RideId == ride.Id)
                {
                    foreach (Transport.Model.Client client in _clientController.GetAllClients())
                    {
                        if (client.Id == booking.ClientId)
                        {
                            seats.Add(booking.SeatNo);
                            bookingsView.Rows.Add($"{client.FirstName} {client.LastName}", booking.SeatNo, "Already booked");
                        }
                    }
                    
                }
            }

            for (int index = 1; index <= 18; index++)
            {
                if (seats.IndexOf(index) == -1)
                {
                    bookingsView.Rows.Add("-", index, "Booking available");
                }
            }
            bookingsView.Sort(bookingsView.Columns["SeatNo"], ListSortDirection.Ascending);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!_rides.Any())
            {
                MessageBox.Show("Please search for a ride.");
                return;
            }
            var booking = new Bookings(_rides[0], _clientController);
            Hide();
            booking.Show();
            booking.FormClosed += (a, b) => Show();
        }
    }
}
