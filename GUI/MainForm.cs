using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using CopyFuzz;
using GUI.Ui;
using RHFYP;
using RHFYP.Cards;
using RHFYP.Interfaces;

namespace GUI
{
    public partial class MainForm : Form, ICopyFuzzifyer
    {
        private readonly TimeSpan _maxElapsedTime = TimeSpan.FromTicks(TimeSpan.TicksPerSecond/10);

        /// <summary>
        ///     Keeps track of player names
        /// </summary>
        private readonly string[] _playerNames;

        private readonly Stopwatch _stopWatch = Stopwatch.StartNew();

        private readonly TimeSpan _targetElapsedTime = TimeSpan.FromTicks(TimeSpan.TicksPerSecond/30);

        private TimeSpan _accumulatedTime;
        private readonly List<ICard> _actionCardList;

        /// <summary>
        ///     Count to center map at the very beginning of the game
        /// </summary>
        private int _centerMapCount;

        private IGame _game;
        private GameUi _gameUi;
        private TimeSpan _lastTime;
        private bool _mouseDown;

        /// <summary>
        ///     The point where the mouse last was clicked
        /// </summary>
        private Point _mouseLocation = new Point(0, 0);

        private readonly int _seed;

        public MainForm(string[] playerNames, int seed,
            List<ICard> actionCardList = null)
        {
            _seed = seed;
            _actionCardList = actionCardList;
            InitializeComponent();

            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            Application.Idle += HandleApplicationIdle;

            _playerNames = playerNames;
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

        private void Form1_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.Sizable;
            WindowState = FormWindowState.Maximized;
            Size = new Size(850, 550);

            _game = new Game(_seed);

            if (_actionCardList == null)
                _game.GenerateCards();
            else
                _game.GenerateCards(_actionCardList);

            _game.SetupPlayers(_playerNames);
            _gameUi = new GameUi(_game, this, Close);

            // Emlulates the form being resized so that everything draw correctly.
            MainForm_SizeChanged(null, EventArgs.Empty);
            CenterToScreen();

            _gameUi.CenterMap(ClientSize.Width, ClientSize.Height);
            Focus();
        }

        private void HandleApplicationIdle(object sender, EventArgs e)
        {
            while (IsApplicationIdle())
            {
                Tick();
            }
        }

        /// <summary>
        ///     Draws an updated from to the screen.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Paint arguments with graphics object to paint to.</param>
        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            if (_centerMapCount < 5)
            {
                _gameUi.CenterMap(ClientSize.Width, ClientSize.Height);
                _centerMapCount++;
            }

            _gameUi?.Draw(e.Graphics, ClientSize.Width, ClientSize.Height);

            e.Graphics.DrawRectangle(new Pen(Color.FromArgb(30, 40, 35)), 0, 0, ClientSize.Width, ClientSize.Height);
        }

        /// <summary>
        ///     Occurs when the mouse is moved over the form.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event arguments.</param>
        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            MouseMoveEvent?.Invoke(sender, e);

            // To drag the gameViewer
            if (_mouseDown)
            {
                _gameUi.MoveMap(e.X - _mouseLocation.X, e.Y - _mouseLocation.Y);
            }

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
            MouseDownEvent?.Invoke(sender, e);

            _mouseLocation = e.Location;
            _mouseDown = true;
        }

        /// <summary>
        ///     Occurs when a mouse button is released over the form.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Which button was pressed.</param>
        private void MainForm_MouseUp(object sender, MouseEventArgs e)
        {
            MouseUpEvent?.Invoke(sender, e);

            _gameUi.SendClick(e.X, e.Y);
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
            _gameUi.CenterMap(ClientSize.Width, ClientSize.Height);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            KeyDownEvent?.Invoke(sender, e);
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Close();
                    break;
                case Keys.C:
                    _gameUi.CenterMap(ClientSize.Width, ClientSize.Height);
                    break;
                case Keys.E:
                    _game.GameState = GameState.Ended;
                    break;
                case Keys.R:
                    _game.Players[1].Winner = true;
                    break;
                default:
                    _gameUi.SendKey(e);
                    break;
            }
        }
        private void MainForm_MouseClick(object sender, MouseEventArgs e)
        {
            MouseClickEvent?.Invoke(sender, e);
        }

        #endregion

        #region ICopyFuzzifyer Implementation

        private OnKeyDownDel _onKeyDown;
        private OnMouseClickDel _onMouseClick;
        private OnMouseDownDel _onMouseDown;
        private OnMouseMoveDel _onMouseMove;
        private OnMouseUpDel _onMouseUp;

        private delegate void OnMouseMoveDel(MouseEventArgs e);
        private delegate void OnMouseClickDel(MouseEventArgs e);
        private delegate void OnMouseDownDel(MouseEventArgs e);
        private delegate void OnMouseUpDel(MouseEventArgs e);
        private delegate void OnKeyDownDel(KeyEventArgs e);

        public event Action<object, KeyEventArgs> KeyDownEvent;
        public event Action<object, MouseEventArgs> MouseMoveEvent;
        public event Action<object, MouseEventArgs> MouseUpEvent;
        public event Action<object, MouseEventArgs> MouseDownEvent;
        public event Action<object, MouseEventArgs> MouseClickEvent;

        public void PreTest()
        {
            GameUi.AnimationsOn = false;
        }

        public void Launch()
        {
            Application.EnableVisualStyles();
            Application.Run(this);
        }

        public void SimulateMouseMove(MouseEventArgs e)
        {
            _onMouseMove = OnMouseMove;
            var inv = BeginInvoke(_onMouseMove, e);
            EndInvoke(inv);
        }

        public void SimulateClickMouse(MouseEventArgs e)
        {
            _onMouseClick = OnMouseClick;
            var inv = BeginInvoke(_onMouseClick, e);
            EndInvoke(inv);
        }

        public void SimulateMouseDown(MouseEventArgs e)
        {
            _onMouseDown = OnMouseDown;
            var inv = BeginInvoke(_onMouseDown, e);
            EndInvoke(inv);
        }

        public void SimulateMouseUp(MouseEventArgs e)
        {
            _onMouseUp = OnMouseUp;
            var inv = BeginInvoke(_onMouseUp, e);
            EndInvoke(inv);
        }

        public void SimulateSendKey(KeyEventArgs e)
        {
            _onKeyDown = OnKeyDown;
            var inv = BeginInvoke(_onKeyDown, e);
            EndInvoke(inv);
        }

        public int MouseValidYRange => Height;

        public int MouseValidXRange => Width;

        #endregion
    }
}