using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cainos.PixelArtTopDown_Basic
{
    //let camera follow target
    public class CameraFollow : MonoBehaviour
    {

        public Transform target;
        public Vector3 offset = new Vector3(0, 2, -10);

        private void LateUpdate()
        {
            transform.position = target.position + offset;
        }

    }
}
