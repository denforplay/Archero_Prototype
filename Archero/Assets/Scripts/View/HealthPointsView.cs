using System;
using Core.Interfaces;
using UnityEngine;

namespace View
{
    public class HealthPointsView : MonoBehaviour
    {
        private const string HEALTH_TEMPLATE = "{0}/{1}";
        [SerializeField] private GameObject _objectForHp;
        [SerializeField] private TextMesh _healthText;
        private IHealthable _healthObject;

        public void Initialize(IHealthable healthObject)
        {
            _healthObject = healthObject;
            SetText();
            _healthObject.OnHealthChanged += _ => SetText();
        }

        private void Update()
        {
            _healthText.transform.position = _objectForHp.transform.position;
        }

        private void SetText()
        {
            _healthText.text = String.Format(HEALTH_TEMPLATE, _healthObject.CurrentHealthPoints, _healthObject.MaxHealthPoint);
        }
    }
}