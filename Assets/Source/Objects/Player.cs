using System.Collections.Generic;
using Game.Utils;
using Game.Proxy;
using MoonSharp.Interpreter;

namespace Game.Objects
{
    
    //TODO: Add anything
    
    public class Player
    {

        /// <summary>
        /// The <see cref="PlayerScript"/> that is linked to this <see cref="Player"/>. This Script runs all the Lua code of this Player.
        /// </summary>
        public PlayerScript Script;
        
        
        
        /// <summary>
        /// The other <see cref="Player"/> that is in the same game. See: <see cref="Setup"/>
        /// </summary>
        public Player Enemy;
        
        
        /// <summary>
        /// The Lua-Proxy of this player
        /// </summary>
        public LuaPlayer LuaPlayer;
        
        /// <summary>
        /// The <see cref="Base"/> object which this player controls. Instantiated by <see cref="PlayerScript.Setup"/>.
        /// </summary>
        public Base Base;
        
        
        /// <summary>
        /// The list of <see cref="Robot"/> Objects controlled by this <see cref="Player"/>. This list is added to by <see cref="spawnRobot"/>.
        /// </summary>
        public HashSet<Robot> Robots = new HashSet<Robot>();
        
        

        /// <summary>
        /// The <see cref="SourceLoader"/> provides the Lua source as a string after calling: <see cref="SourceLoader.getLuaSource"/>
        /// </summary>
        public SourceLoader Source;

        public Player(SourceLoader loader)
        {
            this.Source = loader;
            this.Script = new PlayerScript(this);
        }
        
        /// <summary>
        /// A 2nd initializer for the Player object. This is run after the consytructors of this and the enemy player. Sets the Enemy for each player and sets  up the script. (<see cref="PlayerScript.Setup"/>).
        /// </summary>
        /// <param name="enemy">The other <see cref="Player"/> object in the game.</param>
        public void Setup(Player enemy)
        {
            this.Enemy = enemy;
            this.Script.Setup();
        }

        /// <summary>
        /// Spawns a new Robot for this player. Does NOT check requirements (See <see cref="LuaBase.spawnRobot"/>)
        /// </summary>
        /// <param name="func">The Function controlling the new Robot (<see cref="PlayerObject.Coroutine"/>)</param>
        public void spawnRobot(DynValue func)
        {
            var robot = new Robot(this,func);
            this.Robots.Add(robot);
        }
    }
}