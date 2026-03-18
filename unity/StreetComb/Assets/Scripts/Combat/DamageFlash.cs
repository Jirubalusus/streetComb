using System.Collections;
using UnityEngine;

namespace StreetComb.Combat
{
    [RequireComponent(typeof(Renderer))]
    public class DamageFlash : MonoBehaviour
    {
        [SerializeField] private Color flashColor = Color.white;
        [SerializeField] private float flashDuration = 0.08f;

        private Renderer _renderer;
        private Color _baseColor;
        private Coroutine _flashRoutine;

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
            _baseColor = _renderer.material.color;
        }

        public void RefreshBaseColor()
        {
            if (_renderer == null)
            {
                _renderer = GetComponent<Renderer>();
            }

            _baseColor = _renderer.material.color;
        }

        public void Flash()
        {
            if (_flashRoutine != null)
            {
                StopCoroutine(_flashRoutine);
            }

            _flashRoutine = StartCoroutine(FlashRoutine());
        }

        private IEnumerator FlashRoutine()
        {
            _renderer.material.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            _renderer.material.color = _baseColor;
            _flashRoutine = null;
        }
    }
}
