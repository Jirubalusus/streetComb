using UnityEngine;

namespace StreetComb.Combat
{
    public class EnergyComponent : MonoBehaviour
    {
        [SerializeField] private float maxEnergy = 100f;
        public float CurrentEnergy { get; private set; }

        private void Awake()
        {
            CurrentEnergy = 0f;
        }

        public void AddEnergy(float amount)
        {
            CurrentEnergy = Mathf.Clamp(CurrentEnergy + amount, 0f, maxEnergy);
        }

        public bool Consume(float amount)
        {
            if (CurrentEnergy < amount)
            {
                return false;
            }

            CurrentEnergy -= amount;
            return true;
        }
    }
}
