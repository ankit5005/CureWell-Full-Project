using SOTI.CureWell.DAL;
using SOTI.CureWell.DAL.Model;
using SOTI.CureWell.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainMethod
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Update Doctor Details
            //   Doctor d = new Doctor(){ DoctorId = 1001, DoctorName = "Anurag" };

            //Update Surgery Details 
            //  Surgery s = new Surgery() { SurgeryId = 5000, DoctorId = 1003, SurgeryDate = Convert.ToDateTime("2011-01-01"),StartTime= 6.00m,EndTime=7.00m,SurgeryCategory="CAR"};
            //CureWellRepository cw = new CureWellRepository();


            //cw.UpdateSurgery(s);

            //Add Docotor 
            Doctor d = new Doctor() {  DoctorName = "Anni Bhai" };
            CureWellRepository cw = new CureWellRepository();
            cw.AddDoctor(d);
        }
    }
}
