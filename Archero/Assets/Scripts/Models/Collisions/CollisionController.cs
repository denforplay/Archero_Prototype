using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Interfaces;

namespace Models.Collisions
{
    public class CollisionController
    {
        private Collisions _collisions = new Collisions();
        private readonly Func<IEnumerable<IRecord>> _startCollideRecordsProvider;
        private readonly Func<IEnumerable<IRecord>> _endCollideRecordsProvider;

        public CollisionController(Func<IEnumerable<IRecord>> startCollideRecordsProvider, Func<IEnumerable<IRecord>> endCollideRecordsProvider)
        {
            _startCollideRecordsProvider = startCollideRecordsProvider;
            _endCollideRecordsProvider = endCollideRecordsProvider;
        }

        public void TryCollide((object, object) pair)
        {
            IEnumerable<IRecord> records = _startCollideRecordsProvider?.Invoke().Where(record => record.IsTarget(pair));

            foreach (var record in records)
                ((dynamic)record).Do((dynamic)pair.Item1, (dynamic)pair.Item2);
        }

        public void TryEndCollide((object, object) pair)
        {
            IEnumerable<IRecord> records = _endCollideRecordsProvider?.Invoke().Where(record => record.IsTarget(pair));

            foreach (var record in records)
                ((dynamic)record).Do((dynamic)pair.Item1, (dynamic)pair.Item2);
        }
    }
}