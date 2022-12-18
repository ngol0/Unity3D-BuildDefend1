using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lam.DefenderBuilder
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] Slider healthSlider;

        public void UpdateHealthbarUI(float value)
        {
            healthSlider.value = value;
        }
    }
}

