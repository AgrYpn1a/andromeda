using Andromeda.Ship.Actions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Andromeda.AI
{

    public sealed class Fighter : AIController
    {
        // Update is called once per frame
        private new void Update()
        {
            base.Update();

            // Move towards
            Vector2 targetDir = _target.position - transform.position;
            if (targetDir.sqrMagnitude >= 5f)
            {
                actions.Enqueue(new Thrust());
            }

            float dot = Vector2.Dot(transform.right, targetDir) + 0.5f;
            if (Mathf.Abs(dot) > float.Epsilon)
            {
                actions.Enqueue(new Rotate(-dot));
            }
        }
    }
}
