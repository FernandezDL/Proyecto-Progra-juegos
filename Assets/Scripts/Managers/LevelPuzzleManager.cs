using System;
using Objects;
using UnityEngine;

namespace Managers
{
    public class LevelPuzzleManager : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private GameObject completeLight;
        [SerializeField] private GameObject cristal;
        [SerializeField] private Lever lever;

        private void Start()
        {
            lever.OnLeverComplete += CompleteInteraction;
        }

        private void CompleteInteraction()
        {
            completeLight.SetActive(true); 
            cristal.SetActive(false);
        }
    }
}
