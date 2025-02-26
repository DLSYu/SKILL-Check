using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{


    [SerializeField]
    private GameObject loadingSlider;

    private float fadeDuration = 1.0f;

    [SerializeField]
    private Sprite[] sprites;
	private int framesPerSprite = 4;
	private bool loop = true;
	private bool destroyOnEnd = false;

	private int index = 0;

    [SerializeField]
	private Image fairyImage;
	private int frame = 0;

    void FixedUpdate () {

    
            if (!loop && index == sprites.Length) return;
            frame++;

            if (frame < framesPerSprite) return;

            fairyImage.sprite = sprites [index];

            frame = 0;
            index++;

            if (index >= sprites.Length) {
                if (loop) index = 0;
                if (destroyOnEnd) Destroy (gameObject);
            }
        
	}
    public void LoadScene(string sceneToLoad)
    {
        if (!this.gameObject.activeSelf)
        {
            this.gameObject.SetActive(true);
           
        }
        this.gameObject.GetComponent<Canvas>().sortingOrder = 999;
        StartCoroutine(LoadSceneAsync(sceneToLoad));
    }

    IEnumerator LoadSceneAsync(string sceneToLoad)
    {
        
        yield return StartCoroutine(FadeInAndOut(true, fadeDuration));
        // Loading operation
        AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(sceneToLoad);

        while (!loadingOperation.isDone)
        {
            float progressVal = Mathf.Clamp01(loadingOperation.progress / 0.9f);
            loadingSlider.GetComponent<Slider>().value = progressVal;
            Debug.Log(loadingOperation.progress);
            yield return null;
        }

        yield return StartCoroutine(FadeInAndOut(false, fadeDuration));

    }

   IEnumerator FadeInAndOut(bool fadeIn, float duration)
{
    float counter = 0f;

    //Set Values depending on if fadeIn or fadeOut
    float a, b;
    if (fadeIn)
    {
        a = 0;
        b = 1;
    }
    else
    {
        a = 1;
        b = 0;
    }


    Color currentColor = Color.clear;

    CanvasGroup canvasGroup = this.GetComponent<CanvasGroup>();


    
    while (counter < duration)
    {
        counter += Time.deltaTime;
        float alpha = Mathf.Lerp(a, b, counter / duration);
        canvasGroup.alpha = alpha;

        yield return null;
    }
}


}
