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
            base.Start();

            // Get the starbase
            _starbase = GameObject.FindGameObjectWithTag("Starbase").GetComponent<Starbase>();
            _target = _starbase.GetNextFreeSlot();

            // Find rotation
            transform.rotation = Quaternion.LookRotation(Vector3.forward, _target.position - transform.position);
        }

        const float fireRate = 1.5f;
        private float _lastFire = 0f;

        private Vector2[] _guns =
        {
            Vector3.up * 1.2f + Vector3.right * 0.5f,
            Vector3.up * 1.2f - Vector3.right * 0.5f,
        };

        // Update is called once per frame
        private new void Update()
        {
            base.Update();

            // Target does not exist
            if (!_starbase) return;

            // Move towards
            Vector2 targetDir = _starbase.transform.position - transform.position;

            // Rotate towards
            float dot = Vector2.Dot(transform.right, targetDir) + 0.5f;
            if (Mathf.Abs(dot) > float.Epsilon)
            {
                actions.Enqueue(new Rotate(-dot));
            }

            if (targetDir.magnitude >= 15f)
            {
                actions.Enqueue(new Thrust());
            }
            else
            {
                // Close enough to shoot
                if (Time.time - _lastFire > fireRate)
                {
                    _lastFire = Time.time;
                    actions.Enqueue(new AIShoot(Ship.Ammo.AmmoType.ROCKET, _guns));
                }
            }
        }
    }
}
