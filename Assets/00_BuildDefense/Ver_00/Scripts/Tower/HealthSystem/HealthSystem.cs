using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lam.DefenderBuilder
{
    public class HealthSystem : MonoBehaviour
    {
        private int healthAmount;
        private int healthMax;

        public System.Action OnDamage;
        public System.Action OnDie;


        public void SetHealthMax(int healthMax)
        {
            this.healthMax = healthMax;
            healthAmount = healthMax;
        }

        public void Damage(int damage)
        {
            healthAmount -= damage;
            healthAmount = Mathf.Clamp(healthAmount, 0, healthMax);

            OnDamage?.Invoke();

            if (IsDead())
            {
                OnDie?.Invoke();
            }
        }

        public bool IsDead()
        {
            return healthAmount <= 0;
        }

        public int GetHealthAmount()
        {
            return healthAmount;
        }

        public float GetNormalizedHealthAmount()
        {
            return (float)healthAmount / healthMax;
        }
    }
}

