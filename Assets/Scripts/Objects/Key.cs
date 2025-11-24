using System;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

namespace Objects
{
    public class Key : Interactable
    {
        [Header("Key")]
        [SerializeField] private int keyIndex;

        [Header("Settings")] 
        [SerializeField] private bool rotate;
        [ShowIf("rotate")] [SerializeField] private float rotationDuration = 5f;

        public event Action<int> OnKeyPickedUp;

        protected override void Start()
        {
            base.Start();

            if (rotate)
            {
                transform
                    .DORotate(new Vector3(0, 360, 0), rotationDuration, RotateMode.FastBeyond360)
                    .SetEase(Ease.Linear)
                    .SetLoops(-1, LoopType.Incremental);   
            }
        }

        protected override void HandleInteraction()
        {
            if (!interactionEnabled) return;
            base.HandleInteraction();
            
            OnKeyPickedUp?.Invoke(keyIndex);
            HUD.Instance.FillIcon(keyIndex);
            Destroy(gameObject);
        }
    }
}
