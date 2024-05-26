using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Master_Jump.Abstractions.Implementations;
using Master_Jump.Controllers;
using Master_Jump.Models;
using Master_Jump.Properties;
using Timer = System.Windows.Forms.Timer;

namespace Master_Jump
{
    public partial class Form1 : Form
    {
        private static int _score;
        
        private float _offset;

        private float _localOffset;

        private static readonly Model PlayerModel = new Model(new Point(100, 350), new Size(40,40));
        
        private static readonly PlayerPhysics PlayerPhysics = new PlayerPhysics(PlayerModel);
        
        private static readonly Player Player = new Player(PlayerModel, PlayerPhysics);
        
        private readonly Timer _timer = new Timer();
        
        public Form1()
        {
            InitializeComponent();
            Init();
            _timer.Interval = 1;
            _timer.Tick += Update;
            _timer.Start();
            KeyDown += OnKeyBoardPressed;
            KeyUp += OnKeyBoardUp;
            Paint += DrawModels;
            MouseClick += OnMouseButtonPressed;
            typeof(Panel).InvokeMember("DoubleBuffered", System.Reflection.BindingFlags.SetProperty |
                                                         System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic, null, panel1, new object[] { true });
        }
        
        private static void Init()
        {
            PlatformController.Platforms.Add(new BasePlatform(new Point(100, 400)));

            PlatformController.GenerateStartPlatforms();
        }
        
        private static void DrawModels(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            foreach (var t in PlatformController.Platforms)
            {
                t.DrawPlatform(graphics);
            }

            foreach (var bullet in BulletController.Bullets)
            {
                bullet.DrawBullet(graphics);
            }

            foreach (var enemy in EnemyController.Enemies)
            {
                enemy.DrawUnit(graphics);
            }
            Player.DrawUnit(graphics);
        }
        
        private static void OnKeyBoardUp(object sender, KeyEventArgs eventArgs)
        {
            if (Player.Physics is PlayerPhysics playerPhysics)
                playerPhysics.XCoords = 0;
        }
        private static void OnKeyBoardPressed(object sender, KeyEventArgs eventArgs)
        {
            if (!(Player.Physics is PlayerPhysics playerPhysics)) return;
            switch (eventArgs.KeyCode)
            {
                case Keys.Right:
                    playerPhysics.XCoords = 6;
                    Player.Sprite = Resources.man2;
                    break;
                case Keys.Left:
                    playerPhysics.XCoords = -6;
                    Player.Sprite = Resources.man;
                    break;
            }
        }
        private void OnMouseButtonPressed(object sender, MouseEventArgs eventArgs)
        {
            var cursorPosition = PointToClient(Cursor.Position);
            BulletController.GenerateBullet(Player.Model, cursorPosition);
            Player.Sprite = Resources.man_shooting;
        }
        private void Update(object sender, EventArgs eventArgs)
        {
            if (PlatformController.Platforms.Count != 0)
            {
                if (Player.Model.Coordinates.Y >
                    PlatformController.Platforms[0].Model.Coordinates.Y + 20 || Player.IsFalling)
                {
                    Player.IsFalling = true;
                    Player.Model.Coordinates.Y -= 1;
                    foreach (var platform in PlatformController.Platforms)
                    {
                        platform.Model.Coordinates.Y -= 8;
                    }
                    foreach (var enemy in EnemyController.Enemies)
                    {
                        enemy.Model.Coordinates.Y -= 8;
                    }
                    PlatformController.ClearPlatforms();
                    Invalidate();
                    return;
                }
            }
            PlatformController.ClearPlatforms();
            EnemyController.ClearEnemy();
            BulletController.ClearBullet();
            bool isTouched = Player.Physics.CalculatePhysics();
            
            if (isTouched)
            {
                _offset = 400 - Player.Model.Coordinates.Y;
                _localOffset = _offset / 23;
            }
            if (_offset>0)
            {
                _offset -= _localOffset;
                TrackPlayer(_localOffset);
            }
            Invalidate();
        }
        
        private void TrackPlayer(float offset)
        {
            var point = Player.Model.Coordinates;
            point.Y += offset;
            Player.Model.Coordinates = point;
            _score += (int)offset;
            label1.Text = _score.ToString();
            foreach (var platform in PlatformController.Platforms)
            {
                platform.Model.Coordinates.Y += offset;
            }

            foreach (var enemy in EnemyController.Enemies)
            {
                enemy.Model.Coordinates.Y += offset;
            }
        }
        public static void MoveBrokenPlatforms()
        {
            var crushedPlatforms = PlatformController.Platforms
                .OfType<BrokenPlatform>()
                .Where(cp => cp.IsTouched)
                .ToList();
            while (crushedPlatforms.Count != 0 )
            {
                foreach (var platform in crushedPlatforms)
                {
                    platform.Model.Coordinates.Y += 7;

                    if (platform.Model.Coordinates.Y >= 600 || Player.IsFalling)
                    {
                        crushedPlatforms.Remove(platform);
                        PlatformController.Platforms.Remove(platform);
                        return;
                    }
                }
                Thread.Sleep(50);
            }
        }
    }
}