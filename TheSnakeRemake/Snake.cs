namespace TheSnakeRemake
{
    /// <summary>
    /// Представляет змею в игре
    /// </summary>
    /// <remarks>
    /// Этот класс управляет движение змеи, частями тела и рисованием в консоль
    /// </remarks>
    public class Snake
    {
        /// <summary>
        /// Представляет цвет головы змеи
        /// </summary>
        private readonly ConsoleColor headColor;
        /// <summary>
        /// Представляет цвет тела змеи
        /// </summary>
        private readonly ConsoleColor bodyColor;

        /// <summary>
        /// Представляет пиксель, действующий как голова змеи
        /// </summary>
        public Pixel Head { get; private set; }
        /// <summary>
        /// Представляет собой очередь пикселей, образующих тело змеи
        /// </summary>
        public Queue<Pixel> Body { get; } = new Queue<Pixel>();

        /// <summary>
        /// Иницилизирует новый экземпляр класса
        /// </summary>
        /// <param name="initialX">Начальная X координата змеи</param>
        /// <param name="initialY">Начальная Y координата змеи</param>
        /// <param name="headColor">Цвет головы змеи</param>
        /// <param name="bodyColor">Цвет тела змеи</param>
        /// <param name="bodyLenght">Длинна тела</param>
        public Snake(int initialX, int initialY, ConsoleColor headColor, ConsoleColor bodyColor, int bodyLenght = 0)
        {
            this.headColor = headColor;
            this.bodyColor = bodyColor;

            Head = new Pixel(initialX, initialY, this.headColor);

            for (int i = bodyLenght; i >= 0; i--)
            {
                Body.Enqueue(new Pixel(Head.X - i - 1, initialY, this.bodyColor));
            }

            Draw();
        }

        /// <summary>
        /// Перемещает змейку в указанном направлении и обновляет ее положение на консоли
        /// </summary>
        /// <param name="direction">Направление, в котором будет двигатся змея</param>
        /// <param name="eat">Указывает съела ли змея пищу и должна ли змея вырости (по умолчанию: false)</param>
        public void Move(Direction direction, bool eat = false)
        {
            Clear();  // удаляет элементы змейки

            Body.Enqueue(new Pixel(Head.X, Head.Y, this.bodyColor));  // рисует тело

            if (!eat)
            {
                Body.Dequeue();  // удаляет первый элемент тела
            }

            if (Program.IsWall)  // проверка режима игры
            {
                /// <summary>
                /// Управление для режима игры со смертью при касание границы игрового поля
                /// </summary>
                switch (direction)
                {
                    case Direction.Right:
                        Head = new Pixel(Head.X + 1, Head.Y, this.headColor);
                        break;
                    case Direction.Left:
                        Head = new Pixel(Head.X - 1, Head.Y, this.headColor);
                        break;
                    case Direction.Up:
                        Head = new Pixel(Head.X, Head.Y - 1, this.headColor);
                        break;
                    case Direction.Down:
                        Head = new Pixel(Head.X, Head.Y + 1, this.headColor);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                /// <summary>
                /// Управление для режима игры без смерти при касание границ игрового поля
                /// </summary>
                switch (direction)
                {
                    case Direction.Right:
                        Head = new Pixel((Head.X + 1) % 30, Head.Y, this.headColor);
                        break;
                    case Direction.Left:
                        Head = new Pixel((Head.X - 1 + 30) % 30, Head.Y, this.headColor);
                        break;
                    case Direction.Up:
                        Head = new Pixel(Head.X, (Head.Y - 1 + 20) % 20, this.headColor);
                        break;
                    case Direction.Down:
                        Head = new Pixel(Head.X, (Head.Y + 1) % 20, this.headColor);
                        break;
                    default: break;
                }
            }

            Draw();

        }

        /// <summary>
        /// Рисует пиксели головы и тела
        /// </summary>
        /// <remarks>
        /// Выполняет итерацию по каждому пикселю в теле
        /// </remarks>
        public void Draw()
        {
            if (Program.IsWall)
            {
                Head.Draw();
                foreach (Pixel pixel in Body)
                {
                    pixel.Draw();
                }
            }
            else
            {
                Head.Draw(Program.GetScreenWidth(), Program.GetScreeenHeight());
                foreach (Pixel pixel in Body)
                {
                    pixel.Draw(Program.GetScreenWidth(), Program.GetScreeenHeight());
                }
            }
        }

        /// <summary>
        /// Очищает пространство занимаемое пикселем головы и тела
        /// </summary>
        /// <remarks>
        /// Выполняет итерацию по каждому пикселю в теле
        /// </remarks>
        public void Clear()
        {
            if (Program.IsWall)
            {
                Head.Clear();
                foreach (Pixel pixel in Body)
                {
                    pixel.Clear();
                }
            }
            else
            {
                Head.Clear(Program.GetScreenWidth(), Program.GetScreeenHeight());
                foreach (Pixel pixel in Body)
                {
                    pixel.Clear(Program.GetScreenWidth(), Program.GetScreeenHeight());
                }
            }
        }
    }
}
