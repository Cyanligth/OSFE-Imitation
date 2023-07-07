using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class SceneManager : MonoBehaviour
{
    private BaseScene curScene;
    LoadingUI ui;

    public BaseScene CurScene
    {
        get
        {
            if (curScene == null)
                curScene = GameObject.FindObjectOfType<BaseScene>();
            return curScene;
        }
    }
    private void Awake()
    {
        ui = GameManager.Resource.Instantiate<LoadingUI>("UI/LoadingUI");
        ui.transform.SetParent(transform);
    }
    public void LoadScene(string name)
    {
        StartCoroutine(Loading(name));
    }

    IEnumerator Loading(string name)
    {
        GameManager.Sound.Clear();
        ui.FadeOut();
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 0;

        AsyncOperation oper = UnitySceneManager.LoadSceneAsync(name);
        // oper.allowSceneActivation = false;
        while (!oper.isDone)
        {
            ui.SetProgress(Mathf.Lerp(0, 0.5f, oper.progress));
            yield return null;
        }

        CurScene.LoadAsync();
        while (curScene.progress < 1)
        {
            ui.SetProgress(Mathf.Lerp(0.5f, 1, curScene.progress));
            yield return null;
        }

        Time.timeScale = 1;
        ui.FadeIn();
        yield return new WaitForSecondsRealtime(0.5f);
    }
}
