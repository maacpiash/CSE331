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
            
        }
    }
}
