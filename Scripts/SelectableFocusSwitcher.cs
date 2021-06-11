using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

 namespace DUCK.Forms
 {
     public class SelectableFocusSwitcher : MonoBehaviour
     {
         [Tooltip("Will add all active selectable UI elements to the list on awake, you cannot decide the order.")]
         [SerializeField]
         public bool autoDetectSelectables = false;

         [Tooltip("Customise the order of which selectables will be focused on pressing tab.")] [SerializeField]
         private List<Selectable> selectables;
         private EventSystem eventSystem;

         void Awake()
         {
             if(selectables == null){
             selectables = new List<Selectable>();
             }
             eventSystem = FindObjectOfType<EventSystem>();
             
             if (autoDetectSelectables)
             {
                 FindActiveSelectionIndex();
             }
         }

         public void FindSelectablesInScene()
         {
             Selectable[] sceneSelectables = FindObjectsOfType<Selectable>();
             for (int i = 0; i < sceneSelectables.Length; i++)
             {
                 AddSelectable(sceneSelectables[i]);
             }
         }

         public void AddSelectable(Selectable selectable)
         {
             if (!selectables.Contains(selectable))
             {
                 selectables.Add(selectable);
             }
         }

         void Update()
         {
             if (Input.GetKeyDown(KeyCode.Tab))
             {
                 OnTab();
             }
         }

         private void OnTab()
         {
             int selectionIndex = FindActiveSelectionIndex();
             CleanDestroyedObjects();
             if (autoDetectSelectables)
             {
                 FindSelectablesInScene();
             }

             if (selectionIndex < selectables.Count - 1)
             {
                 selectionIndex++;
             }
             else
             {
                 selectionIndex = 0;
             }

             if (selectables.Count > 0)
             {
                 SwitchSelectableFocus(selectionIndex);
             }
         }

         private void SwitchSelectableFocus(int index)
         {
             eventSystem.SetSelectedGameObject(selectables[index].gameObject);
         }

         private int FindActiveSelectionIndex()
         {
             GameObject highlightedObject = eventSystem.currentSelectedGameObject;
             for (int i = 0; i < selectables.Count; i++)
             {
                 if (highlightedObject == selectables[i].gameObject) return i;
             }

             return -1;
         }

         private void CleanDestroyedObjects()
         {
             for (int i = selectables.Count - 1; i >= 0; i--)
             {
                 if (selectables[i] == null || selectables[i].gameObject == null)
                     selectables.Remove(selectables[i]);
             }
         }
     }
 }