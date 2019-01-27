using MoonSharp.VsCodeDebugger.SDK;
using UnityEditor;
using UnityEngine;

namespace Game.Utils
{
    public abstract class SourceLoader
    {

        /// <summary>
        /// 
        /// </summary>
        /// <returns>The Lua source name</returns>
        public abstract string getLuaName();
        
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns>The string Lua source</returns>
        public abstract string getLuaSource();
    }

    public class StringSource : SourceLoader
    {
        public readonly string Source;
        public readonly string Name;

        public StringSource(string name, string source)
        {
            this.Name = name;
            this.Source = source;
        }

        public override string getLuaName()
        {
            return Name;
        }

        public override string getLuaSource()
        {
            return Source;
        }
    }

    [System.Serializable]
    public class MonoSource : System.Object
    {
        public string Name;
        
        [TextArea(3,100)]
        public string Source;
        
        

        public SourceLoader toLoader()
        {
            return new StringSource(Name,Source);
        }
    }
    
    
}


