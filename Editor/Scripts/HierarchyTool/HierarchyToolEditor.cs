using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class HierarchyToolEditor
{
    //

    /// <summary>
    /// 
    /// </summary>
    [MenuItem("Sxer/Editor/HierarchyTool/获取所有预制体")]
    public static void CombineSameGameobjectWithPrefab()
    {
        //获取当前场景
        Scene nowScene = EditorSceneManager.GetActiveScene();
        //获取场景根物体
        GameObject[] rootObjs = nowScene.GetRootGameObjects();

        //获取所有预制体
        List<Object> prefabIns = new List<Object>();
        foreach(var x in rootObjs)
        {
            Object tempIns = PrefabUtility.GetPrefabInstanceHandle(x);
            if (!prefabIns.Contains(tempIns))
                prefabIns.Add(tempIns);
            Debug.Log(AssetDatabase.GetAssetPath(tempIns));
            //Debug.LogError(PrefabUtility.GetPrefabInstanceHandle(x).name);
        }


        Debug.Log(nowScene.name);

    }



}
