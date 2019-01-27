using System;
using System.Collections;
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
        
        
        
        private MonoBehaviour mono;
        
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
        public void Setup(MonoBehaviour mono)
        {
            this.mono = mono;
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
        public HashSet<PlayerObject> playerObjects = new HashSet<PlayerObject>();




        private int CoroutineState = 0;
        public readonly static float MaximumObjectTickRunTime = 1;
        
        
        /// <summary>
        /// Runs a single tick-iteration by running all PlayerObjects for that tick. See <see cref="playerObjects"/>.
        /// </summary>
        IEnumerator runTickInternal()
        {
            CoroutineState = 1;

            double endTime = Time.time + MaximumObjectTickRunTime;
            var objects = playerObjects.ToList();
            
            objects.ForEach(v => v.coolDown -= 1);
            
            
            for (;;)
            {
                if (Time.time >= endTime){break;}

                if (objects.All(v =>
                {
                    this.CurrentObject = v;
                    var ret = v.runTick(this);
                    this.CurrentObject = null;
                    return ret;
                }))
                {
                    break;
                }

                yield return null;
            }
            
            CoroutineState = 0;
        }
        

        public void startTick()
        {
            if (CoroutineState != 0) throw new Exception("Cannot start running coroutine");
            mono.StartCoroutine(runTickInternal());
        }

        public bool isDone()
        {
            return CoroutineState == 0;
        }
        

        
        
        /// <summary>
        /// Adds a newly created PlayerObject to the Queue at i ticks sleep. (i=0 means run the next tick)
        /// </summary>
        /// <param name="playerObject"> The PlayerObject to be added</param>
        /// <param name="i">The Tick to first run at.</param>
        public void newObject(PlayerObject playerObject, int i)
        {
            playerObjects.Add(playerObject);
        }
        
    }
}