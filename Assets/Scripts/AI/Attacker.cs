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
            _starbase = GameObject.FindGameObjectWithTag("Starbase").GetComponent<Starbase>();
            _target = _starbase.GetNextFreeSlot();

            // Find rotation
            transform.rotation = Quaternion.LookRotation(Vector3.forward, _target.position - transform.position);
        }

        // Update is called once per frame
        private new void Update()
        {
            base.Update();

            // Move towards
            Vector2 targetDir = _starbase.transform.position - transform.position;

            // Rotate towards
            float dot = Vector2.Dot(transform.right, targetDir) + 0.5f;
            if (Mathf.Abs(dot) > float.Epsilon)
            {
                actions.Enqueue(new Rotate(-dot));
            }

            if (Mathf.Abs(targetDir.magnitude) >= 10f)
            {
                actions.Enqueue(new Thrust());
            }
        }
    }
}
