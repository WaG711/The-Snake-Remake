using TheSnakeRemake.Model;

namespace TheSnakeRemake.UIModel
{
    public class FoodSpawn : IFoodSpawn
    {
        private readonly Random _random;
        private readonly ConsoleColor _color;
        private readonly IGameSettings _gameSettings;

        public FoodSpawn(IGameSettings gameSettings)
        {
            _random = new Random();
            _color = ConsoleColor.Cyan;
            _gameSettings = gameSettings;
        }

        public Pixel SpawnFood(ISnake snake)
        {
            Pixel food = new Pixel(0, 0, _color);
            bool foodSpawn = false;

            while (!foodSpawn)
            {
                int foodX = _random.Next(1, _gameSettings.MapWidth);
                int foodY = _random.Next(1, _gameSettings.MapHeight);

                if (!snake.Body.Any(b => b.X == foodX && b.Y == foodY))
                {
                    if ((foodX != snake.Head.X || foodY != snake.Head.Y)
                        && !(foodX <= 0 || foodX >= _gameSettings.MapWidth
                        || foodY <= 0 || foodY >= _gameSettings.MapHeight))
                    {
                        food = new Pixel(foodX, foodY, _color);
                        foodSpawn = true;
                    }
                }
            }

            return food;
        }
    }
}
