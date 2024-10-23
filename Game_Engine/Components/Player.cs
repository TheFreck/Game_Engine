using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Engine.Components
{
    public class Player
    {
        public Guid GameId;
        public string Name;
        public int Count;

        public Player(Guid gameId)
        {
            GameId = gameId;
        }
    }
}
