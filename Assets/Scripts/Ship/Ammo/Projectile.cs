using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Andromeda.Ship.Ammo
{
    public class Projectile : MonoBehaviour
    {
        public AmmoConfig config;

        private bool _isActive = false;
        public bool IsActive
        {
            get => _isActive;
            set
            {
                if (value)
                {
                    this.gameObject.SetActive(true);
                    this.enabled = true;
                }
                else
                {
                    // Disable mono to not waste processor time
                    this.gameObject.SetActive(false);
                    this.enabled = false;
                }

                _isActive = value;
            }
        }

        private void Update()
        {
            // Doeos not do anything
            if (!IsActive)
            {
                return;
            }

            // Terminate if out of game world
            if (transform.position.sqrMagnitude > GameManager.Instance.gameWorldRadius)
            {
                IsActive = false;
            }

            transform.Translate(transform.up * 10f * Time.deltaTime, Space.World);
        }

        /// <summary>
        /// Controls projectile movement
        /// </summary>
        /// <param name="direction">Make sure to pass normalized direction!</param>
        private Vector2 _dir;
        public void ShootAt(Vector2 direction)
        {
            _dir = direction;
            IsActive = true;


        }
    }
}
