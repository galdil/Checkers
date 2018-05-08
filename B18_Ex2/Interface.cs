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
                Console.WriteLine("Please enter 1 for Player VS Computer. Otherwise enter 2 for a game with two players.");
                tempNumOfPlayers = Console.ReadLine();
                if (checkIfValidPlayerDecision(tempNumOfPlayers) == true)
                {
                    numberOfPlayers = Int32.Parse(tempNumOfPlayers);
                    goodPlayerDecision = true;
                }
            }

            string SecondPlayerName = "Computer";
            if (numberOfPlayers == 2)
            //if number of players is two participants - get second player name
            {
                goodName = false;
                while (goodName == false)
                {
                    Console.WriteLine("Hey, please enter second participant name. No spaces and no max 20 characters please :)");
                    SecondPlayerName = Console.ReadLine();
                    if (checkIfValidName(SecondPlayerName) == true)
                    {
                        goodName = true;
                    }
                }
            }
            //construct a new checkers game with compatible name of two players or player and computer
            Game checkers = new Game(firstPlayerName, SecondPlayerName, sizeOfBoard, numberOfPlayers);
            string winner;
            string RespondForAnotherGame = "yes";
            bool rematch = true;
            bool computerTurn = false;
            while (rematch == true)
            {
                checkers.RestartNewGame();
                while (checkers.GameEnded == false)
                {
                    string playerWantedMove;
                    bool endOfMove = false;
                    while (endOfMove == false)
                    {
                        //case of 2 players game or player and computer while its player's turn
                        if ((numberOfPlayers == 2) || ((numberOfPlayers == 1) && (Game.CurrentPlayer.Name!="Computer")))
                        {
                            Console.WriteLine("Please enter a valid move in the format of COLrow>COLrow or type Q to quit and lose the game.");
                            playerWantedMove = Console.ReadLine();
                            if (checkAvailableMoveFormat(playerWantedMove) == true)
                            {
                                if (Game.CheckAvailableMove(playerWantedMove) == true)
                                {
                                    checkers.Move(playerWantedMove);
                                    endOfMove = true;
                                    if (checkers.StatusOfTheGame == Game.GameStatus.WIN)
                                    {
                                        winner = checkers.WhoWins();
                                        Console.WriteLine(winner + " has won!!");
                                    }
                                }

                                else
                                {
                                    Console.WriteLine("Not a valid move! Please try again");
                                }
                            }

                            else if (playerWantedMove == "Q" && checkers.IsLoserPlayerWhenQuit(Game.CurrentPlayer) == true)
                            {
                                endOfMove = true;
                                checkers.PlayerSurrender();
                            }
                        }
                        //computer's play if needed
                        if ((numberOfPlayers == 1) && (Game.CurrentPlayer.Name == "Computer") && (checkers.GameEnded == false))
                        {
                            checkers.ComputerMove();
                            if (checkers.StatusOfTheGame == Game.GameStatus.WIN)
                            {
                                winner = checkers.WhoWins();
                                Console.WriteLine(winner + " has won!!");
                            }
                        }
                    }
                }
                //game has ended
                tournamentScorePrint();
                bool ValidAnswerPlayAnotherGame = false;
                while (ValidAnswerPlayAnotherGame == false)
                {
                    Console.WriteLine("Would you want to play another round? (yes/no)");
                    RespondForAnotherGame = Console.ReadLine();
                    if (RespondForAnotherGame == "yes")
                    {
                        ValidAnswerPlayAnotherGame = true;
                    }
                    if (RespondForAnotherGame == "no")
                    {
                        rematch = false;
                        ValidAnswerPlayAnotherGame = true;
                    }
                }
            }
            Console.WriteLine("Thank You. Bye Bye :) ");
        }


        //private methods to check validitaion of players information

        private bool checkIfValidName(string i_playerNameStr)
        {
            if ((i_playerNameStr.Length <= 20) && (i_playerNameStr.Contains(" ") == false) && (i_playerNameStr.Length > 0))
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

        //check whether the format of the move is legal
        private bool checkAvailableMoveFormat(String i_move)
        {
            bool validFormat = false;
            if ((i_move.Length == 5) && (i_move[0] >= 'A' && i_move[0] <= 'Z') && (i_move[1] >= 'a' && i_move[1] <= 'z') && (i_move[2] == '>') && (i_move[3] >= 'A' && i_move[3] <= 'Z') && (i_move[4] >= 'a' && i_move[4] <= 'z'))
            {
                validFormat = true;
            }

            return validFormat;
        }

        private void tournamentScorePrint()
        {
            Console.WriteLine(string.Format(@"The tournament score is: {0} : {1}. ( {2} Vs. {3} ) ", Game.FirstPlayer.TournamentScore, Game.SecondPlayer.TournamentScore, Game.FirstPlayer.Name, Game.SecondPlayer.Name));
        }
    }
}