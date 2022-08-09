using Rpg.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class PortalBehaviour : MonoBehaviour
    {
        enum DestinationIdentifier
        {
            A, B , C , D , E
        }

        [SerializeField] int sceneToLoad = -1;
        [SerializeField] Transform spawnPoint;
        [SerializeField] float fadeOutTime = 1f;
        [SerializeField] float fadeInTime = 1f;
        [SerializeField] float fadeWaitTime = 0.5f;

        [SerializeField] DestinationIdentifier destination;

        private void OnTriggerEnter(Collider other)
        {
           if(other.tag == "Player")
            {
                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition()
        {

            
            DontDestroyOnLoad(gameObject);

            Fader fader = FindObjectOfType<Fader>();

            yield return fader.FadeOut(fadeOutTime);
            yield return SceneManager.LoadSceneAsync(sceneToLoad);

            PortalBehaviour otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);

            yield return new WaitForSeconds(fadeWaitTime);
            yield return fader.FadeIn(fadeInTime);

            print("Scene Loaded");
            Destroy(gameObject);
        }

        private PortalBehaviour GetOtherPortal()
        {
            foreach(PortalBehaviour portal in FindObjectsOfType<PortalBehaviour>())
            {
                if (portal == this) continue;
                if (portal.destination != destination) continue;
                return portal;
            }
            return null;
        }

        private void UpdatePlayer(PortalBehaviour otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.transform.rotation = otherPortal.spawnPoint.rotation;
            player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
        }
    }
}

