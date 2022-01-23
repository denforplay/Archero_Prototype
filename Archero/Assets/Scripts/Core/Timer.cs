using System;

namespace Core
{
    public sealed class Timer
    {
        public event Action OnTimerEndEvent;
        private readonly float _time;
        private float _spentTime;
        private bool _isGoing;

        public bool IsCompleted => _spentTime >= _time;
        
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

        private void Stop()
        {
            _isGoing = false;
            OnTimerEndEvent?.Invoke();
        }
    }
}