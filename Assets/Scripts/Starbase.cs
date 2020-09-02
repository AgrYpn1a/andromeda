using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Andromeda
{

    public sealed class Starbase : MonoBehaviour
    {
        [SerializeField]
        private Transform _attackSlots;

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
    }
}
