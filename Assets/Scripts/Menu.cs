using Managers;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private string gameplaySceneKeyCode;
    [SerializeField] private Button playButton;

    private void Start()
    {
        playButton.onClick.AddListener(LoadGameplayScene);
    }

    private void LoadGameplayScene()
    {
        LoadingScreenManager.Instance.LoadScene(gameplaySceneKeyCode);
    }
}
