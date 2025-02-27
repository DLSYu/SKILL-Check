using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIAnimator : MonoBehaviour
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
	private int frame = 0;

    void Awake()
    {
        if (this.gameObject.activeSelf)
        {
            this.gameObject.GetComponent<Canvas>().sortingOrder = 0;
            for (int i = 0; i < objectsToRemoveFromView.Length; i++)
            {
                objectsToRemoveFromView[i].SetActive(false);
            }
        }
    }
    void FixedUpdate () {
        if (this.gameObject.activeSelf)
        {
    
            if (!loop && index == sprites.Length) return;
            frame++;

            if (frame < framesPerSprite) return;

            image.sprite = sprites [index];

            frame = 0;
            index++;

            if (index >= sprites.Length) {
                if (loop) index = 0;
                else
                    {
                        foreach (GameObject obj in objectsToSetActiveAfterRunningAnimation)
                        {
                            obj.SetActive(true);
                        }
                    }
                if (destroyOnEnd) Destroy (gameObject);
            }
        }
	}
    
}
