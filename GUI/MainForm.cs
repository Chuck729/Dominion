using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using RHFYP;

namespace GUI
{
    public partial class MainForm : Form
    {

        /// <summary>
        /// The point where the mouse last was clicked
        /// </summary>
        private Point _lastMousePoint = new Point(0,0);
        private bool _mouseDown;
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
                case (Keys.C):
                    _gameViewer.MapCenter = new Point(ClientSize.Width/2, ClientSize.Height/2);
                    break;
                case (Keys.S):
                    _gameViewer.Map.SelectPointMode = !_gameViewer.Map.SelectPointMode;
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

        /// <summary>
        /// Checks to see if the windows message pump is empty.
        /// </summary>
        /// <returns>True is the windows messege pump is empty.</returns>
        static bool IsApplicationIdle()
        {
            NativeMessage result;
            return PeekMessage(out result, IntPtr.Zero, 0, 0, 0) == 0;
        }

        #region Form Event Handlers

        /// <summary>
        /// Draws an updated from to the screen.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Paint arguments with graphics object to paint to.</param>
        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            _gameViewer?.DrawGame(e.Graphics);
            e.Graphics.DrawRectangle(Pens.Black, 0, 0, ClientSize.Width, ClientSize.Height);
        }

        /// <summary>
        /// Occurs when the mouse is moved over the form.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event arguments.</param>
        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            // To drag the gameViewer
            if (_mouseDown)
            {
                _gameViewer.MapCenter = new Point(_gameViewer.MapCenter.X + (e.Location.X - _lastMousePoint.X), _gameViewer.MapCenter.Y + (e.Location.Y - _lastMousePoint.Y));
            }
            _gameViewer.CursurLocation = e.Location;
            _lastMousePoint = e.Location;
        }

        /// <summary>
        /// Occurs when the mouse is pressed down over the form.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Which button was pressed.</param>
        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            _mouseDown = true;
            _lastMousePoint = e.Location;
        }

        /// <summary>
        /// Occurs when the mouse is clicked over the form.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Which button was pressed.</param>
        private void MainForm_MouseClick(object sender, MouseEventArgs e)
        {
            _gameViewer.SendMouseClick();
        }

        /// <summary>
        /// Occurs when a mouse button is released over the form.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Which button was pressed.</param>
        private void MainForm_MouseUp(object sender, MouseEventArgs e)
        {
            _mouseDown = false;
        }

        #endregion
    }
}
