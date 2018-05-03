using Attendance_System.Model;
using Attendance_System.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

using static System.IO.File;

namespace Attendance_System
{
    class Program
    {
        public static List<Student> students;
        public static Student CurrentStudent;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            students = new List<Student>();

            if (!ReadStudentsData())
                return;
            Application.Run(new MainForm());
            WriteAttendanceRecords();
        }

        static bool ReadStudentsData()
        {
            string addr = Path.Combine(Directory.GetCurrentDirectory(), "Students.csv");

            try
            {
                string[] lines = ReadAllLines(addr);
                int max = lines.Length;
                string[] values;
                for(int i = 1; i < max; i++)
                {
                    values = lines[i].Split(',');
                    students.Add(new Student(values[0], values[1], values[2]));
                }
                return true;
            }
            catch (FileNotFoundException x)
            {
                MessageBox.Show("Database file (Students.csv) could not be found." +
                    Environment.NewLine + "Program will exit now.", "ERROR : " + x.GetType().ToString(),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (IOException x)
            {
                MessageBox.Show("Database file (Students.csv) could not be opened." +
                    Environment.NewLine + "Program will exit now.", "ERROR : " + x.GetType().ToString(),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception x)
            {
                MessageBox.Show(x.ToString(), x.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        static void WriteAttendanceRecords()
        {
            try
            {
                string addr = Path.Combine(Directory.GetCurrentDirectory(), "Attendances.csv");
                var csv = new StringBuilder();
                csv.AppendLine("Name,ID,Attendance,TimeStamp");
                foreach (Student s in students)
                    csv.AppendLine(s.Name + "," + s.StdID + ","
                        + (s.Attend ? "Present," + s.Stamp : "Absent,N/A"));

                File.WriteAllText(addr, csv.ToString());
            }
            catch (FileNotFoundException x)
            {
                MessageBox.Show("Database file (Attendance.csv) could not be found." +
                    Environment.NewLine + "Program will exit now.", "ERROR : " + x.GetType().ToString(),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (IOException x)
            {
                MessageBox.Show("Database file (Attendance.csv) could not be opened." +
                    Environment.NewLine + "Program will exit now.", "ERROR : " + x.GetType().ToString(),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception x)
            {
                MessageBox.Show(x.ToString(), x.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
