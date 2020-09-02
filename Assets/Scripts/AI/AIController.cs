using Andromeda.Ship;
using Andromeda.Ship.Actions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Andromeda.AI
{

    public class AIController : ShipController
    {
        protected Transform _target;

        // Start is called before the first frame update
        protected new void Start()
        {
            base.Start();
            _target = GameObject.FindWithTag("Player").transform;
        }

        protected void Update()
        {
        }

        protected new void FixedUpdate()
        {
            base.FixedUpdate();
        }
    }
}

