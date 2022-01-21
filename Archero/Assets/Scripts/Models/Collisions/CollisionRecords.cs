using System;
using System.Collections.Generic;
using Core;
using Core.Interfaces;

namespace Models.Collisions
{
    public class CollisionRecords
    {
        public IEnumerable<IRecord> StartCollideValues()
        {
            yield return IfCollided((Barrier barrier, Hero hero) =>
            {
            });
        }

        public IEnumerable<IRecord> EndCollideValues()
        {
            yield return IfCollided((Barrier barrier, Hero hero) =>
            {
            });
        }

        private IRecord IfCollided<T1, T2>(Action<T1, T2> action)
        {
            return new Record<T1, T2>(action);
        }
    }
}