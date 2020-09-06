using Andromeda.Ship.Ammo;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Andromeda.Ship
{
    [RequireComponent(typeof(Collider2D))]
    public class ShipController : MonoBehaviour
    {
        public ActorConfig config;
        protected Actor _actor;

        [SerializeField]
        protected GameObject destroyedParticles;
        [SerializeField]
        protected GameObject hitParticles;

        [SerializeField]
        private float shipMass = 3f;

        public float maxVelocity = 1.4f;
        public float maxRotation = 3f;

        protected Action OnDeath;

        protected void Start()
        {
            _actor = new Actor(config);

            if (hitParticles == null)
            {
                // Assign default
                hitParticles = destroyedParticles;
            }
        }

        public void AddThrust(Vector2 direction, float force)
        {
            if (GetComponent<Rigidbody2D>().velocity.magnitude > maxVelocity)
            {
                Debug.Log("Already at max speed");
                return;
            }

            GetComponent<Rigidbody2D>().AddForce(direction * force);
        }

        public void AddRotation(float rotation)
        {
            transform.Rotate(Vector3.forward, rotation * Time.deltaTime);
        }

        protected void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.tag.Equals("Projectile"))
            {
                var ammo = collider.gameObject.GetComponent<Projectile>().GetAmmoConfig;
                _actor.HitPoints -= ammo.Damage;

                // Check death
                if (_actor.HitPoints <= 0)
                {
                    // Died
                    // TODO: avoid dynamic allocation
                    if (OnDeath != null)
                    {
                        // Custom death event
                        OnDeath();
                    }
                    else
                    {
                        // Default death event
                        Instantiate(destroyedParticles, transform.position, Quaternion.identity);
                        Destroy(this.gameObject);
                    }
                }
                else
                {
                    Instantiate(hitParticles, transform.position, Quaternion.identity);
                }
            }
        }

        protected void OnCollisionEnter2D(Collision2D collision)
        {
        }

        protected Queue<ACommand> actions = new Queue<ACommand>();
        public void AddAction(ACommand action) => actions.Enqueue(action);

        /// <summary>
        /// Test code
        /// </summary>
        #region Mono
        protected void FixedUpdate()
        {
            while (actions.Count > 0)
            {
                var action = actions.Dequeue();
                action.Execute(this, _actor);
            }
        }
        #endregion

    }
}
