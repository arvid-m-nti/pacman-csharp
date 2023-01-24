using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PacmanGame2._0
{
    // I Form1.cs [Design] finns är den 800x800 yta som spelet är på, bakgrunden är svart och det finns två textrutor
    public partial class PacmanGame : Form
    {
        // Enum för vilket håll ett föremål går
        public enum Direction
        {
            Up, Left, Down, Right, None
        }
        // Klass för rörarnde rektanglar (spelare & spöken)
        public class MovingRect
        {
            public int PosX { get; set; }
            public int PosY { get; set; }
            public int SpeedX { get; set; }
            public int SpeedY { get; set; }
            public Brush Color { get; set; }
            public int Speed { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
            public Direction CurrentDirection { get; set; }
        }
        // Klass för stillastående rektanglar (väggar, pellets, mm.)
        public class StillRect
        {
            public int PosX { get; set; }
            public int PosY { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
        }
        // 23x22 grid
        private string[,] grid = {
            { "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-" },
            { "-", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "-", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "-" },
            { "-", "e", "-", "-", "-", ".", "-", "-", "-", "-", ".", "-", ".", "-", "-", "-", "-", ".", "-", "-", "-", "e", "-" },
            { "-", ".", "-", "-", "-", ".", "-", "-", "-", "-", ".", "-", ".", "-", "-", "-", "-", ".", "-", "-", "-", ".", "-" },
            { "-", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "-" },
            { "-", ".", "-", "-", "-", ".", "-", ".", "-", "-", "-", "-", "-", "-", "-", ".", "-", ".", "-", "-", "-", ".", "-" },
            { "-", ".", ".", ".", ".", ".", "-", ".", ".", ".", ".", "-", ".", ".", ".", ".", "-", ".", ".", ".", ".", ".", "-" },
            { "-", "-", "-", "-", "-", ".", "-", "-", "-", "-", " ", "-", " ", "-", "-", "-", "-", ".", "-", "-", "-", "-", "-" },
            { "-", "-", "-", "-", "-", ".", "-", " ", " ", " ", " ", " ", " ", " ", " ", " ", "-", ".", "-", "-", "-", "-", "-" },
            { "-", "-", "-", "-", "-", ".", "-", " ", "-", "-", "-", "d", "-", "-", "-", " ", "-", ".", "-", "-", "-", "-", "-" },
            { " ", " ", " ", " ", " ", ".", " ", " ", "-", "-", " ", " ", " ", "-", "-", " ", " ", ".", " ", " ", " ", " ", " " },
            { "-", "-", "-", "-", "-", ".", "-", " ", "-", "-", "-", "-", "-", "-", "-", " ", "-", ".", "-", "-", "-", "-", "-" },
            { "-", "-", "-", "-", "-", ".", "-", " ", " ", " ", " ", " ", " ", " ", " ", " ", "-", ".", "-", "-", "-", "-", "-" },
            { "-", "-", "-", "-", "-", ".", "-", " ", "-", "-", "-", "-", "-", "-", "-", " ", "-", ".", "-", "-", "-", "-", "-" },
            { "-", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "-", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "-" },
            { "-", ".", "-", "-", "-", ".", "-", "-", "-", "-", ".", "-", ".", "-", "-", "-", "-", ".", "-", "-", "-", ".", "-" },
            { "-", "e", ".", ".", "-", ".", ".", ".", ".", ".", ".", " ", ".", ".", ".", ".", ".", ".", "-", ".", ".", "e", "-" },
            { "-", "-", "-", ".", "-", ".", "-", ".", "-", "-", "-", "-", "-", "-", "-", ".", "-", ".", "-", ".", "-", "-", "-" },
            { "-", ".", ".", ".", ".", ".", "-", ".", ".", ".", ".", "-", ".", ".", ".", ".", "-", ".", ".", ".", ".", ".", "-" },
            { "-", ".", "-", "-", "-", "-", "-", "-", "-", "-", ".", "-", ".", "-", "-", "-", "-", "-", "-", "-", "-", ".", "-" },
            { "-", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", ".", "-" },
            { "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-" }
        };
        // Konstanta värden för storleken av olika föremål
        private static int wallSize = 20;
        private static int pelletSize = 3;
        private static int energizerSize = 8;
        private static int playerGhostSize = 18;
        // Listor för olika föremål
        private List<StillRect> walls = new List<StillRect>();
        private List<StillRect> pellets = new List<StillRect>();
        private List<StillRect> energizers = new List<StillRect>();
        private List<MovingRect> ghosts = new List<MovingRect>();
        // Startposition för spelare och alla spöken
        private static int playerStartPosX = 3 / 2 * wallSize + wallSize * 10 + (wallSize - playerGhostSize) / 2;
        private static int playerStartPosY = 3 / 2 * wallSize + wallSize * 15 + (wallSize - playerGhostSize) / 2;
        private static int redGhostPosX = 3 / 2 * wallSize + wallSize * 10 + (wallSize - playerGhostSize) / 2;
        private static int redGhostPosY = 3 / 2 * wallSize + wallSize * 7 + (wallSize - playerGhostSize) / 2;
        private static int blueGhostPosX = 3 / 2 * wallSize + wallSize * 9 + (wallSize - playerGhostSize) / 2;
        private static int blueGhostPosY = 3 / 2 * wallSize + wallSize * 9 + (wallSize - playerGhostSize) / 2;
        private static int pinkGhostPosX = 3 / 2 * wallSize + wallSize * 10 + (wallSize - playerGhostSize) / 2;
        private static int pinkGhostPosY = 3 / 2 * wallSize + wallSize * 9 + (wallSize - playerGhostSize) / 2;
        private static int orangeGhostPosX = 3 / 2 * wallSize + wallSize * 11 + (wallSize - playerGhostSize) / 2;
        private static int orangeGhostPosY = 3 / 2 * wallSize + wallSize * 9 + (wallSize - playerGhostSize) / 2;
        // Spelare
        private MovingRect player = new MovingRect()
        {
            PosX = playerStartPosX,
            PosY = playerStartPosY,
            SpeedX = 0,
            SpeedY = 0,
            Color = Brushes.Yellow,
            Speed = 4,
            Width = playerGhostSize,
            Height = playerGhostSize
        };
        // Variabler för poäng och liv
        private int score = 0;
        private int lives = 3;
        public PacmanGame()
        {
            // Här startas programmet
            InitializeComponent();
            // Sedan öppnas ResetGame funktionen
            ResetGame();
        }
        private void ResetGame()
        {
            // Tömmer alla listor så de kan fyllas igen
            walls.Clear();
            pellets.Clear();
            energizers.Clear();
            ghosts.Clear();
            // Text vid sidan skriver ut hur många poäng och liv man har
            Scoreboard.Text = "Score: " + score;
            PlayerLives.Text = "Lives: " + lives;
            // Sätter spelaren i sin originalposition, stillastående
            player.PosX = playerStartPosX;
            player.PosY = playerStartPosY;
            player.SpeedX = 0;
            player.SpeedY = 0;
            // Lägger till spöken
            ghosts.Add(new MovingRect() { PosX = redGhostPosX, PosY = redGhostPosY, SpeedX = 0, SpeedY = 0, Color = Brushes.Red, Speed = 3, Width = playerGhostSize, Height = playerGhostSize });
            ghosts.Add(new MovingRect() { PosX = blueGhostPosX, PosY = blueGhostPosY, SpeedX = 0, SpeedY = 0, Color = Brushes.LightBlue, Speed = 3, Width = playerGhostSize, Height = playerGhostSize });
            ghosts.Add(new MovingRect() { PosX = pinkGhostPosX, PosY = pinkGhostPosY, SpeedX = 0, SpeedY = 0, Color = Brushes.Pink, Speed = 3, Width = playerGhostSize, Height = playerGhostSize });
            ghosts.Add(new MovingRect() { PosX = orangeGhostPosX, PosY = orangeGhostPosY, SpeedX = 0, SpeedY = 0, Color = Brushes.Orange, Speed = 3, Width = playerGhostSize, Height = playerGhostSize });
            // Fyller i kartan
            for (int row = 0; row < 22; row++)
            {
                for (int symbol = 0; symbol < 23; symbol++)
                {
                    switch (grid[row, symbol])
                    {
                        case "-":
                            walls.Add(new StillRect() { PosX = symbol * wallSize, PosY = row * wallSize, Height = wallSize, Width = wallSize });
                            break;
                        case ".":
                            pellets.Add(new StillRect() { PosX = symbol * wallSize + (wallSize - pelletSize) / 2, PosY = row * wallSize + (wallSize - pelletSize) / 2, Width = pelletSize, Height = pelletSize });
                            break;
                        case "e":
                            energizers.Add(new StillRect() { PosX = symbol * wallSize + (wallSize - energizerSize) / 2, PosY = row * wallSize + (wallSize - energizerSize) / 2, Width = energizerSize, Height = energizerSize });
                            break;
                    }
                }
            }
            // Startar timern
            GameTimer.Start();
        }
        private void PacmanGame_Paint(object sender, PaintEventArgs e)
        {
            // Ritar ut väggar, pellets, energizers, spöken och spelare
            foreach (StillRect wall in walls)
            {
                e.Graphics.FillRectangle(Brushes.DarkBlue, wall.PosX, wall.PosY, wall.Width, wall.Height);
            }
            foreach (StillRect pellet in pellets)
            {
                e.Graphics.FillRectangle(Brushes.White, pellet.PosX, pellet.PosY, pellet.Width, pellet.Height);
            }
            foreach (StillRect energizer in energizers)
            {
                e.Graphics.FillEllipse(Brushes.White, energizer.PosX, energizer.PosY, energizer.Width, energizer.Height);
            }
            foreach (MovingRect ghost in ghosts)
            {
                e.Graphics.FillEllipse(ghost.Color, ghost.PosX, ghost.PosY, ghost.Width, ghost.Height);
            }
            e.Graphics.FillEllipse(player.Color, player.PosX, player.PosY, player.Width, player.Height);
        }
        private void GameTimer_Tick(object sender, EventArgs e)
        {
            // Uppdaterar värden
            Scoreboard.Text = "Score: " + score;
            PlayerLives.Text = "Lives: " + lives;
            PlayerLives.ForeColor = Color.White;
            player.PosX += player.SpeedX;
            player.PosY += player.SpeedY;
            // När man har slut på liv förlorar man, spelet avslutas
            if (lives <= 0)
            {
                PlayerLives.Text = "GAME OVER!";
                PlayerLives.ForeColor = Color.Red;
                player.SpeedX = 0;
                player.SpeedY = 0;
                GameTimer.Stop();
            }
            // När man ätit upp alla pellets så startas spelet om
            if (pellets.Count <= 0)
            {
                player.SpeedX = 0;
                player.SpeedY = 0;
                ResetGame();
            }
            // Spelarens håll diktaterar hur den rör sig
            switch (player.CurrentDirection)
            {
                case Direction.Up:
                    player.SpeedX = 0;
                    player.SpeedY = -player.Speed;
                    break;
                case Direction.Left:
                    player.SpeedX = -player.Speed;
                    player.SpeedY = 0;
                    break;
                case Direction.Down:
                    player.SpeedX = 0;
                    player.SpeedY = player.Speed;
                    break;
                case Direction.Right:
                    player.SpeedX = player.Speed;
                    player.SpeedY = 0;
                    break;
                case Direction.None:
                    player.SpeedX = 0;
                    player.SpeedY = 0;
                    break;
            }
            foreach (StillRect wall in walls)
            {
                // Spelare kollision med vägg
                if (MovingStillRectCollision(player, wall))
                {
                    player.SpeedX = 0;
                    player.SpeedY = 0;
                }
                // Teleportation i tunneln vid sidorna
                if (player.PosX <= 0 &&
                    player.PosY <= wall.Height * 11 &&
                    player.PosY >= wall.Height * 10)
                {
                    player.PosX = wall.Width * 23 - player.Width;
                }
                else if (player.PosX + player.Width >= wall.Width * 23 &&
                        player.PosY <= wall.Height * 11 &&
                        player.PosY >= wall.Height * 10)
                {
                    player.PosX = 0;
                }
            }
            foreach (StillRect pellet in pellets)
            {
                // När spelaren tar pellet försvinner pellet och man poängen ökar
                if (MovingStillRectCollision(player, pellet))
                {
                    pellets.Remove(pellet);
                    score += 10;
                    break;
                }
            }
            foreach (StillRect energizer in energizers)
            {
                // När spelaren tar energizer så försvinner energizer, poäng ökar och man får en powerup
                if (MovingStillRectCollision(player, energizer))
                {
                    energizers.Remove(energizer);
                    score += 50;
                    foreach (MovingRect ghost in ghosts)
                    {
                        ghost.Color = Brushes.Blue;
                    }
                    Wait(10000);
                    ghosts[0].Color = Brushes.Red;
                    ghosts[1].Color = Brushes.LightBlue;
                    ghosts[2].Color = Brushes.Pink;
                    ghosts[3].Color = Brushes.Orange;
                    break;
                }
            }
            foreach (MovingRect ghost in ghosts)
            {
                // Spökets kontroller
                ghost.PosX += ghost.SpeedX;
                ghost.PosY += ghost.SpeedY;
                switch (ghost.CurrentDirection)
                {
                    case Direction.Up:
                        ghost.SpeedX = 0;
                        ghost.SpeedY = -ghost.Speed;
                        break;
                    case Direction.Left:
                        ghost.SpeedX = -ghost.Speed;
                        ghost.SpeedY = 0;
                        break;
                    case Direction.Down:
                        ghost.SpeedX = 0;
                        ghost.SpeedY = ghost.Speed;
                        break;
                    case Direction.Right:
                        ghost.SpeedX = ghost.Speed;
                        ghost.SpeedY = 0;
                        break;
                    case Direction.None:
                        ghost.SpeedX = 0;
                        ghost.SpeedY = 0;
                        break;
                }
                Random rnd = new Random();
                foreach (StillRect wall in walls)
                {
                    if (MovingStillRectCollision(ghost, wall))
                    {
                        // När spöket träffar en vägg så stannar spöken och byter håll, funkar inte jättebra
                        ghost.SpeedX = 0;
                        ghost.SpeedY = 0;
                        int randomizeDirection = rnd.Next(4);
                        if (randomizeDirection == 0 && ghost.CurrentDirection != Direction.Up)
                        {
                            ghost.CurrentDirection = Direction.Up;
                        }
                        if (randomizeDirection == 1 && ghost.CurrentDirection != Direction.Left)
                        {
                            ghost.CurrentDirection = Direction.Left;
                        }
                        if (randomizeDirection == 2 && ghost.CurrentDirection != Direction.Down)
                        {
                            ghost.CurrentDirection = Direction.Down;
                        }
                        if (randomizeDirection == 3 && ghost.CurrentDirection != Direction.Right)
                        {
                            ghost.CurrentDirection = Direction.Right;
                        }
                    }
                }
                if (MovingMovingRectCollision(player, ghost))
                {
                    // Om spelaren kolliderar med ett spöke, då spöket är vanlig färg, dör spelaren
                    if (ghost.Color == Brushes.Red ||
                        ghost.Color == Brushes.LightBlue ||
                        ghost.Color == Brushes.Pink ||
                        ghost.Color == Brushes.Orange)
                    {
                        lives -= 1;
                        player.PosX = playerStartPosX;
                        player.PosY = playerStartPosY;
                        player.SpeedX = 0;
                        player.SpeedY = 0;
                        player.CurrentDirection = Direction.None;
                        ghosts[0].PosX = redGhostPosX;
                        ghosts[0].PosY = redGhostPosY;
                        ghosts[1].PosX = blueGhostPosX;
                        ghosts[1].PosY = blueGhostPosY;
                        ghosts[2].PosX = pinkGhostPosX;
                        ghosts[2].PosY = pinkGhostPosY;
                        ghosts[3].PosX = orangeGhostPosX;
                        ghosts[3].PosY = orangeGhostPosY;
                        break;
                    }
                    // Annars om spöket är blått så dödar spelaren spöket
                    else if (ghost.Color == Brushes.Blue)
                    {
                        score += 200;
                        ghost.Color = Brushes.Transparent;
                        Wait(1000);
                        ghost.PosX = pinkGhostPosX;
                        ghost.PosY = pinkGhostPosY;
                        ghost.CurrentDirection = Direction.None;
                        ghost.SpeedX = 0;
                        ghost.SpeedY = 0;
                        if (ghost == ghosts[0])
                        {
                            ghost.Color = Brushes.Red;
                        }
                        if (ghost == ghosts[1])
                        {
                            ghost.Color = Brushes.LightBlue;
                        }
                        if (ghost == ghosts[2])
                        {
                            ghost.Color = Brushes.Pink;
                        }
                        if (ghost == ghosts[3])
                        {
                            ghost.Color = Brushes.Orange;
                        }
                    }
                }
            }
            Invalidate();
        }
        // När rörande rektangel kolliderar med stillastående rektangel
        private bool MovingStillRectCollision(MovingRect movingRect, StillRect stillRect)
        {
            return (movingRect.PosY + movingRect.SpeedY <= stillRect.PosY + stillRect.Height &&
            movingRect.PosX + movingRect.Width + movingRect.SpeedX >= stillRect.PosX &&
            movingRect.PosY + movingRect.Width + movingRect.SpeedY >= stillRect.PosY &&
            movingRect.PosX + movingRect.SpeedX <= stillRect.PosX + stillRect.Width);
        }
        // När två rörande rektanglar kolliderar
        private bool MovingMovingRectCollision(MovingRect movingRectA, MovingRect movingRectB)
        {
            return (movingRectA.PosY + movingRectA.SpeedY <= movingRectB.PosY + movingRectB.Height &&
            movingRectA.PosX + movingRectA.Width + movingRectA.SpeedX >= movingRectB.PosX &&
            movingRectA.PosY + movingRectA.Width + movingRectA.SpeedY >= movingRectB.PosY &&
            movingRectA.PosX + movingRectA.SpeedX <= movingRectB.PosX + movingRectB.Width);
        }
        // Kontroller
        private void PacmanGame_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    player.CurrentDirection = Direction.Up;
                    break;
                case Keys.A:
                    player.CurrentDirection = Direction.Left;
                    break;
                case Keys.S:
                    player.CurrentDirection = Direction.Down;
                    break;
                case Keys.D:
                    player.CurrentDirection = Direction.Right;
                    break;
            }
        }
        // Funktion som körs för att "vänta" x antal millisekunder
        public void Wait(int ms)
        {
            var tempTimer = new Timer();
            if (ms <= 0) return;

            tempTimer.Interval = ms;
            tempTimer.Enabled = true;
            tempTimer.Start();

            tempTimer.Tick += (s, e) =>
            {
                tempTimer.Enabled = false;
                tempTimer.Stop();
            };

            while (tempTimer.Enabled)
            {
                Application.DoEvents();
            }
        }
        private void Form1_Load(object sender, EventArgs e){}
    }
}