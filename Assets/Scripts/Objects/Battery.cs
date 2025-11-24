using System;
using UnityEngine;

namespace Objects
{
    public class Battery : Interactable
    {
        [Header("Battery Settings")]
        [SerializeField] private int batteryID;
        
        [Header("Battery Components")]
        [SerializeField] private GameObject batteryLight;

        private bool isComplete;
        private bool isHandled;
        
        public int BatteryID => batteryID;
        public bool IsComplete => isComplete;
        
        public event Action<int> OnBatteryTurnedOn;
        
        protected override void HandleInteraction()
        {
            if (!interactionEnabled) return;
            if (isComplete) return;
            if (isHandled) return;
            
            base.HandleInteraction();
            isHandled = true;
            batteryLight.SetActive(true);
            OnBatteryTurnedOn?.Invoke(batteryID);
        }

        public void DeactivateBattery()
        {
            batteryLight.SetActive(false);
            isHandled = false;
        }

        public void CompleteBattery()
        {
            isComplete = true;
        }
    }
}
