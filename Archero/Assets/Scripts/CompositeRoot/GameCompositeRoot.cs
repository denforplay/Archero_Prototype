using System;
using Core.PopupSystem;
using Cysharp.Threading.Tasks;
using UnityEngine;
using View.Popups;

namespace CompositeRoot
{
    public class GameCompositeRoot : CompositeRoot
    {
        [SerializeField] private GamePopup _gamePopup;
        [SerializeField] private PopupSystem _popupSystem;
        [SerializeField] private HeroCompositeRoot _heroRoot;
        [SerializeField] private EnemiesCompositeRoot _enemyRoot;
        private bool _isRestarted;
        
        public override void Compose()
        {
            _heroRoot.Model.OnHealthChanged += CheckIfHeroIsAlive;
            _enemyRoot.OnRestartEvent += RestartGame;
            _gamePopup.OnPauseButtonEvent += PauseGame;
            StartGame();
        }
        
        private async void StartGame()
        {
            var popup = _popupSystem.SpawnPopup<InfoPopup>();
            for (int i = 1; i <= 3; i++)
            {
                popup.SetInfo(i.ToString());
                await UniTask.Delay(TimeSpan.FromSeconds(i));
            }

            _popupSystem.DeletePopUp();
            _heroRoot.StartRoot();
            _enemyRoot.StartRoot();
        }

        private void PauseGame()
        {
            Time.timeScale = 0;
            var popup = _popupSystem.SpawnPopup<PausePopup>();
            popup.Closing += _ => Time.timeScale = 1;
            popup.OnRestartEvent += RestartGame;
        }

        private void RestartGame()
        {
            _heroRoot.RestartHero();
            _enemyRoot.Restart();
        }
        
        private void CheckIfHeroIsAlive(int health)
        {
            if (health < 0 && !_isRestarted)
            {
                _isRestarted = true;
                var popup = _popupSystem.SpawnPopup<LosePopup>();
                popup.OnRestartEvent += () =>
                {
                    _popupSystem.DeletePopUp();
                    RestartGame();
                };
            }
        }
    }
}