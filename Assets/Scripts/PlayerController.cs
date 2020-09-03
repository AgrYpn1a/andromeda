using Andromeda.Ship;
using Andromeda.Ship.Actions;
using Andromeda.Ship.Ammo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Andromeda
{

    [RequireComponent(typeof(Collider2D))]
    public sealed class PlayerController : ShipController
    {
        // Called once only
        private new void Start()
        {
            base.Start();

            InputManager.Instance.InputMap[KeyCode.UpArrow] = new Thrust();
            InputManager.Instance.InputMap[KeyCode.LeftArrow] = new Rotate(1);
            InputManager.Instance.InputMap[KeyCode.RightArrow] = new Rotate(-1);
            InputManager.Instance.InputMap[KeyCode.LeftControl] = new Shoot(AmmoType.DEFAULT,
                new Vector2[]
                {
                    Vector3.up * 1.2f
                });
        }

        private void Update()
        {
            var inputs = InputManager.Instance.GetInput();
            while (inputs.Count > 0)
            {
                var action = inputs.Dequeue();
                if (!action.IsPhysics)
                {
                    action.Execute(this, _actor);
                }
                else
                {
                    this.AddAction(action);
                }
            }

            if (transform.position.sqrMagnitude >= 300f)
            {
                Debug.LogWarning("Game Over out of range!");
            }
        }

        private new void OnCollisionEnter2D(Collision2D collision)
        {

        }

    }
}
