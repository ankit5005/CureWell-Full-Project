using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOTI.CureWell.DAL.Models
{
    /// <summary>
    /// Represents the surgeries performed by the doctors
    /// </summary>
    public class Surgery
    {
        /// <summary>
        /// Gets or sets Surgery Id
        /// </summary>
        public int SurgeryId { get; set; }
        /// <summary>
        /// Gets or sets Doctor Id
        /// </summary>
        public int DoctorId { get; set; }
        /// <summary>
        /// Gets or sets the date of Surgery 
        /// </summary>
        public DateTime SurgeryDate { get; set; }

        /// <summary>
        /// Gets or sets the start time of the surgery
        /// </summary>
        public Decimal StartTime { get; set; }

        /// <summary>
        /// Gets or sets the end time of the surgery
        /// </summary>
        public Decimal EndTime { get; set; }
        /// <summary>
        /// Gets or sets the category of the surgery
        /// </summary>
        public string SurgeryCategory { get; set; }
    }
}