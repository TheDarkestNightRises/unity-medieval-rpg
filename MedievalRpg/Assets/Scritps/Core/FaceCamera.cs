using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class FaceCamera : MonoBehaviour
    {
        void LateUpdate()
        {
            transform.forward = Camera.main.transform.forward;
        }
    }
}

