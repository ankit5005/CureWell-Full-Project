using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOTI.CureWell.DAL.Models
{
    /// <summary>
    /// Represents a specialization with a code and a name.
    /// </summary>
    public class Specialization
    {
        /// <summary>
        /// Gets or sets the code for the specialization.
        /// </summary>

        public string SpecializationCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the specialization.
        /// </summary>

        public string SpecializationName { get; set; }
    }
}