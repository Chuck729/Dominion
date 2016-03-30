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
        private Point _mouseLocation = new Point(0,0);
        private bool _mouseDown;
        private GameUi _gameUi;
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
            //FormBorderStyle = FormBorderStyle.None;
            //WindowState = FormWindowState.Maximized;
            Location = new Point(0, 0);

            _game = new Game();
            _gameUi = new GameUi(_game);

            // Emlulates the form being resized so that everything draw correctly.
            MainForm_SizeChanged(null, EventArgs.Empty);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (e.KeyCode)
            {
                case (Keys.Escape):
                    Close();
                    break;
                case Keys.C:
                    _gameUi.CenterMap(ClientSize.Width, ClientSize.Height);
                    break;
                default:
                    _gameUi.SendKey(e);
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

            _gameUi?.Draw(e.Graphics);

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
                _gameUi.MoveMap(e.X - _mouseLocation.X, e.Y - _mouseLocation.Y);
            }

            _gameUi.MouseLocation = e.Location;
            _mouseLocation = e.Location;

            _gameUi.SendMouseLocation(e.X, e.Y);
        }

        /// <summary>
        /// Occurs when the mouse is pressed down over the form.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Which button was pressed.</param>
        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            _mouseDown = true;
        }

        /// <summary>
        /// Occurs when the mouse is clicked over the form.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Which button was pressed.</param>
        private void MainForm_MouseClick(object sender, MouseEventArgs e)
        {
            _gameUi.SendClick(e.X, e.Y);
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

        /// <summary>
        /// When the forms size is changed
        /// </summary>
        /// <param name="sender">Form sender.</param>
        /// <param name="e">Event arguments.</param>
        /// <remarks>Used to change the gameviewer resolution when the window is changed.</remarks>
        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            if (_gameUi == null) return;
            _gameUi.XResolution = ClientSize.Width;
            _gameUi.YResolution = ClientSize.Height;
            _gameUi.AdjustSidebar(ClientSize.Width, ClientSize.Height);
            _gameUi.CenterMap(ClientSize.Width, ClientSize.Height);
        }

        #endregion
    }
}
