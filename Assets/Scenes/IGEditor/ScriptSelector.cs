using System;
using System.IO;
using Game.Objects;
using UnityEngine;

namespace Scenes.IGEditor
{
    public class ScriptSelector : MonoBehaviour
    {
        public GameObject prefab;

        public string BasePath;

        public GameObject ThisWindow;

        public Editor editor;
        
        
        public ScriptSelectorChild selected { get; protected set; }


        private bool visible = true;
        public bool Visible
        {
            get { return visible; }
            set { visible = value; ThisWindow.SetActive(value); }
        }

        public ScriptSelectorChild newChild()
        {
            GameObject obj = GameObject.Instantiate<GameObject>(prefab, this.transform);
            var childInfo = obj.GetComponent<ScriptSelectorChild>();
            childInfo.parent = this;
            return childInfo;
        }
        
        
        private void Start()
        {
            
            BasePath = Path.Combine(Application.dataPath, "UserData/Scripts");


            var files = Directory.GetFiles(BasePath, "*.xml");
            
            foreach (var file in files)
            {
                var childInfo = newChild();
                childInfo.Load(file);
            }
        }

        public void Select(ScriptSelectorChild next)
        {
            if (selected == next) return;

            if (selected != null) selected.Selected = false;

            next.Selected = true;
            selected = next;


        }

        public void Open()
        {
            if (selected == null) return;
            
            editor.Open();
            
            
        }

        public void New()
        {
            editor.New();
        }
        
        
        
        
        
        
    }
}