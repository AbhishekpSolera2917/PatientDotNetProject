using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using PatientDatabaseLibrary;



namespace PatientDatabaseDemo
{
    public class PatientProcedureDB
    {
        private SqlConnection conn;
        private SqlDataReader reader;
        private SqlCommand sqlCmd;

        public PatientProcedureDB(string serverName, string database, string username, string password)
        {
            conn = new SqlConnection($"Server={serverName}; Database={database}; User Id={username}; Password={password}");
        }

        public void AddPatient(Patient p)
        {
            string name = p.PName;
            string gender = "";

            if (p.PGender == Gender.MALE) gender = "M";
            else gender = "F";

            string mobile = p.PMobile;
            double weight = p.PWeight;
            double height = p.PHeight;
            string disease = p.PDisease;


            string insertQuery = $"AddPatient";

            sqlCmd = new SqlCommand(insertQuery, conn);

            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.Add(new SqlParameter("@name", name));
            sqlCmd.Parameters.Add(new SqlParameter("@gender", gender));
            sqlCmd.Parameters.Add(new SqlParameter("@mobile", mobile));
            sqlCmd.Parameters.Add(new SqlParameter("@weight", weight));
            sqlCmd.Parameters.Add(new SqlParameter("@height", height));
            sqlCmd.Parameters.Add(new SqlParameter("@disease", disease));

            try
            {
                conn.Open();
                sqlCmd.ExecuteNonQuery();
                Console.WriteLine("Patient record inserted successfully");
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (conn.State != ConnectionState.Closed) conn.Close();
            }
        }

        public bool EditPatient(int pId, Patient p)
        {
            int id = pId;
            string name = p.PName;
            string gender = "";

            if (p.PGender == Gender.MALE) gender = "M";
            else gender = "F";

            string mobile = p.PMobile;
            double weight = p.PWeight;
            double height = p.PHeight;
            string disease = p.PDisease;
            string status = "";


            string updateQuery = $"UpdatePatient";

            sqlCmd = new SqlCommand(updateQuery, conn);

            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.Add(new SqlParameter("@id", id));
            sqlCmd.Parameters.Add(new SqlParameter("@name", name));
            sqlCmd.Parameters.Add(new SqlParameter("@gender", gender));
            sqlCmd.Parameters.Add(new SqlParameter("@mobile", mobile));
            sqlCmd.Parameters.Add(new SqlParameter("@weight", weight));
            sqlCmd.Parameters.Add(new SqlParameter("@height", height));
            sqlCmd.Parameters.Add(new SqlParameter("@disease", disease));
            SqlParameter statusParamter = new SqlParameter("@status", SqlDbType.VarChar, 100);
            statusParamter.Direction = ParameterDirection.Output;
            sqlCmd.Parameters.Add(statusParamter);

            int queryStatus = 0;

            try
            {
                conn.Open();

                queryStatus = sqlCmd.ExecuteNonQuery();

                status = sqlCmd.Parameters[5].Value.ToString();

                Console.WriteLine(status);
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (conn.State != ConnectionState.Closed) conn.Close();
            }

            if (queryStatus == 0) return false;
            else return true;
        }

        public bool RemovePatient(int id)
        {
            string status = "";
            string deleteQuery = $"DeletePatient";

            sqlCmd = new SqlCommand(deleteQuery, conn);

            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.Add(new SqlParameter("@id", id));
            SqlParameter statusParamter = new SqlParameter("@status", SqlDbType.VarChar, 100);
            statusParamter.Direction = ParameterDirection.Output;
            sqlCmd.Parameters.Add(statusParamter);


            int queryStatus = 0;

            try
            {
                conn.Open();

                queryStatus = sqlCmd.ExecuteNonQuery();

                status = sqlCmd.Parameters["@status"].Value.ToString();

                Console.WriteLine(status);
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (conn.State != ConnectionState.Closed) conn.Close();
            }

            if (queryStatus == 0) return false;
            else return true;
        }

