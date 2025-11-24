using System;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class PauseManager : BaseManager<PauseManager>
    {
        [Header("Components")]
        [SerializeField] private GameObject pauseWidget;
        
        [Header("Buttons")]
        [SerializeField] private Button goHomeButton;
        [SerializeField] private Button restartLevelButton;
        [SerializeField] private Button resumeButton;
        

        private void Awake()
        {
            GameManager.Instance.PlayerController.OnPauseInput += () =>
            {
                enabled = true;
            };
            
            enabled = false;
        }

        private void OnEnable()
        {
            pauseWidget.SetActive(true);
            
            goHomeButton.onClick.AddListener(LoadMainLevel);
            restartLevelButton.onClick.AddListener(RestartLevel);
            resumeButton.onClick.AddListener(ResumeGame);
            
            Time.timeScale = 0f;
        }

        private void OnDisable()
        {
            pauseWidget.SetActive(false);
            
            goHomeButton.onClick.RemoveAllListeners();
            restartLevelButton.onClick.RemoveAllListeners();
            resumeButton.onClick.RemoveAllListeners();
            
            Time.timeScale = 1f;
        }

        private void LoadMainLevel()
        {
            LoadingScreenManager.Instance.LoadScene(GameManager.Instance.MainSceneKeyCode);
        }

        private void RestartLevel()
        {
            LoadingScreenManager.Instance.LoadScene(GameManager.Instance.LevelKeyCode);
        }

        private void ResumeGame()
        {
            enabled = false;
        }
    }
}
