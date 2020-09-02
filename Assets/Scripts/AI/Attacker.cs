using Andromeda.Ship;
using Andromeda.Ship.Actions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Andromeda.AI
{

    public sealed class Attacker : AIController
    {

        private Transform _targetPos;
        private Starbase _starbase;

        private new void Start()
        {
            // Get the starbase
            _target = GameObject.FindGameObjectWithTag("Starbase").transform;
            _starbase = _target.GetComponent<Starbase>();

            // Find rotation
            transform.rotation = Quaternion.LookRotation(Vector3.forward, _target.position - transform.position);
        }

        // Update is called once per frame
        private new void Update()
        {
            base.Update();

            // Move towards
            Vector2 targetDir = _target.position - transform.position;
            if (Mathf.Abs(targetDir.magnitude) >= 10f)
            {
                actions.Enqueue(new Thrust());
            }
        }
    }
}
