using System;
using Core.PopupSystem;
using UnityEngine;
using UnityEngine.UI;

namespace View.Popups
{
    public class PausePopup : Popup
    {
        public event Action OnRestartEvent;
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _restartButton;

        private void Start()
        {
            _continueButton.onClick.AddListener(OnClosing);
            _restartButton.onClick.AddListener(() => OnRestartEvent?.Invoke());
            _restartButton.onClick.AddListener(OnClosing);
        }

        public override void EnableInput()
        {
            _restartButton.interactable = true;
            _continueButton.interactable = true;
        }

        public override void DisableInput()
        {
            _restartButton.interactable = false;
            _continueButton.interactable = false;
        }
    }
}