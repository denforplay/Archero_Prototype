using Models.MainHero;
using TMPro;
using UnityEngine;

namespace View
{
    public class CoinsView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _coinsText;
        
        private Hero _hero;

        public void Initialize(Hero hero)
        {
            _hero = hero;
            hero.OnCoinsChanged += SetCoinsText;
        }

        private void SetCoinsText(int coins)
        {
            _coinsText.text = coins.ToString();
        }
    }
}