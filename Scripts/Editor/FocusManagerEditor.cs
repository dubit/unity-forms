using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace DUCK.Forms.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(FocusManager))]
    public class FocusManagerEditor : UnityEditor.Editor
    {

        private SerializedProperty autoDetectSelectables;
        private SerializedProperty selectables;
        ReorderableList list;

        void OnEnable()
        {

            selectables = serializedObject.FindProperty("selectables");
            list = new ReorderableList(serializedObject, selectables, true, true, true, true);
            autoDetectSelectables = serializedObject.FindProperty("autoDetectSelectables");
            list.drawElementCallback = DrawListItems; // Delegate to draw the elements on the list
            list.drawHeaderCallback =
                DrawHeader; // Skip this line if you set displayHeader to 'false' in your ReorderableList constructor.

        }

        public override void OnInspectorGUI()
        {

            serializedObject.Update();
            autoDetectSelectables.boolValue = EditorGUILayout.Toggle("Auto Detect", autoDetectSelectables.boolValue);
            list.DoLayoutList();
            serializedObject.ApplyModifiedProperties();

            FocusManager myScript = (FocusManager) target;
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
            EditorGUI.PropertyField(new Rect(20,
                    rect.y, rect.width, EditorGUIUtility.singleLineHeight),
                element);

        }

        void DrawHeader(Rect rect)
        {
            string name = "Selectables";
            EditorGUI.LabelField(rect, name);
        }
    }
}