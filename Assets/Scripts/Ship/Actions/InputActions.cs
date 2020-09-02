using Andromeda.Ship.Ammo;
using UnityEngine;

namespace Andromeda.Ship.Actions
{
    public sealed class Thrust : ACommand
    {
        public Thrust() : base(true) { }

        public override void Execute(ShipController ship, Actor actor)
        {
            ship.AddThrust(ship.transform.up, 15f);
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
            ship.AddRotation(_direction * 65f);
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
                Projectile proj = AmmoPool.Instance.GetProjectile(_ammoType);
                proj.transform.rotation = ship.transform.rotation;
                proj.transform.position = ship.transform.position + ship.transform.TransformDirection(_guns[0]);

                Vector2 shootDir = ship.transform.up;
                proj.ShootAt(shootDir);
            }
        }
    }

}