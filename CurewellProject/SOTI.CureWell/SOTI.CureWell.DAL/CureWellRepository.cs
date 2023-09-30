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
    public class CureWellRepository : IDoctor, ISurgery
    {
        private SqlConnection connection = null;
        private SqlCommand command = null;

        public List<DoctorSpecialization> GetDoctorBySpecialization(string SpecializationCode)
        {
            using (connection = new SqlConnection(SqlConnectionString.GetConnectionString()))
            {

                string query = "Select * from DoctorSpecialization";
                List<DoctorSpecialization> DS = new List<DoctorSpecialization>();
                connection.Open();
                using (command = new SqlCommand(query, connection))
                {
                 

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                        DoctorSpecialization ds=new DoctorSpecialization
                            {
                              
                                DoctorId = (int)reader["DoctorId"],
                            SpecializationCode = (string)reader["SpecializationCode"],
                            SpecializationDate = (DateTime)reader["SpecializationDate"] 
                                // Add more property assignments as needed
                            };

                            DS.Add(ds);
                        }
                    }

                }
                return DS;
            }

        }
        public List<Surgery> GetAllSurgeryTypeForToday()
        {
            using (connection = new SqlConnection(SqlConnectionString.GetConnectionString()))
            {

                string query = "Select  SurgeryId,DoctorId,SurgeryDate,EndTime,StartTime,SurgeryCategory  from Surgery";
                List<Surgery>doctorSurgery= new List<Surgery>();
                connection.Open();
                using (command = new SqlCommand(query, connection))
                {


                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Surgery doctorSpecialization = new Surgery
                            {
                                SurgeryId= (int)reader["SurgeryId"],
                                DoctorId = (int)reader["DoctorId"],
                                SurgeryDate = (DateTime)reader["SurgeryDate"],
                                EndTime = (decimal)reader["EndTime"],
                                StartTime = (decimal)reader["StartTime"],
                                SurgeryCategory = (string)reader["SurgeryCategory"]
                                // Add more property assignments as needed
                            };

                            doctorSurgery.Add(doctorSpecialization);
                        }
                    }

                }
                return doctorSurgery;
            }
        }

        public List<Specialization> GetAllSpecializations()
        {
            using (connection = new SqlConnection(SqlConnectionString.GetConnectionString()))
            {

                string query = "Select  SpecializationCode,SpecializationName  from Specialization";
                List<Specialization> doctorS = new List<Specialization>();
                connection.Open();
                using (command = new SqlCommand(query, connection))
                {


                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Specialization doctorSpecialization = new Specialization
                            {
                                SpecializationCode = (string)reader["SpecializationCode"],
                                SpecializationName = (string)reader["SpecializationName"]
                                // Add more property assignments as needed
                            };

                            doctorS.Add(doctorSpecialization);
                        }
                    }

                }
                return doctorS;
            }
        }

        public List<Doctor> GetAllDoctor()
        {
            using (connection = new SqlConnection(SqlConnectionString.GetConnectionString()))
            {
                
                string query = "Select  DoctorId,DoctorName  from Doctor";
                List<Doctor> doctors = new List<Doctor>();
                connection.Open();
                using (command = new SqlCommand(query, connection))
                {
                   

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Doctor doctor = new Doctor
                            {
                                DoctorId = (int)reader["DoctorID"],
                                DoctorName = (string)reader["DoctorName"]
                                // Add more property assignments as needed
                            };

                            doctors.Add(doctor);
                        }
                    }
                    
                    }
                return doctors;
            }
        }

        public bool AddDoctor(Doctor dObj)
        {
            using (connection = new SqlConnection(SqlConnectionString.GetConnectionString()))
            {
                string query = "insert into Doctor (DoctorName) values (@name)";
                using (command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", dObj.DoctorName);

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

