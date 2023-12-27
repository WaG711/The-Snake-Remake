using TheSnakeRemake.Model;

namespace TheSnakeRemake.UIModel
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
        private readonly int _mapWidth;
        private readonly int _mapHeight;
        private readonly ISnake _snake;
        private readonly IGameSettings _gameSettings;

        public SnakeMove(ISnake snake, IGameSettings gameSettings)
        {
            _snake = snake;
            _gameSettings = gameSettings;
            _mapWidth = _gameSettings.MapWidth;
            _mapHeight = _gameSettings.MapHeight;
        }

        public void MoveSnake(Direction direction, bool isEat)
        {
            _snake.Clear();

            _snake.Body.Enqueue(new Pixel(_snake.Head.X, _snake.Head.Y, _snake.BodyColor));

            if (!isEat)
            {
                _snake.Body.Dequeue();
            }

            switch (direction)
            {
                case Direction.Right:
                    _snake.Head = new Pixel((_snake.Head.X + 1) % _mapWidth, _snake.Head.Y, _snake.HeadColor);
                    break;
                case Direction.Left:
                    _snake.Head = new Pixel((_snake.Head.X - 1 + _mapWidth) % _mapWidth, _snake.Head.Y, _snake.HeadColor);
                    break;
                case Direction.Up:
                    _snake.Head = new Pixel(_snake.Head.X, (_snake.Head.Y - 1 + _mapHeight) % _mapHeight, _snake.HeadColor);
                    break;
                case Direction.Down:
                    _snake.Head = new Pixel(_snake.Head.X, (_snake.Head.Y + 1) % _mapHeight, _snake.HeadColor);
                    break;
                default:
                    break;
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
