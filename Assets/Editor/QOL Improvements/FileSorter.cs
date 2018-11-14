using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FileSorter : AssetPostprocessor
{
    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        for( int i = 0; i < importedAssets.Length; ++i)
        {
            //Check if the asset is within a subfolder.
            bool isInAssetsDirectory = !importedAssets[i].TrimStart("Assets/".ToCharArray()).Contains("/");

            if (isInAssetsDirectory)
            {
                SortAsset(importedAssets[i]);
            }
        }
    }
   
    static void SortAsset(string path)
    {
        string oldPath = path;
        path = path.Replace("Assets/", "");
        if (path.EndsWith(".mat") || path.EndsWith(".anim") || path.EndsWith(".fbx")
            || path.EndsWith(".controller") || path.EndsWith(".prefab") || path.EndsWith(".unity")
            || path.EndsWith(".png"))
        {
            string newPath = GetFilePath(path);
            Debug.Log(newPath);
            AssetDatabase.MoveAsset(oldPath, "Assets/" + newPath);
        }
        else if (path.EndsWith(".cs"))
        {

//            AssetDatabase.MoveAsset(path, path.Replace("Assets/", "Assets/Scripts/"));
        }
        else if (path.EndsWith(".shader"))
        {

        }
        else if (path.EndsWith(".jpg") || path.EndsWith(".bmp") || path.EndsWith(".jpeg"))
        {

        }
        else
        {
            Debug.Log(path.TrimStart("Assets/".ToCharArray()) + " placed in Assets/ folder. Are you sure this is the correct folder?");
        }
    }

    static string GetFilePath(string name)
    {
        string[] nameWords = name.Split(new char[] { '_' });
        string path = "";

        if(nameWords[0] == "Temp")
        {
            path += "Resources/Temp/";
            return path;
        }


        switch (nameWords[0])
        {
            case "Mesh":
            case "Mat":
                path += "Resources/Meshes/";
                break;
            case "Anim":
            case "Ctrl":
                path += "Resources/Animation/";
                break;
            case "Tx":
                path += "Resources/Textures/";
                break;
            case "Pref":
                path += "Resources/Prefab/";
                break;
            case "Ld":
                path += "Scenes/Ld/";
                break;
        }

        if(nameWords[0] == "Ld")
        {
            if (!AssetDatabase.IsValidFolder("Assets/" + path + nameWords[1]))
            {
                AssetDatabase.CreateFolder("Assets/Scenes/Ld", nameWords[1]);
            }

            path += nameWords[1] + "/" + name;

            return path;
        }

        switch (nameWords[1])
        {
            case "Chara":
                path += "Character/";
                break;
            case "Prop":
                path += "Props/";
                break;
            case "Stage":
                path += "Stage/";
                break;
            case "Boss":
                path += "Boss/";
                break;
            case "Box":
                path += "Box/";
                break;
        }

        if(nameWords[1] == "Prop")
        {
            if(!AssetDatabase.IsValidFolder("Assets/" + path + nameWords[2]))
            {
                AssetDatabase.CreateFolder("Assets/Resources/Props", nameWords[2]);
            }
            path += nameWords[2] + "/";
        }

        path += name;

        return path; 
    }
}