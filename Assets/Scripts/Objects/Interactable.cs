using DG.Tweening;
using Managers;
using UnityEngine;

namespace Objects
{
    public abstract class Interactable : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private CanvasGroup interactionCanvas;
        
        protected bool interactionEnabled;
        private Tween fadeTween;
        
        public bool InteractionEnabled => interactionEnabled;

        protected virtual void Start()
        {
            GameManager.Instance.PlayerController.OnInteractionInput += HandleInteraction;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            
            interactionEnabled = true;
            DisplayInteractionCanvas();
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            
            interactionEnabled = false;
            HideInteractionCanvas();
        }

        private void DisplayInteractionCanvas()
        {
            if (fadeTween != null && fadeTween.IsPlaying()) fadeTween.Kill();
            fadeTween = interactionCanvas.DOFade(1, 0.5f);
        }

        private void HideInteractionCanvas()
        {
            if (fadeTween != null && fadeTween.IsPlaying()) fadeTween.Kill();
            fadeTween = interactionCanvas.DOFade(0, 0.25f);
        }

        protected virtual void HandleInteraction()
        {
            if (!interactionEnabled) return;
            
            interactionEnabled = false;
            HideInteractionCanvas();
        }
    }
}
