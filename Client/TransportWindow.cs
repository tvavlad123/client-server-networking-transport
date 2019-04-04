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

        public delegate void UpdateDataCallback(DataGridView dataGridView1, DataGridView dataGridView2,
            List<Ride> list1, List<Ride> list2);

        public void NotifyOnEvent()
        {
            ridesView.Rows.Clear();
            bookingsView.Rows.Clear();
            foreach (Ride ride in _clientController.GetAllRides()) {
                ridesView.Rows.Add(ride.Destination, ride.Date.ToString("yyyy-MM-dd"), ride.Hour.ToString(@"HH\:mm"), 
                    _clientController.AvailableSeatsRide(ride));
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
                                 x.Hour.ToString(@"HH\:mm").Contains(searchByHour.Text.Trim())).ToList() : new List<Ride>();
                BeginInvoke(new UpdateDataCallback(Target), ridesView, bookingsView, allRides, customRides);
            }
        }

        private void Target(DataGridView datagridview1, DataGridView datagridview2, List<Ride> list1, List<Ride> list2)
        {
            _rides = new List<Ride>();
            ridesView.Rows.Clear();
            bookingsView.Rows.Clear();
            foreach (var ride in list1)
                ridesView.Rows.Add(ride.Destination, ride.Date.ToString("yyyy-MM-dd"), ride.Hour.ToString(@"HH\:mm"), _clientController.AvailableSeatsRide(ride));
        }

       

        private void button1_Click(object sender, System.EventArgs e)
        {
            if (searchByDestination.Text.Trim().Length == 0)
            {
                MessageBox.Show("Destination cannot be empty.");
                return;
            }

            if (!DateTime.TryParse(searchByDate.Text, out DateTime parsedDate))
            {
                MessageBox.Show("Not a valid date.");
                return;
            }

            if (!DateTime.TryParse(searchByHour.Text, out DateTime parsedHour))
            {
                MessageBox.Show("Not a valid hour.");
                return;
            }

            bookingsView.Rows.Clear();
            _rides = new List<Ride>();
            List<int> seats = new List<int>();
            
            _clientController.GetCustomRides(searchByDestination.Text.Trim(), searchByDate.Text.Trim(),
                searchByHour.Text.Trim())
                .ForEach(
                ride =>
                {
                    _rides.Add(ride);
                    var clientNameSeat = _clientController.FilterByClient(ride.Id);

                    foreach (var tuple in clientNameSeat)
                    {
                        bookingsView.Rows.Add(tuple.Item1, tuple.Item2, "Already booked");
                        seats.Add(tuple.Item2);
                    }
                    for (int index = 1; index <= RideService.MaxAvailablePlaces; index++)
                    {
                        if (seats.IndexOf(index) == -1)
                        {
                            bookingsView.Rows.Add("-", index, "Book");
                        }
                    }
                });
            bookingsView.Sort(bookingsView.Columns["SeatNo"], ListSortDirection.Ascending);

            if (!_rides.Any())
            {
                MessageBox.Show("No such ride found.");
            }
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
            var rezervare = new Bookings(_rides[0], _clientController);
            Hide();
            rezervare.Show();
            rezervare.FormClosed += (a, b) =>
            {
                button1_Click(this, new EventArgs());
                Show();
            };
        }
    }
}
