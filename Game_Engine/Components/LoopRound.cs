using Game_Engine.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Engine.Components
{
    public class LoopRound
    {
        public readonly Guid GameId;
        public List<Action> GameActions;
        public List<Action> PlayerActions;
        public List<string> ExecutionOrder;

        public LoopRound(Guid gameId)
        {
            GameId = gameId;
            GameActions = new List<Action>();
            PlayerActions = new List<Action>();
            ExecutionOrder = new List<string>();
        }

        public void ExecuteActions()
        {
            do
            {
                var action = GameActions.FirstOrDefault();
                action.Invoke();
                GameActions.Remove(action);
            } while (GameActions.Count > 0);
            do
            {
                var action = PlayerActions.FirstOrDefault();
                action.Invoke();
                PlayerActions.Remove(action);
            } while (PlayerActions.Count > 0);
            return;
        }

        public void QueueAction(Action newAction, ActionTypes actionType)
        {
            if (actionType == ActionTypes.Game)
            {
                GameActions.Add(newAction);
            }
            if (actionType == ActionTypes.Player)
            {
                PlayerActions.Add(newAction);
            }
        }
    }
}
