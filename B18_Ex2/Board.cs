using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B18_Ex2
{
    public class Board
    {
        public static void Main()
        {
            Board a = new Board(6);
            a.PrintBoard(a.m_Board, 6, "Gal");
            Console.ReadLine();
        }
        private Char[,] m_Board;
        private int m_Size = 0;
        internal Board(int i_Size)
        {
            m_Size = i_Size;
            m_Board = new Char[i_Size, i_Size];
            InitSquares(m_Board, m_Size);
        }

        public static void InitSquares(Char[,] arr, int size)
        {
            for (int i = 0; i < (size / 2) - 1; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if ((i + j) % 2 == 1)
                    {
                        arr[i, j] = 'O';
                        arr[size - 1 - i, size - 1 - j] = 'X';
                    }
                }
            }
        }

        public void PrintBoard(Char[,] board, int boardSize, String currentPlayer)
        {
            StringBuilder boardToPrint = columnsLetters(boardSize);
            boardToPrint.Append(genarateEqualsLine(boardSize));
            Char letter = 'a';
            for (int j = 0; j < boardSize; j++)
            {
                String rowToAppend = letter++ + "|";
                boardToPrint.Append(rowToAppend);
                for (int i = 0; i < boardSize; i++)
                {
                    String currentRow = " " + board[j, i] + " |";
                    boardToPrint.Append(currentRow);

                }

                boardToPrint.AppendLine();
                boardToPrint.Append(genarateEqualsLine(boardSize));
            }

            boardToPrint.Append(currentPlayer + "'s turn:");
            Console.WriteLine(boardToPrint.ToString());
        }

        //creating the first line of the board
        private StringBuilder columnsLetters(int rowSize)
        {
            StringBuilder BoardColumnsLetters = new StringBuilder("   ");
            Char letter = 'A';
            for (int i = 0; i < rowSize; i++)
            {
                BoardColumnsLetters.Append(letter++);
                BoardColumnsLetters.Append("   ");
            }
            BoardColumnsLetters.Append("\n");
            return BoardColumnsLetters;
        }
        //genarating a line of equal sign respectively to the size
        private String genarateEqualsLine(int rowSize)
        {
            StringBuilder equalsLine = new StringBuilder(" =");
            String equalSign = "====";
            for (int i = 0; i < rowSize; i++)
            {
                equalsLine.Append(equalSign);
            }
            return (equalsLine.ToString() + "\n");
        }
    }
}
