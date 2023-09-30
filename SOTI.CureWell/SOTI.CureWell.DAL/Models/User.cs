using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOTI.CureWell.DAL.Models
{
    public class User
    {
        /// <summary>
        /// Gets or sets the User Id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Gets or sets User Name
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Gets or sets User Email
        /// </summary>
        public string EmailId { get; set; }

        /// <summary>
        /// Gets or sets User Age
        /// </summary>
        public int Age { get; set; }
        
        /// <summary>
        /// Gets or sets User Phone Number 
        /// </summary>
        public string PhoneNumber { get; set; }
        
        /// <summary>
        /// Gets or sets User Address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets User Role
        /// </summary>
        public string Role { get; set; }
        
        /// <summary>
        /// Gets or sets User Password
        /// </summary>
        public string Password { get; set; }
    }
}
