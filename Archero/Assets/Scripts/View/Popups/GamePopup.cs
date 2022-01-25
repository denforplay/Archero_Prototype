using System;
using Core.PopupSystem;
using UnityEngine;
using UnityEngine.UI;

namespace View.Popups
{
    public class GamePopup : Popup
    {
        public event Action OnPauseButtonEvent;
        
        [SerializeField] private Button _pauseButton;

        private void Start()
        {
            _pauseButton.onClick.AddListener(() => OnPauseButtonEvent?.Invoke());
        }

        public override void EnableInput()
        {
            _pauseButton.interactable = true;
        }

        public override void DisableInput()
        {
            _pauseButton.interactable = false;
        }
    }
}