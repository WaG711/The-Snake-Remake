using TheSnakeRemake.Model;

namespace TheSnakeRemake
{
    public class SnakeFactory
    {
        private readonly IGameSettings _gameSettings;

        public SnakeFactory(IGameSettings gameSettings)
        {
            _gameSettings = gameSettings;
        }

        public ISnake CreateSnake()
        {
            ConsoleColor headColor = ConsoleColor.Blue;
            ConsoleColor bodyColor = ConsoleColor.DarkBlue;
            int startX = 4;
            int startY = 5;

            ISnake snake = new Snake(startX, startY, headColor, bodyColor, _gameSettings);
            return snake;
        }
    }
}
