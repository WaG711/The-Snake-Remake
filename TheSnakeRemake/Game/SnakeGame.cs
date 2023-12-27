using System.Diagnostics;
using TheSnakeRemake.UI;
using TheSnakeRemake.UIModel;

namespace TheSnakeRemake.GameSettings
{
    public class SnakeGame : ISnakeGame
    {
        private readonly Stopwatch _stopwatch;
        private readonly SnakeFactory _snakeFactory;
        private readonly IGameSettings _gameSettings;
        private readonly ISnakeMove _snakeMove;
        private readonly IFoodSpawn _foodSpawn;
        private readonly IConsoleGUI _consoleGUI;
        private readonly IConsoleUI _consoleUI;
        private Direction _currentMove;
        private ISnake _snake;
        private IPixel _food;
        private int _lagMs;

        public SnakeGame()
        {
            _stopwatch = new Stopwatch();
            _gameSettings = new GameSettings();

            _consoleUI = new ConsoleUI(_gameSettings);
            _consoleGUI = new ConsoleGUI(_gameSettings);
            _foodSpawn = new FoodSpawn(_gameSettings);
            _snakeFactory = new SnakeFactory(_gameSettings);

            _currentMove = Direction.Right;
            _snake = _snakeFactory.CreateSnake();
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
                DoEatFood();
                _lagMs = (int)_stopwatch.ElapsedMilliseconds;
            }

            _consoleGUI.DisplayEnd();
            _consoleUI.ChooseMenu();
        }

        private void DoEatFood()
        {
            if (_snake.Head.X == _food.X && _snake.Head.Y == _food.Y)
            {
                HandleFoodEaten();
                _consoleGUI.DisplayScore();
            }
            else
            {
                _snake.PerformMovement(_currentMove);
            }
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
                return _snake.Head.X == 0
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
            _snake = _snakeFactory.CreateSnake();
            _food = _foodSpawn.SpawnFood(_snake);
            _consoleGUI.Reset();
            _lagMs = 0;
        }
    }
}
