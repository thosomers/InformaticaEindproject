using System.Collections.Generic;
using Game.Utils;
using Game.Proxy;
using MoonSharp.Interpreter;

namespace Game.Objects
{
    
    //TODO: Add anything
    
    public class Player
    {

        public PlayerScript Script;
        public Player Enemy;
        public LuaPlayer LuaPlayer;
        public LuaEnemy LuaEnemy;

        public Base Base;
        public HashSet<Robot> Robots;
        
        

        public SourceLoader Source;

        public Player(SourceLoader loader)
        {
            this.Source = loader;
            this.Script = new PlayerScript(this);
        }
        
        public void Setup(Player enemy)
        {
            this.Enemy = enemy;
            this.Script.Setup();
        }

        public void spawnRobot(DynValue func)
        {
            var Robot = new Robot(this,func);
            this.Robots.Add(Robot);
        }
    }
}