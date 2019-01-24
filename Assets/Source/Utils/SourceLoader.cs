namespace Game.Utils
{
    public abstract class SourceLoader
    {

        public abstract string getLuaName();
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
    
    
    
    
}


