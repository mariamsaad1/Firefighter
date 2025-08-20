using UnityEngine;
using UnityEditor;

public class MissingScriptsFinder : EditorWindow
{
    [MenuItem("Tools/Find Missing Scripts")]
    public static void ShowWindow()
    {
        GetWindow<MissingScriptsFinder>("Missing Scripts Finder");
    }

    void OnGUI()
    {
        if (GUILayout.Button("Find Missing Scripts in Scene"))
        {
            FindInScene();
        }
    }

    private void FindInScene()
    {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        int missingCount = 0;

        foreach (GameObject go in allObjects)
        {
            Component[] components = go.GetComponents<Component>();
            foreach (var c in components)
            {
                if (c == null)
                {
                    Debug.LogWarning($"⚠️ Missing Script found on GameObject: {go.name}", go);
                    missingCount++;
                }
            }
        }

        Debug.Log($"✅ Scan complete. Found {missingCount} missing scripts.");
    }
}
