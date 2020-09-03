using Andromeda.Ship.Actions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Andromeda.AI
{

    public sealed class Fighter : AIController
    {
        private Vector2[] _guns = new Vector2[]
        {
            Vector3.up * 1.2f
        };

        const float fireRate = 0.5f;
        private float _lastFire = 0f;

        private new void Start()
        {
            base.Start();
        }

        // Update is called once per frame
        private new void Update()
        {
            base.Update();

            // Move towards
            Vector2 targetDir = _target.position - transform.position;
            if (targetDir.magnitude >= 5f)
            {
                actions.Enqueue(new Thrust());
            }

            // Face target
            float dot = Vector2.Dot(transform.right, targetDir) + 0.5f;
            if (Mathf.Abs(dot) > float.Epsilon)
            {
                actions.Enqueue(new Rotate(-dot));
            }

            // Shoot
            if (targetDir.magnitude <= 15f && Time.time - _lastFire >= fireRate)
            {
                _lastFire = Time.time;
                actions.Enqueue(new AIShoot(Ship.Ammo.AmmoType.DEFAULT_AI, _guns));
            }
        }
    }
}
