namespace TheSnakeRemake
{
    public readonly struct Pixel : IPixel
    {
        private readonly char _pixelChar = '█';
        private readonly ConsoleColor _color;

        public Pixel(int x, int y, ConsoleColor color)
        {
            X = x;
            Y = y;
            _color = color;
        }
        public int X { get; }
        public int Y { get; }

        public void Clear()
        {
            Console.SetCursorPosition(X, Y);
            Console.Write(' ');
        }

        public void Clear(int screenWidth, int screenHeight)
        {
            int wrappedX = GetWrappedCoordinate(X, screenWidth);
            int wrappedY = GetWrappedCoordinate(Y, screenHeight);

            Console.SetCursorPosition(wrappedX, wrappedY);
            Console.Write(' ');
        }

        public void Draw()
        {
            Console.ForegroundColor = _color;
            Console.SetCursorPosition(X, Y);
            Console.Write(_pixelChar);
        }

        public void Draw(int screenWidth, int screenHeight)
        {
            Console.ForegroundColor = _color;

            int wrappedX = GetWrappedCoordinate(X, screenWidth);
            int wrappedY = GetWrappedCoordinate(Y, screenHeight);

            Console.SetCursorPosition(wrappedX, wrappedY);
            Console.Write(_pixelChar);
        }

        private int GetWrappedCoordinate(int coordinate, int consoleSize)
        {
            return (coordinate % consoleSize + consoleSize) % consoleSize;
        }
    }
}
