using System;
using Core.PopupSystem;
using UnityEngine;
using UnityEngine.UI;

namespace View.Popups
{
    public class LosePopup : Popup
    {
        public event Action OnRestartEvent;
        
        [SerializeField] private Button _restartButton;

        private void Start()
        {
            _restartButton.onClick.AddListener(() => OnRestartEvent?.Invoke());
        }

        public override void EnableInput()
        {
            _restartButton.interactable = true;
        }

        public override void DisableInput()
        {
            _restartButton.interactable = false;
        }
    }
}