using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using GUI.Ui;
using RHFYP;

namespace GUI
{
    public partial class MainForm : Form
    {
        private readonly TimeSpan _maxElapsedTime = TimeSpan.FromTicks(TimeSpan.TicksPerSecond/10);
        private readonly Stopwatch _stopWatch = Stopwatch.StartNew();

        private readonly TimeSpan _targetElapsedTime = TimeSpan.FromTicks(TimeSpan.TicksPerSecond/30);

        private TimeSpan _accumulatedTime;
        private IGame _game;
        private GameUi _gameUi;
        private TimeSpan _lastTime;
        private bool _mouseDown;
        private bool _movedSinceMouseDown;

        /// <summary>
        /// Keeps track of player names 
        /// </summary>
        private string[] playerNames;

        /// <summary>
        ///     The point where the mouse last was clicked
        /// </summary>
        private Point _mouseLocation = new Point(0, 0);


        public MainForm()
        {
            InitializeComponent();

            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            Application.Idle += HandleApplicationIdle;
        }

        public MainForm(string name1, string name2, string name3, string name4)
        {
            InitializeComponent();

            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            Application.Idle += HandleApplicationIdle;

            if (name4 == null)
            {
                playerNames = name3 == null ? new[] { name1, name2 } : new[] { name1, name2, name3 };
            } else
            {
                playerNames = new[] { name1, name2, name3, name4 };
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //FormBorderStyle = FormBorderStyle.None;
            //WindowState = FormWindowState.Maximized;
            Size = new Size(850, 550);

            _game = new Game();
            _game.GenerateCards();
            _game.SetupPlayers(playerNames);
            _gameUi = new GameUi(_game);

            // Emlulates the form being resized so that everything draw correctly.
            MainForm_SizeChanged(null, EventArgs.Empty);
            CenterToScreen();

            _gameUi.CenterMap(ClientSize.Width, ClientSize.Height);
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

        private void HandleApplicationIdle(object sender, EventArgs e)
        {
            while (IsApplicationIdle())
            {
                Tick();
            }
        }

        private void Tick()
        {
            var currentTime = _stopWatch.Elapsed;
            var elapsedTime = currentTime - _lastTime;
            _lastTime = currentTime;

            if (elapsedTime > _maxElapsedTime)
            {
                elapsedTime = _maxElapsedTime;
            }

            _accumulatedTime += elapsedTime;

            var updated = false;

            while (_accumulatedTime >= _targetElapsedTime)
            {
                Update();

                _accumulatedTime -= _targetElapsedTime;
                updated = true;
            }

            if (updated)
            {
                Invalidate();
            }
        }

        private new void Update()
        {
            // ...
        }

        [DllImport("user32.dll")]
        private static extern int PeekMessage(out NativeMessage message, IntPtr window, uint filterMin, uint filterMax,
            uint remove);

        /// <summary>
        ///     Checks to see if the windows message pump is empty.
        /// </summary>
        /// <returns>True is the windows messege pump is empty.</returns>
        private static bool IsApplicationIdle()
        {
            NativeMessage result;
            return PeekMessage(out result, IntPtr.Zero, 0, 0, 0) == 0;
        }


        [StructLayout(LayoutKind.Sequential)]
        private struct NativeMessage
        {
            private readonly IntPtr Handle;
            private readonly uint Message;
            private readonly IntPtr WParameter;
            private readonly IntPtr LParameter;
            private readonly uint Time;
            private readonly Point Location;
        }

        #region Form Event Handlers

        /// <summary>
        ///     Draws an updated from to the screen.
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
        ///     Occurs when the mouse is moved over the form.
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

            _movedSinceMouseDown = true;
            _gameUi.MouseLocation = e.Location;
            _mouseLocation = e.Location;

            _gameUi.SendMouseLocation(e.X, e.Y);
        }

        /// <summary>
        ///     Occurs when the mouse is pressed down over the form.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Which button was pressed.</param>
        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            _movedSinceMouseDown = false;
            _mouseLocation = e.Location;
            _mouseDown = true;
        }

        /// <summary>
        ///     Occurs when the mouse is clicked over the form.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Which button was pressed.</param>
        private void MainForm_MouseClick(object sender, MouseEventArgs e)
        {
            if (!_movedSinceMouseDown)
            {
                _gameUi.SendClick(e.X, e.Y);
            }
        }

        /// <summary>
        ///     Occurs when a mouse button is released over the form.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Which button was pressed.</param>
        private void MainForm_MouseUp(object sender, MouseEventArgs e)
        {
            _mouseDown = false;
        }

        /// <summary>
        ///     When the forms size is changed
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
            _gameUi.CardInfo.AdjustSizeAndPosition(ClientSize.Width, ClientSize.Height);
        }

        #endregion
    }
}