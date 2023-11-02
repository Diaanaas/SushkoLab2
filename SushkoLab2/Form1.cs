using SushkoLab2.CLasses;

namespace SushkoLab2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void DrawButton_Click(object sender, EventArgs e)
        {
            Drawer.CreateGraphics(MainPictureBox);
            Drawer.DrawCoordinateSystem();
            Drawer.DrawFunction();
        }
    }
}