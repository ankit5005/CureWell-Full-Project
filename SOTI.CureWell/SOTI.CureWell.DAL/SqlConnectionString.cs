using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOTI.CureWell.DAL
{
    /// <summary>
    /// Class to establish connection with the database
    /// </summary>
    public class SqlConnectionString
    {
        /// <summary>
        /// returns the connection string required to connect to the database
        /// </summary>
        public static string GetConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["CureWell"].ConnectionString;
            }
        }
    }
}
