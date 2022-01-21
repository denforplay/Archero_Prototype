using System;

namespace Core
{
    public sealed class Timer
    {
        public event Action OnTimerEndEvent;
        private float _time;
        private float _spentTime;
        private bool _isGoing;

        public float SpentTime => _spentTime;
        
        public Timer(float time)
        {
            _time = time;
        }

        public void Start()
        {
            if (_time > 0)
                _isGoing = true;
        }

        public void Tick(float deltaTime)
        {
            if (_isGoing)
            {
                _spentTime += deltaTime;
                if (_spentTime >= _time)
                {
                    Stop();
                }
            }
        }

        public void Stop()
        {
            _isGoing = false;
        }
    }
}