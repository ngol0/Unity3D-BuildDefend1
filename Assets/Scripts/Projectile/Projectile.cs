using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lam.DefenderBuilder
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] int damagePoint;

        private void OnParticleCollision(GameObject other)
        {
            //Debug.Log(":::" + other.name + "is hit");
            var enemyHealth = other.GetComponent<HealthSystem>();
            if (enemyHealth == null) return;
            enemyHealth.Damage(damagePoint);
        }
    }
}

