using System.Diagnostics;

namespace TheSnakeRemake
{
    /// <summary>
    /// Представляет игру, ее функционал и элементы
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Представляет ширину игровой области 
        /// </summary>
        private const int MapWidth = 30;
        /// <summary>
        /// Представляет высоту игровой области
        /// </summary>
        private const int MapHeight = 20;
        /// <summary>
        /// Представляет ширину консоли
        /// </summary>
        private const int ScreenWidth = MapWidth * 3;
        /// <summary>
        /// Представляет высоту консоли
        /// </summary>
        private const int ScreenHeight = MapHeight * 3;
        /// <summary>
        /// цвет для границ игровой области
        /// </summary>
        private static readonly ConsoleColor BorderColor = ConsoleColor.White;
        /// <summary>
        /// Генератор случайных чисел для генерации координат еды
        /// </summary>
        private static readonly Random Random = new Random();
        /// <summary>
        /// Определяет цвет еды
        /// </summary>
        private const ConsoleColor FoodColor = ConsoleColor.DarkGreen;
        /// <summary>
        /// Определяет цвет головы змеи
        /// </summary>
        private const ConsoleColor HeadColor = ConsoleColor.Blue;
        /// <summary>
        /// Определяет цвет тела змеи
        /// </summary>
        private const ConsoleColor BodyColor = ConsoleColor.DarkBlue;
        /// <summary>
        /// Продолжительность каждого кадра в милисекундах для определения скорости игры
        /// </summary>
        private static int FrameMs;
        /// <summary>
        /// Выбирает режим игры
        /// </summary>
        private static bool isWall;
        /// <summary>
        /// Получение доступа к меню
        /// </summary>
        private static bool isMenu = true;

        /// <summary>
        /// Свойство
        /// </summary>
        public static bool IsWall { get; }
        /// <summary>
        /// Гетор на получение значения
        /// </summary>
        /// <returns>
        /// Возвращает ширину консоли
        /// </returns>
        public static int GetScreenWidth()
        {
            return ScreenWidth;
        }
        /// <summary>
        /// Гетор на получение значения
        /// </summary>
        /// <returns>
        /// Возвращает высоту консоли
        /// </returns>
        public static int GetScreeenHeight()
        {
            return ScreenHeight;
        }

        /// <summary>
        /// Иницилизирует окно консоли, настраивает игровую среду
        /// </summary>
        static void Main()
        {
            Console.SetWindowSize(ScreenWidth, ScreenHeight);
            Console.SetBufferSize(ScreenWidth, ScreenHeight);
            Console.CursorVisible = false;

            while (true)
            {
                if (isMenu)
                {
                    Mode();
                    Speed();
                }
                StartGame();
                Thread.Sleep(100);
            }
        }

