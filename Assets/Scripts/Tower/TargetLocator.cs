using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Lam.DefenderBuilder.Enemies;

namespace Lam.DefenderBuilder.Tower
{
    public class TargetLocator : MonoBehaviour
    {
        [SerializeField] Transform weapon;
        [SerializeField] ParticleSystem projectileParticle;
        [SerializeField] float targetRange = 15f;
        Transform target = null;

        void Update()
        {
            FindClosestTarget();
            AimWeapon();
        }

        private void FindClosestTarget()
        {
            Enemy[] enemies = FindObjectsOfType<Enemy>();
            if (enemies.Length == 0) return;
            Enemy closestEnemy = null;
            float maxDistance = Mathf.Infinity;

            foreach (var enemy in enemies)
            {
                float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);

                if ((targetDistance - maxDistance) < 0.1f)
                {
                    closestEnemy = enemy;
                    maxDistance = targetDistance;
                }
            }

            target = closestEnemy.transform;
        }

        private void AimWeapon()
        {
            if (target == null) 
            {
                ToggleAttackMode(false);
                return;
            }

            float aimDistance = Vector3.Distance(transform.position, target.position);

            weapon.LookAt(target);

            if (aimDistance < targetRange)
            {
                ToggleAttackMode(true);
            }
            else
            {
                ToggleAttackMode(false);
            }
        }

        private void ToggleAttackMode(bool canAttack)
        {
            var emission = projectileParticle.emission;
            emission.enabled = canAttack;
        }
    }
}

