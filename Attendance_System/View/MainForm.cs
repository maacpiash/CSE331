using Attendance_System.Model;
using System;
using System.IO;
using System.IO.Ports;
using System.Windows.Forms;

using static Attendance_System.Program;

namespace Attendance_System.View
{
    public partial class MainForm : Form
    {
        string tagID;
        bool found;
        public MainForm()
        {
            InitializeComponent();
            tagID = "";
            try { serialPort1.Open(); }
            catch (IOException e)
            {
                MessageBox.Show("Lost connection with RFID module!", "ERROR : "
                    + e.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (UnauthorizedAccessException x)
            {
                MessageBox.Show("The port could not be accessed!", "ERROR : "
                        + x.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            button2.Click += (sender, e) => (new CreditsForm()).Show();
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if(tagID.Length < 13)
                tagID += serialPort1.ReadExisting();
            if (tagID.Length == 13)
                if (RecordAttendance())
                    ShowStudentDetails(CurrentStudent);
        }

        private void ShowStudentDetails(Student s)
        {
            MessageBox.Show("Name: " + s.Name + '\n' + "ID: " + s.StdID + '\n'
                + "Time of entrance: " + s.Stamp.ToString(), "Attendance recorded "
                + "successfully!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private bool RecordAttendance()
        {
            //MessageBox.Show(tagID);
            found = false;
            foreach(Student s in students)
            {
                if(tagID.Contains(s.TagID))
                {
                    s.Stamp = DateTime.Now;
                    s.Attend = true;
                    found = true;
                    CurrentStudent = s;
                    break;
                }
            }
            tagID = "";
            if (found)
                return true;
            MessageBox.Show("Student record not found.", "ERROR : " + tagID,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            serialPort1.Close();
            Close();
        }
    }
}
