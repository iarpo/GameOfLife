using System;
using System.Text;

namespace GameOfLife
{
    class Program
    {
        static int[,] playBoard;

        static void Main(string[] args)
        {
            bool stillPlaying = true;

            BoardSetup();
            PrintBoard();

            while (stillPlaying == true)
            {
                NextBoardState(playBoard);
                PrintBoard();
                Console.WriteLine("Are you still playing? y/n");

                string playerChoice = Console.ReadLine();

                if (playerChoice == "y")
                    stillPlaying = true;
                else
                    stillPlaying = false;
            }
        }

        private static void NextBoardState(int[,] playBoard)
        {
            //for loop through each cell
            //check neighbouring cells and tally dead and alive cells
            //update cell according to neighbours

            for (int y = 0; y < playBoard.GetLength(0); y++)
            {
                for (int x = 0; x < playBoard.GetLength(1); x++)
                {
                    int neighbours = 0;
                    int cell = playBoard[y, x];

                    try { neighbours += playBoard[(--y), (--x)]; } //SW 
                    catch (IndexOutOfRangeException e) { }; //array is out of bounds

                    try { neighbours += playBoard[(--y), (x)]; } //S
                    catch (IndexOutOfRangeException e) { }; //array is out of bounds

                    try { neighbours += playBoard[(--y), (++x)]; } //SE
                    catch (IndexOutOfRangeException e) { }; //array is out of bounds

                    try { neighbours += playBoard[(y), (--x)]; } //W
                    catch (IndexOutOfRangeException e) { }; //array is out of bounds

                    try { neighbours += playBoard[(y), (++x)]; } //E
                    catch (IndexOutOfRangeException e) { }; //array is out of bounds

                    try { neighbours += playBoard[(++y), (--x)]; } //NW
                    catch (IndexOutOfRangeException e) { }; //array is out of bounds

                    try { neighbours += playBoard[(++y), (x)]; } //N
                    catch (IndexOutOfRangeException e) { }; //array is out of bounds

                    try { neighbours += playBoard[(++y), (++x)]; } //NE
                    catch (IndexOutOfRangeException e) { }; //array is out of bounds

                    //todo there must be a better way to do this check with DRY code

                    if (cell == 0 && neighbours == 3)
                        playBoard[y, x] = 1;
                    else if (neighbours < 2 || neighbours > 3)
                        playBoard[y, x] = 0;
                    //todo rules seem right but array valued aren't updating
                }
            }
        }


        private static void BoardSetup()
        {
            DefineBoardSize();
            FillRandomBoardValues();
        }

        private static void DefineBoardSize()
        {
            Console.WriteLine("Welcome to the game! Time to choose the size of your environment. \n" +
                "What is the X dimension of your board?");
            int xBoardScale = int.Parse(Console.ReadLine());
            Console.WriteLine("What is the Y dimension of your board?");
            int yBoardScale = int.Parse(Console.ReadLine());

            playBoard = new int[yBoardScale, xBoardScale]; //Initialise playboard array

        }

        private static void FillRandomBoardValues()
        {
            Random r = new Random();

            for (int x = 0; x < playBoard.GetLength(0); x++)
            {
                for (int y = 0; y < playBoard.GetLength(1); y++)
                {
                    playBoard[x, y] = r.Next(0, 2); //assign each cell a random int 0/1
                }
            }

        }

        private static void PrintBoard()
        {
            for (int x = 0; x < playBoard.GetLength(0); x++)
            {
                StringBuilder completeLine = new StringBuilder();

                for (int y = 0; y < playBoard.GetLength(1); y++)
                {
                    completeLine.Append($"{playBoard[x, y]} ");
                }
                Console.WriteLine(completeLine);
            }
        }
    }
}
