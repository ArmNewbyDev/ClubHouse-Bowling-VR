using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MyPlatform;

public class SceneTransitionManager : MonoBehaviour
{
    public FadeScreen fadeScreen;

    public void GoToScene(int sceneIndex)
    {
        StartCoroutine(GoToSceneAsyncRoutine(sceneIndex));
    }

    IEnumerator GoToSceneAsyncRoutine(int sceneIndex)
    {
        fadeScreen.FadeOut();
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        operation.allowSceneActivation = false;
        
        float timer = 0f;
        while (timer <= fadeScreen.fadeDuration && !operation.isDone)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        //Launch the new scene
        operation.allowSceneActivation = true;
    }
}
