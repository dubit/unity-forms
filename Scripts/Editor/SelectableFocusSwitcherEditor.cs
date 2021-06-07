using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace DUCK.Forms.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(SelectableFocusSwitcher))]
    public class SelectableFocusSwitcherEditor : UnityEditor.Editor
    {
        private SerializedProperty autoDetectSelectables;
        private SerializedProperty selectables;
        private ReorderableList list;

        void OnEnable()
        {
            selectables = serializedObject.FindProperty("selectables");
            list = new ReorderableList(serializedObject, selectables, true, true, true, true);
            autoDetectSelectables = serializedObject.FindProperty("autoDetectSelectables");
            list.drawElementCallback = DrawListItems;
            list.drawHeaderCallback = DrawHeader;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            autoDetectSelectables.boolValue = EditorGUILayout.Toggle("Auto Detect", autoDetectSelectables.boolValue);
            list.DoLayoutList();
            serializedObject.ApplyModifiedProperties();

            SelectableFocusSwitcher myScript = (SelectableFocusSwitcher) target;
            if (!autoDetectSelectables.boolValue)
            {
                if (GUILayout.Button("Detect Selectables"))
                {
                    myScript.FindSelectablesInScene();
                }
            }
        }

        void DrawListItems(Rect rect, int index, bool isActive, bool isFocused)
        {
            SerializedProperty element = list.serializedProperty.GetArrayElementAtIndex(index);
            EditorGUI.PropertyField(new Rect(20, rect.y, rect.width, EditorGUIUtility.singleLineHeight), element);
        }

        void DrawHeader(Rect rect)
        {
            string name = "Selectables";
            EditorGUI.LabelField(rect, name);
        }
    }
}