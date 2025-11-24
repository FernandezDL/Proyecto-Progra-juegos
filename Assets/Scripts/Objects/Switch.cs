using System;
using DG.Tweening;
using UnityEngine;

namespace Objects
{
    public class Switch : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Lever lever;
        [SerializeField] private float movementDuration = 5f;
        [SerializeField] private Transform endPos;
        
        private bool connected;
        private Tween movementTween;
        
        public event Action<bool> OnCorrectInteraction;

        private void Start()
        {
            lever.OnLeverInteracted += EvaluateSuccess;
            
            movementTween = transform.DOMove(endPos.position, movementDuration)
                .SetLoops(-1, LoopType.Yoyo);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Connection"))
            {
                connected = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Connection"))
            {
                connected = false;
            }
        }

        private void EvaluateSuccess()
        {
            OnCorrectInteraction?.Invoke(connected);
            if (connected) movementTween.Pause();
        }
    }
}
