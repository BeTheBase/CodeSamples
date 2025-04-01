using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Babylonian
{
    public class PlayerBase : EntityBaseClass
    {
        [SerializeField] public SPUM_Prefabs SpumAnimator;

        public delegate void OnPlayerAttackHandler(float angle);
        public OnPlayerAttackHandler OnPlayerAttack;
    }
}