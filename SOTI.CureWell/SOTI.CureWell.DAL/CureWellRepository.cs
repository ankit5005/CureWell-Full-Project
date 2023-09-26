using SOTI.CureWell.DAL.Interfaces;
using SOTI.CureWell.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SOTI.CureWell.DAL
{
    /// <summary>
    /// Implements all the interfaces 
    /// </summary>
    public class CureWellRepository:IDoctor,ISurgery,ISpecialization,IDoctorSpecialization,IUser
    {
        private SqlConnection _connection = null;
        private SqlDataAdapter _adapter = null;
        private DataSet _ds = null;
        public CureWellRepository() { }

        /// <summary>
        /// Retrieves all the doctors from the doctor table
        /// </summary>
        /// <returns>List of doctors</returns>
        public List<Doctor> GetAllDoctors()
        {
            List<Doctor> listDoctor = null;
            try
            {
                using (_connection = new SqlConnection(SqlConnectionString.GetConnectionString))
                {
                    using (_adapter = new SqlDataAdapter("Select * from Doctor", _connection))
                    {
                        using (_ds = new DataSet())
                        {
                            
                            _adapter.Fill(_ds, "Doctor");
                            listDoctor = _ds.Tables["Doctor"].AsEnumerable().Select(x => new Doctor
                            {
                                DoctorId = x.Field<int>("DoctorId"),
                                DoctorName = x.Field<string>("DoctorName")
                            }).ToList();
                        }
                    }
                }
            }
            catch(SqlException ex)
            {
                Console.WriteLine(ex);
            }
            catch(NullReferenceException ex)
            {
                Console.WriteLine(ex);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            return listDoctor;
        }

        /// <summary>
        /// Adds a new doctor in the doctor table
        /// </summary>
        /// <param name="doctorName"></param>
        /// <returns>a boolean value true or false indicating true or false</returns>
        public bool AddDoctor(string doctorName)
        {
            try
            {
                using (_connection = new SqlConnection(SqlConnectionString.GetConnectionString))
                {
                    using (_adapter = new SqlDataAdapter("usp_AddDoctor", _connection))
                    {
                        _adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                        _adapter.SelectCommand.Parameters.AddWithValue("@DoctorName",doctorName);

                        using (_ds = new DataSet())
                        {
                            _adapter.Fill(_ds, "Doctor");
                            return true;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex);
                
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine(ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                
            }
            return false;
        }

        /// <summary>
        /// Retrieves all the surgeries that are performed today from the surgery table
        /// </summary>
        /// <returns>List of all the surgeries performed today</returns>
        public List<Surgery> GetAllSurgeryTypeForToday()
        {
            List<Surgery> surgeries = null;
            try
            {
                using (_connection = new SqlConnection(SqlConnectionString.GetConnectionString))
                {
                    using (_adapter = new SqlDataAdapter("Select * from Surgery", _connection))
                    {
                        using (_ds = new DataSet())
                        {
                            _adapter.Fill(_ds, "Surgery");

                            string today = DateTime.Today.ToString("yyyy-MM-dd");

                            surgeries = _ds.Tables["Surgery"].AsEnumerable()
                                .Where(p => p.Field<DateTime>("SurgeryDate").ToString("yyyy-MM-dd") == today)
                                .Select(x => new Surgery
                                {
                                    SurgeryId = x.Field<int>("SurgeryId"),
                                    DoctorId = x.Field<int>("DoctorId"),
                                    SurgeryDate = x.Field<DateTime>("SurgeryDate"),
                                    StartTime = x.Field<Decimal>("StartTime"),
                                    EndTime = x.Field<decimal>("EndTime"),
                                    SurgeryCategory = x.Field<string>("SurgeryCategory"),
                                }).ToList();
                            return surgeries;
                        }
                    }
                }

            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex);
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine(ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return surgeries;
        }

        /// <summary>
        /// Retrieves a list of Specialization objects from the database using a stored procedure.
        /// </summary>
        /// <returns>A List of Specialization objects containing the retrieved data.</returns>
        public List<Specialization> GetAllSpecialization()
        {
            List<Specialization> specialization = new List<Specialization>();
            try
            {
                using (SqlConnection _connection = new SqlConnection(SqlConnectionString.GetConnectionString))
                {
                    using (SqlDataAdapter _adapter = new SqlDataAdapter("usp_GetAllSpecialization", _connection))
                    {

                        DataTable dt = new DataTable();
                        _adapter.Fill(dt);
                        if (dt != null)
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                specialization.Add(new Specialization
                                {
                                    SpecializationCode = row["SpecializationCode"].ToString(),
                                    SpecializationName = row["SpecializationName"].ToString()
                                });
                            }
                        }

                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex);
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine(ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return specialization;

        }
        
        /// <summary>
        /// Deletes a doctor from the doctor table using a stored procedure
        /// </summary>
        /// <param name="doctorId"></param>
        /// <returns>a boolean value true or false indicating Success or failure</returns>
        public bool DeleteDoctor(int doctorId)
        {
            try
            {
                using (_connection = new SqlConnection(SqlConnectionString.GetConnectionString))
                {
                    using (_adapter = new SqlDataAdapter("usp_DeleteDoctor", _connection))
                    {
                        _adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                        _adapter.SelectCommand.Parameters.AddWithValue("@DoctorId", doctorId);
                        SqlParameter ret = new SqlParameter() 
                        { Direction=ParameterDirection.ReturnValue};
                        _adapter.SelectCommand.Parameters.Add(ret);
                        using (_ds = new DataSet())
                        {
                            _adapter.Fill(_ds, "Doctor");

                            if (Convert.ToInt32(ret.Value) > 0)
                            {
                                return true;

                            }
                            return false;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex);
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine(ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return false;
        }

        /// <summary>
        /// Updates a particular doctor in the doctor table
        /// </summary>
        /// <param name="dObj"></param>
        /// <returns>a boolean value true or false indicating success or failure</returns>
        public bool UpdateDoctorDetails(Doctor dObj)
        {
            try 
            {
                using (_connection = new SqlConnection(SqlConnectionString.GetConnectionString))
                {
                   
                    using (_adapter = new SqlDataAdapter("usp_UpdateDoctorDetails", _connection))
                    {
                        _adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                        _adapter.SelectCommand.Parameters.AddWithValue("@DoctorId", dObj.DoctorId);
                        _adapter.SelectCommand.Parameters.AddWithValue("@DoctorName", dObj.DoctorName);
                        SqlParameter ret = new SqlParameter() { Direction = ParameterDirection.ReturnValue };
                        _adapter.SelectCommand.Parameters.Add(ret);

                        using (_ds = new DataSet())
                        {
                            _adapter.Fill(_ds, "Doctor");

                            if (Convert.ToInt32(ret.Value) > 0)
                            {
                                return true;

                            }
                            return false;
                        }
                    }
                }
            }
            catch(SqlException ex)
            {
                Console.WriteLine(ex);
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine(ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return false;
        }

        /// <summary>
        /// Updates a particular surgery in the surgery table
        /// </summary>
        /// <param name="sObj"></param>
        /// <returns>a boolean value true or false indicating success or failure</returns>
        public bool UpdateSurgery(Surgery sObj)
        {
            try
            {
                using (_connection = new SqlConnection(SqlConnectionString.GetConnectionString))
                {

                    using (_adapter = new SqlDataAdapter("usp_UpdateSurgery", _connection))
                    {

                        _adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                        _adapter.SelectCommand.Parameters.AddWithValue("@SurgeryId", sObj.SurgeryId);
                        _adapter.SelectCommand.Parameters.AddWithValue("@StartTime", sObj.StartTime);
                        _adapter.SelectCommand.Parameters.AddWithValue("@EndTime", sObj.EndTime);
                        SqlParameter ret = new SqlParameter() { Direction = ParameterDirection.ReturnValue };
                        _adapter.SelectCommand.Parameters.Add(ret);
                        using (_ds = new DataSet())
                        {
                            _adapter.Fill(_ds, "Surgery");

                            if (Convert.ToInt32(ret.Value) > 0)
                            {
                                return true;
                            }
                            return false;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex);
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine(ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return false;
        }

        /// <summary>
        /// Retrieves all the doctors with the specified specialization
        /// </summary>
        /// <param name="specializationCode"></param>
        /// <returns>a list of doctor specializations</returns>
        public List<Doctor> GetDoctorsBySpecialization(string specializationCode)
        {
            List<Doctor> doctorSpecializationList = null;
            try
            {
                using (_connection = new SqlConnection(SqlConnectionString.GetConnectionString))
                {
                    using (_adapter = new SqlDataAdapter("usp_DoctorsBySpecialization", _connection))
                    {
                        _adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                        _adapter.SelectCommand.Parameters.AddWithValue("@specializationCode", specializationCode);
                        using (_ds = new DataSet())
                        {
                            _adapter.Fill(_ds, "DoctorsSpecialization");
                            doctorSpecializationList = _ds.Tables["DoctorsSpecialization"].AsEnumerable()
                                .Select(x => new Doctor
                                {
                                    DoctorId = x.Field<int>("DoctorId"),
                                    DoctorName = x.Field<string>("DoctorName")
                                }).ToList();
                            return doctorSpecializationList;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex);
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine(ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return doctorSpecializationList;
        }

        public Surgery GetSurgeryById(int surgeryId)
        {
            Surgery surgery = null;
            try
            {
                using (_connection = new SqlConnection(SqlConnectionString.GetConnectionString))
                {
                    using (_adapter = new SqlDataAdapter("Select * from Surgery", _connection))
                    {
                        using (_ds = new DataSet())
                        {
                            _adapter.Fill(_ds, "Surgery");
                            _adapter.FillSchema(_ds, SchemaType.Source);



                            surgery = _ds.Tables["Surgery"].AsEnumerable()
                                .Where(p => p.Field<int>("SurgeryId") == surgeryId)
                                .Select(x => new Surgery
                                {
                                    SurgeryId = x.Field<int>("SurgeryId"),
                                    DoctorId = x.Field<int>("DoctorId"),
                                    SurgeryDate = x.Field<DateTime>("SurgeryDate"),
                                    StartTime = x.Field<Decimal>("StartTime"),
                                    EndTime = x.Field<decimal>("EndTime"),
                                    SurgeryCategory = x.Field<string>("SurgeryCategory")
                                }).FirstOrDefault();



                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex);
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine(ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return surgery;
        }


        public List<User> GetAllUsers()
        {
            List<User> users = null;
            try
            {
                using (_connection = new SqlConnection(SqlConnectionString.GetConnectionString))
                {
                    using (_adapter = new SqlDataAdapter("Select * from Users", _connection))
                    {
                        using (_ds = new DataSet())
                        {
                            _adapter.Fill(_ds, "Users");
                            users = _ds.Tables["Users"].AsEnumerable().Select(x => new User
                            {
                                UserId = x.Field<int>("UserId"),
                                UserName = x.Field<string>("UserName"),
                                EmailId=x.Field<string>("EmailId"),
                                Age=x.Field<int>("Age"),
                                PhoneNumber=x.Field<string>("PhoneNumber"),
                                Address=x.Field<string>("Address"),
                                Role=x.Field<string>("Role"),
                                Password=x.Field<string>("Password")
                            }).ToList();
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex);
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine(ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return users;
        }

        public bool AddUser(User user)
        {
            try
            {
                using (_connection = new SqlConnection(SqlConnectionString.GetConnectionString))
                {
                    using (_adapter = new SqlDataAdapter("usp_AddUser", _connection))
                    {
                        _adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                        _adapter.SelectCommand.Parameters.AddWithValue("@UserName", user.UserName);
                        _adapter.SelectCommand.Parameters.AddWithValue("@EmailId", user.EmailId);
                        _adapter.SelectCommand.Parameters.AddWithValue("@Age", user.Age);
                        _adapter.SelectCommand.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);
                        _adapter.SelectCommand.Parameters.AddWithValue("@Address", (user?.Address?.Length>0)?user.Address:"");
                        _adapter.SelectCommand.Parameters.AddWithValue("@Password", user.Password);
                        using (_ds = new DataSet())
                        {
                            _adapter.Fill(_ds, "Users");
                            return true;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex);

            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine(ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

            }
            return false;
        }

        public User GetUserById(int userId)
        {
            User user = null;
            try
            {
                using (_connection = new SqlConnection(SqlConnectionString.GetConnectionString))
                {
                    using (_adapter = new SqlDataAdapter("Select * from Users", _connection))
                    {
                        using (_ds = new DataSet())
                        {
                            _adapter.Fill(_ds, "Users");
                            user = _ds.Tables["Users"].AsEnumerable().Select(x => new User
                            {
                                UserId = x.Field<int>("UserId"),
                                UserName = x.Field<string>("UserName"),
                                EmailId = x.Field<string>("EmailId"),
                                Age = x.Field<int>("Age"),
                                PhoneNumber = x.Field<string>("PhoneNumber"),
                                Address = x.Field<string>("Address"),
                                Role = x.Field<string>("Role"),
                                Password = x.Field<string>("Password")
                            }).FirstOrDefault(x => x.UserId == userId);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex);
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine(ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return user;

        }

        public Task<User> ValidateUserAsync(string email, string password)
        {
            return Task.Run(() =>
            {
                using (SqlConnection connection = new SqlConnection(SqlConnectionString.GetConnectionString))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter("Select * from Users", connection))
                    {
                        using (DataSet ds = new DataSet())
                        {
                            adapter.Fill(ds, "Users");
                            return ds.Tables[0].AsEnumerable()
                                .Select(u => new User
                                {
                                    EmailId = u.Field<string>("EmailId"),
                                    Password = u.Field<string>("Password"),
                                    Role = u.Field<string>("Role"),
                                })
                                .FirstOrDefault(x => x.EmailId == email && x.Password == password);
                        }
                    }
                }
            });
        }
    }
}
