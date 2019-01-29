using System;
using System.IO;
using TMPro;
using UnityEngine;
using System.Xml;
using UnityEngine.Experimental.UIElements;
using Image = UnityEngine.UI.Image;


namespace Scenes.IGEditor
{
    public class ScriptSelectorChild : MonoBehaviour
    {
        public TMP_Text Title;
        public TMP_Text Date;
        public string LuaFileName;
        
        public ScriptSelector parent;

        public Color mainColor;
        public Color selectedColor;
        
        public string LuaPath
        {
            get { return Path.Combine(parent.BasePath, LuaFileName); }
        }

        public string DataPath
        {
            get { return Path.Combine(parent.BasePath, Title.text.ToLower()) + ".xml"; }
        }


        private bool selected;

        public bool Selected
        {
            get { return selected; }
            set { selected = value;
                this.GetComponent<Image>().color = selected ? selectedColor : mainColor;
            }
        }


        protected XmlElement SerializeField(XmlDocument doc,string name, string value)
        {
            var node = doc.CreateElement(name);
            node.InnerText = value;
            return node;
        }
        protected void ParseField(XmlElement parent, string name, out string val)
        {
            val = parent[name].InnerText;
        }
        protected void ParseField(XmlElement parent, string name, TMP_Text val)
        {
            val.text = parent[name].InnerText;
        }
        
        public void Save(string path)
        {
            XmlDocument xmlDoc = new XmlDocument();
            
            
            var rootNode = xmlDoc.CreateElement("Script");

            xmlDoc.AppendChild(rootNode);

            rootNode.AppendChild(SerializeField(xmlDoc, "Title", Title.text));
            rootNode.AppendChild(SerializeField(xmlDoc, "Date", Date.text));
            rootNode.AppendChild(SerializeField(xmlDoc, "LuaFile", LuaFileName));
            
            xmlDoc.Save(path);
        }

        public void Load(string path)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(path);

            var rootNode = doc["Script"];

            ParseField(rootNode,"Title", Title);
            ParseField(rootNode,"Date", Date);
            ParseField(rootNode,"LuaFile", out LuaFileName);
        }
        
        

        public void OnClick()
        {
            Debug.Log("Selecting: " + this);
            parent.Select(this);
        }
    }
}