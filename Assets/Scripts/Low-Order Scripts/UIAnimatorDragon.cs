using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIAnimatorDragon : MonoBehaviour
{
    [Header("Animation Sprites")]
    [SerializeField]
    private Sprite[] sprites;


    [Header("Dragon To Set Active")]
    [SerializeField]
    private GameObject fullDragon;

    [Header("Particle System To Set Active")]
    [SerializeField]
    private GameObject particleDragon;

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

    void OnEnable()
    {
        if (animCoroutine != null)
            StopCoroutine(animCoroutine);

        animCoroutine = StartCoroutine(PlayAnimationUnscaled());
    }

    IEnumerator PlayAnimationUnscaled()
    {
        particleDragon.SetActive(true);
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

            if (index == 24)
            {
                fullDragon.SetActive(true);

            }

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
    }
}
