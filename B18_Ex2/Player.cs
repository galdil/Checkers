using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B18_Ex2
{
    internal class Player
    {
        private string m_Name;
        private Dictionary<string, Tool> m_Tools;
        private int m_MovingDirection;
        private int m_NumOfTools;
        private int m_Score;
        private bool m_JustCaptured;
        private char m_PlayerSymbol;
        private char m_KingSymbol;

        internal Player(string i_Name, int i_BoardSize, char i_PlayerSymbol)
        {
            m_Tools = new Dictionary<string, Tool>();
            m_Name = i_Name;
            m_Score = 0;
            m_JustCaptured = false;
            m_NumOfTools = initNumOfTools(i_BoardSize);
            m_PlayerSymbol = i_PlayerSymbol;

            if (i_PlayerSymbol == 'O')
            {
                m_MovingDirection = 1;
                m_KingSymbol = 'U';
            }

            else
            {
                m_MovingDirection = -1;
                m_KingSymbol = 'K';
            }
        }

        internal static int initNumOfTools(int i_BoardSize)
        {
            int numOfTools = 0;
            switch (i_BoardSize)
            {
                case 6:
                    numOfTools = 6;
                    break;
                case 8:
                    numOfTools = 12;
                    break;
                case 10:
                    numOfTools = 20;
                    break;
            }

            return numOfTools;
        }

        public Dictionary<string, Tool> Tools
        {
            get { return m_Tools; }
            set { m_Tools = value; }
        }

        public char KingSymbol
        {
            get { return m_KingSymbol; }
            set { m_KingSymbol = value; }
        }

        public char PlayerSymbol
        {
            get { return m_PlayerSymbol; }
            set { m_PlayerSymbol = value; }
        }
        public bool JustCaptured
        {
            get { return m_JustCaptured; }
            set { m_JustCaptured = value; }
        }
        public int MovingDirection
        {
            get { return m_MovingDirection; }
        }
        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        public int Score
        {
            get
            {
                int score = 0;
                foreach (var item in this.Tools)
                {
                    score += item.Value.Value;
                }
                return score;
            }
            set { m_Score = value; }
        }

        public int ToolsCounter
        {
            get { return m_NumOfTools; }
            set { m_NumOfTools = value; }
        }

        internal bool IsPlayerAbleToMove()
        {
            bool ableToMove = false;

            foreach (var item in Tools)
            {
                if (item.Value.IsAbleToMove() || item.Value.ToolIsAbleToCapture())
                {
                    ableToMove = true;
                }
            }
            return ableToMove;
        }

        internal bool IsPlayerAbleToCapture()
        {
            bool ableToMove = false;

            foreach (var item in Tools)
            {
                if (item.Value.ToolIsAbleToCapture())
                {
                    ableToMove = true;
                }
            }
            return ableToMove;
        }
    }
}
