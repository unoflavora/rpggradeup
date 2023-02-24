using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RPG.Control;
using RPG.Core;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;

namespace RPG.Assets.Scripts.Cinematics
{
    public class CinematicControlRemover : MonoBehaviour
    {
        private PlayableDirector _playableDirector;

        private void Start()
        {
            _playableDirector = GetComponent<PlayableDirector>();
            _playableDirector.played += DisableControl;
            _playableDirector.stopped += EnableControl;

        }
        void DisableControl (PlayableDirector director)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<ActionScheduler>().CancelAction();
            player.GetComponent<PlayerController>().enabled = false;
        }

        void EnableControl (PlayableDirector director) 
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<PlayerController>().enabled = true;
        }
    }
}