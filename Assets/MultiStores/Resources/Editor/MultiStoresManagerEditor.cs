using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using UnityEngine.Animations;

// Creates a custom Label on the inspector for all the scripts named ScriptName
// Make sure you have a ScriptName script in your
// project, else this will not work.
[CustomEditor(typeof(MultiStoresManager))]
public class MultiStoresManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        GUILayout.Label("Multi Store Manager");

        MultiStoresManager myScript = (MultiStoresManager)target;

        myScript.store = (MultiStoresManager.Store)EditorGUILayout.EnumPopup("Store", myScript.store);
        myScript.PackageName = EditorGUILayout.TextField("Package Name", myScript.PackageName);

        switch (myScript.store)
        {
            case MultiStoresManager.Store.Google:
                PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, myScript.PackageName);
                break;
            case MultiStoresManager.Store.Huwaui:
                PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, myScript.PackageName + ".huawei");
                break;
            default:
                break;
        }
        GUILayout.Space(8);
        GUILayout.Label("Multi Store Objets");

        myScript.googleplayservices = (playservice)EditorGUILayout.ObjectField(myScript.googleplayservices,typeof( playservice) , myScript.googleplayservices);
        myScript.huwauiservices = (huwauiservices)EditorGUILayout.ObjectField(myScript.huwauiservices, typeof(huwauiservices) , myScript.huwauiservices);

        if (!Application.isPlaying)
        {
            EditorUtility.SetDirty(myScript);
            EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
        }
    }
}