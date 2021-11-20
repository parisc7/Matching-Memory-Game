using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace The_Concentration_Game
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //splashScreen
            splashForm aSplashForm = new splashForm();
            aSplashForm.ShowDialog();
            //

            Application.Run(new MemoryGameForm());
          //  Application.Run(new smallMemoryGameForm());

            
        }
    }
}
