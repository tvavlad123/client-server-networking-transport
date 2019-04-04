using Client;
using services;
using System;
using System.Windows.Forms;
using Transport.Repository;
using Transport.Service;

namespace Transport.Controller
{
    public partial class Login : Form
    {
        private readonly ClientController _controller;

        public Login(ITransportServer server)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            _controller = new ClientController(server);
            this.FormClosing += (a, b) => Application.Exit();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (_controller.LogIn(UsernameBox.Text, PasswordBox.Text))
                {
                    var mainWindow = new TransportWindow(_controller);
                    Hide();
                    mainWindow.FormClosed += (a, b) => Show();
                    mainWindow.Show();
                }
                else
                    MessageBox.Show(@"Invalid username/password", @"Login Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                UsernameBox.Clear();
                PasswordBox.Clear();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, @"Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
