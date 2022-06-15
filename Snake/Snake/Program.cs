using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Snake
{
    internal class Program
    {
        struct Position 
        { 
            public int row;
            public int col;
            public Position(int row, int col)
            {
                this.row = row;
                this.col = col;
            }
        }
        static void Main(string[] args)
        {
            byte right = 0;
            byte left = 1;
            byte down = 2;
            byte up = 3;

            Position[] directions = new Position[]
            {
                new Position(0, 1), //right
                new Position(0, -1), //left
                new Position(1, 0), //down
                new Position(-1, 0), //up
            };

            double sleepTime = 100;
            int direction = right;
            Console.BufferHeight = Console.WindowHeight;
            Random randomNumber = new Random();
            Position food = new Position(randomNumber.Next(0 ,Console.WindowHeight), randomNumber.Next(0, Console.WindowWidth));



            Queue<Position> snakeElements = new Queue<Position>();

            for (int i = 0; i <= 5; i++)
            {
                snakeElements.Enqueue(new Position(0, i));
            }

            foreach (Position position in snakeElements)
            {
                Console.SetCursorPosition(position.col, position.row);
                Console.Write("*");
            }

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo userInput = Console.ReadKey();
                    if (userInput.Key == ConsoleKey.LeftArrow)
                    {
                       if (direction != right)
                        {
                            direction = left;
                        }
                    }
                    if (userInput.Key == ConsoleKey.RightArrow)
                    {
                        if (direction != left)
                        {
                            direction = right;
                        }
                    }
                    if (userInput.Key == ConsoleKey.DownArrow)
                    {
                        if (direction != up)
                        {
                            direction = down;
                        }
                    }
                    if (userInput.Key == ConsoleKey.UpArrow)
                    {
                        if (direction != down)
                        {
                            direction=up;
                        }
                    }
                }

                Position snakeHead = snakeElements.Last();
                Position nextDirection = directions[direction];
                Position newSnakeHead = new Position(snakeHead.row + nextDirection.row, snakeHead.col + nextDirection.col);
                
                if (newSnakeHead.row < 0||
                    newSnakeHead.col < 0||
                    newSnakeHead.row >= Console.WindowHeight||
                    newSnakeHead.col >= Console.WindowWidth||
                    snakeElements.Contains(newSnakeHead))
                {
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine("GAME OVER!");
                    Console.WriteLine($"Your points are {(snakeElements.Count - 6) * 10}");
                    return;
                }
                snakeElements.Enqueue(newSnakeHead);
                
                if (newSnakeHead.row == food.row && newSnakeHead.col == food.col)
                {
                    food = new Position(randomNumber.Next(0, Console.WindowHeight), randomNumber.Next(0, Console.WindowWidth));

                }
                else
                {
                    snakeElements.Dequeue();
                }

                Console.Clear();

                foreach (Position position in snakeElements)
                {
                    Console.SetCursorPosition(position.col, position.row);
                    Console.Write("*");
                }

                Console.SetCursorPosition(food.col, food.row);
                Console.Write("@");

                sleepTime -= 0.02;
                Thread.Sleep((int)sleepTime);
            }

        }
    }
}
