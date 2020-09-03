using Andromeda.Ship;
using Andromeda.Ship.Actions;
using Andromeda.Ship.Ammo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Andromeda
{

    [RequireComponent(typeof(Collider2D))]
    public sealed class PlayerController : ShipController
    {
        [SerializeField]
        private Slider _hpSlider;
        private float _maxHp;

        // Called once only
        private new void Start()
        {
            base.Start();

            _maxHp = _actor.HitPoints;

            InputManager.Instance.InputMap[KeyCode.UpArrow] = new Thrust();
            InputManager.Instance.InputMap[KeyCode.LeftArrow] = new Rotate(1);
            InputManager.Instance.InputMap[KeyCode.RightArrow] = new Rotate(-1);
            InputManager.Instance.InputMap[KeyCode.LeftControl] = new Shoot(AmmoType.DEFAULT,
                new Vector2[]
                {
                    Vector3.up * 1.2f
                });

            InputManager.Instance.InputMap[KeyCode.RightControl] = new Shoot(AmmoType.DEFAULT,
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
            if (collision.gameObject.tag.Equals("Projectile"))
            {
                var ammo = collision.gameObject.GetComponent<Projectile>().GetAmmoConfig;
                _actor.HitPoints -= ammo.Damage;

                // Check death
                if (_actor.HitPoints <= 0)
                {
                    // Died
                    Instantiate(destroyedParticles, transform.position, Quaternion.identity);

                    // Game Over
                    GameManager.Instance.GameOver();
                }
                else
                {
                    // TODO: Display hit particles
                    Instantiate(destroyedParticles, transform.position, Quaternion.identity);
                }
            }

            _hpSlider.value = _actor.HitPoints / _maxHp;
        }

    }
}
