using System;
using static System.DateTime;

namespace Attendance_System.Model
{
    public class Student
    {
        public string Name { get => _name; set => _name = value; }
        private string _name;

        public string StdID { get => _stdid; set => _stdid = value; }
        private string _stdid;

        public string TagID { get => _tagid; set => _tagid = value; }
        private string _tagid;

        public bool Attend { get => _attend; set => _attend = value; }
        private bool _attend;

        public DateTime Stamp
        {
            get => _stamp;
            set => _stamp = Compare(value, Now) > 0 ? Now : value;
        }
        private DateTime _stamp;

        public Student(string n, string i, string t)
        {
            _name = n;
            _stdid = i;
            _tagid = t;
            _attend = false;
        }
    }
}
