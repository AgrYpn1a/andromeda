using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Andromeda
{

    public sealed class PlayerCamera : MonoBehaviour
    {
        [SerializeField]
        private Transform _target;
        [SerializeField]
        private float _followSpeed = 2f;

        private float _distance = 0f;

        private void Start()
        {
            _distance = GetComponent<Camera>().orthographicSize;
        }

        private void FixedUpdate()
        {
            //transform.position = Vector2.MoveTowards(transform.position, _target.position, _followSpeed * Time.deltaTime);
            //transform.position = transform.position + Vector3.forward * -10f;

            // Out of bounds
            //if(((Vector2)_target.position).sqrMagnitude >= 100f)
            //{
            //    return;
            //}

            transform.position = Vector3.Lerp(transform.position, _target.position, Time.deltaTime);
            transform.position = transform.position + Vector3.forward * -_distance;
        }
    }
}
