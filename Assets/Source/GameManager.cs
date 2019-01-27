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


        public void Setup(MonoBehaviour mono)
        {
            UserData.RegisterAssembly();
            Player1.Setup(Player2,mono);
            Player2.Setup(Player1,mono);
        }
        
        public void Update()
        {
            if (!(Player1.Script.isDone() && Player2.Script.isDone()))
            {
                return;
            }
            
            
            Player1.Script.startTick();
            Player2.Script.startTick();
        }
        
    }
}