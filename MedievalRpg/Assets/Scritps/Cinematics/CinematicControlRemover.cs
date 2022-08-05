using RPG.Control;
using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicControlRemover : MonoBehaviour
    {
        GameObject player;
        private void Start()
        {
            GetComponent<PlayableDirector>().played += DisableControl;
            GetComponent<PlayableDirector>().stopped += EnableControl;
        }
        void DisableControl(PlayableDirector pd)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<Scheduler>().CancelCurrentAction();
            player.GetComponent<PlayerController>().enabled = false;
        }

        void EnableControl(PlayableDirector pd)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<PlayerController>().enabled = true;
        }
    }
}

