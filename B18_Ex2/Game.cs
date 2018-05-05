using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B18_Ex2
{
    internal class Game
    {
        public enum GameStatus { WIN, TIE, NONE };

        private static Player m_FirstPlayer;
        private static Player m_SecondPlayer;
        private static Player m_CurrentPlayer;
        private static int m_NumberOfPlayers;
        private static Board m_Board;
        private static bool m_GameEnded = false;
        private static string m_LastMove;
        public GameStatus m_StatusOfTheGame;

        internal Game(string i_FirstPlayerName, string i_SecondPlayerName, int i_SizeOfBoard, int i_NumberOfPlayers)
        {
            m_StatusOfTheGame = GameStatus.NONE;
            m_FirstPlayer = new Player(i_FirstPlayerName, i_SizeOfBoard, 'X');
            m_SecondPlayer = new Player(i_SecondPlayerName, i_SizeOfBoard, 'O');
            m_CurrentPlayer = m_FirstPlayer;
            m_NumberOfPlayers = i_NumberOfPlayers;
            m_Board = new Board(i_SizeOfBoard);
            m_Board.printFirstBoard();
        }

        internal void RestartNewGame()
        {
            GameEnded = false;
            m_Board.initSquares(m_Board.BoardAcceser, m_Board.BoardSize);
            FirstPlayer.ToolsCounter = Player.initNumOfTools(m_Board.BoardSize);
            SecondPlayer.ToolsCounter = Player.initNumOfTools(m_Board.BoardSize);
        }
        public static Player FirstPlayer
        {
            get { return m_FirstPlayer; }
        }
        public static Player SecondPlayer
        {
            get { return m_SecondPlayer; }
        }
        public static Player CurrentPlayer
        {
            get { return m_CurrentPlayer; }
        }
        public static bool GameEnded
        {
            get { return m_GameEnded; }
            set { m_GameEnded = value; }
        }

        public static Board Board
        {
            get { return m_Board; }
        }

        public static bool CheckAvailableMove(string i_Move)
        {
            bool validMove = false;
            char fromCol = i_Move[0];
            char fromRow = i_Move[1];
            char toCol = i_Move[3];
            char toRow = i_Move[4];
            int indexFromCol = fromCol - 'A';
            int indexFromRow = fromRow - 'a';
            int indexToCol = toCol - 'A';
            int indexToRow = toRow - 'a';

            if (CurrentPlayer.JustCaptured == true)
            {
                validMove = false;
                bool captureWithSameTool = (i_Move.Substring(0, 2) == m_LastMove);
                bool ableToCapture = IsAbleToCapture(indexToCol, indexToRow, indexFromCol, indexFromRow);

                if(captureWithSameTool && ableToCapture)
                {
                    validMove = true;
                }

                return validMove;
            }

            else if (CurrentPlayer.IsPlayerAbleToCapture())
            {
                validMove = IsAbleToCapture(indexToCol, indexToRow, indexFromCol, indexFromRow);
            }

            else
            {
                if (NotOutOfBoundsChecker(indexToCol, indexToRow)
                && checkFreeSquares(indexToCol, indexToRow)
                && checkIfThisMySquares(indexFromCol, indexFromRow)
                && (isLegalDest(indexToCol, indexToRow, indexFromCol, indexFromRow))
                || IsAbleToCapture(indexToCol, indexToRow, indexFromCol, indexFromRow))
                {
                    validMove = true;
                }
            }
            return validMove;
        }
        //checks if the dest square is not out of bounds
        internal static bool NotOutOfBoundsChecker(int i_ToCol, int i_ToRow)
        {
            bool validMove = true;
            if ((i_ToCol >= m_Board.BoardSize) || (i_ToRow >= m_Board.BoardSize) || (i_ToRow < 0) || (i_ToCol < 0))
            {
                validMove = false;
            }
            return validMove;
        }
        //checks if the destination square is free
        private static bool checkFreeSquares(int i_ToCol, int i_ToRow)
        {
            bool validMove = true;
            if (m_Board.BoardAcceser[i_ToRow, i_ToCol] != null)
            {
                validMove = false;
            }
            return validMove;
        }

        //check whether the requested move is done from the correct player position
        private static bool checkIfThisMySquares(int i_FromCol, int i_FromRow)
        {
            bool validMove = false;
            if (m_CurrentPlayer.Tools.ContainsKey(Tool.ConvertToStringPosition(i_FromRow, i_FromCol)))
            {
                validMove = true;
            }
            return validMove;
        }
        //checks if a capture move is legal
        internal static bool IsAbleToCapture(int i_ToCol, int i_ToRow, int i_FromCol, int i_FromRow)
        {
            bool validMove = false;
            Player prevPlayer = (m_CurrentPlayer.Name == m_FirstPlayer.Name) ? m_SecondPlayer : m_FirstPlayer;
            bool isOpponentToolBetween = prevPlayer.Tools.ContainsKey(Tool.ConvertToStringPosition(((i_ToRow + i_FromRow) / 2), (i_ToCol + i_FromCol) / 2));

            if ((i_ToRow - i_FromRow == m_CurrentPlayer.MovingDirection * 2) && isOpponentToolBetween
                && checkFreeSquares(i_ToCol, i_ToRow))
            {
                if ((Math.Abs(i_FromCol - i_ToCol) == 2))
                {
                    validMove = true;
                }
            }
            return validMove;
        }
        //checks if the destination square is in a legal place and free
        private static bool isLegalDest(int i_ToCol, int i_ToRow, int i_FromCol, int i_FromRow)
        {
            bool validMove = false;
            //if the next regular move is legal
            if ((i_ToRow - i_FromRow == m_CurrentPlayer.MovingDirection) && checkFreeSquares(i_ToCol, i_ToRow))
            {
                if ((Math.Abs(i_FromCol - i_ToCol) == 1))
                {
                    validMove = true;
                }
            }

            return validMove;
        }


        internal void Move(string i_Move)
        {
            char fromCol = i_Move[0];
            char fromRow = i_Move[1];
            char toCol = i_Move[3];
            char toRow = i_Move[4];
            int indexFromCol = fromCol - 'A';
            int indexFromRow = fromRow - 'a';
            int indexToCol = toCol - 'A';
            int indexToRow = toRow - 'a';
            bool aimToCapture = Math.Abs(indexFromRow - indexToRow) == 2;
            //changing the board after a move
            Tool toolToMove = m_Board.BoardAcceser[indexFromRow, indexFromCol];
            m_Board.BoardAcceser[indexFromRow, indexFromCol] = null;
            m_Board.BoardAcceser[indexToRow, indexToCol] = toolToMove;
            toolToMove.SetPositions(indexToRow, indexToCol, Tool.ConvertToStringPosition(indexToRow, indexToCol));
            //modifies the dictionary
            CurrentPlayer.Tools.Remove(Tool.ConvertToStringPosition(indexFromRow, indexFromCol));
            CurrentPlayer.Tools.Add(Tool.ConvertToStringPosition(indexToRow, indexToCol), toolToMove);
           
            if (aimToCapture)
            {
                string opponentToolPosition = Tool.ConvertToStringPosition(((indexToRow + indexFromRow) / 2), (indexToCol + indexFromCol) / 2);
                m_Board.BoardAcceser[((indexToRow + indexFromRow) / 2), (indexToCol + indexFromCol) / 2] = null;
                Player prevPlayer = (CurrentPlayer.Name == FirstPlayer.Name) ? SecondPlayer : FirstPlayer;
                prevPlayer.Tools.Remove(opponentToolPosition);
            }
            //checks if the player can capture another tool
            if (aimToCapture
                && checkIfNextCaptureIsLegal(indexToCol, indexToRow, indexFromCol, indexFromRow))
            {
                m_LastMove = i_Move.Substring(3,2);
                Console.WriteLine(m_LastMove);
                CurrentPlayer.JustCaptured = true;
                m_Board.printGameBoard(i_Move, true);
            }
            else
            {
                if (m_CurrentPlayer.Equals(m_FirstPlayer))
                {
                    m_CurrentPlayer = m_SecondPlayer;
                }

                else
                {
                    m_CurrentPlayer = m_FirstPlayer;
                }

                CurrentPlayer.JustCaptured = false;
                m_Board.printGameBoard(i_Move, false);
            }
        }

        //checks if there is an option to capture another tool after one has been capture.
        private static bool checkIfNextCaptureIsLegal(int i_ToCol, int i_ToRow, int i_FromCol, int i_FromRow)
        {
            bool legalCapture = false;
            int columnOfFirstOptionToCapture = i_ToCol + 2;
            int rowOfOptionsToCapture = i_ToRow + (2 * m_CurrentPlayer.MovingDirection);
            int columnOfSecondOptionToCapture = i_ToCol - 2;

            //checks if the player can capture another tool
            if (NotOutOfBoundsChecker(columnOfFirstOptionToCapture, rowOfOptionsToCapture)
                && (IsAbleToCapture(columnOfFirstOptionToCapture, rowOfOptionsToCapture, i_ToCol, i_ToRow))

                || NotOutOfBoundsChecker(columnOfSecondOptionToCapture, rowOfOptionsToCapture)
                && (IsAbleToCapture(columnOfSecondOptionToCapture, rowOfOptionsToCapture, i_ToCol, i_ToRow)))
            {
                legalCapture = true;
            }

            return legalCapture;
        }
        internal bool IsLoserPlayer(Player i_PlayerToCheck)
        {
            bool isLosing = false;
            if (i_PlayerToCheck == FirstPlayer)
            {
                isLosing = (FirstPlayer.Score < SecondPlayer.Score) ? true : false;
            }
            if (i_PlayerToCheck == SecondPlayer)
            {
                isLosing = (SecondPlayer.Score < FirstPlayer.Score) ? true : false;
            }
            return isLosing;
        }

        internal string WhoWins()
        {
            string winner = "";
            int margin = Math.Abs(FirstPlayer.Score - SecondPlayer.Score);
            if (FirstPlayer.Score > SecondPlayer.Score)
            {
                winner = FirstPlayer.Name;
                FirstPlayer.Score += margin;
            }
            if (SecondPlayer.Score > FirstPlayer.Score)
            {
                winner = SecondPlayer.Name;
                SecondPlayer.Score += margin;
            }
            return winner;
        }
    }
}