        /// <summary>
        /// Метод для определения режима игры
        /// </summary>
        static void Mode()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(23, 23);
            Console.WriteLine("Введите 1 для игры со смертельными стенами");
            Console.SetCursorPosition(23, 25);
            Console.WriteLine("Введите 0 для игры без смертельных стен");
            Console.SetCursorPosition(42, 27);
            string? mode = Console.ReadLine();
            isWall = mode switch
            {
                "0" => false,
                "1" => true,
                _ => true,
            };
        }

        /// <summary>
        /// Метод для определения скорости змейки
        /// </summary>
        static void Speed()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(30, 21);
            Console.WriteLine("Введите 0 для медленного режима");
            Console.SetCursorPosition(30, 23);
            Console.WriteLine("Введите 1 для среднего режима");
            Console.SetCursorPosition(30, 25);
            Console.WriteLine("Введите 2 для быстрого режима");
            Console.SetCursorPosition(42, 27);
            string? difficulty = Console.ReadLine();
            FrameMs = difficulty switch
            {
                "0" => 190,
                "1" => 140,
                "2" => 92,
                _ => 140,
            };
        }

        /// <summary>
        /// Главная панель управления игрой
        /// </summary>
        static void StartGame()
        {
            Console.Clear();  // очищает консоль

            Direction currentMove = Direction.Right;  // задает начальное движение

            bool isEnd = false;  // проверка на конец игры по максимальному количества очков

            var snake = new Snake(10, 5, HeadColor, BodyColor);  // начальные координаты змеи и ее цвет

            Pixel food = GenFood(snake);  // создаем пиксель еды

            if (isWall)  // рисует пиксели для определенного режима
            {
                DrawBorder();  // рисует границы
                food.Draw();
            }
            else
            {
                food.Draw(ScreenWidth, ScreenHeight);
            }

            int score = 0;  // переменная по набору очков

            int lagMs = 0;  // отвечает за время потраченное время на генерацию еды и обработку движения

            var sw = new Stopwatch();  // создаем таймер

            while (true)
            {
                sw.Restart();  // обнуляем таймер
                Direction oldMove = currentMove;  // корректируем движение
                while (sw.ElapsedMilliseconds <= FrameMs - lagMs)   // проверяет, является ли время, прошедшее с начала кадра, меньше или равно доступному времени для обработки кадра
                {

                    if (currentMove == oldMove)  // проверка на стороны движения
                    {
                        currentMove = ReadMove(currentMove);  // задаем новую сторону движения
                    }
                    Thread.Sleep(1);  // остановка для разрузги ЦП
                }
                sw.Restart();  // обнуляем таймер
                
                if (snake.Head.X == food.X && snake.Head.Y == food.Y)  // проверка уоординат головы и тела на координаты еды
                {
                    snake.Move(currentMove, true);  // изменяем метод для того что бы очередь оставила первый элемент
                    food = GenFood(snake);  // генерируем новую еду
                    
                    if (isWall)
                    {
                        food.Draw();
                    }
                    else
                    {
                        food.Draw(ScreenWidth, ScreenHeight);
                    }

                    if (score % 5 == 0)
                    {
                        FrameMs -= 1;
                    }

                    score++;  // прибавление результата
                    Task.Run(() => Console.Beep(1200, 200));  // звук при поедание еды

                    if (score * 9 >= (MapWidth * MapHeight) - 27)  // счет максимального количества очков
                    {
                        isEnd = true;
                        break;
                    }
                }
                else
                {
                    snake.Move(currentMove);  // оставляет полученное движение
                }

                if (isWall)
                {
                    if (snake.Head.X == MapWidth - 1
                    || snake.Head.X == 0
                    || snake.Head.Y == MapHeight - 1
                    || snake.Head.Y == 0  // проверка на пересечение координат головы с краем игровой области
                    || snake.Body.Any(b => b.X == snake.Head.X && b.Y == snake.Head.Y))  // и на сталкновение головы с телом
                    {
                        break;
                    }
                }
                else
                {
                    if (snake.Body.Any(b => b.X == snake.Head.X && b.Y == snake.Head.Y))  // проверка на сталкновение головы с телом
                    { 
                        break;
                    }
                }

                lagMs = (int)sw.ElapsedMilliseconds;  // получаем время затраченное на один кадр работы
                Console.ForegroundColor = isWall ? (ConsoleColor.Black) : (ConsoleColor.White);  // цвет текста
                Console.BackgroundColor = isWall ? (BorderColor) : (ConsoleColor.Black); // цвет фона текста
                Console.SetCursorPosition(40, 1);  // позиция текста с очками
                Console.WriteLine($"Score - {score}");
                Console.ResetColor();
            }

            Console.Clear();  // очистка консоли
            snake.Clear();  // стерание змеии

            if (isWall)  // очищает от пикселей для определенного режима
            {
                food.Clear();
            }
            else
            {
                food.Clear(ScreenWidth, ScreenHeight);
            }

            if (isEnd)  // проверка на максимальный счет
            {
                Console.SetCursorPosition(41, 16);
                Console.WriteLine("YOU WON");
            }

            Console.SetCursorPosition(40, 20);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Score - {score}");
            Task.Run(() => Console.Beep(200, 600));  // звук при проигрыши

            Console.SetCursorPosition(15, 23);
            Console.WriteLine("Введите 1 для выхода в меню или нажмите ENTER для перезапуска");
            Console.SetCursorPosition(42, 25);
            string? userInput = Console.ReadLine();
            isMenu = userInput switch
            {
                "0" => false,
                "1" => true,
                _ => false,
            };
        }

        /// <summary>
        /// Создает новый пиксель в пределах игровой области
        /// </summary>
        /// <param name="snake">Чтобы еда не создалась на змее</param>
        /// <returns>Пиксель еды</returns>
        static Pixel GenFood(Snake snake)
        {
            Pixel food = new Pixel(0, 0, FoodColor);  // экземпляр еды
            bool foodGenerated = false;  // переменная для разрешения генерации еды

            while (!foodGenerated)
            {
                int foodX = Random.Next(1, MapWidth - 1);  // ограничитель для координаты X
                int foodY = Random.Next(1, MapHeight - 1);  // ограничитель для координаты Y

                if (!snake.Body.Any(b => b.X == foodX && b.Y == foodY))  // Проверка, нет ли пикселя на теле змейки
                {
                    if ((foodX != snake.Head.X || foodY != snake.Head.Y)
                        && !(foodX <= 0 || foodX >= MapWidth - 1 || foodY <= 0 || foodY >= MapHeight - 1))  // Проверка, нет ли пикселя на голове змейки и нет ли за границами игровой области
                    {
                        food = new Pixel(foodX, foodY, FoodColor);  // генерация еды
                        foodGenerated = true;  // забирает разрешение
                    }
                }
            }

            return food;
        }

        /// <summary>
        /// Считывает и интерпитирует ввод с клавиатуры, для определения движения
        /// </summary>
        /// <param name="currentDirection">Текущее направление движения змейки</param>
        /// <returns>Новое напрвление, учитывая ввод с клавиатуры</returns>
        static Direction ReadMove(Direction currentDirection)
        {
            if (!Console.KeyAvailable)  // проверка на отжатие клавишь
            {
                return currentDirection;
            }

            ConsoleKey key = Console.ReadKey(true).Key;  // экзмпляр клавиши

            return key switch  // разрешает, если не поворачивается в противоположную сторону
            {
                ConsoleKey.UpArrow when currentDirection != Direction.Down => Direction.Up,
                ConsoleKey.DownArrow when currentDirection != Direction.Up => Direction.Down,
                ConsoleKey.LeftArrow when currentDirection != Direction.Right => Direction.Left,
                ConsoleKey.RightArrow when currentDirection != Direction.Left => Direction.Right,
                _ => currentDirection,
            };
        }

        /// <summary>
        /// Рисует границу игровой области на экране консоли
        /// </summary>
        static void DrawBorder()
        {
            for (int i = 0; i < MapWidth; i++)  // отрисовка границ по ширине
            {
                if (isWall)
                {
                    new Pixel(i, 0, BorderColor).Draw();
                    new Pixel(i, MapHeight - 1, BorderColor).Draw();
                }
                else
                {
                    new Pixel(i, 0, BorderColor).Draw(ScreenWidth, ScreenHeight);
                    new Pixel(i, MapHeight - 1, BorderColor).Draw(ScreenWidth, ScreenHeight);
                }
            }

            for (int i = 0; i < MapHeight; i++)  // отрисовка границ по высоте
            {
                if (isWall)
                {
                    new Pixel(0, i, BorderColor).Draw();
                    new Pixel(MapWidth - 1, i, BorderColor).Draw();
                }
                else
                {
                    new Pixel(0, i, BorderColor).Draw(ScreenWidth, ScreenHeight);
                    new Pixel(MapWidth - 1, i, BorderColor).Draw(ScreenWidth, ScreenHeight);
                }
            }
        }
    }
}