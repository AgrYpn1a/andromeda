using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Andromeda.Utils
{

    public class Rotate : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _angles;

        private void Update()
        {
            transform.Rotate(_angles * Time.deltaTime);
        }
    }
}
