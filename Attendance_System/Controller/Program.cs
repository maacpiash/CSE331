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

            ReadStudentsData();
            Application.Run(new MainForm());
            WriteAttendanceRecords();
        }

        static void ReadStudentsData()
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
                
            }
            catch (Exception x)
            {
                MessageBox.Show(x.GetType().ToString());
            }
        }

        static void WriteAttendanceRecords()
        {
            string addr = Path.Combine(Directory.GetCurrentDirectory(), "Attendances.csv");
            var csv = new StringBuilder();
            csv.AppendLine("Name,ID,Attendance,TimeStamp");
            foreach (Student s in students)
                csv.AppendLine(s.Name + "," + s.StdID + ","
                    + (s.Attend ? "Present," + s.Stamp : "Absent,N/A"));

            File.WriteAllText(addr, csv.ToString());
        }
    }
}
