using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class GameOverManager : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private GameObject gameOverWidget;
        
        [Header("Buttons")]
        [SerializeField] private Button goHomeButton;
        [SerializeField] private Button restartLevelButton;

        private void Awake()
        {
            GameManager.Instance.OnGameOver += () =>
            {
                enabled = true;
            };

            enabled = false;
        }
        
        private void OnEnable()
        {
            gameOverWidget.SetActive(true);
            
            goHomeButton.onClick.AddListener(LoadMainLevel);
            restartLevelButton.onClick.AddListener(RestartLevel);
            
            Time.timeScale = 0f;
        }

        private void LoadMainLevel()
        {
            LoadingScreenManager.Instance.LoadScene(GameManager.Instance.MainSceneKeyCode);
        }

        private void RestartLevel()
        {
            LoadingScreenManager.Instance.LoadScene(GameManager.Instance.LevelKeyCode);
        }
    }
}
