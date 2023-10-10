using System;
using System.Threading;

namespace RectangularFigure
{
    class Program
    {
        // El tamaño de la consola
        static int consoleWidth = 80;
        static int consoleHeight = 25;

        // El tamaño del rectángulo
        static int rectangleWidth = 10;
        static int rectangleHeight = 5;

        // La posición inicial del rectángulo
        static int rectangleX = consoleWidth / 2 - rectangleWidth / 2;
        static int rectangleY = 0;

        // La velocidad de caída del rectángulo
        static int speed = 1;

        // La altura del suelo
        static int groundHeight = 3;

        // El array bidimensional que representa el rectángulo
        static char[,] rectangle = new char[rectangleHeight, rectangleWidth];

        // El método que llena el array con el símbolo '*'
        static void FillRectangle()
        {
            for (int i = 0; i < rectangleHeight; i++)
            {
                for (int j = 0; j < rectangleWidth; j++)
                {
                    rectangle[i, j] = '*';
                }
            }
        }

        // El método que dibuja el rectángulo en la consola
        static void DrawRectangle()
        {
            Console.SetCursorPosition(rectangleX, rectangleY);
            for (int i = 0; i < rectangleHeight; i++)
            {
                for (int j = 0; j < rectangleWidth; j++)
                {
                    Console.Write(rectangle[i, j]);
                }
                Console.WriteLine();
                Console.SetCursorPosition(rectangleX, rectangleY + i + 1);
            }
        }

        // El método que borra el rectángulo de la consola
        static void ClearRectangle()
        {
            Console.SetCursorPosition(rectangleX, rectangleY);
            for (int i = 0; i < rectangleHeight; i++)
            {
                for (int j = 0; j < rectangleWidth; j++)
                {
                    Console.Write(' ');
                }
                Console.WriteLine();
                Console.SetCursorPosition(rectangleX, rectangleY + i + 1);
            }
        }

        // El método que actualiza la posición del rectángulo
        static void UpdateRectangle()
        {
            // Si el rectángulo no ha tocado el suelo, se mueve hacia abajo
            if (rectangleY + rectangleHeight < consoleHeight - groundHeight)
            {
                rectangleY += speed;
            }
            // Si el rectángulo ha tocado el suelo, se detiene el programa
            else
            {
                Environment.Exit(0);
            }
        }

        // El método que dibuja el suelo en la consola
        static void DrawGround()
        {
            Console.SetCursorPosition(0, consoleHeight - groundHeight);
            for (int i = 0; i < groundHeight; i++)
            {
                for (int j = 0; j < consoleWidth; j++)
                {
                    Console.Write('_');
                }
                Console.WriteLine();
            }
        }

        // El método que rota el rectángulo en la consola
        static void RotateRectangle()
        {
            // Se crea un nuevo array para guardar el rectángulo rotado
            char[,] rotatedRectangle = new char[rectangleWidth, rectangleHeight];

            // Se copia el rectángulo original al array rotado de forma transpuesta e invertida
            for (int i = 0; i < rectangleHeight; i++)
            {
                for (int j = 0; j < rectangleWidth; j++)
                {
                    rotatedRectangle[j, i] = rectangle[rectangleHeight - i - 1, j];
                }
            }

            // Se asigna el array rotado al original
            rectangle = rotatedRectangle;

            // Se intercambia el ancho y el alto del rectángulo
            int temp = rectangleWidth;
            rectangleWidth = rectangleHeight;
            rectangleHeight = temp;

            // Se ajusta la posición del rectángulo para que no se salga de la consola
            if (rectangleX + rectangleWidth > consoleWidth)
            {
                rectangleX = consoleWidth - rectangleWidth;
            }

            if (rectangleY + rectangleHeight > consoleHeight - groundHeight)
            {
                rectangleY = consoleHeight - groundHeight - rectangleHeight;
            }

        }

        // El método principal del programa
        static void Main(string[] args)
        {
            // Se configura la consola
            Console.CursorVisible = false;
            Console.SetWindowSize(consoleWidth, consoleHeight);
            Console.SetBufferSize(consoleWidth, consoleHeight);

            // Se llena el array del rectángulo
            FillRectangle();

            // Se dibuja el suelo
            DrawGround();

            // Se inicia el bucle principal del programa
            while (true)
            {
                // Se dibuja el rectángulo
                DrawRectangle();

                // Se espera un tiempo
                Thread.Sleep(100);

                // Se borra el rectángulo
                ClearRectangle();

                // Se actualiza la posición del rectángulo
                UpdateRectangle();

                // Se verifica si el usuario ha presionado una tecla de flecha
                if (Console.KeyAvailable)
                {
                    // Se obtiene la tecla presionada
                    ConsoleKey key = Console.ReadKey(true).Key;

                    // Si es una tecla de flecha, se rota el rectángulo
                    if (key == ConsoleKey.UpArrow || key == ConsoleKey.DownArrow || key == ConsoleKey.LeftArrow || key == ConsoleKey.RightArrow)
                    {
                        RotateRectangle();
                    }
                }
            }
        }
    }
}
