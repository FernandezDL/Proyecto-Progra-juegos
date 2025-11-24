using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Managers
{
    public class LoadingScreenManager : PersistentBaseManager<LoadingScreenManager>
    {
                [SerializeField] private GameObject loadingScreenPrefab;
                [SerializeField] private TMP_Text loadingScreenText;
                [SerializeField] private Slider loadingScreenImage;
        
                private bool bIsLoading;
        
                public async void LoadScene(string sceneName)
                {
                    bIsLoading = true;
                    loadingScreenImage.value = 0;
                    
                    StartCoroutine(CouLoadingText());
                    
                    var scene = SceneManager.LoadSceneAsync(sceneName);
                    if (scene != null)
                    {
                        scene.allowSceneActivation = false;
        
                        loadingScreenPrefab.SetActive(true);
                        do
                        {
                            await Task.Delay(100);
                            loadingScreenImage.value = scene.progress;
                        } while (scene.progress < 0.9f);
        
                        await Task.Delay(750);
                        
                        Time.timeScale = 1f;
                        
                        scene.allowSceneActivation = true;
                        
                        await Task.Delay(750);
                        loadingScreenPrefab.SetActive(false);
                        bIsLoading = false;
                    }
                }
        
                private IEnumerator CouLoadingText()
                {
                    int dotCount = 0;
                    while (bIsLoading)
                    {
                        dotCount = (dotCount % 3) + 1; // 1 → 2 → 3 → 1 ...
                        loadingScreenText.text = "Cargando" + new string('.', dotCount);
                        yield return new WaitForSecondsRealtime(0.5f); // adjust delay here
                    }
                    loadingScreenText.text = "";
                }
    }
}
