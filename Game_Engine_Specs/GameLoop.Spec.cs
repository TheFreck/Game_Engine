using Game_Engine.Components;
using Game_Engine.Enums;
using Game_Engine.Logic;
using Game_Engine.Repos;
using Game_Engine.Services;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Game_Engine_Specs
{
    public class With_Logic_Setup
    {
        Establish context = () =>
        {
            gameId = Guid.NewGuid();
            gameRepoMock = new Mock<IGameRepo>();
            playerRepoMock = new Mock<IPlayerRepo>();
            playerServiceMock = new Mock<IPlayerService>();
            player1 = new Player(gameId);
            player2 = new Player(gameId);
            player3 = new Player(gameId);
            player1.Name = "player1";
            player2.Name = "player2";
            player3.Name = "player3";
            gameAssets = new GameAssets
            {
                GameId = gameId,
                Players = new List<Player>
                {
                    player1,player2, player3
                }
            };
        };

        protected static Guid gameId;
        protected static Mock<IGameRepo> gameRepoMock;
        protected static Mock<IPlayerRepo> playerRepoMock;
        protected static Mock<IPlayerService> playerServiceMock;
        protected static Player player1;
        protected static Player player2;
        protected static Player player3;
        protected static GameAssets gameAssets;
    }

    public class When_Performing_Loop_Tasks : With_Logic_Setup
    {
        Establish context = () =>
        {
            gameService = new GameService(playerServiceMock.Object, gameRepoMock.Object,gameId);
            callbackResult = false;
            callback = (a) => callbackResult = a;
        };

        Because of = () => gameService.CalculateRound(gameAssets, callback);

        It Should_Iterate_Each_Players_Count = () =>
        {
            player1.Count.ShouldEqual(1);
            player2.Count.ShouldEqual(1);
            player3.Count.ShouldEqual(1);
        };

        It Should_Call_PlayerService_To_Update_Players = () => playerServiceMock.Verify(p => p.UpdatePlayer(Moq.It.IsAny<Player>()),Times.Exactly(3));

        It Should_Callback = () => callbackResult.ShouldBeTrue();

        private static GameService gameService;
        private static bool callbackResult;
        private static Action<bool> callback;
        private static ContextCallback loopCallback;
    }

    public class When_Running_Game_Loop_For_5_Seconds : With_Logic_Setup 
    {
        Establish context = () =>
        {
            gameUpdates = new List<string>();
            loopRound = new LoopRound(gameId);


        };



        private static List<string> gameUpdates;
        private static LoopRound loopRound;
    }
}