﻿namespace TheSnakeRemake
{
    public interface IPixel
    {
        public int X { get; }
        public int Y { get; }
        void Draw();
        void Draw(int screenWidth, int screenHeight);
        void Clear();
        void Clear(int screenWidth, int screenHeight);
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

    public interface IConsoleGUI
    {
        public int MapWidth { get; }
        public int MapHeight { get; }
        public int ScreenWidth { get; }
        public int ScreenHeight { get; }
        public int Score { get; }
        public bool IsMaxScore { get; }
        void DisplayScore();
        void DisplayEnd();
        void DrawBorder();
        Pixel SpawnFood(ISnake snake);
        bool CheckScore();
        void AddScore();
        void Reset();
    }

    public interface IConsoleUI
    {
        public bool Menu {  get; }
        public bool Mode { get; }
        public int Speed { get; }
        bool SetMenu();
        bool SetMode();
        int SetSpeed();
        void ReductionSpeed();
        void Reset();
    }

    public interface ISnakeGame
    {
        void RunGame();
    }
}
