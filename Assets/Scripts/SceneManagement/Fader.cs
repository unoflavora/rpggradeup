using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{
    [SerializeField] float fadeTime;
    CanvasGroup canvasGroup;

    private void Awake() 
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

   public IEnumerator FadeIn ()
   {
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime / fadeTime ;
            yield return null;
        }
   }

   public IEnumerator FadeOut ()
   {
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime / (fadeTime / 3) ;
            yield return null;
        }
   }
}
