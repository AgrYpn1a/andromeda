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
        private float shipMass = 3f;

        public float maxVelocity = 1.4f;
        public float maxRotation = 3f;

        public ShipController()
        {
        }

        protected void Start()
        {
            _actor = new Actor(config);

            //GetComponent<Rigidbody2D>().useAutoMass = false;
            //GetComponent<Rigidbody2D>().mass = shipMass;
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

        protected void OnCollisionEnter2D(Collision2D collision)
        {

            // TODO: avoid dynamic allocation
            Instantiate(destroyedParticles, transform.position, Quaternion.identity);
            Destroy(this.gameObject);

        }

        protected Queue<ACommand> actions = new Queue<ACommand>();

        public void AddAction(ACommand action)
        {
            actions.Enqueue(action);
        }

        /// <summary>
        /// Test code
        /// </summary>
        #region Mono
        protected void FixedUpdate()
        {
            //GetComponent<Rigidbody2D>().velocity = Vector2.MoveTowards(GetComponent<Rigidbody2D>().velocity, Vector2.zero, 2f * Time.fixedDeltaTime);
            //GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity * 0.9f;

            while (actions.Count > 0)
            {
                var action = actions.Dequeue();
                action.Execute(this, _actor);
            }
        }
        #endregion

    }
}
