using UnityEngine;

public class PersistentObjects : MonoBehaviour
{
    [SerializeField] GameObject[] persistenObjectPrefabs;
    
    private void Awake() 
    {
        SpawnPersistenObjects();
    }

    private void SpawnPersistenObjects()
    {
        foreach(var persistentObject in persistenObjectPrefabs)
            DontDestroyOnLoad(persistentObject);
    }
}
