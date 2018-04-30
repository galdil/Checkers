using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B18_Ex2
{
    class Interface
    {
        internal void Start()
        {
            //getting and checking valid name for first player
            bool goodName = false;
            string firstPlayerName = "default";
            while (goodName == false)
            {
                Console.WriteLine("Hey, please enter your name. No spaces and no max 20 characters please :)");
                firstPlayerName = Console.ReadLine();
                if (checkIfValidName(firstPlayerName) == true)
                {
                    goodName = true;
                }
            }
            //getting and checking valid size of board
            bool goodBoardSize = false;
            string tempSize;
            //initial sizeofboard
            int sizeOfBoard = 0;
            while (goodBoardSize == false)
            {
                Console.WriteLine("please enter a valid size for the border. Either 6, 8 or 10:");
                tempSize = Console.ReadLine();
                if (checkIfValidBoardSize(tempSize) == true)
                {
                    sizeOfBoard = Int32.Parse(tempSize);
                    goodBoardSize = true;
                }
            }
            //check how many players are playing
            bool goodPlayerDecision = false;
            string tempNumOfPlayers;
            int numberOfPlayers = 1;
            while (goodPlayerDecision == false)
            {
                Console.WriteLine("please enter 2 for two participates or 1 for a player against computer:");
                tempNumOfPlayers = Console.ReadLine();
                if (checkIfValidPlayerDecision(tempNumOfPlayers) == true)
                {
                    numberOfPlayers = Int32.Parse(tempNumOfPlayers);
                    goodPlayerDecision = true;
                }
            }
            if (numberOfPlayers == 1)
            //if the player is playing against the computer
            {
                Game checkersAgainstComputer = new Game(firstPlayerName, sizeOfBoard, numberOfPlayers);
            }
            else
            //if number of players is two participants
            {
                goodName = false;
                string SecondPlayerName = "default";
                while (goodName == false)
                {
                    Console.WriteLine("Hey, please enter second participant name. No spaces and no max 20 characters please :)");
                    SecondPlayerName = Console.ReadLine();
                    if (checkIfValidName(SecondPlayerName) == true)
                    {
                        goodName = true;
                    }
                }
                Game twoPlayersCheckers = new Game(firstPlayerName, SecondPlayerName, sizeOfBoard, numberOfPlayers);

            }
        }



        private bool checkIfValidName(string i_playerNameStr)
        {
            if ((i_playerNameStr.Length <= 20) && (i_playerNameStr.Contains(" ") == false))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool checkIfValidBoardSize(string i_tempBoardSize)
        {
            int sizeOfBoard;
            bool goodBoardSize = Int32.TryParse(i_tempBoardSize, out sizeOfBoard);
            //succesful convertion of number into integer
            if (goodBoardSize)
            {
                //check if valid size of board
                if (sizeOfBoard == 6 || sizeOfBoard == 8 || sizeOfBoard == 10)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            // not a number
            else
            {
                return false;
            }
        }

        private bool checkIfValidPlayerDecision(string i_tempNumOfPlayers)
        {
            int playerDecision;
            bool goodDecision = Int32.TryParse(i_tempNumOfPlayers, out playerDecision);
            //check if input is number and is player against computer (etc. option 1) or two players (etc. option 2)
            if (goodDecision == true)
            {
                if (playerDecision == 2 || playerDecision == 1)
                {
                    return true;
                }
                else
                //invalid input
                {
                    return false;
                }
            }
            else
            //if couldn't convert into number
            {
                return false;
            }
        }
    }
}

