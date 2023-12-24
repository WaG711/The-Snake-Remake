namespace TheSnakeRemake
{
    internal class Program
    {
        static void Main()
        {
            ISnakeGame game = new SnakeGame();
            game.RunGame();
        }
    }
}
