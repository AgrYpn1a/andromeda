using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Andromeda
{

    public sealed class Starbase : Ship.ShipController
    {
        [SerializeField]
        private Transform _attackSlots;
        [SerializeField]
        private Slider _hpSlider;

        private float _maxHp;

        private new void Start()
        {
            base.Start();
            _maxHp = _actor.HitPoints;
            OnDeath = () =>
            {
                _isDead = true;
                Instantiate(destroyedParticles, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
                GameManager.Instance.GameOver(1.5f);
            };
        }

        public bool HasFreeAttackSlots()
        {
            bool hasFreeSlots = false;
            foreach (Transform slot in _attackSlots)
            {
                hasFreeSlots = hasFreeSlots || slot.childCount == 0;
            }

            return hasFreeSlots;
        }

        public Transform GetNextFreeSlot()
        {
            Transform slotTr = default;

            foreach (Transform slot in _attackSlots)
            {
                if (slot.childCount == 0)
                {
                    slotTr = slot;
                }
            }

            return slotTr;
        }

        private bool _isDead = false;

        private new void OnCollisionEnter2D(Collision2D collision)
        {
            if (_isDead) return;

            base.OnCollisionEnter2D(collision);
            _hpSlider.value = _actor.HitPoints / _maxHp;
        }
    }
}
