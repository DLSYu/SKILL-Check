using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CutscenePlayer : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private LoadingScreen loadingScreen;
    [Header("UI References")]
    [SerializeField] private Image displayImage;

    [Header("Animation Data")]
    [SerializeField] private List<Sprite> animationFrames;
    [SerializeField] private List<float> animationDuration;
    [SerializeField] private List<GameObject> subtitle;
    [SerializeField] private List<float> subtitleStartTime;
    [SerializeField] private List<float> subtitleEndTime;

    bool isSubtitleDone = false;
    bool isMovieDone = false;

    bool hasUserTapped = false;


    private void Start()
    {
        StartCoroutine(PlayCutscene());
        StartCoroutine(LoadTitleScreenOnceReady());

    }

    void Update()
    {
        if (Input.anyKeyDown)
            hasUserTapped = true;
    }

    private IEnumerator LoadTitleScreenOnceReady()
    {

        yield return new WaitUntil(() => (isSubtitleDone && isMovieDone) || hasUserTapped);
        loadingScreen.LoadScene("TitleScreen");
    }

    private IEnumerator PlayCutscene()
    {
        float currentTime = 0f;
        float checkInterval = 0.05f; // check every 50ms for accuracy

        int i = 0;
        int j = 0;
        float animationFrameDuration = 0.0f;
        displayImage.sprite = animationFrames[i];
        while ((!isMovieDone || !isSubtitleDone) && !hasUserTapped)
        {

            if (!isMovieDone)
            {

                if (animationFrameDuration > animationDuration[i])
                {
                    i++;
                    if (i < animationFrames.Count)
                    {
                        displayImage.sprite = animationFrames[i];
                        animationFrameDuration = 0.0f;
                    }
                    else
                        isMovieDone = true;
                }

            }

            if (!isSubtitleDone)
            {
                if (currentTime >= subtitleEndTime[j])
                {

                    Destroy(subtitle[j]);
                    j++;

                    if (j >= subtitle.Count)
                    {
                        isSubtitleDone = true;
                    }
                }
                else if (currentTime >= subtitleStartTime[j])
                {
                    subtitle[j].SetActive(true);
                }

            }


            yield return new WaitForSeconds(checkInterval);
            currentTime += 0.05f;
            animationFrameDuration += 0.05f;
        }


    }




    public void OnPointerClick(PointerEventData eventData)
    {
        hasUserTapped = true;
    }

}