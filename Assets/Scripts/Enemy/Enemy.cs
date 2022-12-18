using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lam.DefenderBuilder.Enemies
{
    public class Enemy : MonoBehaviour
    {
        [Header("Ref")]
        [SerializeField] HealthSystem healthSystem;

        [Header("Stats")]
        [SerializeField] int healthMax = 90;
        [SerializeField] int difficultyRamp = 5;
        [SerializeField] float difficultSpeed = 0.001f;
        [SerializeField] int hitPoint = 20;
        [SerializeField][Range(0f, 5f)] float speed = 1f; //prevent spped variable to get negative values

        public float Speed => speed;
        public int HitPoint => hitPoint;

        private void OnEnable() 
        {
            healthSystem.SetHealthMax(healthMax);

            healthSystem.OnDie += OnDisappear;
        }

        private void OnDisable() 
        {
            healthSystem.OnDie -= OnDisappear;
        }

        private void OnDisappear()
        {
            healthMax += difficultyRamp;
            speed += difficultSpeed;

            gameObject.SetActive(false);
            GameManager.Instance.OnScore();
        }
    }
}

