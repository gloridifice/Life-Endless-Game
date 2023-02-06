using System;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace GGJ2023.UI
{
    [CustomEditor(typeof(KeyBox))]
    public class KeyBoxEditor : UnityEditor.Editor
    {
        private KeyBox keyBox;
        private void OnEnable()
        {
            keyBox = (KeyBox)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            Undo.RecordObject(keyBox.Text, "descriptive name of this operation 1");

            keyBox.Text.text = EditorGUILayout.TextField("Key Name", keyBox.Text.text);
            keyBox.Text.fontSize = EditorGUILayout.FloatField("Font Size", keyBox.Text.fontSize);
            
            EditorUtility.SetDirty(keyBox.Text);
            PrefabUtility.RecordPrefabInstancePropertyModifications(keyBox.Text);
            EditorSceneManager.MarkSceneDirty(keyBox.gameObject.scene);

        }
    }
}