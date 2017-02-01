namespace Snake
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    public class Snake
    {
        public static void Main()
        {
            Random random = new Random();
            int startingX = Console.WindowWidth;
            int startingY = Console.WindowHeight / 4;
            int directionX = 1;
            int directionY = 0;
            char snakeHead = '@';
            char snakeBody = '+';
            char food = '*';
            int finalResult = 0;

            int foodX = random.Next(0, Console.WindowWidth);
            int foodY = random.Next(0, Console.WindowHeight);

            List<int> snakeX = new List<int>();
            List<int> snakeY = new List<int>();

            int snakeBodyCount = 7;

            Console.WindowWidth = Console.WindowWidth;
            Console.WindowHeight = Console.WindowHeight;
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.Title = "Lekov's snake";
            var snakeColor = ConsoleColor.Black;
            var foodColor = ConsoleColor.White;

            for (int i = 0; i < snakeBodyCount; i++)
            {
                startingX += directionX;
                startingY += directionY;

                snakeX.Add(startingX);
                snakeY.Add(startingY);                
            }

            while (true)
            {
                Console.ForegroundColor = snakeColor;
                for (int i = 0; i < snakeX.Count; i++)
                {
                    char element = snakeBody;
                    if (i < snakeX.Count - 1)
                    {
                        snakeX[i] = snakeX[i + 1];
                        snakeY[i] = snakeY[i + 1]; 
                    }
                    else
                    {
                        // die if you hit left or right wall
                        if (snakeX[i] + directionX > Console.WindowWidth - 1)
                        {
                            PrintGameOver(finalResult);

                            Thread.Sleep(1);
                            return;
                        }

                        if (snakeX[i] + directionX < 0)
                        {
                            PrintGameOver(finalResult);

                            Thread.Sleep(1);
                            return;
                        }

                        // die if you hit up or down wall
                        if (snakeY[i] + directionY > Console.WindowHeight - 1)
                        {
                            PrintGameOver(finalResult);

                            Thread.Sleep(1);
                            return;
                        }

                        if (snakeY[i] + directionY < 0)
                        {
                            PrintGameOver(finalResult);

                            Thread.Sleep(1);
                            return;
                        }

                        snakeX[i] += directionX;
                        snakeY[i] += directionY;
                        bool isCurrentlyEaten = false;

                        if (snakeX[i] == foodX && snakeY[i] == foodY)
                        {
                            foodX = random.Next(0, Console.WindowWidth);
                            foodY = random.Next(0, Console.WindowHeight);
                            snakeX.Add(snakeX[i]);
                            snakeY.Add(snakeY[i]);
                            isCurrentlyEaten = true;
                            finalResult++;
                        }

                        // die if you eat yourself
                        for (int j = 0; j < snakeX.Count - 1; j++)
                        {
                            if (snakeX[i] == snakeX[j] &&
                                snakeY[i] == snakeY[j] && !isCurrentlyEaten)
                            {
                                PrintGameOver(finalResult);

                                Thread.Sleep(1);
                                return;
                            }
                        }

                        element = snakeHead;
                    }

                    Console.SetCursorPosition(snakeX[i], snakeY[i]);
                    Console.Write(element);
                }

                Console.ForegroundColor = foodColor;
                Console.SetCursorPosition(foodX, foodY);
                Console.Write(food);

                // Collision checking

                // Change
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.LeftArrow)
                    {
                        if (directionX != 1)
                        {
                            directionX = -1;
                            directionY = 0;
                        }
                    }

                    if (key.Key == ConsoleKey.RightArrow)
                    {
                        if (directionX != -1)
                        {
                            directionX = 1;
                            directionY = 0;
                        }
                    }

                    if (key.Key == ConsoleKey.DownArrow)
                    {
                        if (directionY != -1)
                        {
                            directionY = 1;
                            directionX = 0;
                        }
                    }

                    if (key.Key == ConsoleKey.UpArrow)
                    {
                        if (directionY != 1)
                        {
                            directionX = 0;
                            directionY = -1;
                        }
                    }
                }

                Thread.Sleep(100);
                Console.Clear();
            }
        }

        private static void PrintGameOver(int finalResult)
        {
            Console.Clear();
                      
                Console.WriteLine($@"
                       GAME OVER!!!!!!
          TONIGHT YOU DINE IN HELL, MOTHERFUCKER !!!!
                   RESULT: Shit Eaten - {finalResult}
                         uuuuuuuuu
                      uu$$$$$$$$$$$uu
                   uu$$$$$$$$$$$$$$$$$uu
                  u$$$$$$$$$$$$$$$$$$$$$u
                 u$$$$$$$$$$$$$$$$$$$$$$$u
                u$$$$$$$$$$$$$$$$$$$$$$$$$u
                u$$$$$$$$$$$$$$$$$$$$$$$$$u
                u$$$$$$     $$$      $$$$$u
                u$$$$       u$u       $$$$u
                u$$$u       u$u       u$$$u
                 $$$u      u$$$u      u$$$
                  u$$$$uu$$$   $$$uu$$$$u
                   u$$$$$$$u   u$$$$$$$u
                     u$$$$$$$u$$$$$$$u
                      u$*$*$*$*$*$*$u
           uuu        $$u$ $ $ $ $u$$       uuu
          u$$$$        $$$$$u$u$u$$$       u$$$$
           $$$$$uu      *$$$$$$$$$*     uu$$$$$$
         u$$$$$$$$$$$uu    *****    uuuu$$$$$$$$$$
         $$$$***$$$$$$$$$$uuu   uu$$$$$$$$$***$$$*
          ***      ""$$$$$$$$$$$uu ""$""
                    uuuu ""$$$$$$$$$$uuu
           u$$$uuu$$$$$$$$$uu ""$$$$$$$$$$$uuu$$$
           $$$$$$$$$$""""           ""$$$$$$$$$$$""
            ""$$$$$""                      ""$$$$""
              $$$""                         $$$$""");
            
        }
    }
}
