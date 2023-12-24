using System.Diagnostics;

namespace TheSnakeRemake
{
    public class SnakeGame : ISnakeGame
    {
        private readonly ConsoleColor _headColor = ConsoleColor.Blue;
        private readonly ConsoleColor _bodyColor = ConsoleColor.DarkBlue;
        private readonly int _startX = 10;
        private readonly int _startY = 5;
        private readonly Stopwatch _stopwatch;
        private readonly IConsoleUI _consoleUI;
        private readonly ISnakeMove _snakeMove;
        private readonly Snake _snake;
        private readonly int _maxScore = 500;
        private readonly IConsoleGUI _consoleGUI;
        private IPixel _food;
        private Direction _currentMove;
        private int _lagMs = 0;
        private bool _isMenu = true;

        public SnakeGame()
        {
            _stopwatch = new Stopwatch();
            _consoleUI = new ConsoleUI();
            _consoleGUI = new ConsoleGUI();

            _currentMove = Direction.Right;
            _snake = new Snake(_startX, _startY, _headColor, _bodyColor);
            _snakeMove = new SnakeMove(_snake);
            _food = _consoleGUI.SpawnFood(_snake);
        }
        public void RunGame()
        {
            Console.CursorVisible = false;

            while (true)
            {
                if (_isMenu)
                {
                    _consoleUI.SetMode();
                    _consoleUI.SetSpeed();
                }
                ProcessFrame();
                Thread.Sleep(100);

                ResetGame();
            }
        }

        private void ProcessFrame()
        {
            Console.Clear();
            ProcessWall();
            while (true)
            {
                _stopwatch.Restart();
                Direction oldMove = _currentMove;
                while (_stopwatch.ElapsedMilliseconds <= _consoleUI.Speed - _lagMs)
                {
                    UpdateMove(oldMove);
                }
                _stopwatch.Restart();

                if (_snake.Head.X == _food.X && _snake.Head.Y == _food.Y)
                {
                    HandleFoodEaten();

                    if (_consoleGUI.Score >= _maxScore)
                    {
                        _consoleGUI.IsMaxScore = true;
                        break;
                    }
                }
                else
                {
                    _snake.PerformMovement(_currentMove);
                }

                if (CheckCollision())
                {
                    break;
                }
                _lagMs = (int)_stopwatch.ElapsedMilliseconds;
                _consoleGUI.DisplayScore();
            }
            _consoleGUI.DisplayEnd();
            _consoleUI.SetMenu(ref _isMenu);
        }

        private void UpdateMove(Direction oldMove)
        {
            if (_currentMove == oldMove)
            {
                _currentMove = _snakeMove.ReadMove(_currentMove);
            }
        }

        public void ProcessWall()
        {
            if (_consoleUI.Mode)
            {
                _consoleGUI.DrawBorder();
                _food.Draw();
            }
            else
            {
                _food.Draw(_consoleGUI.ScreenWidth, _consoleGUI.ScreenHeight);
            }
        }

        private void HandleFoodEaten()
        {
            _snake.PerformMovement(_currentMove, true);
            _food = _consoleGUI.SpawnFood(_snake);

            if (_consoleUI.Mode)
            {
                _food.Draw();
            }
            else
            {
                _food.Draw(_consoleGUI.ScreenWidth, _consoleGUI.ScreenHeight);
            }

            if (_consoleGUI.Score % 5 == 0)
            {
                _consoleUI.Speed -= 1;
            }

            _consoleGUI.Score++;
            Task.Run(() => Console.Beep(1200, 200));
        }

        private bool CheckCollision()
        {
            if (_consoleUI.Mode)
            {
                return _snake.Head.X == _consoleGUI.MapWidth - 1
                    || _snake.Head.X == 0
                    || _snake.Head.Y == _consoleGUI.MapHeight - 1
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
            _food = _consoleGUI.SpawnFood(_snake);
            _lagMs = 0;
            _consoleGUI.Reset();
            _consoleUI.Reset();
        }
    }
}
