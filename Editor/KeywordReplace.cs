//RRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRR
//  RConApps		2022-08-11
//	RConUtility
//  KeywordReplace
//  Replaces keywords in new script templates
//RRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRR

using System;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class KeywordReplace : UnityEditor.AssetModificationProcessor
    {
        public static void OnWillCreateAsset(string path)
        {
            string[] acceptedFiletypes = { ".cs" };

            // check what kind of file is being made
            path = path.Replace(".meta", "");
            int index = path.LastIndexOf(".");
            if(index < 0) return;
            string file = path.Substring(index);

            // only work with viable scripts
            bool accept = false;
            for(int i = 0; i < acceptedFiletypes.Length; i++)
            {
                accept = accept || file == acceptedFiletypes[i];
            }
            if(!accept) return;

            // get the file
            index = Application.dataPath.LastIndexOf("Assets");
            if(index < 0) return;
            path = Application.dataPath.Substring(0, index) + path;
            file = System.IO.File.ReadAllText(path);

            // replace macros
            file = file.Replace("#CREATIONDATE#", DateTime.Now + "");
            file = file.Replace("#PROJECTNAME#", PlayerSettings.productName);
            file = file.Replace("#COMPANYNAME#", PlayerSettings.companyName);
            file = file.Replace("#DEFAULTNAMESPACE#", PlayerSettings.productName);

            //save
            System.IO.File.WriteAllText(path, file);
            AssetDatabase.Refresh();
        }
    }
}
