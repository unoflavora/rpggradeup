using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using RPG.Saving;
using UnityEngine.AI;

namespace RPG.Assets.Scripts.Core
{
   public class Portal : MonoBehaviour 
   {
        [SerializeField] public int sceneToLoad = -1;
        [SerializeField] public Transform spawnPoint;

        private int thisScene;

        private void Start() 
        {
            thisScene = SceneManager.GetActiveScene().buildIndex;
            Debug.Log(thisScene);
        }

        private void OnTriggerEnter(Collider other) 
        {
            if(other.gameObject.CompareTag("Player"))
            {
                StartCoroutine(Transition());
            }    
        }

        private IEnumerator Transition()
        {
            DontDestroyOnLoad(gameObject);

            FindObjectOfType<SavingWrapper>().Save();
            yield return GameObject.FindObjectOfType<Fader>().FadeIn();

            yield return SceneManager.LoadSceneAsync(sceneToLoad);
            FindObjectOfType<SavingWrapper>().Load();
            
            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);

            yield return GameObject.FindObjectOfType<Fader>().FadeOut();

            Destroy(gameObject);
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");

            player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);

            player.transform.rotation = otherPortal.spawnPoint.rotation;
        }

        private Portal GetOtherPortal()
        {
            Portal[] portals = GameObject.FindObjectsOfType<Portal>();
            return portals.First<Portal>(portal => portal.sceneToLoad == thisScene);
        }
    }
}