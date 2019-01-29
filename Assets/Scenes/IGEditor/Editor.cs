using System;
using System.Globalization;
using System.IO;
using MoonSharp.Interpreter;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes.IGEditor
{
    public class Editor : MonoBehaviour
    {

        public bool isDisplaying;
        
        public TMP_InputField LuaText;
        public TMP_InputField Name;

        public ScriptSelector selector;
        public ScriptSelectorChild Info;



        public void Open()
        {
            this.Info = selector.selected;
            
            Name.text = Info.Title.text;

            var reader = new StreamReader(Info.LuaPath);

            LuaText.text = reader.ReadToEnd();
            reader.Close();

            isDisplaying = true;
            selector.Visible = false;

        }

        public void New()
        {
            this.Info = null;
            
            Name.text = "Name:";
            LuaText.text = "";
            
            isDisplaying = true;
            selector.Visible = false;

        }

        public void Save()
        {
            if (!isDisplaying) return;
            if (Info == null)
            {
                Info = selector.newChild();
                Info.Title.text = Name.text;
                Info.Date.text = DateTime.Today.ToString("d MMM yyyy");
                Info.LuaFileName = Name.text.ToLower() + ".lua";
            }

            if (Info.Title.text != Name.text)
            {
                File.Delete(Info.DataPath);
                Info.Title.text = Name.text;
            }
            
            Info.Save(Info.DataPath);

            var writer = new StreamWriter(Info.LuaPath);
            writer.Write(LuaText.text);
            writer.Flush();
            writer.Close();
        }

        public void Exit()
        {
            if (!isDisplaying) return;
            Save();
            isDisplaying = false;
            selector.Visible = true;
            
            
            
            
            //Move back to the previous scene?
        }
        
        public void OpenCommand()
        {
            if (!isDisplaying) return;
            Save();
            isDisplaying = false;
            selector.Visible = true;
        }









    }
}