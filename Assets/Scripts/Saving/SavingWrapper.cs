using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Saving {
    public class SavingWrapper : MonoBehaviour
    {
        const string defaultSaveFile = "save";
        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }
        }

        public void Load()
        {
            GetComponent<SavingSystem>().Load(defaultSaveFile);
        }

        public void Save()
        {
            GetComponent<SavingSystem>().Save(defaultSaveFile);
        }

    }
}

