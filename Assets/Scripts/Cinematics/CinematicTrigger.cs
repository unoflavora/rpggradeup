using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

internal class CinematicTrigger : MonoBehaviour
{
    private bool _isTriggered = false;
    private void OnTriggerEnter(Collider other) 
    {
        if (_isTriggered) return;
        if (!other.gameObject.CompareTag("Player")) return;

        _isTriggered = true;
        GetComponent<PlayableDirector>().Play();
    }

}
