using PatientDatabaseDemo;
using PatientDatabaseLibrary;
using System;

namespace PatientAssigment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            PatientCLIView patientView = new PatientCLIView();

            bool exit = false;

            while (!exit)
            {
                int choice = patientView.Menu();

                switch (choice)
                {
                    case 1:
                        patientView.AddPatientView();
                        break;

                    case 2:
                        patientView.EditPatientView();
                        break;

                    case 3:
                        patientView.RemovePatientView();
                        break;

                    case 4:
                        patientView.FindPatientView();
                        break;

                    case 5:
                        patientView.GetPatientByNameView();
                        break;

                    case 6:
                        patientView.GetPatientByGenderView();
                        break;

                    case 7:
                        patientView.GetAllPatientView();
                        break;

                    case 8:
                        exit = true;
                        GC.Collect();
                        break;
                }
            }
        }
    }
}

