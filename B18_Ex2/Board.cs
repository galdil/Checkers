using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B18_Ex2
{
    internal class Board
    {
        private Tool[,] m_Board;
        private int m_SizeOfBoard;
        internal Board(int i_Size)
        {
            m_SizeOfBoard = i_Size;
            m_Board = new Tool[i_Size, i_Size];
            initSquares(m_Board, m_SizeOfBoard);
        }

        public Tool[,] BoardAcceser
        {
            get { return m_Board; }
            set { m_Board = value; }
        }

        public int BoardSize
        {
            get { return m_SizeOfBoard; }
            set { m_SizeOfBoard = value; }
        }
        //insert the players tools to the board
        internal void initSquares(Tool[,] io_BoardToInit, int io_SizeOfBoard)
        {
            Game.FirstPlayer.Tools.Clear();
            Game.SecondPlayer.Tools.Clear();
            Tool toolOfTheFirstPlayer;
            Tool toolOfTheSecondPlayer;
            for (int i = 0; i < (io_SizeOfBoard / 2) - 1; i++)
            {
                for (int j = 0; j < io_SizeOfBoard; j++)
                {
                    io_BoardToInit[io_SizeOfBoard / 2, j] = null;
                    io_BoardToInit[(io_SizeOfBoard / 2) - 1, j] = null;
                    if ((i + j) % 2 == 1)
                    {
                        //adding new tool to the first player dictionary and adding it to the board 
                        toolOfTheFirstPlayer = new Tool(io_SizeOfBoard - 1 - i, io_SizeOfBoard - 1 - j, 'X');
                        Game.FirstPlayer.Tools.Add(toolOfTheFirstPlayer.StringPosition, toolOfTheFirstPlayer);
                        io_BoardToInit[io_SizeOfBoard - 1 - i, io_SizeOfBoard - 1 - j] = toolOfTheFirstPlayer;
                        //adding new tool to the second player dictionary and adding it to the board
                        toolOfTheSecondPlayer = new Tool(i, j, 'O');
                        Game.SecondPlayer.Tools.Add(toolOfTheSecondPlayer.StringPosition, toolOfTheSecondPlayer);
                        io_BoardToInit[i, j] = toolOfTheSecondPlayer;
                    }

                    else
                    {
                        io_BoardToInit[i, j] = null;
                    }
                }
            }
        }

        private void PrintBoard()
        {
            StringBuilder boardToPrint = rowOfLetters(m_SizeOfBoard);
            boardToPrint.Append(genarateEqualsLine(m_SizeOfBoard));
            Char letter = 'a';

            Ex02.ConsoleUtils.Screen.Clear();
            for (int j = 0; j < m_SizeOfBoard; j++)
            {
                string rowToAppend = letter++ + "|";
                boardToPrint.Append(rowToAppend);
                for (int i = 0; i < m_SizeOfBoard; i++)
                {
                    char symbol = (m_Board[j, i] == null) ? '\0' : m_Board[j, i].Symbol;
                    string currentRow = " " + symbol + " |";
                    boardToPrint.Append(currentRow);
                }

                boardToPrint.AppendLine();
                boardToPrint.Append(genarateEqualsLine(m_SizeOfBoard));
            }

            Console.WriteLine(boardToPrint.ToString());
        }
        //print the beginning board
        internal void printFirstBoard()
        {
            PrintBoard();
            Console.WriteLine(Game.CurrentPlayer.Name + "'s turn:\n");
        }
        //print the board after a move
        internal void printGameBoard(string i_LastMove, bool anotherTurnForCurrent)
        {
            Player prevPlayer;

            if (anotherTurnForCurrent == true)
            {
                prevPlayer = Game.CurrentPlayer;
            }

            else
            {
                prevPlayer = (Game.CurrentPlayer.Name == Game.FirstPlayer.Name) ? Game.SecondPlayer : Game.FirstPlayer;
            }

            PrintBoard();
            Console.WriteLine(string.Format(@"{0}'s move was ({1}): {2}", prevPlayer.Name, indexToSymbol(i_LastMove), i_LastMove));
            Console.WriteLine(string.Format(@"{0}'s turn ({1}):", Game.CurrentPlayer.Name, Game.CurrentPlayer.PlayerSymbol));
        }
        
        //return the symbol in a specific square
        private char indexToSymbol(string i_Position)
        {
            char fromCol = i_Position[3];
            char fromRow = i_Position[4];
            int indexFromCol = fromCol - 'A';
            int indexFromRow = fromRow - 'a';

            return m_Board[indexFromRow, indexFromCol].Symbol;

        }

        //creating the first line of the board
        private StringBuilder rowOfLetters(int i_RowSize)
        {
            StringBuilder rowOfLetters = new StringBuilder("   ");
            Char letter = 'A';
            for (int i = 0; i < i_RowSize; i++)
            {
                rowOfLetters.Append(letter++);
                rowOfLetters.Append("   ");
            }

            rowOfLetters.Append("\n");
            return rowOfLetters;
        }
        //genarating a line of equal sign respectively to the size
        private string genarateEqualsLine(int i_RowSize)
        {
            StringBuilder lineOfEquals = new StringBuilder(" =");
            string equalSign = "====";
            for (int i = 0; i < i_RowSize; i++)
            {
                lineOfEquals.Append(equalSign);
            }

            return (lineOfEquals.ToString() + "\n");
        }
    }
}
