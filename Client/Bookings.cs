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
        private Ride _ride;

        private readonly ClientController _clientController;

        public Bookings(Ride ride, ClientController clientController)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            _ride = ride;
            _clientController = clientController;
            noPlaces.Maximum = 18;
            label1.Text = ride.Destination + Environment.NewLine + ride.Date.ToString("yyyy-MM-dd") + Environment.NewLine +
                          ride.Hour.ToString(@"HH\:mm");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Transport.Model.Client client in _clientController.GetAllClients())
                {
                    if (client.FirstName.Equals(firstName.Text.Trim()) && client.LastName.Equals(lastName.Text.Trim())) {
                        Random randomNumber = new Random();
                        bool correct = true;
                        int rInt = 0;
                        while (correct)
                        {
                            rInt = randomNumber.Next(1, 18);
                            foreach (Booking booking in _clientController.GetAllBookings())
                            {
                                if (rInt == booking.SeatNo)
                                {
                                    break;
                                }
                            }
                            correct = false;
                        }
                        _clientController.AddBooking(_ride, new Booking
                        {
                            Id = 1,
                            RideId = _ride.Id,
                            ClientId = client.Id,
                            SeatNo = rInt
                        }, client);
                    }

                }
                
                MessageBox.Show(@"Rezervare facuta cu succes !", @"Rezervare", MessageBoxButtons.OK,
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
