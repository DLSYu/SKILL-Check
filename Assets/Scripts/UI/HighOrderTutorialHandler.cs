using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TutorialAnimator : MonoBehaviour, IPointerClickHandler
{

    [Header("Disable Controls")]
    [SerializeField]
    private GameObject player;

    [Header("Animation Sprites")]
    [SerializeField]
    private List<Sprite> sprites;

    [Header("Messages")]
    [SerializeField]
    private GameObject[] messagesToDisplay;

    [Header("Animation Sprites")]
    [SerializeField]
    private List<int> messagesWhereinAnimationIsReused;

    [Header("Things to Highlight Backgrounds")]
    [SerializeField]
    private Sprite[] blackBackgroundImages; // 0 is all black


    [Header("Adjust Parameters in Animation")]

    [SerializeField]
    private int framesPerSprite = 4;

    private int frameIndex = 0;
    private int firstFrameIndex = 0;
    private int stepIndex = 0;
    private int messageIndex = -1;

    [Header("Image to animate")]

    [SerializeField]
    private Image imageToAnimate;

    [SerializeField]
    private Image backgroundImageAnimate;

    private Coroutine animCoroutine;

    private bool clickedOnce = false;

    private bool toProceedToNextStep = false;

    void OnEnable()
    {
        Time.timeScale = 0;
        // disable controls
        player.GetComponent<PlayerMovement>().enabled = false;

        if (animCoroutine != null)
            StopCoroutine(animCoroutine);

        animCoroutine = StartCoroutine(PlayAnimationUnscaled());
    }

    IEnumerator PlayAnimationUnscaled()
    {

        float frameDuration = framesPerSprite / 60f; // adjust if needed
        float timer;

        while (true) // run while still not at the end
        {

            if (toProceedToNextStep)
            {
                ProceedToNextStep();
                toProceedToNextStep = false;
                clickedOnce = false;
            }
            if (frameIndex >= sprites.Count)
                break;

            imageToAnimate.sprite = sprites[frameIndex];


            timer = 0f;
            while (timer < frameDuration)
            {
                timer += Time.unscaledDeltaTime;
                yield return null;
            }

            int spritesPerStep;

            if (firstFrameIndex % 3 == 0)
            {
                spritesPerStep = 0;
            }
            else
            {
                spritesPerStep = 1;
            }

            frameIndex++;

            if (frameIndex > (firstFrameIndex + spritesPerStep)) // only loop animations wherein the fairy stays stationary
            {
                if (firstFrameIndex % 3 != 0) frameIndex = firstFrameIndex; // loop when fairy is stationary
                else if (firstFrameIndex % 3 == 0)                // else play next step
                {
                    toProceedToNextStep = true;
                }
            }


        }

        // enable controls
        player.GetComponent<PlayerMovement>().enabled = true;

        Time.timeScale = 1;
        Destroy(gameObject);
    }

    private void ProceedToNextStep()
    {


        if (!messagesWhereinAnimationIsReused.Contains(messageIndex))
        {
            if (firstFrameIndex % 3 == 0)
            {
                firstFrameIndex++;
            }
            else
            {
                firstFrameIndex += 2;
            }

            frameIndex = firstFrameIndex;

            if (clickedOnce)
                stepIndex++;


            Debug.Log("stepIndex in nextStep: " + stepIndex);
            if (stepIndex < blackBackgroundImages.Length)
                backgroundImageAnimate.sprite = blackBackgroundImages[stepIndex];

        }

        if (firstFrameIndex % 3 != 0)
        {
            frameIndex = firstFrameIndex;

            if (messageIndex >= 0 && messageIndex < messagesToDisplay.Length)
                messagesToDisplay[messageIndex].SetActive(false);

            messageIndex++;

            if (messageIndex < messagesToDisplay.Length)
                messagesToDisplay[messageIndex].SetActive(true);
        }






    }


    public void OnPointerClick(PointerEventData eventData)
    {

        if (firstFrameIndex % 3 != 0)
        {
            if (!clickedOnce)
            {
                clickedOnce = true;
                toProceedToNextStep = true;

            }
        }
    }


}
