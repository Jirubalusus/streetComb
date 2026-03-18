using System;
using UnityEngine;

namespace StreetComb.Combat
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private float maxHealth = 100f;

        public event Action<float, float> OnHealthChanged;
        public event Action OnKO;

        public float CurrentHealth { get; private set; }
        public float MaxHealth => maxHealth;

        private void Awake()
        {
            CurrentHealth = maxHealth;
        }

        public void ResetHealth()
        {
            CurrentHealth = maxHealth;
            OnHealthChanged?.Invoke(CurrentHealth, maxHealth);
        }

        public void ApplyDamage(float amount)
        {
            if (CurrentHealth <= 0f) return;

            CurrentHealth = Mathf.Max(0f, CurrentHealth - amount);
            OnHealthChanged?.Invoke(CurrentHealth, maxHealth);

            if (CurrentHealth <= 0f)
            {
                OnKO?.Invoke();
            }
        }
    }
}
