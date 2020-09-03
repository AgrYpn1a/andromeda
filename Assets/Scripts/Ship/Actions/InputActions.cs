using Andromeda.Ship.Ammo;
using UnityEngine;

namespace Andromeda.Ship.Actions
{
    public sealed class Thrust : ACommand
    {
        public Thrust() : base(true) { }

        public override void Execute(ShipController ship, Actor actor)
        {
            ship.AddThrust(ship.transform.up, actor.ThrustPower);
        }
    }

    public sealed class Rotate : ACommand
    {
        private float _direction;
        public Rotate(float direction) : base(false)
        {
            _direction = direction;
        }

        public override void Execute(ShipController ship, Actor actor)
        {
            ship.AddRotation(_direction * actor.RotationCoef);
        }
    }

    public sealed class Shoot : ACommand
    {
        private Vector2[] _guns;
        private AmmoType _ammoType;

        public Shoot(AmmoType ammoType, Vector2[] guns) : base(false) { _ammoType = ammoType; _guns = guns; }

        public override void Execute(ShipController ship, Actor actor)
        {
            if (State == KeyState.PRESSED)
            {
                for (int i = 0; i < _guns.Length; i++)
                {
                    Projectile proj = AmmoPool.Instance.GetProjectile(_ammoType);
                    proj.transform.rotation = ship.transform.rotation;
                    proj.transform.position = ship.transform.position + ship.transform.TransformDirection(_guns[i]);

                    Vector2 shootDir = ship.transform.up;
                    proj.ShootAt(shootDir);
                }
            }
        }
    }

    public sealed class AIShoot : ACommand
    {
        private Vector2[] _guns;
        private AmmoType _ammoType;

        public AIShoot(AmmoType ammoType, Vector2[] guns) : base(false) { _ammoType = ammoType; _guns = guns; }

        public override void Execute(ShipController ship, Actor actor)
        {
            for (int i = 0; i < _guns.Length; i++)
            {
                Projectile proj = AmmoPool.Instance.GetProjectile(_ammoType);
                proj.transform.rotation = ship.transform.rotation;
                proj.transform.position = ship.transform.position + ship.transform.TransformDirection(_guns[i]);

                Vector2 shootDir = ship.transform.up;
                proj.ShootAt(shootDir);
            }

        }
    }

}