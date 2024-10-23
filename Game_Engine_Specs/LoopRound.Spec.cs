using Game_Engine.Components;
using Game_Engine.Enums;
using Game_Engine.Logic;
using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Engine_Specs
{
    public class With_Loop_Setup
    {
        Establish context = () =>
        {
            gameId = Guid.NewGuid();
        };



        protected static Guid gameId;
    }
    public class When_Adding_Actions_To_LoopRound : With_Loop_Setup
    {
        Establish context = () =>
        {
            loopRound = new LoopRound(gameId);
            newAction = () => Console.WriteLine("Round");
            actionType = ActionTypes.Game;
        };

        Because of = () => loopRound.QueueAction(newAction, actionType);

        It Should_Add_Action_To_Correct_Queue = () => loopRound.GameActions.ShouldContainOnly(newAction);
        It Should_Not_Add_Action_To_Wrong_Queue = () => loopRound.PlayerActions.ShouldNotContain(newAction);

        private static Guid gameId;
        private static LoopRound loopRound;
        private static Action newAction;
        private static ActionTypes actionType;
    }

    public class When_Executing_Actions : With_Loop_Setup
    {
        Establish context = () =>
        {

            loopRound = new LoopRound(gameId);
            action1 = () => loopRound.ExecutionOrder.Add("First");
            action2 = () => loopRound.ExecutionOrder.Add("Second");
            action3 = () => loopRound.ExecutionOrder.Add("Third");
            loopRound.QueueAction(action1, ActionTypes.Game);
            loopRound.QueueAction(action2, ActionTypes.Player);
            loopRound.QueueAction(action3, ActionTypes.Game);
        };

        Because of = () => loopRound.ExecuteActions();

        It Should_Perform_Game_Actions_First = () =>
        {
            loopRound.ExecutionOrder[0].ShouldEqual("First");
            loopRound.ExecutionOrder[1].ShouldEqual("Third");
            loopRound.ExecutionOrder[2].ShouldEqual("Second");
        };
        private static LoopRound loopRound;
        private static Action action1;
        private static Action action2;
        private static Action action3;
    }
}
