namespace TheSnakeRemake.Model
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

        public void Draw()
        {
            Console.ForegroundColor = _color;
            Console.SetCursorPosition(X, Y);
            Console.Write(_pixelChar);
            Console.ResetColor();
        }
    }
}
