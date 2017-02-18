namespace Snake
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    public class Snake
    {
        public static void Main()
        {
            //ConsoleWidth=120,ConsoleHeight=30
            Random random = new Random();
            int startingX = 15;
            int startingY = 10;
            int directionX = 1;
            int directionY = 0;
            char snakeHead = '@';
            char snakeBody = '+';
            //char food = '*';
            int finalResult = 0;
            int bonusPoints = 0;

            int foodX = random.Next(0, 49);
            int foodY = random.Next(0, 24);

            List<int> snakeX = new List<int>();
            List<int> snakeY = new List<int>();

            int snakeBodyCount = 7;

            Console.WindowWidth = 50;
            Console.WindowHeight = 25;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Title = "Lekov's snake";
            var snakeColor = ConsoleColor.Yellow;
            var foodColor = ConsoleColor.Cyan;

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
                        // die if you hit right wall
                        if (snakeX[i] + directionX > 49)
                        {
                            PrintGameOver(finalResult);

                            Thread.Sleep(1);
                            return;
                        }

                        // die if you hit left wall
                        if (snakeX[i] + directionX < 0)
                        {
                            PrintGameOver(finalResult);

                            Thread.Sleep(1);
                            return;
                        }
                        
                        // die if you hit down wall
                        if (snakeY[i] + directionY > 24)
                        {
                            PrintGameOver(finalResult);

                            Thread.Sleep(1);
                            return;
                        }

                        // die if you hit up wall
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
                            Console.Beep(9000, 150);

                            foodX = random.Next(0, 49);
                            foodY = random.Next(0, 24);
                            snakeX.Add(snakeX[i]);
                            snakeY.Add(snakeY[i]);
                            isCurrentlyEaten = true;
                            finalResult += bonusPoints;
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
                int bonusNumber = 0;
                DateTime dateTimeNow = DateTime.Now;
                if(dateTimeNow.Second % 10 > 6)
                {
                    Console.WriteLine(3);
                    bonusPoints = 3;
                }
                else if(dateTimeNow.Second % 10 > 3)
                {
                    Console.WriteLine(5);
                    bonusPoints = 5;
                }
                else if(dateTimeNow.Second % 10>0)
                {
                    Console.WriteLine(9);
                    bonusPoints = 9;
                }
                else
                {
                    foodX = random.Next(0, 49);
                    foodY = random.Next(0, 24);
                }
   
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
            Console.WriteLine(
$@"                    GAME OVER
                FINAL RESULT:  -   {finalResult}
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
                 $$u$ $ $ $ $u$$       
                  $$$$$u$u$u$$$       
                   *$$$$$$$$$*   
                      *****    ");
            Console.Beep(1500, 700);
            Console.Beep(1500, 700);
            Console.Beep(1500, 4000);
        }
    }
}
