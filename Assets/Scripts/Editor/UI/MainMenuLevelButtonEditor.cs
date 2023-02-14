using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace GGJ2023.UI
{
    [CustomEditor(typeof(MainMenuLevelButton))]
    public class MainMenuLevelButtonEditor : Editor
    {
        private MainMenuLevelButton button;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            button = target as MainMenuLevelButton;
            
            Undo.RecordObject(button, "descriptive name of this operation");
            Undo.RecordObject(button.CountText, "descriptive name of this operation 1");

            button.level = EditorGUILayout.IntField("Level", button.level);
            button.levelIndexName = EditorGUILayout.TextField("Level Index Name", button.levelIndexName);
            button.CountText.text = button.levelIndexName;
            
            EditorUtility.SetDirty(button);
            EditorUtility.SetDirty(button.CountText);
            PrefabUtility.RecordPrefabInstancePropertyModifications(button);
            PrefabUtility.RecordPrefabInstancePropertyModifications(button.CountText);
            EditorSceneManager.MarkSceneDirty(button.gameObject.scene);

        }
    }
}