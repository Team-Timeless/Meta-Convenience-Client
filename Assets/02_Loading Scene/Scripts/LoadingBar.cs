using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    public static string nextScene;

    [SerializeField]
    Image loadingBar;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadScene());
    }

    /**
     * @brief LoadingScene ȣ��
     * @param string sceneName �ҷ��� scene �̸�
     */
    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("Loading Scene");
    }

    /**
     * @brief Loading Bar ����
     */
    IEnumerator LoadScene()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;
        float timer = 0.0f;

        while (!op.isDone)
        {
            yield return null;

            if (op.progress < 0.9f)
            {
                loadingBar.fillAmount = op.progress;
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                loadingBar.fillAmount = Mathf.Lerp(0.9f, 1.0f, timer);
                if (loadingBar.fillAmount >= 1.0f)
                {
                    NetworkMng.I.ConnectToServer();
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
