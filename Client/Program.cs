using System;
using System.Collections.Generic;
using System.Windows.Forms;
using networking;
using Transport.Controller;

namespace client
{
    class MyApplicationContext : ApplicationContext
    {
        private void onFormClosed(object sender, EventArgs e)
        {
            if (Application.OpenForms.Count == 0)
            {
                ExitThread();
            }
        }

        public MyApplicationContext()
        {
            //If WinForms exposed a global event that fires whenever a new Form is created,
            //we could use that event to register for the form's `FormClosed` event.
            //Without such a global event, we have to register each Form when it is created
            //This means that any forms created outside of the ApplicationContext will not prevent the 
            //application close.
            var server = new ServerProxy("127.0.0.1", 8081);
            var forms = new List<Form>() {
                new Login(server),
                new Login(server)
            };
            foreach (var form in forms)
            {
                form.FormClosed += onFormClosed;
            }

            //to show all the forms on start
            //can be included in the previous foreach
            foreach (var form in forms)
            {
                form.Show();
            }

            //to show only the first form on start
            //forms[0].Show();
        }
    }
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var server = new ServerProxy("127.0.0.1", 8081);
            Application.Run(new Login(server));
        }
    }
}
