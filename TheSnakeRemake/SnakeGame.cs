using System.Diagnostics;

namespace TheSnakeRemake
{
    public class SnakeGame : ISnakeGame
    {
        private readonly ConsoleColor _headColor = ConsoleColor.Blue;
        private readonly ConsoleColor _bodyColor = ConsoleColor.DarkBlue;
        private readonly int _startX = 4;
        private readonly int _startY = 5;
        private readonly Stopwatch _stopwatch;
        private readonly ISnakeMove _snakeMove;
        private readonly IFoodSpawn _foodSpawn;
        private readonly Snake _snake;
        private readonly IConsoleGUI _consoleGUI;
        private readonly IConsoleUI _consoleUI;
        private IPixel _food;
        private Direction _currentMove;
        private int _lagMs = 0;
        private readonly IGameSettings _gameSettings;

        public SnakeGame()
        {
            _stopwatch = new Stopwatch();
            _gameSettings = new GameSettings();
            _consoleUI = new ConsoleUI(_gameSettings);
            _consoleGUI = new ConsoleGUI(_gameSettings);
            _foodSpawn = new FoodSpawn(_gameSettings);

            _currentMove = Direction.Right;
            _snake = new Snake(_startX, _startY, _headColor, _bodyColor, _gameSettings);
            _snakeMove = new SnakeMove(_snake, _gameSettings);
            _food = _foodSpawn.SpawnFood(_snake);
        }

        public void RunGame()
        {
            Console.CursorVisible = false;

            while (true)
            {
                if (_gameSettings.Menu)
                {
                    _consoleUI.ChooseMode();
                    _consoleUI.ChooseSpeed();
                }

                ProcessFrame();
                Thread.Sleep(100);

                ResetGame();
            }
        }

        private void ProcessFrame()
        {
            Console.Clear();
            _consoleGUI.DrawBorder();
            _food.Draw();

            while (!(CheckCollision() || _consoleGUI.CheckScore()))
            {
                _stopwatch.Restart();
                Direction oldMove = _currentMove;

                while (_stopwatch.ElapsedMilliseconds <= _gameSettings.Speed - _lagMs)
                {
                    UpdateMove(oldMove);
                }

                _stopwatch.Restart();

                if (_snake.Head.X == _food.X && _snake.Head.Y == _food.Y)
                {
                    HandleFoodEaten();
                }
                else
                {
                    _snake.PerformMovement(_currentMove);
                }

                _lagMs = (int)_stopwatch.ElapsedMilliseconds;
                _consoleGUI.DisplayScore();
            }

            _consoleGUI.DisplayEnd();
            _consoleUI.ChooseMenu();
        }

        private void UpdateMove(Direction oldMove)
        {
            if (_currentMove == oldMove)
            {
                _currentMove = _snakeMove.ReadMove(_currentMove);
            }
        }

        private void HandleFoodEaten()
        {
            _snake.PerformMovement(_currentMove, true);
            _food = _foodSpawn.SpawnFood(_snake);
            _food.Draw();

            if (_consoleGUI.Score % 5 == 0)
            {
                _gameSettings.ReductionSpeed();
            }

            _consoleGUI.AddScore();
            Task.Run(() => Console.Beep(1200, 200));
        }

        private bool CheckCollision()
        {
            if (_gameSettings.Mode)
            {
                return _snake.Head.X == _gameSettings.MapWidth
                    || _snake.Head.X == 0
                    || _snake.Head.Y == _gameSettings.MapHeight
                    || _snake.Head.Y == 0
                    || _snake.Body.Any(b => b.X == _snake.Head.X && b.Y == _snake.Head.Y);
            }
            else
            {
                return _snake.Body.Any(b => b.X == _snake.Head.X && b.Y == _snake.Head.Y);
            }
        }

        private void ResetGame()
        {
            _currentMove = Direction.Right;
            _snake.Reset(_startX, _startY, _headColor, _bodyColor);
            _food = _foodSpawn.SpawnFood(_snake);
            _lagMs = 0;
            _consoleGUI.Reset();
        }
    }
}
