using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B18_Ex2
{
    class Game
    {
        private string m_FirstPlayerName;
        private string m_SecondPlayerName;
        private int m_SizeOfBoard = 0;
        private int m_NumberOfPlayers = 0;
        private Char[,] m_Board;

        internal Game(string i_FirstPlayerName, string i_SecondPlayerName, int i_SizeOfBoard, int i_NumberOfPlayers)
        {
            m_FirstPlayerName = i_FirstPlayerName;
            m_SecondPlayerName = i_SecondPlayerName;
            m_SizeOfBoard = i_SizeOfBoard;
            m_NumberOfPlayers = i_NumberOfPlayers;
            m_Board = new Char[i_SizeOfBoard, i_SizeOfBoard];
            initSquares(m_Board, i_SizeOfBoard);
        }

        private void initSquares(Char[,] io_BoardToInit, int io_SizeOfBoard)
        {
            for (int i = 0; i < (io_SizeOfBoard / 2) - 1; i++)
            {
                for (int j = 0; j < io_SizeOfBoard; j++)
                {
                    if ((i + j) % 2 == 1)
                    {
                        io_BoardToInit[i, j] = 'O';
                        io_BoardToInit[io_SizeOfBoard - 1 - i, io_SizeOfBoard - 1 - j] = 'X';
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

