using System.Collections.Generic;
using MoonSharp.Interpreter;
using Game.Objects;
using System.Linq;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace Game.Utils
{
    public class PlayerScript : Script
    {
        public readonly Player player;
        
        public PlayerScript(Player player) : base(CoreModules.Preset_HardSandbox)
        {
            this.player = player;
        }

        /// <summary>
        /// The <see cref="PlayerObject"/> that is currently being emulated in LUA code. See: <see cref="runTick"/>
        /// </summary>
        public PlayerObject CurrentObject = null;

        /// <summary>
        /// The function to Set-Up the Script object. Adds Player and Enemy to the Globals and creates the Base object using the 'Base' function declared in the source.
        /// </summary>
        public void Setup()
        {
            LuaUtils.Setup(this);
            
            
            this.Globals["Player"] = player.LuaPlayer;
            this.Globals["Enemy"] = player.Enemy.LuaPlayer;
            DynValue func = this.LoadString(this.player.Source.getLuaSource(), this.Globals,
                this.player.Source.getLuaName());
            func.Function.Call();
            player.Base = new Base(player,this.Globals.Get("Base"));
        }



        /// <summary>
        /// A complete list of PlayerObjects belonging to <see cref="player"/>. They are sorted in next-tick to run order. (so dequeue returns the objects to run the next tick.)
        /// </summary>
        public Queue<List<PlayerObject>> playerObjects = new Queue<List<PlayerObject>>();

        
        /// <summary>
        /// Runs a single tick-iteration by running all PlayerObjects for that tick. See <see cref="playerObjects"/>.
        /// </summary>
        public void runTick()
        {
            var objects = playerObjects.Dequeue();
            foreach (var obj in objects)
            {
                this.CurrentObject = obj;
                int newtick = obj.runTick(this);
                this.CurrentObject = null;
                while (newtick > playerObjects.Count-1)
                {
                   playerObjects.Enqueue(new List<PlayerObject>());
                }
                playerObjects.ElementAt(newtick-1).Add(obj);
            }
        }

        
        
        /// <summary>
        /// Adds a newly created PlayerObject to the Queue at i ticks sleep. (i=0 means run the next tick)
        /// </summary>
        /// <param name="playerObject"> The PlayerObject to be added</param>
        /// <param name="i">The Tick to first run at.</param>
        public void newObject(PlayerObject playerObject, int i)
        {
            while (playerObjects.Count < i+1)
            {
                playerObjects.Enqueue(new List<PlayerObject>());
            }
            playerObjects.ElementAt(i).Add(playerObject);
        }
    }
}