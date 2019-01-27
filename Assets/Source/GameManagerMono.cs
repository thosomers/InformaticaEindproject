using Game.Utils;
using UnityEngine;

namespace Game
{
    public class GameManagerMono : MonoBehaviour
    {
        public GameManager Game;

        
        public MonoSource player1;
        public MonoSource player2;
        
        

        void Start()
        {
            Game = new GameManager(player1.toLoader(),player2.toLoader());
            Game.Setup();
        }

        private void Update()
        {
            Game.runTick();
        }
    }
}