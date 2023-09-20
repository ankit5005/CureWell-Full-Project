using SOTI.CureWell.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOTI.CureWell.DAL.Interfaces
{
    public interface IDoctor
    {
        List<Doctor> GetAllDoctors();
        bool AddDoctor(string doctorName);

        bool DeleteDoctor(int doctorId);
        bool UpdateDoctorDetails(Doctor dObj);
    }
}
