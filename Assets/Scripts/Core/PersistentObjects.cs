using UnityEngine;

public class PersistentObjects : MonoBehaviour
{
    [SerializeField] GameObject persistenObjectPrefab;
    static bool hasSpawned = false;

    private void Awake() {
        {
            if(hasSpawned) return;
            SpawnPersistenObjects();
            hasSpawned = true;
        }
    }

    private void SpawnPersistenObjects()
    {
        var persistentObject = Instantiate(persistenObjectPrefab);
        DontDestroyOnLoad(persistentObject);
    }
}
