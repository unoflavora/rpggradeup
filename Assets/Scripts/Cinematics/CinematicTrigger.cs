using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Assets.Scripts.Cinematics
{
    class CinematicTrigger : MonoBehaviour
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
}

