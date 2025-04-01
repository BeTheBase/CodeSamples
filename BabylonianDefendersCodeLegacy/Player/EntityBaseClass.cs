using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Babylonian
{
    public abstract class EntityBaseClass : MonoBehaviour
    {
        public Buttons[] InputButtons;
        public MonoBehaviour[] DissableScripts;

        protected Rigidbody2D Body2D;

        protected CollisionState CollisionState;

        protected InputState InputState;

        protected AudioManager AudioManager;

        protected virtual void Awake()
        {
            Body2D = GetComponent<Rigidbody2D>();

            InputState = GetComponent<InputState>();

            CollisionState = GetComponent<CollisionState>();
        }

        public virtual void ToggleScripts(bool value)
        {
            foreach(var script in DissableScripts)
            {
                script.enabled = value;
            }
        }
    }
}
