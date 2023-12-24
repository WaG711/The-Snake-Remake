namespace TheSnakeRemake
{
    public struct ConsoleUI : IConsoleUI
    {
        private int _selectedSpeed;
        private bool _selectedMode;

        public bool Mode { get; set; }
        public int Speed { get; set; }

        public void Reset()
        {
            Mode = _selectedMode;
            Speed = _selectedSpeed;
        }
        public bool SetMenu(ref bool isMenu)
        {
            Console.SetCursorPosition(10, 7);
            Console.WriteLine("Введите 1 для выхода в меню или нажмите ENTER для перезапуска");
            Console.SetCursorPosition(38, 9);
            string? userInput = Console.ReadLine();
            isMenu = userInput switch
            {
                "1" => true,
                _ => false,
            };
            return isMenu;
        }

        public bool SetMode()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(10, 5);
            Console.WriteLine("Введите 1 для игры со смертельными стенами");
            Console.SetCursorPosition(10, 7);
            Console.WriteLine("Введите 0 для игры без смертельных стен");
            Console.SetCursorPosition(31, 9);
            string? userInput = Console.ReadLine();
            Mode = userInput switch
            {
                "0" => false,
                "1" => true,
                _ => true,
            };
            _selectedMode = Mode;
            return Mode;
        }

        public int SetSpeed()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(10, 5);
            Console.WriteLine("Введите 0 для медленного режима");
            Console.SetCursorPosition(10, 7);
            Console.WriteLine("Введите 1 для среднего режима");
            Console.SetCursorPosition(10, 9);
            Console.WriteLine("Введите 2 для быстрого режима");
            Console.SetCursorPosition(22, 11);
            string? userInput = Console.ReadLine();
            Speed = userInput switch
            {
                "0" => 190,
                "1" => 140,
                "2" => 92,
                _ => 140,
            };
            _selectedSpeed = Speed;
            return Speed;
        }
    }
}
