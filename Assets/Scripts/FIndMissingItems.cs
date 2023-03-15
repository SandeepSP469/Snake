using UnityEngine;
using UnityEditor;
using System.Linq;

public static class FIndMissingItems
{
    [MenuItem("My Menu/Find Missing Scripts In Project")]
    static void FindMissingScriptsInProjectMenuItem()
    {
        string[] prefabPaths = AssetDatabase.GetAllAssetPaths().Where(path => path.EndsWith(".prefab", System.StringComparison.OrdinalIgnoreCase)).ToArray();

        foreach(string path in prefabPaths)
        {
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);

            foreach(Component component in prefab.GetComponentsInChildren<Component>())
            {
                if(component == null)
                {
                    Debug.Log("Prefab found with missing script " + path, prefab);
                }
            }
        }
    }
}
