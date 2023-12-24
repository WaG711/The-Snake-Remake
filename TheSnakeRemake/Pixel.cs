namespace TheSnakeRemake
{
    /// <summary>
    /// Представляет пиксельную структуру для консольной графики
    /// </summary>
    public readonly struct Pixel
    {
        /// <summary>
        /// Символ для заполнения пустых ячеек
        /// </summary>
        private const char PixelChar = '█';
        /// <summary>
        /// Представляет X координату пикселя
        /// </summary>
        public int X { get; }
        /// <summary>
        /// Представляет Y координату пикселя
        /// </summary>
        public int Y { get; }
        /// <summary>
        /// Представляет цвет пикселя
        /// </summary>
        public ConsoleColor Color { get; }
        /// <summary>
        /// Представляет размер пикселя
        /// </summary>
        public int PixelSize { get; }

        /// <summary>
        /// Представляет пиксель
        /// </summary>
        /// <param name="x">Координата X</param>
        /// <param name="y">Координата Y</param>
        /// <param name="color">Цвет пикселя</param>
        /// <param name="pixelSize">Размер пикселя</param>
        public Pixel(int x, int y, ConsoleColor color, int pixelSize = 3)
        {
            X = x;
            Y = y;
            Color = color;
            PixelSize = pixelSize;
        }

        /// <summary>
        /// Для режима со смертью при касание границы игрового поля
        /// </summary>
        /// <remarks>
        /// Пиксель рисуется в виде блока символов 3x3
        /// </remarks>
        public void Draw()
        {
            Console.ForegroundColor = Color;  // задается цвет пикселя

            for (int x = 0; x < PixelSize; x++)
            {
                for (int y = 0; y < PixelSize; y++)
                {
                    Console.SetCursorPosition(X * PixelSize + x, Y * PixelSize + y);
                    Console.Write(PixelChar);
                }
            }
        }

        /// <summary>
        /// Для режима со смертью при касание границы игрового поля
        /// </summary>
        /// <remarks>
        /// Этод метод используется для очистки пространства, заменяемого пикселем, нарисованным на консоли
        /// </remarks>
        public void Clear()
        {
            for (int x = 0; x < PixelSize; x++)
            {
                for (int y = 0; y < PixelSize; y++)
                {
                    Console.SetCursorPosition(X * PixelSize + x, Y * PixelSize + y);  // определяет местонахождение пикселя и перезаписывает
                    Console.Write(' ');
                }
            }
        }

        /// <summary>
        /// Для режима без смерти при касание границы игрового поля
        /// </summary>
        /// <param name="consoleWidth">Ширина окна консоли</param>
        /// <param name="consoleHeight">Высота окна консоли</param>
        /// <remarks>
        /// Пиксель рисуется в виде блока символов 3x3
        /// </remarks>
        public void Draw(int consoleWidth, int consoleHeight)
        {
            Console.ForegroundColor = Color;

            int wrappedX = (X % consoleWidth + consoleWidth) % consoleWidth;
            int wrappedY = (Y % consoleHeight + consoleHeight) % consoleHeight;

            for (int x = 0; x < PixelSize; x++)
            {
                for (int y = 0; y < PixelSize; y++)
                {
                    int drawX = (wrappedX * PixelSize + x) % consoleWidth;
                    int drawY = (wrappedY * PixelSize + y) % consoleHeight;

                    Console.SetCursorPosition(drawX, drawY);
                    Console.Write(PixelChar);
                }
            }
        }

        /// <summary>
        /// Для режима без смерти при касание границы игрового поля
        /// </summary>
        /// <param name="consoleWidth">Ширина окна консоли</param>
        /// <param name="consoleHeight">Высота окна консоли</param>
        /// <remarks>
        /// Метод SetCursorPosition определяет местонахождение пикселя и перезаписывает.
        /// </remarks>
        public void Clear(int consoleWidth, int consoleHeight)
        {
            for (int x = 0; x < PixelSize; x++)
            {
                for (int y = 0; y < PixelSize; y++)
                {
                    int wrappedX = ((X * PixelSize + x) % consoleWidth + consoleWidth) % consoleWidth;
                    int wrappedY = ((Y * PixelSize + y) % consoleHeight + consoleHeight) % consoleHeight;

                    Console.SetCursorPosition(wrappedX, wrappedY);
                    Console.Write(' ');
                }
            }
        }
    }
}
