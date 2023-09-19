using SOTI.CureWell.DAL.Model;
using SOTI.CureWell.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOTI.CureWell.DAL
{
    public class CureWellRepository : ICureWell
    {
        private SqlConnection connection = null;
        private SqlCommand command = null;
        public  bool UpdateDoctorDetails(Doctor dObj)
        {
            using (connection = new SqlConnection(SqlConnectionString.GetConnectionString()))
            {
                string query = "update Doctor set DoctorName=@name where doctorId=@Id";
                using (command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", dObj.DoctorName);
                    command.Parameters.AddWithValue("@Id",dObj.DoctorId);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("rows updated.");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("No rows updated.");
                    }
                }
            }
            return false;

        
   
}

        public bool UpdateSurgery(Surgery sObj)
        {
            using (connection = new SqlConnection(SqlConnectionString.GetConnectionString()))
            {
                string query = "update Surgery set DoctorId=@Id,SurgeryDate=@SDate,StartTime=@startTime," +
                "EndTime=@endTime,SurgeryCategory=@sCategory where SurgeryId=@SId";
                using (command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SId", sObj.SurgeryId);
                    command.Parameters.AddWithValue("@Id", sObj.DoctorId);
                    command.Parameters.AddWithValue("@SDate", sObj.SurgeryDate);
                    command.Parameters.AddWithValue("@startTime", sObj.StartTime);
                    command.Parameters.AddWithValue("@endTime", sObj.EndTime);
                    command.Parameters.AddWithValue("@sCategory",sObj.SurgeryCategory);
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {

                        Console.WriteLine("Update successful.");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("No rows updated.");
                    }
                }
                return false;
            }
        }

        }


}

