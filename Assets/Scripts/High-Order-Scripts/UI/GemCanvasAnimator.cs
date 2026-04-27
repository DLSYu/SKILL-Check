using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GemCanvasAnimator : MonoBehaviour, IPointerClickHandler
{
    [Header("Animation Sprites")]
    [SerializeField]
    private Sprite[] sprites;


    [Header("Objects to Set Active")]
    [SerializeField]
    private GameObject[] objectsToRemoveFromView;


    [SerializeField]
    private GameObject[] objectsToSetActiveAfterRunningAnimation;

    [Header("Adjust Parameters in Animation")]

    [SerializeField]
    private int framesPerSprite = 4;
    [SerializeField]
    private bool loop = false;
    [SerializeField]
    private bool destroyOnEnd = false;

    [SerializeField]
    private int index = 0;

    [Header("Image to animate")]

    [SerializeField]
    private Image image;

    private Coroutine animCoroutine;

    private bool isDoneShowingAnimation = false;

    void OnEnable()
    {
        if (animCoroutine != null)
            StopCoroutine(animCoroutine);

        animCoroutine = StartCoroutine(PlayAnimationUnscaled());
    }

    void Update()
    {
        if (isDoneShowingAnimation)
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.E))
            {
                UIManagerTemplate.Instance.exitGemCanvas();
                isDoneShowingAnimation = false;
            }
        }
    }

    IEnumerator PlayAnimationUnscaled()
    {
        index = 0;

        foreach (GameObject obj in objectsToRemoveFromView)
            obj.SetActive(false);

        float frameDuration = framesPerSprite / 60f; // adjust if needed
        float timer = 0f;

        while (loop || index < sprites.Length)
        {
            image.sprite = sprites[index];

            timer = 0f;
            while (timer < frameDuration)
            {
                timer += Time.unscaledDeltaTime;
                yield return null;
            }

            index++;
            if (index >= sprites.Length)
            {
                if (loop) index = 0;
                else break;
            }
        }

        foreach (GameObject obj in objectsToSetActiveAfterRunningAnimation)
            obj.SetActive(true);

        if (destroyOnEnd)
            Destroy(gameObject);


        isDoneShowingAnimation = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isDoneShowingAnimation)
        {
            UIManagerTemplate.Instance.exitGemCanvas();
            isDoneShowingAnimation = false;
        }
    }
}
