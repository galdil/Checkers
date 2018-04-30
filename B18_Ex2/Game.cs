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

        internal Game(string i_FirstPlayerName, string i_SecondPlayerName, int i_SizeOfBoard, int i_NumberOfPlayers)
        {
            m_FirstPlayerName = i_FirstPlayerName;
            m_SecondPlayerName = i_SecondPlayerName;
            m_SizeOfBoard = i_SizeOfBoard;
            m_NumberOfPlayers = i_NumberOfPlayers;
        }
        internal Game(string i_FirstPlayerName, int i_SizeOfBoard, int i_NumberOfPlayers)
        {
            m_FirstPlayerName = i_FirstPlayerName;
            m_SecondPlayerName = "Computer";
            m_SizeOfBoard = i_SizeOfBoard;
            m_NumberOfPlayers = i_NumberOfPlayers;
        }
    }
}
