using System;
using System.Collections.Generic;
using PatientDatabaseLibrary;


namespace PatientDatabaseDemo
{
    public class PatientCLIView
    {
        PatientProcedureDB patientDB;

        List<Patient> patients;

        public PatientCLIView()
        {
            patientDB = new PatientProcedureDB(".", "PatientDB", "sa", "sushLALA83#");
        }

        public int Menu()
        {
            Console.WriteLine("\nPlease select a Choice : \n");
            Console.WriteLine("(1) Add new patient");
            Console.WriteLine("(2) Modify a patient");
            Console.WriteLine("(3) Remove a patient");
            Console.WriteLine("(4) Find a patient");
            Console.WriteLine("(5) Find patients by their name");
            Console.WriteLine("(6) Find patients by their Gender");
            Console.WriteLine("(7) Patient summary");
            Console.WriteLine("(8) Exit\n");

            int choice = int.Parse(Console.ReadLine());
            return choice;
        }

        public void AddPatientView()
        {
            Console.Write("Enter Patient Name : ");
            string name = Console.ReadLine();

            Console.Write("Enter Patient Gender : ");
            string g = Console.ReadLine();
            Gender gender;
            if (g.ToLower() == "male") gender = Gender.MALE;
            else gender = Gender.FEMALE;

            Console.Write("Enter Patient Mobile : ");
            string mobile = Console.ReadLine();

            Console.Write("Enter Patient Weight : ");
            double weight = double.Parse(Console.ReadLine());

            Console.Write("Enter Patient Height : ");
            double height = double.Parse(Console.ReadLine());

            Console.Write("Enter Patient Disease : ");
            string disease = Console.ReadLine();

            patientDB.AddPatient(new Patient(1, name, gender, mobile, weight, height, disease));

        }

        public void EditPatientView()
        {
            Console.Write("Enter Patient ID : ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Enter Patient Name : ");
            string name = Console.ReadLine();

            Console.Write("Enter Patient Gender : ");
            string g = Console.ReadLine();
            Gender gender;
            if (g.ToLower() == "male") gender = Gender.MALE;
            else gender = Gender.FEMALE;

            Console.Write("Enter Patient Mobile : ");
            string mobile = Console.ReadLine();

            Console.Write("Enter Patient Weight : ");
            double weight = double.Parse(Console.ReadLine());

            Console.Write("Enter Patient Height : ");
            double height = double.Parse(Console.ReadLine());

            Console.Write("Enter Patient Disease : ");
            string disease = Console.ReadLine();

            try
            {
                patientDB.EditPatient(id, new Patient(id, name, gender, mobile, weight, height, disease));
            }
            catch (PatientNotExistsException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void RemovePatientView()
        {

            Console.Write("Enter Patient ID : ");
            int id = int.Parse(Console.ReadLine());

            try
            {
                patientDB.RemovePatient(id);
            }
            catch (PatientNotExistsException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void FindPatientView()
        {
            Console.Write("Enter Patient ID : ");
            int id = int.Parse(Console.ReadLine());

            try
            {
                Patient p = patientDB.FindPatient(id);
                Console.WriteLine(p);
            }
            catch (PatientNotExistsException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void GetAllPatientView()
        {
            try
            {
                List<Patient> patients = patientDB.GetAllPatient();

                Console.WriteLine("__________________________________________________________________________________________________________________");
                Console.WriteLine("ID \t Name \t Gender \t Mobile \t Weight \t Height \t Disease");
                Console.WriteLine("__________________________________________________________________________________________________________________");

                foreach (Patient p in patients)
                {
                    Console.WriteLine($"{p.PId} \t {p.PName} \t {p.PGender} \t {p.PMobile} \t {p.PWeight} \t {p.PHeight} \t {p.PDisease}");
                }

                Console.WriteLine("__________________________________________________________________________________________________________________");
            }
            catch (EmptyException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void GetPatientByNameView()
        {
            try
            {
                Console.Write("Enter a name : ");
                string name = Console.ReadLine();

                List<Patient> patients = patientDB.GetPatientByName(name);

                Console.WriteLine("__________________________________________________________________________________________________________________");
                Console.WriteLine("ID \t Name \t Gender \t Mobile \t Weight \t Height \t Disease");
                Console.WriteLine("__________________________________________________________________________________________________________________");

                foreach (Patient p in patients)
                {
                    Console.WriteLine($"{p.PId} \t {p.PName} \t {p.PGender} \t {p.PMobile} \t {p.PWeight} \t {p.PHeight} \t {p.PDisease}");
                }

                Console.WriteLine("__________________________________________________________________________________________________________________");
            }
            catch (EmptyException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void GetPatientByGenderView()
        {
            try
            {
                Console.Write("Enter a gender : ");

                string g = Console.ReadLine();
                Gender gender;

                if (g.ToLower() == "male") gender = Gender.MALE;
                else gender = Gender.FEMALE;

                List<Patient> patients = patientDB.GetPatientByGender(gender);

                Console.WriteLine("__________________________________________________________________________________________________________________");
                Console.WriteLine("ID \t Name \t Gender \t Mobile \t Weight \t Height \t Disease");
                Console.WriteLine("__________________________________________________________________________________________________________________");

                foreach (Patient p in patients)
                {
                    Console.WriteLine($"{p.PId} \t {p.PName} \t {p.PGender} \t {p.PMobile} \t {p.PWeight} \t {p.PHeight} \t {p.PDisease}");
                }

                Console.WriteLine("__________________________________________________________________________________________________________________");
            }
            catch (EmptyException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
