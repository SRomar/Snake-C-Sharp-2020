/**
 * 
 * T.P. Snake 
 * 
 * @author Santiago Romar
 * Profesora: Natalia Gonzalez
 * Curso: 6to 1ra
 * Escuela: Hogar Naval Stella Maris
 * Año: 2020
 * 
 * 
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace $safeprojectname$
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
