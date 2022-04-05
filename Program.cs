using System.Diagnostics;

using System;
using System.IO;
using System.Text;


namespace Game
{
    class program
    {
        // Method to run tests on methods
        static void Methodtests()
        {
            int size = 3;
            string[] BoardString = new string[size];
            char[][] BoardChar = new char[size][]; // Creating the actual Board as a char multi-dimentional array
            for (int j = 0; j < size; j++)
            {
                BoardString[j] = string.Concat(Enumerable.Repeat("#", size));
            }
            PrintBoard(BoardString, ref BoardChar);
            TestCheckbomb(BoardChar); // Testing method
            ManualMines(BoardChar); //Testing method
            // Console.WriteLine("All tests passed");
        }
        // Method to let the cursor move inside the Board
        static void TryMove(int proposedRow, int proposedColumn, char[][] mapRows)
        {
            if ((proposedRow < 0) || (proposedRow > Console.BufferHeight) || (proposedRow > mapRows.Length - 1))
            {
                return;
            }
            if ((proposedColumn < 0) || (proposedColumn > Console.BufferWidth) || (proposedColumn > mapRows[0].Length - 1))
            {
                return;
            }
            Console.CursorTop = proposedRow;
            Console.CursorLeft = proposedColumn;
        }

        // Method to Print the Board
        static void PrintBoard(string[] mapString, ref char[][] mapChar)
        {
            for (int i = 0; i < mapChar.Length; i++)
            {
                mapChar[i] = mapString[i].ToCharArray();
                Console.WriteLine(mapChar[i]);
            }
        }

