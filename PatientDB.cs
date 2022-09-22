using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using PatientDatabaseLibrary;



namespace PatientDatabaseDemo
{
    public class PatientDB
    {
        private SqlConnection conn;
        private SqlDataReader reader;
        private SqlCommand sqlCmd;

        public PatientDB(string serverName, string database, string username, string password)
        {
            conn = new SqlConnection($"Server={serverName}; Database={database}; User Id={username}; Password={password}");
        }

        public void AddPatient(Patient p)
        {
            int id = p.PId;
            string name = p.PName;
            string gender = "";

            if (p.PGender == Gender.MALE) gender = "M";
            else gender = "F";

            string mobile = p.PMobile;
            double weight = p.PWeight;
            double height = p.PHeight;
            string disease = p.PDisease;


            string insertQuery = $"INSERT INTO Patient VALUES ({id}, '{name}', '{gender}', '{mobile}', {weight}, {height}, '{disease}')";

            sqlCmd = new SqlCommand(insertQuery, conn);

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


            string updateQuery = $"UPDATE Patient SET name='{name}', gender'{gender}', mobile='{mobile}', weight{weight}, height={height}, disease='{disease}' WHERE id={id}";

            sqlCmd = new SqlCommand(updateQuery, conn);

            int queryStatus = 0;

            try
            {
                conn.Open();
                queryStatus = sqlCmd.ExecuteNonQuery();
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

            string deleteQuery = $"DELETE FROM Patient WHERE id={id}";

            sqlCmd = new SqlCommand(deleteQuery, conn);

            int queryStatus = 0;

            try
            {
                conn.Open();
                queryStatus = sqlCmd.ExecuteNonQuery();
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

            string findPatientQuery = $"SELECT * FROM Patient WHERE id={pId}";

            sqlCmd = new SqlCommand(findPatientQuery, conn);

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

            string getAllPatientQuery = $"SELECT * FROM Patient";

            sqlCmd = new SqlCommand(getAllPatientQuery, conn);

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

        //public int GetIndex(int id)
        //{
        //    int index = 0;

        //    foreach (Patient p in patients)
        //    {
        //        if (p.PId == id) return index;
        //        index++;
        //    }
        //    return -1;
        //}

        public List<Patient> GetPatientByName(string pName)
        {
            List<Patient> patients = new List<Patient>();

            string GetPatientByNameQuery = $"SELECT * FROM Patient WHERE name='{pName}'";

            sqlCmd = new SqlCommand(GetPatientByNameQuery, conn);

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

            string GetPatientByGenderQuery = $"SELECT * FROM Patient WHERE gender='{patientGender}'";

            sqlCmd = new SqlCommand(GetPatientByGenderQuery, conn);

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

        public int GeneratePatientId()
        {
            string maxIdquery = "SELECT MAX(id) FROM Patient";
            sqlCmd = new SqlCommand(maxIdquery, conn);
            int genId = 0;

            try
            {
                conn.Open();
                var id = sqlCmd.ExecuteScalar();

                if (id.ToString() == "") genId = 1;
                else genId = Convert.ToInt32(id) + 1;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (conn.State != ConnectionState.Closed) conn.Close();
            }
            return genId;
        }
    }
}