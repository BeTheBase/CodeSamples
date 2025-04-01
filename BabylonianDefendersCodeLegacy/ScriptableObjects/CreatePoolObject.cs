using UnityEngine;
using System.Collections;
using UnityEditor;


public class CreatePoolObject : MonoBehaviour
{
#if UNITY_EDITOR

    [MenuItem("Assets/Create/ScriptablePoolObject")]
    public static void CreateMyAsset()
    {
        ScriptablePoolObject asset = ScriptableObject.CreateInstance<ScriptablePoolObject>();

        AssetDatabase.CreateAsset(asset, "Assets/Resources/NewScriptableObject.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }

#endif

}
