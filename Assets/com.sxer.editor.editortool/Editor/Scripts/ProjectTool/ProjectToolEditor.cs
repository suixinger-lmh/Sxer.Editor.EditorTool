using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace Sxer.Editor.EditorTool
{
    public class ProjectToolEditor: UnityEditor.Editor
    {
        [MenuItem("Sxer/Editor/ProjectTool/整理选中文件夹")]
        public static void SortFileAssets()
        {
            //判断选中为文件夹
            string dirPath = AssetDatabase.GetAssetPath(Selection.activeObject);

            if (AssetDatabase.IsValidFolder(dirPath))
            {
                //获取文件夹下所有资源
                string[] paths = Directory.GetFiles(dirPath);
                //资源归类
                Dictionary<FileType, List<string>> typeSortPath = SortPaths(paths);
                
                foreach (var x in typeSortPath)
                {
                    if (x.Key == FileType.Meta || x.Key == FileType.Error)
                        continue;
                   
                    if (x.Value != null)
                    {
                        //根据类别生成文件夹
                        string tempStr = CreateSortFolder(x.Key, dirPath);
                        //资源移动
                        foreach(var t in x.Value)
                        {
                            AssetDatabase.MoveAsset(t, t.Replace(dirPath, dirPath + "/" + tempStr));
                        }
                    }
                }
            }
        }


        static string CreateSortFolder(FileType type,string dir)
        {
            string path = string.Empty;
            switch (type)
            {
                case FileType.Texture:
                    path = "Textures";
                    break;
                case FileType.Material:
                    path = "Materials";
                    break;
                case FileType.Model:
                    path = "Models";
                    break;
                case FileType.Prefab:
                    path = "Prefabs";
                    break;
                case FileType.Script:
                    path = "Scripts";
                    break;
                case FileType.Scene:
                    path = "Scenes";
                    break;
            }
            AssetDatabase.CreateFolder(dir, path);
            return path;
        }

        static Dictionary<FileType, List<string>> SortPaths(string[] paths)
        {
            Dictionary<FileType, List<string>> tempSort = new Dictionary<FileType, List<string>>();
            FileType temptype;
            foreach (var x in paths)
            {
                temptype = GetFileType(x);
                if (tempSort.ContainsKey(temptype))
                    tempSort[temptype].Add(x);
                else
                    tempSort.Add(temptype, new List<string>() {x});
            }
            return tempSort;
        }

        enum FileType
        {
            Meta,
            Texture,
            Prefab,
            Model,
            Material,
            Script,
            Scene,
            Error
        }
        static FileType GetFileType(string path)
        {
            if (IsFile_Meta(path))
                return FileType.Meta;
            if (IsFile_Texture(path))
                return FileType.Texture;
            if (IsFile_Prefab(path))
                return FileType.Prefab;
            if (IsFile_Model(path))
                return FileType.Model;
            if (IsFile_Material(path))
                return FileType.Material;
            if (IsFile_Script(path))
                return FileType.Script;
            if (IsFile_Scene(path))
                return FileType.Scene;

            return FileType.Error;
        }

        static bool IsFile_Meta(string path)
        {
            if (path.EndsWith(".meta"))
                return true;
            return false;
        }
        static bool IsFile_Texture(string path)
        {
            if (path.EndsWith(".png") || path.EndsWith(".PNG")
                || path.EndsWith(".jpg") || path.EndsWith(".JPG") || path.EndsWith(".jpeg")
                || path.EndsWith(".bmp")
                || path.EndsWith(".gif")
                || path.EndsWith(".dds")
                || path.EndsWith(".tif"))
                return true;
            return false;
        }
        static bool IsFile_Material(string path)
        {
            if (path.EndsWith(".mat"))
                return true;
            return false;
        }
        static bool IsFile_Prefab(string path)
        {
            if (path.EndsWith(".prefab"))
                return true;
            return false;
        }
        static bool IsFile_Scene(string path)
        {
            if (path.EndsWith(".unity"))
                return true;
            return false;
        }
        static bool IsFile_Script(string path)
        {
            if (path.EndsWith(".cs"))
                return true;
            return false;
        }
        static bool IsFile_Model(string path)
        {
            if (path.EndsWith(".FBX") || path.EndsWith(".fbx")
                || path.EndsWith(".obj") || path.EndsWith(".OBJ"))
                return true;
            return false;
        }



    }

}
