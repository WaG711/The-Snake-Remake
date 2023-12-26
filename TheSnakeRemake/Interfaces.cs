namespace TheSnakeRemake
{
    public interface IPixel
    {
        public int X { get; }
        public int Y { get; }
        void Draw();
        void Clear();
    }

    public interface ISnake
    {
        public Pixel Head { get; set; }
        public Queue<Pixel> Body { get; }
        void PerformMovement(Direction direction, bool isEat = false);
        void Draw();
        void Clear();
        void Reset(int initialX, int initialY, ConsoleColor headColor, ConsoleColor bodyColor);
    }

    public interface ISnakeMove
    {
        void MoveSnake(Direction direction, bool isEat);
        Direction ReadMove(Direction currentDirection);
    }

    public interface IFoodSpawn
    {
        Pixel SpawnFood(ISnake snake);
    }

    public interface IGameSettings
    {
        public int MapWidth { get; }
        public int MapHeight { get; }
        public bool Menu { get; }
        public bool Mode { get; }
        public int Speed { get; }
        bool SetMenu(string? userInput);
        bool SetMode(string? userInput);
        int SetSpeed(string? userInput);
        void ReductionSpeed();
    }

    public interface IConsoleGUI
    {
        public int Score { get; }
        public bool IsMaxScore { get; }
        void DisplayScore();
        void DisplayEnd();
        void DrawBorder();
        bool CheckScore();
        void AddScore();
        void Reset();
    }

    public interface IConsoleUI
    {
        void ChooseMenu();
        void ChooseMode();
        void ChooseSpeed();
    }

    public interface ISnakeGame
    {
        void RunGame();
    }
}
