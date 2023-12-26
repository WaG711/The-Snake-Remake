namespace TheSnakeRemake
{
    public enum Direction
    {
        Right,
        Left,
        Up,
        Down
    }

    public class SnakeMove : ISnakeMove
    {
        private readonly Snake _snake;
        private readonly IGameSettings _gameSettings;

        public SnakeMove(Snake snake)
        {
            _snake = snake;
            _gameSettings = new GameSettings();
        }

        public void MoveSnake(Direction direction, bool isEat)
        {
            _snake.Clear();

            _snake.Body.Enqueue(new Pixel(_snake.Head.X, _snake.Head.Y, _snake.BodyColor));

            if (!isEat)
            {
                _snake.Body.Dequeue();
            }

            if (_gameSettings.Mode)
            {
                switch (direction)
                {
                    case Direction.Right:
                        _snake.Head = new Pixel(_snake.Head.X + 1, _snake.Head.Y, _snake.HeadColor);
                        break;
                    case Direction.Left:
                        _snake.Head = new Pixel(_snake.Head.X - 1, _snake.Head.Y, _snake.HeadColor);
                        break;
                    case Direction.Up:
                        _snake.Head = new Pixel(_snake.Head.X, _snake.Head.Y - 1, _snake.HeadColor);
                        break;
                    case Direction.Down:
                        _snake.Head = new Pixel(_snake.Head.X, _snake.Head.Y + 1, _snake.HeadColor);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (direction)
                {
                    case Direction.Right:
                        _snake.Head = new Pixel((_snake.Head.X + 1) % 30, _snake.Head.Y, _snake.HeadColor);
                        break;
                    case Direction.Left:
                        _snake.Head = new Pixel((_snake.Head.X - 1 + 30) % 30, _snake.Head.Y, _snake.HeadColor);
                        break;
                    case Direction.Up:
                        _snake.Head = new Pixel(_snake.Head.X, (_snake.Head.Y - 1 + 20) % 20, _snake.HeadColor);
                        break;
                    case Direction.Down:
                        _snake.Head = new Pixel(_snake.Head.X, (_snake.Head.Y + 1) % 20, _snake.HeadColor);
                        break;
                    default: break;
                }
            }

            _snake.Draw();
        }

        public Direction ReadMove(Direction currentDirection)
        {
            if (!Console.KeyAvailable)
            {
                return currentDirection;
            }

            ConsoleKey key = Console.ReadKey(true).Key;
            return key switch
            {
                ConsoleKey.UpArrow when currentDirection != Direction.Down => Direction.Up,
                ConsoleKey.DownArrow when currentDirection != Direction.Up => Direction.Down,
                ConsoleKey.LeftArrow when currentDirection != Direction.Right => Direction.Left,
                ConsoleKey.RightArrow when currentDirection != Direction.Left => Direction.Right,
                _ => currentDirection,
            };
        }
    }
}
