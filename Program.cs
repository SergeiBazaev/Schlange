using System;


namespace Schlange
{
    internal class Program

    {
        // Параметры еды
        static int food_x;
        static int food_y;

        static void SpaunFood()
        {
           

            Random rand = new Random();

            food_x = rand.Next(2, 116);
            if (food_x % 2 != 0) food_x += 1;
            food_y = rand.Next(6, 36);
        }
        static void Main(string[] args)
        {
            //  Параметры программы
            Console.SetWindowSize(120, 40);
            Console.SetBufferSize(120, 40);
            Console.CursorVisible = false;

            bool isGame = true;
            bool win = false;

            // Параметры змейки
            int head_x = 20;
            int head_y = 10;
            int dir = 0;
            int snakeLen = 10;
            int[] body_x = new int[120];
            int[] body_y = new int[120];

            int pleyScore = 0;
            int record = 0;

            // Стартовое значение змейки

            for (int i = 0; i < snakeLen; i++)
            {
                body_x[i] = head_x - (i * 2);
                body_y[i] = 10;
            }  

            // Стартовое значение еды
            SpaunFood();
            // Игровой цикл
            while (true)
            {
                
                Console.ForegroundColor = ConsoleColor.Blue;
                for (int x = 1; x < 119; x++)
                {
                    Console.SetCursorPosition(x, 5);
                    Console.Write("█");
                }
                for (int x = 1; x < 119; x++)
                {
                    Console.SetCursorPosition(x, 37);
                    Console.Write("█");
                }
                for (int x = 6; x < 38; x++)
                {
                    Console.SetCursorPosition(1, x);
                    Console.Write("█");
                }
                for (int x = 6; x < 38; x++)
                {
                    Console.SetCursorPosition(118, x);
                    Console.Write("█");
                }

                while (isGame == true)
                {
                    // 1. Очистка

                    for (int i = 0; i < snakeLen; i++)
                    {
                        Console.SetCursorPosition(body_x[i], body_y[i]);
                        Console.Write("  ");
                    }

                    Console.SetCursorPosition(head_x, head_y);
                    Console.Write("  ");

                    Console.SetCursorPosition(food_x, food_y);
                    Console.Write("  ");
                    // 2. Расчет

                    // Движение змейки
                    if (Console.KeyAvailable == true)
                    {
                        ConsoleKeyInfo key;
                        Console.SetCursorPosition(0, 0);
                        key = Console.ReadKey();

                        Console.SetCursorPosition(0, 0);
                        Console.Write("  ");

                        if (key.Key == ConsoleKey.D && dir != 2) dir = 0;
                        if (key.Key == ConsoleKey.S && dir != 3) dir = 1;
                        if (key.Key == ConsoleKey.A && dir != 0) dir = 2;
                        if (key.Key == ConsoleKey.W && dir != 1) dir = 3;
                    }

                    if (dir == 0) head_x += 2;
                    if (dir == 1) head_y += 1;
                    if (dir == 2) head_x -= 2;
                    if (dir == 3) head_y -= 1;

                    // Бесконечное поле
                    if (head_x < 2) head_x = 116;
                    if (head_x > 116) head_x = 2;
                    if (head_y < 6) head_y = 36;
                    if (head_y > 36) head_y = 6;

                    for (int i = snakeLen; i > 0; i--)
                    {
                        body_x[i] = body_x[i - 1];
                        body_y[i] = body_y[i - 1];
                    }
                    body_x[0] = head_x;
                    body_y[0] = head_y;

                    for (int i = 1; i < snakeLen; i++)
                    {
                        if (body_x[i] == head_x && body_y[i] == head_y)
                        {
                            isGame = false;
                            if (pleyScore > record)
                            {
                                record = pleyScore;
                            }

                        }
                    }


                    

                   

                    // Еда

                    if (head_x == food_x && head_y == food_y)
                    {
                        SpaunFood();
                        for (int i = 1; i < snakeLen; i++)
                        {
                            if (food_x == body_x[i] && food_y == body_y[i])
                            {
                                while (true)
                                {
                                    SpaunFood();
                                    if (food_x != body_x[i] && food_y != body_y[i])
                                    {
                                        break;
                                    }
                                }
                            }
                        }

                        snakeLen++;
                        pleyScore = snakeLen - 10;
                    }

                    if(snakeLen == 110)
                    {
                        win = true;
                        record = pleyScore;
                        break;
                    }
                    // 3. Отрисовка


                    Console.ForegroundColor = ConsoleColor.Green;
                    for (int i = 0; i < snakeLen; i++)
                    {
                        Console.SetCursorPosition(body_x[i], body_y[i]);
                        Console.Write("██");
                    }

                    Console.SetCursorPosition(head_x, head_y);
                    Console.Write("██");

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.SetCursorPosition(food_x, food_y);
                    Console.Write("██");

                    Console.SetCursorPosition(2, 1);
                    Console.Write("Счет: " + pleyScore);

                    Console.SetCursorPosition(2, 3);
                    Console.Write("Рекорд: " + record);



                    // 4. Ожидание
                    System.Threading.Thread.Sleep(70);


                }

                if(win == true)
                {
                    Console.SetCursorPosition(50, 1);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Победа!");
                    isGame = false;
                }
                else
                {
                    Console.SetCursorPosition(50, 1);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Проиграл!");
                    
                }


                

                Console.SetCursorPosition(50, 3);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Для перезапуска игры нажми клавишу \"R\"");
                Console.WriteLine();
                Console.Write("   ");

                if (isGame == false)
                {
                    if (Console.KeyAvailable == true)
                    {
                        ConsoleKeyInfo key;
                        key = Console.ReadKey();
                        if (key.Key == ConsoleKey.R)
                        {
                            isGame = true;

                            Console.Clear();
                            head_x = 20;
                            head_y = 10;
                            dir = 0;
                            snakeLen = 10;

                             body_x = new int[120];
                             body_y = new int[120];

                            for (int i = 0; i < snakeLen; i++)
                            {
                                body_x[i] = head_x - (i * 2);
                                body_y[i] = 10;
                            }

                            pleyScore = 0;
                            
                        }
                    }

                }


            }

            Console.ReadLine();
        }
    }
}
