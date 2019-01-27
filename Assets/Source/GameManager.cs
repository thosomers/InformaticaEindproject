using Game.Objects;
using Game.Utils;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Game
{
    public class GameManager
    {
        private Player Player1;
        private Player Player2;
        

        public GameManager(SourceLoader l1, SourceLoader l2)
        {
            Player1 = new Player(l1);
            Player2 = new Player(l2);
            
        }


        public void Setup()
        {
            UserData.RegisterAssembly();
            Player1.Setup(Player2);
            Player2.Setup(Player1);
        }
        
        public void runTick()
        {
            Player1.Script.runTick();
            Player2.Script.runTick();
        }
        
    }
}