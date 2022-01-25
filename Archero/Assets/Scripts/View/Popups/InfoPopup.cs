using Core.PopupSystem;
using TMPro;
using UnityEngine;

namespace View.Popups
{
    public class InfoPopup : Popup
    {
        [SerializeField] private TextMeshProUGUI _infoText;

        public InfoPopup SetInfo(string info)
        {
            _infoText.text = info;
            return this;
        }
        
        public override void EnableInput()
        {
        }

        public override void DisableInput()
        {
        }
    }
}