/// <summary>
/// Author: AkilarLiao
/// Date: 2023/06/17
/// Desc:
/// </summary>
/// 
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CustomCommon
{
    public static class EditorUtilityFunctions
    {
        public static T CreateAssetWithActiveScene<T>(string name) where T :
            ScriptableObject
        {   
            return CreateAsset<T>(string.Format("Assets/{0}",
                GetAtciteScnePath()), name);
        }

        public static string GetAtciteScnePath()
        {
            string path = SceneManager.GetActiveScene().path;
            path = path.Substring("Assets/".Length);
            var index = path.LastIndexOf("/");
            return index >=0 ? path.Remove(index) : "";
        }

        public static T CreateAsset<T>(string pathName, string name) where T :
            ScriptableObject
        {
            T asset = ScriptableObject.CreateInstance<T>();
            string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(
                pathName + "/" + name + ".asset");
            AssetDatabase.CreateAsset(asset, assetPathAndName);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            return asset;
        }
    }
}