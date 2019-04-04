using Client;
using System;
using System.Windows.Forms;
using Transport.Model;
using Transport.Repository;
using Transport.Service;

namespace Transport.Controller
{
    public partial class Bookings : Form
    {
        private readonly Ride _ride;
        private readonly ClientController _clientController;

        public Bookings(Ride ride, ClientController clientController)
        {
            InitializeComponent();
            _ride = ride;
            _clientController = clientController;
            label.Text = $"{ride.Destination}{Environment.NewLine}{ride.Date.ToString("yyyy-MM-dd")} " +
                $"{Environment.NewLine}{ride.Hour.ToString(@"HH\:mm")}";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var bookingService = new BookingService(new BookingDBRepository(DBUtils.GetProperties()));
            var clientService = new ClientService(new ClientDBRepository(DBUtils.GetProperties()));
            try
            {
                _clientController.AddClient(new Model.Client
                {
                    Id = 1,
                    LastName = lastName.Text.Trim(),
                    FirstName = firstName.Text.Trim()
                });
                Random randomNumber = new Random();
                bool correct = true;
                int rInt = 0;
                while (correct)
                {
                    rInt = randomNumber.Next(1, 18);
                    foreach (Booking booking in bookingService.GetAll())
                    {
                        if (rInt == booking.SeatNo)
                        {
                            break;
                        }
                    }
                    correct = false;
                }

                bookingService.Add(new Booking
                {
                    Id = 1,
                    RideId = _ride.Id,
                    ClientId = clientService.FilterClientById(firstName.Text.Trim(), lastName.Text.Trim()),
                    SeatNo = rInt
                });
                MessageBox.Show(@"Booked successfully!", @"Booking", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                Close();
            }
            catch (Exception repositoryException)
            {
                MessageBox.Show(repositoryException.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
