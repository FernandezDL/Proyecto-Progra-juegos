using System;
using DG.Tweening;
using UnityEngine;

namespace Objects
{
    public class Guard : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float moveDuration = 2f;
        [SerializeField] private float rotateDuration = 0.5f;
        
        [Header("Components")]
        [SerializeField] private Transform targetTransform;
        [SerializeField] private Transform orgTransform;

        private void Start()
        {
            var origin = transform.position;
            var patrolSequence = DOTween.Sequence();

            patrolSequence
                .Append(transform.DOMove(targetTransform.position, moveDuration).SetEase(Ease.Linear))
                .Append(transform.DORotate(new Vector3(0, 180, 0), rotateDuration, RotateMode.LocalAxisAdd))
                .Append(transform.DOMove(origin, moveDuration).SetEase(Ease.Linear))
                .Append(transform.DORotate(new Vector3(0, 180, 0), rotateDuration, RotateMode.LocalAxisAdd))
                .SetLoops(-1, LoopType.Restart);

            patrolSequence.Play();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                var cc = other.GetComponent<CharacterController>();
                if (cc != null) cc.enabled = false;

                other.transform.position = orgTransform.position;

                if (cc != null) cc.enabled = true;
            }
        }
    }
}
