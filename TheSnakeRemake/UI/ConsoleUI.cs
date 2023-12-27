namespace TheSnakeRemake.UI
{
    public class ConsoleUI : IConsoleUI
    {
        private readonly ConsoleColor _color;
        private readonly IGameSettings _gameSettings;

        public ConsoleUI(IGameSettings gameSettings)
        {
            _gameSettings = gameSettings;
            _color = ConsoleColor.White;
        }

        public void ChooseMenu()
        {
            Console.ForegroundColor = _color;
            Console.SetCursorPosition(10, 7);
            Console.WriteLine("Введите 1 для выхода в меню или нажмите ENTER для перезапуска");
            Console.SetCursorPosition(38, 9);
            string? userInput = Console.ReadLine();
            _gameSettings.SetMenu(userInput);
        }

        public void ChooseMode()
        {
            Console.Clear();
            Console.ForegroundColor = _color;
            Console.SetCursorPosition(10, 5);
            Console.WriteLine("Введите 1 для игры со смертельными стенами");
            Console.SetCursorPosition(10, 7);
            Console.WriteLine("Введите 0 для игры без смертельных стен");
            Console.SetCursorPosition(31, 9);
            string? userInput = Console.ReadLine();
            _gameSettings.SetMode(userInput);
        }

        public void ChooseSpeed()
        {
            Console.Clear();
            Console.ForegroundColor = _color;
            Console.SetCursorPosition(10, 5);
            Console.WriteLine("Введите 0 для медленного режима");
            Console.SetCursorPosition(10, 7);
            Console.WriteLine("Введите 1 для среднего режима");
            Console.SetCursorPosition(10, 9);
            Console.WriteLine("Введите 2 для быстрого режима");
            Console.SetCursorPosition(22, 11);
            string? userInput = Console.ReadLine();
            _gameSettings.SetSpeed(userInput);
        }
    }
}