        public Patient FindPatient(int pId)
        {
            Patient patient = null;

            string findPatientQuery = $"FindPatient";

            sqlCmd = new SqlCommand(findPatientQuery, conn);

            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.Add(new SqlParameter("@id", pId));

            try
            {
                conn.Open();
                reader = sqlCmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    int id = (int)reader["id"];
                    string name = (string)reader["name"];
                    Gender gender;

                    if ((string)reader["gender"] == "M") gender = Gender.MALE;
                    else gender = Gender.FEMALE;

                    string mobile = (string)reader["mobile"];
                    double weight = (double)reader["weight"];
                    double height = (double)reader["height"];
                    string disease = (string)reader["disease"];

                    patient = new Patient(id, name, gender, mobile, weight, height, disease);
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (conn.State != ConnectionState.Closed) conn.Close();
            }

            return patient;
        }

        public List<Patient> GetAllPatient()
        {
            List<Patient> patients = new List<Patient>();

            string getAllPatientQuery = $"PatientSummary";

            sqlCmd = new SqlCommand(getAllPatientQuery, conn);

            sqlCmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                reader = sqlCmd.ExecuteReader();

                while (reader.Read())
                {
                    int id = (int)reader["id"];
                    string name = (string)reader["name"];
                    Gender gender;

                    if ((string)reader["gender"] == "M") gender = Gender.MALE;
                    else gender = Gender.FEMALE;

                    string mobile = (string)reader["mobile"];
                    double weight = (double)reader["weight"];
                    double height = (double)reader["height"];
                    string disease = (string)reader["disease"];

                    patients.Add(new Patient(id, name, gender, mobile, weight, height, disease));
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (conn.State != ConnectionState.Closed) conn.Close();
            }

            return patients;
        }

        public List<Patient> GetPatientByName(string pName)
        {
            List<Patient> patients = new List<Patient>();

            string getPatientByNameQuery = $"GetPatientByName";

            sqlCmd = new SqlCommand(getPatientByNameQuery, conn);

            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.Add(new SqlParameter("@name", pName));

            try
            {
                conn.Open();
                reader = sqlCmd.ExecuteReader();

                while (reader.Read())
                {
                    int id = (int)reader["id"];
                    string name = (string)reader["name"];
                    Gender gender;

                    if ((string)reader["gender"] == "M") gender = Gender.MALE;
                    else gender = Gender.FEMALE;

                    string mobile = (string)reader["mobile"];
                    double weight = (double)reader["weight"];
                    double height = (double)reader["height"];
                    string disease = (string)reader["disease"];

                    patients.Add(new Patient(id, name, gender, mobile, weight, height, disease));
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (conn.State != ConnectionState.Closed) conn.Close();
            }

            return patients;
        }

        public List<Patient> GetPatientByGender(Gender pGender)
        {
            List<Patient> patients = new List<Patient>();

            string patientGender = "";

            if (pGender == Gender.MALE) patientGender = "M";
            else patientGender = "F";

            string getPatientByGenderQuery = $"GetPatientByGender";

            sqlCmd = new SqlCommand(getPatientByGenderQuery, conn);

            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.Add(new SqlParameter("@gender", patientGender));

            try
            {
                conn.Open();
                reader = sqlCmd.ExecuteReader();

                while (reader.Read())
                {
                    int id = (int)reader["id"];
                    string name = (string)reader["name"];
                    Gender gender;

                    if ((string)reader["gender"] == "M") gender = Gender.MALE;
                    else gender = Gender.FEMALE;

                    string mobile = (string)reader["mobile"];
                    double weight = (double)reader["weight"];
                    double height = (double)reader["height"];
                    string disease = (string)reader["disease"];

                    patients.Add(new Patient(id, name, gender, mobile, weight, height, disease));
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (conn.State != ConnectionState.Closed) conn.Close();
            }

            return patients;
        }
    }
}