using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RPG.Core;
using UnityEngine.AI;

namespace RPG.Saving
{
    [ExecuteInEditMode]
    public class SaveableEntity : MonoBehaviour
    {
        [SerializeField] string uniqueIdentifier = "";

        public string GetUniqueIdentifier()
        {
            return uniqueIdentifier;
        }
       
        public object CaptureState()
        {
            print("Capturing state for " + GetUniqueIdentifier());

            return new SerializedVector3(transform.position);
        }

        public void RestoreState(JObject state)
        {
            var actionScheduler = GetComponent<ActionScheduler>();
            SerializedVector3 position = state.ToObject<SerializedVector3>();;

            if (actionScheduler != null)
            {
                GetComponent<ActionScheduler>().CancelAction();
                GetComponent<NavMeshAgent>().Warp(new Vector3(position.x, position.y, position.z));
            }
        }

        #if UNITY_EDITOR
        private void Update()
        {
            if (Application.IsPlaying(gameObject)) return;
            if (String.IsNullOrEmpty(gameObject.scene.path)) return;

            SerializedObject serializedObject = new SerializedObject(this);
            SerializedProperty uid = serializedObject.FindProperty("uniqueIdentifier");

            if(String.IsNullOrEmpty(uniqueIdentifier))
            {
                uid.stringValue = System.Guid.NewGuid().ToString();
                serializedObject.ApplyModifiedProperties();
            }
        }
        #endif
    }
}

