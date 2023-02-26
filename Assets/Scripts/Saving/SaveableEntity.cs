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
            Dictionary<string, object> stateOfThisObject = new Dictionary<string, object>();
            foreach(var saveable in GetComponents<ISaveable>())
            {
                stateOfThisObject[saveable.GetType().ToString()] = saveable.CaptureState();
            }
            return stateOfThisObject;
        }

        public void RestoreState(JObject state)
        {
            Dictionary<string, object> stateOfThisObject = state.ToObject<Dictionary<string, object>>();

            foreach(var saveable in GetComponents<ISaveable>())
            {
                var objectType = saveable.GetType().ToString();

                if(stateOfThisObject.ContainsKey(objectType))
                    saveable.RestoreState(stateOfThisObject[objectType]);
            }
        }

        #if UNITY_EDITOR
        private void Update()
        {
            if (Application.IsPlaying(gameObject)) return;
            if (String.IsNullOrEmpty(gameObject.scene.path)) return;

            SerializedObject serializedObject = new SerializedObject(this);
            SerializedProperty uid = serializedObject.FindProperty("uniqueIdentifier");

            if (String.IsNullOrEmpty(uniqueIdentifier) || !IsUnique(uniqueIdentifier))
            {
                uid.stringValue = System.Guid.NewGuid().ToString();
                
                serializedObject.ApplyModifiedProperties();
            }
            
            SavingSystem.GlobalLookup[uniqueIdentifier] = this;
        }

        private bool IsUnique(string uid)
        {
            if (!SavingSystem.GlobalLookup.ContainsKey(uniqueIdentifier)) return true;
            if (SavingSystem.GlobalLookup[uid] == this) return true;
            if (SavingSystem.GlobalLookup[uid] == null)
            {
                SavingSystem.GlobalLookup.Remove(uid);
                return true;
            }

            if (SavingSystem.GlobalLookup[GetUniqueIdentifier()] != this)
            {
                SavingSystem.GlobalLookup.Remove(uid);
                return true;
            }

            return false;
        }
        #endif
    }
}

