using System;
using UnityEngine;

namespace Objects
{
    public class Lever : Interactable
    {
        [Header("Components")]
        [SerializeField] private GameObject interacted;
        [SerializeField] private GameObject unInteracted;
        [SerializeField] private Switch puzzleSwitch;
        
        private bool isComplete;

        public event Action OnLeverInteracted;
        public event Action OnLeverComplete;

        protected override void Start()
        {
            base.Start();
            puzzleSwitch.OnCorrectInteraction += EvaluateInteraction;
        }

        protected override void HandleInteraction()
        {
            if (!interactionEnabled) return;
            if (isComplete) return;
            
            base.HandleInteraction();
            interacted.SetActive(true);
            unInteracted.SetActive(false);
            OnLeverInteracted?.Invoke();
        }

        private void EvaluateInteraction(bool interactionSucceeded)
        {
            if (interactionSucceeded) CompleteInteraction();
            else ResetInteraction();
        }

        private void ResetInteraction()
        {
            interacted.SetActive(false);
            unInteracted.SetActive(true);
        }

        private void CompleteInteraction()
        {
            isComplete = true;
            OnLeverComplete?.Invoke();
        }
    }
}
