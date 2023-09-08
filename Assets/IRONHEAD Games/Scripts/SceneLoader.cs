using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public OVROverlay overlay_bg;
    public OVROverlay overlay_LoadingText;
    public static SceneLoader instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(ShowOverlayAndLoad(sceneName));
    }

    IEnumerator ShowOverlayAndLoad(string sceneName)
    {
        overlay_bg.enabled = true;
        overlay_LoadingText.enabled = true;

        GameObject centerEye = GameObject.Find("CenterEyeAnchor");
        overlay_LoadingText.gameObject.transform.position = centerEye.transform.position + new Vector3(0, 0, 3f);
        
        
        yield return new WaitForSeconds(5f);

        AsyncOperation asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        overlay_bg.enabled = false;
        overlay_LoadingText.enabled = false;

        yield return null;
    }
}
