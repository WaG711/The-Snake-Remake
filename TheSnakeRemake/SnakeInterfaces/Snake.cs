namespace TheSnakeRemake
{
    public class Snake : ISnake
    {
        private readonly ISnakeMove _snakeMove;

        public Snake(int initialX, int initialY, ConsoleColor headColor, ConsoleColor bodyColor, IGameSettings gameSettings)
        {
            HeadColor = headColor;
            BodyColor = bodyColor;

            Head = new Pixel(initialX, initialY, HeadColor);

            for (int i = 1; i < 2; i++)
            {
                int bodySegmentX = Head.X - i;
                Body.Enqueue(new Pixel(bodySegmentX, initialY, BodyColor));
            }

            _snakeMove = new SnakeMove(this, gameSettings);
        }

        public Pixel Head { get; set; }
        public Queue<Pixel> Body { get; } = new Queue<Pixel>();
        public ConsoleColor HeadColor { get; private set; }
        public ConsoleColor BodyColor { get; private set; }

        public void Reset(int initialX, int initialY, ConsoleColor headColor, ConsoleColor bodyColor)
        {
            HeadColor = headColor;
            BodyColor = bodyColor;

            Head = new Pixel(initialX, initialY, HeadColor);
            Body.Clear();

            for (int i = 1; i < 2; i++)
            {
                int bodySegmentX = Head.X - i;
                Body.Enqueue(new Pixel(bodySegmentX, initialY, BodyColor));
            }
        }

        public void Clear()
        {
            Head.Clear();

            foreach (Pixel pixel in Body)
            {
                pixel.Clear();
            }
        }

        public void Draw()
        {
            Head.Draw();

            foreach (Pixel pixel in Body)
            {
                pixel.Draw();
            }
        }

        public void PerformMovement(Direction direction, bool isEat = false)
        {
            _snakeMove.MoveSnake(direction, isEat);
        }
    }
}
