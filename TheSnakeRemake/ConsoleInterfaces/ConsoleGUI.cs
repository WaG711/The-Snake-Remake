namespace TheSnakeRemake
{
    public class ConsoleGUI : IConsoleGUI
    {
        private readonly int _maxScore = 500;
        private readonly int _mapWidth;
        private readonly int _mapHeight;
        private readonly ConsoleColor _color;
        private readonly ConsoleColor _foodColor;
        private readonly Random _random;
        private readonly IConsoleUI _consoleUI;

        public ConsoleGUI()
        {
            _mapWidth = 30;
            _mapHeight = 20;
            _color = ConsoleColor.White;
            _foodColor = ConsoleColor.DarkGreen;
            _random = new Random();
            _consoleUI = new ConsoleUI();
        }

        public int MapWidth { get => _mapWidth; }
        public int MapHeight { get => _mapHeight; }
        public int Score { get; private set; } = 0;
        public bool IsMaxScore { get; private set; } = false;

        public void AddScore()
        {
            Score++;
        }
        public void Reset()
        {
            Score = 0;
            IsMaxScore = false;
        }

        public bool CheckScore()
        {
            if (Score >= _maxScore)
            {
                IsMaxScore = false;
                return true;
            }
            return false;
        }

        public Pixel SpawnFood(ISnake snake)
        {
            Pixel food = new Pixel(0, 0, _foodColor);
            bool foodSpawn = false;

            while (!foodSpawn)
            {
                int foodX = _random.Next(1, _mapWidth - 1);
                int foodY = _random.Next(1, _mapHeight - 1);

                if (!snake.Body.Any(b => b.X == foodX && b.Y == foodY))
                {
                    if ((foodX != snake.Head.X || foodY != snake.Head.Y)
                        && !(foodX <= 0 || foodX >= _mapWidth - 1 || foodY <= 0 || foodY >= _mapHeight - 1))
                    {
                        food = new Pixel(foodX, foodY, _foodColor);
                        foodSpawn = true;
                    }
                }
            }

            return food;
        }

        public void DisplayScore()
        {
            Console.ForegroundColor = _consoleUI.Mode ? ConsoleColor.Black : ConsoleColor.White;
            Console.BackgroundColor = _consoleUI.Mode ? _color : ConsoleColor.Black;
            Console.SetCursorPosition(33, 0);
            Console.WriteLine($"Score - {Score}");
            Console.ResetColor();
        }

        public void DisplayEnd()
        {
            Console.Clear();

            if (IsMaxScore)
            {
                Console.SetCursorPosition(33, 3);
                Console.WriteLine("YOU WON");
            }

            Console.SetCursorPosition(33, 5);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Score - {Score}");

            Task.Run(() => Console.Beep(200, 600));
        }

        public void DrawBorder()
        {
            for (int i = 0; i < _mapWidth; i++)
            {
                DrawPixel(i, 0);
                DrawPixel(i, _mapHeight - 1);
            }

            for (int i = 0; i < _mapHeight; i++)
            {
                DrawPixel(0, i);
                DrawPixel(_mapWidth - 1, i);
            }
        }

        private void DrawPixel(int x, int y)
        {
            if (_consoleUI.Mode)
            {
                new Pixel(x, y, _color).Draw();
            }
            else
            {
                new Pixel(x, y, _color).Draw(MapWidth, MapHeight);
            }
        }
    }
}
