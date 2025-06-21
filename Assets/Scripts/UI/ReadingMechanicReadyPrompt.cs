using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ReadingMechanicReadyPrompt : MonoBehaviour
{

    [Header("Animation Sprites")]
    [SerializeField]
    private List<Sprite> sprites;


    [Header("Animation Sprites")]
    [SerializeField]
    private List<int> messagesWhereinAnimationIsReused;

    [Header("Set Objects Active After Running Animation")]
    [SerializeField]
    private GameObject[] objectsToSetActiveAfterRunningAnimation;

    [Header("Adjust Parameters in Animation")]

    [SerializeField]
    private int framesPerSprite = 4;

    private int frameIndex = 0;
    private int firstFrameIndex = 0;
    private int stepIndex = 0;
    private int messageIndex = 0;

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
        if (animCoroutine != null)
            StopCoroutine(animCoroutine);

        frameIndex = 0;
        firstFrameIndex = 0;

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
            {

                this.gameObject.SetActive(false);
                break;
            }


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
        Time.timeScale = 1;
        Destroy(this.gameObject);

    }

    public void NextStep()
    {
        ProceedToNextStep();


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


            if (clickedOnce)
                stepIndex++;


            Debug.Log("stepIndex in nextStep: " + stepIndex);


            if (firstFrameIndex > 0)
                foreach (GameObject obj in objectsToSetActiveAfterRunningAnimation)
                    obj.SetActive(true);



        }

        if (firstFrameIndex % 3 != 0)
        {
            frameIndex = firstFrameIndex;

        }

        if (firstFrameIndex == 3)
        {
            foreach (GameObject obj in objectsToSetActiveAfterRunningAnimation)
                obj.SetActive(false);


        }




    }



}
