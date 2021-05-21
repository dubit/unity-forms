﻿using UnityEngine;
using UnityEngine.UI;

 namespace DUCK.Forms
 {
     /// <summary>
     /// A component to dynamically add a selectable to the focus manager. If a focus manager does not exist in scene,
     /// a new one will be created on Awake.
     /// </summary>

     [RequireComponent(typeof(Selectable))]
     public class FocusMarkedSelectable : MonoBehaviour
     {
         private void Awake()
         {
             FocusManager focusManager = FindObjectOfType<FocusManager>();
             if (focusManager == null)
             {
                 GameObject obj = new GameObject("Focus Manager");
                 focusManager = obj.AddComponent<FocusManager>();
             }

             focusManager.AddSelectable(this.GetComponent<Selectable>());
         }
     }
 }
