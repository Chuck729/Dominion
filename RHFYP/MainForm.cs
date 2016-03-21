using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace RHFYP
{
    public partial class MainForm : Form
    {
        private GameViewer _gameViewer;
        private Game _game;

        public MainForm()
        {
            InitializeComponent();

            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            Application.Idle += HandleApplicationIdle;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            Location = new Point(0, 0);

            _game = new Game();
            _gameViewer = new GameViewer(this, _game, ClientSize.Width, ClientSize.Height);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case (Keys.Escape):
                    Close();
                    break;
            }    
        }
        void HandleApplicationIdle(object sender, EventArgs e)
        {
            while (IsApplicationIdle())
            {
                Update();
                Render();
            }
        }

        new void Update()
        {
            // ...
        }

        void Render()
        {
            
            Refresh();
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NativeMessage
        {
            public IntPtr Handle;
            public uint Message;
            public IntPtr WParameter;
            public IntPtr LParameter;
            public uint Time;
            public Point Location;
        }

        [DllImport("user32.dll")]
        public static extern int PeekMessage(out NativeMessage message, IntPtr window, uint filterMin, uint filterMax, uint remove);

        static bool IsApplicationIdle()
        {
            NativeMessage result;
            return PeekMessage(out result, IntPtr.Zero, (uint)0, (uint)0, (uint)0) == 0;
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            _gameViewer?.DrawGame(e.Graphics);
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            _gameViewer.CursurLocation = e.Location;
        }
    }
}
