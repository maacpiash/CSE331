using System.Drawing;
using System.Windows.Forms;

namespace Attendance_System.View
{
    public partial class CreditsForm : Form
    {
        public CreditsForm()
        {
            InitializeComponent();
            Reposition();
            Resize += (sender, e) => Reposition();
        }

        private void Reposition()
        {
            int x = (Width - label1.Width) / 2;
            label1.Location = new Point(x, 9);
            x = (Width - label2.Width) / 2;
            label2.Location = new Point(x, 37);
        }
    }
}
