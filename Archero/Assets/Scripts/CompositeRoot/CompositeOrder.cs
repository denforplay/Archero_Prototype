using System;
using System.Collections.Generic;
using UnityEngine;

namespace CompositeRoot
{
    public class CompositeOrder : MonoBehaviour
    {
        [SerializeField] private List<CompositeRoot> _compositeRoots;

        public void Start()
        {
            for (int i = 0; i < _compositeRoots.Count; i++)
            {
                _compositeRoots[i].Compose();
                _compositeRoots[i].enabled = true;
            }
        }
    }
}