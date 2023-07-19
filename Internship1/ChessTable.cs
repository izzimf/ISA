using System;

class Program
{
    static int boardSize = 8;
    static int[,] board = new int[boardSize, boardSize];
    static int count = 0;

    static void Main(string[] args)
    {
        CalculateFigures(0, 0, 0);

        Console.WriteLine("Maximum number of shapes: " + count);
        PrintBoard();
    }

    static void CalculateFigures(int row, int col, int figuresPlaced)
    {
        if (row >= boardSize)
        {
            count = Math.Max(count, figuresPlaced);
            return;
        }

        if (col >= boardSize)
        {
            CalculateFigures(row + 1, 0, figuresPlaced);
            return;
        }

        if (IsValidMove(row, col))
        {
            board[row, col] = 1;
            CalculateFigures(row, col + 1, figuresPlaced + 1);
            board[row, col] = 0;
        }

        CalculateFigures(row, col + 1, figuresPlaced);
    }

    static bool IsValidMove(int row, int col)
    {
        if (row - 2 >= 0 && col - 1 >= 0 && board[row - 2, col - 1] == 1)
            return false;

        if (row - 2 >= 0 && col + 1 < boardSize && board[row - 2, col + 1] == 1)
            return false;

        if (row - 1 >= 0 && col - 2 >= 0 && board[row - 1, col - 2] == 1)
            return false;

        if (row - 1 >= 0 && col + 2 < boardSize && board[row - 1, col + 2] == 1)
            return false;

        if (row + 1 < boardSize && col - 2 >= 0 && board[row + 1, col - 2] == 1)
            return false;

        if (row + 1 < boardSize && col + 2 < boardSize && board[row + 1, col + 2] == 1)
            return false;

        if (row + 2 < boardSize && col - 1 >= 0 && board[row + 2, col - 1] == 1)
            return false;

        if (row + 2 < boardSize && col + 1 < boardSize && board[row + 2, col + 1] == 1)
            return false;

        return true;
    }

    static void PrintBoard()
    {
        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                if (board[i, j] == 1)
                    Console.Write("K ");
                else
                    Console.Write("__");
            }
            Console.WriteLine();
        }
    }
}