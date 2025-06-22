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

    private Coroutine frameCoroutine;
    private Coroutine messageCoroutine;

    bool isSubtitleDone = false;
    bool isMovieDone = false;

    bool hasUserTapped = false;


    private void Start()
    {
        frameCoroutine = StartCoroutine(PlayFrames());
        messageCoroutine = StartCoroutine(HandleTimedMessages());
        StartCoroutine(LoadTitleScreenOnceReady());

    }

    private IEnumerator LoadTitleScreenOnceReady()
    {

        yield return new WaitUntil(() => (isSubtitleDone && isMovieDone) || hasUserTapped);
        loadingScreen.LoadScene("TitleScreen");
    }


    private IEnumerator PlayFrames()
    {

        for (int i = 0; i < animationFrames.Count && !hasUserTapped; i++)
        {
            displayImage.sprite = animationFrames[i];
            yield return new WaitForSeconds(animationDuration[i]);
        }

        isMovieDone = true;

    }

    private IEnumerator HandleTimedMessages()
    {
        float currentTime = 0f;
        float checkInterval = 0.05f; // check every 50ms for accuracy


        int i = 0;
        while (i < subtitle.Count && !hasUserTapped)
        {

            if (currentTime >= subtitleStartTime[i])
            {
                StartCoroutine(ShowMessage(subtitle[i], subtitleEndTime[i] - subtitleStartTime[i]));
                i++;
            }


            yield return new WaitForSeconds(checkInterval);
            currentTime += 0.05f;
        }

        isSubtitleDone = true;
    }

    private IEnumerator ShowMessage(GameObject message, float duration)
    {
        message.SetActive(true);
        yield return new WaitForSeconds(duration);
        Destroy(message);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        hasUserTapped = true;
    }

}