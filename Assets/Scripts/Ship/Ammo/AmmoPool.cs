using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Andromeda.Ship.Ammo
{

    [System.Serializable]
    struct ProjectileType
    {
        public AmmoType type;
        public GameObject prefab;
    }

    public sealed class AmmoPool : MonoSingleton<AmmoPool>
    {
        [SerializeField]
        private List<ProjectileType> _types;
        private Dictionary<AmmoType, List<Projectile>> _instances = new Dictionary<AmmoType, List<Projectile>>();

        private void Start()
        {
            // TODO: Make this more general to work on all different types
            // Make DEFAULT instances
            _instances[AmmoType.DEFAULT] = new List<Projectile>();
            var type = _types.Find(t => t.type == AmmoType.DEFAULT);

            _instances[AmmoType.DEFAULT_AI] = new List<Projectile>();
            var type2 = _types.Find(t => t.type == AmmoType.DEFAULT_AI);

            _instances[AmmoType.ROCKET] = new List<Projectile>();
            var type3 = _types.Find(t => t.type == AmmoType.ROCKET);

            Expand(AmmoType.DEFAULT, type, 100);
            Expand(AmmoType.DEFAULT_AI, type2, 200);
            Expand(AmmoType.ROCKET, type3, 16);
        }

        private int _currProjectile = 0;
        public Projectile GetProjectile(AmmoType ammoType)
        {
            _currProjectile = (_currProjectile + 1) % _instances[ammoType].Count;

            // We probably need more ammo
            if (_instances[ammoType][_currProjectile].IsActive)
            {
                var type = _types.Find(t => t.type == AmmoType.DEFAULT);
                Expand(ammoType, type, 10);

                // Find next inactive
                bool found = false;
                while (!found)
                {
                    _currProjectile = (_currProjectile + 1) % _instances[ammoType].Count;
                    if (!_instances[ammoType][_currProjectile].IsActive)
                    {
                        found = true;
                    }

                }
            }

            return _instances[ammoType][_currProjectile];
        }

        private void Expand(AmmoType ammoType, ProjectileType projType, int ammount)
        {
            for (int i = 0; i < ammount; i++)
            {
                Projectile prj = Instantiate(projType.prefab).GetComponent<Projectile>();
                prj.IsActive = false;

                _instances[ammoType].Add(prj);
            }
        }
    }
}