namespace TheSnakeRemake
{
    public class ConsoleGUI : IConsoleGUI
    {
        private readonly IGameSettings _gameSettings;
        private readonly int _maxScore = 500;

        public ConsoleGUI(IGameSettings gameSettings)
        {
            _gameSettings = gameSettings;
        }

        public int Score { get; private set; }
        public bool IsMaxScore { get; private set; }

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
                IsMaxScore = true;
                return true;
            }
            return false;
        }

        public void DisplayScore()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(33, 1);
            Console.WriteLine($"Score - {Score}");
            Console.ResetColor();
        }

        public void DisplayEnd()
        {
            Console.Clear();

            if (IsMaxScore)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(35, 3);
                Console.WriteLine("YOU WON");
                Console.ResetColor();
            }

            Console.SetCursorPosition(33, 5);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Score - {Score}");

            Task.Run(() => Console.Beep(200, 600));
        }

        public void DrawBorder()
        {
            for (int i = 0; i <= _gameSettings.MapWidth; i++)
            {
                DrawPixel(i, 0);
                DrawPixel(i, _gameSettings.MapHeight);
            }

            for (int i = 0; i <= _gameSettings.MapHeight; i++)
            {
                DrawPixel(0, i);
                DrawPixel(_gameSettings.MapWidth, i);
            }
        }

        private void DrawPixel(int x, int y)
        {
            if (_gameSettings.Mode)
            {
                new Pixel(x, y, ConsoleColor.White).Draw();
                return;
            }

            if (x == _gameSettings.MapWidth || y == _gameSettings.MapHeight)
            {
                new Pixel(x, y, ConsoleColor.DarkGreen).Draw();
            }
        }
    }
}
