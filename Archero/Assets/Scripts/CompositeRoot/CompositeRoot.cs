using System;
using UnityEngine;

namespace CompositeRoot
{
    public abstract class CompositeRoot : MonoBehaviour
    {
        private void Start()
        {
            Compose();
        }

        public abstract void Compose();
    }
}