        // Method to place Mines in random Places
        static void RandomMines(int size, ref char[][] Board)
        {
            int amountbombs = ((size * size) * 20) / 100; // setting the amount of bombs to be the 20% aprox of all the cells
            System.Random randomCol = new System.Random(1);
            System.Random randomRow = new System.Random(2);
            for (int i = 0; i < amountbombs; i++)
            {
                Board[randomCol.NextInt64(0, Board.Length)][randomRow.NextInt64(0, Board.Length)] = '*';
            }
            Console.WriteLine("The amount of Bombs to find is: " + MineCounter(Board)); // information for the user to let him know how many bombs are placed

        }
        static void ManualMines(char[][] FakeMap) // Method Test
        {
            FakeMap[0][0] = '*';
            FakeMap[0][1] = '*';
            FakeMap[0][2] = '*';

            Debug.Assert(MineCounter(FakeMap) == 3);
            Debug.Assert(MineCounter(FakeMap) != 8);

        }
        // Method to count how many mines are in the board.
        static int MineCounter(char[][] Board)
        {
            int count = 0;
            for (int i = 0; i < Board.Length; i++)
            {
                for (int j = 0; j < Board.Length; j++)
                {
                    if (Board[j][i] == '*')
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        // This method look arround the cell where is placed the cursor and check how many mines are arround
        static int CheckBombsArround(int ActualRow, int ActualColumn, char[][] Map)
        {
            int BombsArround = 0;
            if ((ActualRow + 1 < Map.Length))
            {
                if (Map[ActualRow + 1][ActualColumn] == '*')
                {
                    BombsArround++;
                }
                if ((ActualColumn - 1 >= 0))
                {
                    if (Map[ActualRow + 1][ActualColumn - 1] == '*')
                    {
                        BombsArround++;
                    }
                }
                if ((ActualColumn + 1 < Map[0].Length))
                {
                    if (Map[ActualRow + 1][ActualColumn + 1] == '*')
                    {
                        BombsArround++;
                    }
                }
            }
            if ((ActualRow - 1 >= 0))
            {
                if (Map[ActualRow - 1][ActualColumn] == '*')
                {
                    BombsArround++;
                }
                if ((ActualColumn - 1 >= 0))
                {
                    if (Map[ActualRow - 1][ActualColumn - 1] == '*')
                    {
                        BombsArround++;
                    }
                }
                if ((ActualColumn + 1 < Map[0].Length))
                {
                    if (Map[ActualRow - 1][ActualColumn + 1] == '*')
                    {
                        BombsArround++;
                    }
                }
            }
            if ((ActualColumn + 1 < Map[0].Length))
            {
                if (Map[ActualRow][ActualColumn + 1] == '*')
                {
                    BombsArround++;
                }
            }
            if ((ActualColumn - 1 >= 0))
            {
                if (Map[ActualRow][ActualColumn - 1] == '*')
                {
                    BombsArround++;
                }
            }
            return BombsArround;
        }

        // This boolean method checks the cell where is placed the cursor and returns true if it is a mine o false if is not
        static bool CheckBomb(int ActualRow, int ActualColumn, char[][] Map)
        {
            if (Map[ActualRow][ActualColumn] == '*')
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        static void TestCheckbomb(char[][] FakeMap) // Method Test
        {

            FakeMap[0][0] = '*';
            FakeMap[0][1] = '*';
            FakeMap[0][2] = '*';
            Debug.Assert(CheckBomb(0, 1, FakeMap) == true);
            Debug.Assert(CheckBomb(0, 2, FakeMap) == true);
            Debug.Assert(CheckBomb(2, 0, FakeMap) == false);

        }

        static void Instructions(ref string name) // presentation for the user
        {
            Console.Clear();
            string input = " ";
            while (input != "done")
            {
                Console.WriteLine("Be welcome to MINESWEEPER");
                Console.WriteLine("Please write your name below:");
                name = Console.ReadLine();
                Console.Clear();
                Console.WriteLine("Your objective is to find all the bombs on the board");
                Console.WriteLine();
                Console.WriteLine("To move arround the board you are able to use WASD, or the arrows on your keyboard");
                Console.WriteLine("To surrender hit ESCAPE");
                Console.WriteLine("To select the cell, if you think that isn't a bomb use ENTER");
                Console.WriteLine("If you think that the cell where you are is a bomb use SPACEBAR");
                Console.WriteLine();
                Console.WriteLine("Be careful, if it isn't a bomb, you will recive an error, each error adds 3s to your final time");
                Console.WriteLine();
                Console.WriteLine("If you think that you are ready to go, type \"done\", if you need a TUTORIAl, type \"help\": ");
                input = Console.ReadLine();

                if (input == "help") // if the user needs more help, the program will print a tutorial.
                {
                    Console.Clear();
                    Console.WriteLine("It seems that you need help... Be welcome to the explanation of MINESWEEPER");
                    Console.WriteLine();
                    Console.WriteLine("You will be presented a board, of the size that you want, (if you are here I recomend 5)");
                    Console.WriteLine("every # is a cell, it can be a mine, or not.");
                    Console.WriteLine();
                    Console.WriteLine("To start, select a random cell, and hit enter. GOOD LUCK!");
                    Console.WriteLine("If you had luck, and your cell wasn't a bomb, you will be displayed a number or an empty space");
                    Console.WriteLine();
                    Console.WriteLine("if you had a number, it represents the amount of bombs arrond your cell");
                    Console.WriteLine("if you had an empty space, you are able to hit all the cells arround, because you dont have any mines arround");
                    Console.WriteLine("If you think that a cell is a bomb use SPACEBAR, if it turns green, YOU DID IT!");
                    Console.WriteLine("Now you just need to create a strategy to win the game.");
                    Console.WriteLine();
                    Console.WriteLine("If you think that you are ready to go, type \"done\": ");
                    input = Console.ReadLine();
                }
                Console.Clear();
            }
        }
        static ConsoleKey Movement(Stopwatch stopwatch, char[][] BoardChar, ref int errors, ref int scores)
        {
            ConsoleKey keyIn;
            do // do-while loop to get the value of the keys that are pressed and to give an answer
            {
                keyIn = Console.ReadKey(true).Key;
                switch (keyIn)
                {
                    case ConsoleKey.LeftArrow: // if the user hits the left arrow he will move to the left
                    case ConsoleKey.A:         // if the user hits the 'A' he will move to the left
                        TryMove(Console.CursorTop, Console.CursorLeft - 1, BoardChar);
                        break;

                    case ConsoleKey.RightArrow: // if the user hits the right arrow he will move to the right
                    case ConsoleKey.D:          // if the user hits the 'D' he will move to the Right
                        TryMove(Console.CursorTop, Console.CursorLeft + 1, BoardChar);
                        break;

                    case ConsoleKey.DownArrow:  // if the user hits the down arrow he will move down
                    case ConsoleKey.S:          // if the user hits the 'S'  he will move down
                        TryMove(Console.CursorTop + 1, Console.CursorLeft, BoardChar);
                        break;

                    case ConsoleKey.UpArrow:    // if the user hist the up arrrow he will move up
                    case ConsoleKey.W:          // if the user hist the 'W' he will move up
                        TryMove(Console.CursorTop - 1, Console.CursorLeft, BoardChar);
                        break;

                    case ConsoleKey.Enter:      // the enter button is used for the cells that aren't bombs
                        switch (CheckBomb(Console.CursorTop, Console.CursorLeft, BoardChar))
                        {
                            case true:          // if the cell is a bomb the game will finish and it will show a message for the player
                                Console.Clear();
                                Console.WriteLine("You hitted a bomb, good luck for next time");
                                stopwatch.Stop();
                                break;

                            case false:         // if the cell isn't a bomb you have two posibilities
                                if (CheckBombsArround(Console.CursorTop, Console.CursorLeft, BoardChar) != 0) // First possibility, the cell has one or more bombs arround and it will change form '#' to the amount of bombs arround it
                                {
                                    Console.Write(CheckBombsArround(Console.CursorTop, Console.CursorLeft, BoardChar));
                                    Console.CursorLeft--;
                                    break;
                                }
                                else // second possibility, the cell hasn't any mines arround so it will change to an empty space
                                {
                                    Console.Write(' ');
                                    Console.CursorLeft--;
                                    break;
                                }
                        }
                        break;

                    case ConsoleKey.Spacebar: // the spacebar is used to let the user say where he thinks that the bombs are
                        if (CheckBomb(Console.CursorTop, Console.CursorLeft, BoardChar) == true)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("*");
                            Console.CursorLeft--;                           // if the cell has a bomb, it will display a '*' in green, and will add one to scores
                            Console.ForegroundColor = ConsoleColor.Gray;
                            scores++;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("*");
                            Console.CursorLeft--;                           // if the cell has a bomb, it will display a '*' in red, and will add 1 error
                            Console.ForegroundColor = ConsoleColor.Gray;
                            errors++;
                        }
                        break;

                }
                if (keyIn == ConsoleKey.Escape) // if the player hits Escape, he is surrendering, so the game will finish
                {
                    stopwatch.Stop();
                    break;
                }
                if (scores == MineCounter(BoardChar)) // when the scores counter matches with the amount of bombs, it means that the player won
                {
                    stopwatch.Stop();
                    break;
                }

            } while (true);
            return keyIn;
        }
        static int AskNumBombs()
        {
            int size;
            do
            {
                Console.WriteLine("How many Rows and Columns do you want? (please enter just 1 number)");
                try
                {
                    size = int.Parse(Console.ReadLine());
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("That's not an int! (try again)");
                }
                catch (OverflowException)
                {
                    Console.WriteLine($"That int doesn't fit. Please enter a number between {0} and {int.MaxValue}");
                }
            } while (true);
            return size;
        }
        static void SetBoard(int size, out string[] BoardString, out char[][] BoardChar)
        {
            BoardString = new string[size];
            BoardChar = new char[size][];
            for (int j = 0; j < size; j++)
            {
                BoardString[j] = string.Concat(Enumerable.Repeat("#", size));
            }
        }
        static void GameRun(Stopwatch stopwatch, char[][] BoardChar, out int errors, out int scores)
        {
            stopwatch.Start();
            errors = 0;
            scores = 0;
            ConsoleKey keyIn;
            Console.CursorTop = 0;
            Console.CursorLeft = 0;
            keyIn = Movement(stopwatch, BoardChar, ref errors, ref scores);
            Console.Clear();
        }
        static void Win(Stopwatch stopwatch, char[][] BoardChar, int errors, int scores, string name)
        {
            double elapsedseconds = stopwatch.ElapsedMilliseconds / 1000.0;
            elapsedseconds += errors;
            if (scores == MineCounter(BoardChar)) // this prints the final message for the user when he finishes
            {
                Console.WriteLine($"Congratulations {name}! You won!");
                Console.WriteLine($"The time that you made is: {elapsedseconds}s");
                Console.WriteLine($"You had {errors} errors");
            }
        }
        static void PrintHighscores(String fileName, Stopwatch stopwatch, char[][] BoardChar, int errors, int size, string name, int scores, ref List<double> scorelist)
        {
            double elapsedseconds = stopwatch.ElapsedMilliseconds / 1000.0;
            elapsedseconds += errors;
            if (scores == MineCounter(BoardChar) && BelongInTop10(fileName, elapsedseconds, ref scorelist))
            {
                using (StreamWriter writer = new StreamWriter(fileName, true))
                {
                    writer.WriteLine(name + ", " + elapsedseconds);
                }
                // need to do sorting method
            }
            if (elapsedseconds < scorelist[10])
            {
                // need to do a sorting method   
            }

        }
        static bool BelongInTop10(string file, double time, ref List<double> scores)
        {
            // read all items
            using (StreamWriter writer = new StreamWriter(file))
            {
                // this line creates the file with the name if it doesn't exist
                // if it already exists it will not do anyrhing
            }

            using (StreamReader reader = new StreamReader(file))
            {

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] nameAndScore = line.Split(",");
                    double score = double.Parse(nameAndScore[1]);
                    scores.Add(score);
                }
            }
            // if  lenght < 10, return true
            if (scores.Count < 10)
            {
                return true;
            }
            return false;

        }
        static void Main()
        {
            // Methodtests(); // Method to run tests
            string name = " ";
            List<double> scorelist = new List<double>();
            Instructions(ref name);
            Console.Clear();
            Stopwatch stopwatch = new Stopwatch(); // Stopwatch to record the time of the player
            int size = AskNumBombs();   // Asking the player how many rows and columns wants
            String fileName = "highsocres" + size + "x" + size + ".txt";
            Console.Clear();
            string[] BoardString;
            char[][] BoardChar;
            SetBoard(size, out BoardString, out BoardChar);
            PrintBoard(BoardString, ref BoardChar); // printing the board
            RandomMines(size, ref BoardChar); // placing the cells
            int errors, scores;
            GameRun(stopwatch, BoardChar, out errors, out scores);
            Win(stopwatch, BoardChar, errors, scores, name);
            PrintHighscores(fileName, stopwatch, BoardChar, errors, size, name, scores, ref scorelist);
        }
    }
}

// add 1 foreach
// add parallel arrays 
// add tuple (?)    