﻿using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using System;
using Newtonsoft.Json.Linq;

namespace RPG.Saving
{
     public class SavingSystem : MonoBehaviour
     {
          public void Save(string saveFile)
          {
               Dictionary<string, object> state = LoadFile(saveFile);

               CaptureState(state);

               SaveFile(saveFile, state);
          }

          public void Load(string saveFile)
          {
               RestoreState(LoadFile(saveFile));
          }

          private void SaveFile(string saveFile, Dictionary <string, object> dataToSave)
          {
               string path = GetSavePath(saveFile);
               
               if (!File.Exists(path)) CreateFile(saveFile);

               using (FileStream stream = File.Open(path, FileMode.Create))
               {
                    string data = JsonConvert.SerializeObject(dataToSave);
                    byte[] bytes = Encoding.UTF8.GetBytes(data);
                    stream.Write(bytes, 0, bytes.Length);
               };
          }

          private void CaptureState(Dictionary<string, object> state)
          {
               foreach(SaveableEntity saveable in FindObjectsOfType<SaveableEntity>())
               {
                    state[saveable.GetUniqueIdentifier()] = saveable.CaptureState();
               }
          }

          private Dictionary<string, object> LoadFile(string saveFile)
          {
               var path = GetSavePath(saveFile);

               if (!File.Exists(path)) 
               {
                    return new Dictionary<string, object>();
               }
               
               using (FileStream stream = File.Open(path, FileMode.Open))
               {
                    byte[] buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, buffer.Length);
                    var data = Encoding.UTF8.GetString(buffer);
                    return JsonConvert.DeserializeObject<Dictionary<string, object>>(data);
               };

               
          }

          private void CreateFile(string saveFile)
          {
               var path = GetSavePath(saveFile);
               FileStream stream = File.Create(path);
               stream.Close();
          }

          private void RestoreState(Dictionary<string, object> state)
          {
               foreach(SaveableEntity saveable in FindObjectsOfType<SaveableEntity>())
               {
                    if(state.ContainsKey(saveable.GetUniqueIdentifier()))
                    {
                         var saveableEntityData = state[saveable.GetUniqueIdentifier()];
                         saveable.RestoreState(saveableEntityData as JObject);
                    }
               }
          }

          private string GetSavePath(string saveFile)
          {
               return Path.Combine(Application.persistentDataPath, saveFile + ".sav");
          }
     }
}

