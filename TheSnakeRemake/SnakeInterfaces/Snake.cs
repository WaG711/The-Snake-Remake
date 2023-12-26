namespace TheSnakeRemake
{
    public class Snake : ISnake
    {
        private readonly ISnakeMove _snakeMove;
        private readonly IConsoleUI _consoleUI;
        private readonly IConsoleGUI _consoleGUI;

        public Snake(int initialX, int initialY, ConsoleColor headColor, ConsoleColor bodyColor)
        {
            HeadColor = headColor;
            BodyColor = bodyColor;

            Head = new Pixel(initialX, initialY, HeadColor);

            for (int i = 1; i < 2; i++)
            {
                int bodySegmentX = Head.X - i;
                Body.Enqueue(new Pixel(bodySegmentX, initialY, BodyColor));
            }

            _snakeMove = new SnakeMove(this);
            _consoleGUI = new ConsoleGUI();
            _consoleUI = new ConsoleUI();
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
            int screenWidth = _consoleUI.Mode ? 0 : _consoleGUI.MapWidth;
            int screenHeight = _consoleUI.Mode ? 0 : _consoleGUI.MapHeight;

            Head.Clear(screenWidth, screenHeight);
            foreach (Pixel pixel in Body)
            {
                pixel.Clear(screenWidth, screenHeight);
            }
        }

        public void Draw()
        {
            int screenWidth = _consoleUI.Mode ? 0 : _consoleGUI.MapWidth;
            int screenHeight = _consoleUI.Mode ? 0 : _consoleGUI.MapHeight;

            Head.Draw(screenWidth, screenHeight);
            foreach (Pixel pixel in Body)
            {
                pixel.Draw(screenWidth, screenHeight);
            }
        }

        public void PerformMovement(Direction direction, bool isEat = false)
        {
            _snakeMove.MoveSnake(direction, isEat);
        }
    }
}
