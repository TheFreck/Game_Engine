using Game_Engine.Components;
using Game_Engine.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Engine.Services
{
    public interface IGameService {
        void StartLoop();
        void StopLoop();
        void CalculateRound(GameAssets gameAssets, Action<bool> loopCallback);
    }

    public class GameService : IGameService
    {
        public readonly Guid GameId = Guid.NewGuid();
        public readonly IPlayerService playerService;
        public readonly IGameRepo gameRepo;
        public bool IsRunning = false;
        public List<string> GameProgression;
        public ColorStates states;

        public GameService(IPlayerService playerService, IGameRepo gameRepo, Guid gameId)
        {
            this.playerService = playerService;
            this.gameRepo = gameRepo;
            this.GameProgression = new List<string>();
        }

        public void StartLoop() => IsRunning = true;

        public void StopLoop() => IsRunning = false;

        public void CalculateRound(GameAssets gameAssets, Action<bool> loopCallback)
        {
            var roundUpdate = string.Empty;
            foreach(var player in gameAssets.Players)
            {
                player.Count += 1;
                playerService.UpdatePlayer(player);
                roundUpdate += $"{player.Name}:{player.Count}; ";
            }
            GameProgression.Add(roundUpdate);
            loopCallback(true);
        }
    }
}
