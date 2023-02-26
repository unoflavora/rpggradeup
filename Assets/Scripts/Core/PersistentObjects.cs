using UnityEngine;


public class PersistentObjects : MonoBehaviour
{
    [SerializeField] GameObject[] persistentObjectPrefabs;

    static bool hasSpawned = false;
    
    private void Awake() 
    {
        if (hasSpawned) return;

        SpawnPersistenObjects();

        hasSpawned = true;
    }

    private void SpawnPersistenObjects()
    {
        foreach(var persistentObject in persistentObjectPrefabs)
        {
            var persistentGameobject = Instantiate(persistentObject);
            DontDestroyOnLoad(persistentGameobject);
        }
    }
}
