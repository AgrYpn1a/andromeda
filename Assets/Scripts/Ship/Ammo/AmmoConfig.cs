using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Andromeda.Ship.Ammo
{

    [CreateAssetMenu(fileName = "AmmoConfig", menuName = "Andromeda/Ammo/AmmoConfig", order = 1)]
    public class AmmoConfig : ScriptableObject
    {
        public int Damage = 10;
        public float Speed = 2f;
    }
}
