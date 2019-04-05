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
            
        }

        private void Target(DataGridView datagridview1, DataGridView datagridview2, List<Ride> list1, List<Ride> list2)
        {

        }

       

        private void button1_Click(object sender, System.EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }
    }
}
