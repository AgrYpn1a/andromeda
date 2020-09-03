using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Andromeda.Ship
{
    [CreateAssetMenu(fileName = "Actor", menuName = "Andromeda/Ships/ActorConfig", order = 1)]
    public class ActorConfig : ScriptableObject
    {
        public int HitPoints;
        public float ThrustPower;
        public float RotationCoef;
    }

    public class Actor
    {
        public int HitPoints;
        public float ThrustPower;
        public float RotationCoef;

        public Actor(ActorConfig config)
        {
            HitPoints = config.HitPoints;
            ThrustPower = config.ThrustPower;
            RotationCoef = config.RotationCoef;
        }
    }
}
