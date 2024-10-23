using Game_Engine.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Engine.Services
{
    public class GameLoop
    {
        public bool IsReady;
        public bool IsRunning;
        public LoopRound round;
        public IGameService gameService;
        public GameAssets assets;

        public GameLoop(IGameService gameService, GameAssets assets)
        {
            this.gameService = gameService;
            this.assets = assets;
        }

        public void Loop()
        {
            Action<bool> Callback = (bool ready) =>
            {
                IsReady = ready;
            };

            do
            {
                IsReady = false;
                gameService.CalculateRound(assets, Callback);
                if (!IsReady) Thread.Sleep(10);
            } while (IsRunning);

        }

        //public LoopRound RunLoop(LoopRound round, GameService service, GameAssets assets)
        //{
        //    if (IsRunning)
        //    {
        //        var threads = new List<Thread>();
        //        threads.Add(new Thread(Loop));

        //    }
        //}
    }
}
