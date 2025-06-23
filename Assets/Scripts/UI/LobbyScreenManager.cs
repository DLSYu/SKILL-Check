using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using System.Data.Common;
using UnityEngine.Rendering.Universal;
using System;

public class LobbyScreenManager : MonoBehaviour, IDataPersistence
{
    [SerializeField] Camera camera;
    [SerializeField] GameObject leftButton;
    [SerializeField] GameObject rightButton;
    [SerializeField] int cameraMoveDistance;
    [SerializeField] int cameraMoveSpeed;
    [SerializeField] GameObject mainCharacter;
    [SerializeField] Animator UIAnimator;
    // [SerializeField] Animator transitionAnimator;
    private Vector2 MovementAmount;
    [SerializeField] Vector3 libraryPosition;
    [SerializeField] Vector3 ruinsPosition;
    [SerializeField] Vector3 forestPosition;
    [SerializeField] private AudioSource libraryMusic;
    [SerializeField] private AudioSource otherMusic;

    [SerializeField] private GameObject introHandler;

    [SerializeField] private GameObject warningScreen;
    [SerializeField] private LoadingScreen loadingScreen;
    [SerializeField] private LoadingScreen backToTitleScreenLoadingScreen;
    [SerializeField] private LibrarianDialogue librarianDialogue;

    private int currentScreenIndex = 0; // -1 is left, 0 is center, 1 is right
    private Vector3 targetPosition;

    private float duration = 1.5f;
    private float elapsedTime;
    private Vector3 characterPosition;

    private float percentageComplete = 0f;
    private float characterPercentageComplete = 0f;
    private Vector3 prevCharacterPos;
    private Vector3 prevCameraPos;

    //private bool buttonsEnabled = true;
    [SerializeField] private float disableButtonDuration = 1f;

    [SerializeField] private List<GameObject> bookClickables;
    [SerializeField] private List<GameObject> finishedBookClickables;
    [SerializeField] private List<GameObject> bookGlows;
    [SerializeField] private List<GameObject> statueClickables;
    [SerializeField] private List<GameObject> statueGlows;


    [SerializeField] private GameObject particleSystemObject;

    private int ho_level_to_reveal = -1;
    private bool ho_level_has_played_animation = false;
    private int lo_level_to_reveal = -1;
    private bool lo_level_has_played_animation = false;

    private int hoTotalStages = 5;
    private int loTotalStages = 5;

    private float fadeDuration = 3.0f;

    private bool alreadyPlayingDialogue = false;


    [SerializeField] private GameObject afterLevelLO1;
    [SerializeField] private GameObject afterLevelLO3;
    [SerializeField] private GameObject afterLevelLO5;

    [SerializeField] private GameObject afterLevelHO1;
    [SerializeField] private GameObject afterLevelHO3;
    [SerializeField] private GameObject afterLevelHO5;

    private void Start()
    {

        prevCharacterPos = mainCharacter.transform.position;
        prevCameraPos = camera.transform.position;
        targetPosition = camera.transform.position;

    }

    private void Update()
    {
        checkCharacterPosition();

        //GetMovemmentAmount of character when moving
        //Debug.Log(MovementAmount.magnitude);

        UIAnimator.SetFloat("Speed", MovementAmount.magnitude);

        if (MovementAmount.x < 0)
        {
            mainCharacter.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (MovementAmount.x > 0)
        {
            mainCharacter.GetComponent<SpriteRenderer>().flipX = false;
        }

        elapsedTime += Time.deltaTime;
        percentageComplete = elapsedTime / duration;

        characterPercentageComplete = elapsedTime / (duration / 1.5f);
        camera.transform.position = Vector3.Lerp(prevCameraPos, targetPosition, Mathf.SmoothStep(0, 1, percentageComplete));

        mainCharacter.transform.position = Vector3.Lerp(prevCharacterPos, characterPosition, Mathf.SmoothStep(0, 1, characterPercentageComplete));

        if (characterPercentageComplete >= 1f)
        {
            MovementAmount = Vector2.zero;
            mainCharacter.GetComponent<SpriteRenderer>().flipX = false;
        }

        if (percentageComplete >= 1f)
        {
            showActiveButton();
        }
    }
    public void clickLeftButton()
    {
        leftButton.SetActive(false);
        rightButton.SetActive(false);
        libraryMusic.mute = true;
        otherMusic.mute = true;
        // transitionAnimator.SetTrigger("startFade");
        prevCharacterPos = mainCharacter.transform.position;
        prevCameraPos = camera.transform.position;
        MovementAmount.x = -1f;
        elapsedTime = 0;
        targetPosition = new Vector3(camera.transform.position.x - cameraMoveDistance, camera.transform.position.y, camera.transform.position.z);
        currentScreenIndex--;

        if (currentScreenIndex == -1)
        {

            if (ho_level_to_reveal != -1)
            {
                statueClickables[ho_level_to_reveal - 1].SetActive(true);
                statueGlows[ho_level_to_reveal - 1].SetActive(true);
                statueClickables[ho_level_to_reveal - 1].GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
                StartCoroutine(FadeInAndOut(statueClickables[ho_level_to_reveal - 1], true, fadeDuration));
                StartCoroutine(FadeInAndOutLight(statueGlows[ho_level_to_reveal - 1], true, fadeDuration));
                Instantiate(particleSystemObject, statueClickables[ho_level_to_reveal - 1].transform);

                ho_level_has_played_animation = true;
                DataPersistenceManager.instance.SaveGame();

            }
        }
    }

    public void clickRightButton()
    {
        leftButton.SetActive(false);
        rightButton.SetActive(false);
        libraryMusic.mute = true;
        otherMusic.mute = true;
        // transitionAnimator.SetBool("startFade", true);
        prevCharacterPos = mainCharacter.transform.position;
        prevCameraPos = camera.transform.position;
        MovementAmount.x = 1f;
        elapsedTime = 0;
        targetPosition = new Vector3(camera.transform.position.x + cameraMoveDistance, camera.transform.position.y, camera.transform.position.z);
        currentScreenIndex++;


        if (currentScreenIndex == 1)
        {
            if (lo_level_to_reveal != -1)
            {

                bookClickables[lo_level_to_reveal - 1].SetActive(true);
                bookGlows[lo_level_to_reveal - 1].SetActive(true);
                StartCoroutine(FadeInAndOut(bookClickables[lo_level_to_reveal - 1], true, fadeDuration));
                StartCoroutine(FadeInAndOutLight(bookGlows[lo_level_to_reveal - 1], true, fadeDuration));
                Instantiate(particleSystemObject, bookClickables[lo_level_to_reveal - 1].transform);
                lo_level_has_played_animation = true;

                DataPersistenceManager.instance.SaveGame();
            }
        }

    }

    public void ClickXButton()
    {
        warningScreen.SetActive(true);
    }

    public void ClickYesButton()
    {
        backToTitleScreenLoadingScreen.LoadScene("TitleScreen");
    }

    public void ClickNoButton()
    {
        warningScreen.SetActive(false);
    }

    private void showActiveButton()
    {

        if (currentScreenIndex == -1)
        {
            leftButton.SetActive(false);
            rightButton.SetActive(true);
            otherMusic.mute = false;
            libraryMusic.mute = true;
        }
        else if (currentScreenIndex == 1)
        {
            rightButton.SetActive(false);
            leftButton.SetActive(true);
            otherMusic.mute = false;
            libraryMusic.mute = true;
        }
        else if (currentScreenIndex == 0)
        {
            leftButton.SetActive(true);
            rightButton.SetActive(true);
            libraryMusic.mute = false;
            otherMusic.mute = true;
        }

        // transitionAnimator.SetBool("startFade", false);
    }

    private void checkCharacterPosition()
    {
        if (currentScreenIndex == -1)
        {
            characterPosition = ruinsPosition;

        }
        else if (currentScreenIndex == 1)
        {
            characterPosition = forestPosition;
        }
        else if (currentScreenIndex == 0)
        {
            characterPosition = libraryPosition;
        }
    }

    public void LoadData(GameData data)
    {
        bool hasSetAtLeastOneClickable_High = false;

        bool hasSetAtLeastOneClickable_Low = false;
        int lo_completed_stage_index = 0;
        int ho_completed_stage_index = 0;

        bool hasIntroBeenPlayed;


        // save file checking
        foreach (KeyValuePair<String, bool> key in data.stageCompletionDictionary)
        {
            Debug.Log(key.ToString());

            if (key.Key[0] == 'H')
            {
                hasSetAtLeastOneClickable_High = true;

                for (int i = 0; i < hoTotalStages; i++)
                {
                    if (key.Key == "HO_" + (i + 1).ToString())
                    {
                        statueClickables[i].SetActive(true);
                        statueGlows[i].SetActive(true);

                        if (ho_completed_stage_index < (i + 1))
                            ho_completed_stage_index = i + 1;

                        break;
                    }
                }

            }
            else if (key.Key[0] == 'L')
            {
                hasSetAtLeastOneClickable_Low = true;
                for (int i = 0; i < loTotalStages; i++)
                {
                    if (key.Key == "LO_" + (i + 1).ToString())
                    {
                        finishedBookClickables[i].SetActive(true);
                        bookGlows[i].SetActive(true);

                        if (lo_completed_stage_index < (i + 1))
                            lo_completed_stage_index = i + 1;

                        break;
                    }
                }
            }

        }

        // brand new file, no save file yet
        if (!hasSetAtLeastOneClickable_High)
        {
            ho_level_to_reveal = 1;
        }

        if (!hasSetAtLeastOneClickable_Low)
        {
            lo_level_to_reveal = 1;
        }

        // open next stage
        if (ho_completed_stage_index < hoTotalStages)
        {
            string nextStage = "HO_" + (ho_completed_stage_index + 1).ToString();
            bool hasAnimationPreviouslyBeenPlayed;
            data.alreadyPlayedAnimationForNewlyOpenedStage.TryGetValue(nextStage.ToString(), out hasAnimationPreviouslyBeenPlayed);

            if (!hasAnimationPreviouslyBeenPlayed)
            {
                ho_level_to_reveal = ho_completed_stage_index + 1;
            }
            else
            {
                statueClickables[ho_completed_stage_index].GetComponent<SpriteRenderer>().color = new Color(0, 0, 0);
                statueClickables[ho_completed_stage_index].SetActive(true);
                statueGlows[ho_completed_stage_index].SetActive(true);
            }
        }

        if (lo_completed_stage_index < loTotalStages)
        {
            string nextStage = "LO_" + (lo_completed_stage_index + 1).ToString();
            bool hasAnimationPreviouslyBeenPlayed;
            data.alreadyPlayedAnimationForNewlyOpenedStage.TryGetValue(nextStage.ToString(), out hasAnimationPreviouslyBeenPlayed);

            if (!hasAnimationPreviouslyBeenPlayed)
            {
                lo_level_to_reveal = lo_completed_stage_index + 1;
            }
            else
            {
                bookClickables[lo_completed_stage_index].SetActive(true);
                bookGlows[lo_completed_stage_index].SetActive(true);
            }
        }

        data.alreadyPlayedAnimationForNewlyOpenedStage.TryGetValue("Intro", out hasIntroBeenPlayed);
        if (!hasIntroBeenPlayed)
        {
            introHandler.SetActive(true);
            alreadyPlayingDialogue = true;
        }


        bool result;
        bool alreadyCleared;
        // more if conditions here

        data.stageCompletionDictionary.TryGetValue("LO_1", out alreadyCleared);
        if (alreadyCleared)
        {
            data.alreadyPlayedAnimationForNewlyOpenedStage.TryGetValue("LO_1_CLEAR", out result);
            if (!alreadyPlayingDialogue && !result)
            {
                afterLevelLO1.SetActive(true);
                data.alreadyPlayedAnimationForNewlyOpenedStage.Add("LO_1_CLEAR", true);
                alreadyPlayingDialogue = true;
            }
            librarianDialogue.openingDialogue.Add("It's important to preserve history like how we have to remember the bakunawa story!");
            librarianDialogue.openingDialogue.Add("A bakunawa is supposedly huge and scary, but who says otherwise?");
        }

        data.stageCompletionDictionary.TryGetValue("LO_2", out alreadyCleared);
        if (alreadyCleared)
        {
            librarianDialogue.openingDialogue.Add("Always respect your parents and elders, alright?");
            librarianDialogue.openingDialogue.Add("Siblings like Apolaqui and Mayari may fight and argue, but what's important is that they forgive and make up.");
        }

        data.stageCompletionDictionary.TryGetValue("LO_3", out alreadyCleared);
        if (alreadyCleared)
        {
            data.alreadyPlayedAnimationForNewlyOpenedStage.TryGetValue("LO_3_CLEAR", out result);
            if (!alreadyPlayingDialogue && !result)
            {
                afterLevelLO3.SetActive(true);
                data.alreadyPlayedAnimationForNewlyOpenedStage.Add("LO_3_CLEAR", true);
                alreadyPlayingDialogue = true;
            }
            librarianDialogue.openingDialogue.Add("It's odd how Juan's father turned him into a rooster. How could that even happen in real life?");
            librarianDialogue.openingDialogue.Add("Juan was lazy, but he didn't deserve that kind of punishment.");
        }


        data.stageCompletionDictionary.TryGetValue("LO_4", out alreadyCleared);
        if (alreadyCleared)
        {

            librarianDialogue.openingDialogue.Add("I wish cats and dogs would work together in harmony.");
            librarianDialogue.openingDialogue.Add("The dogs who were wagging their tails earlier by the library were pretty happy. I guess they weren't the old dog.");
        }


        data.stageCompletionDictionary.TryGetValue("LO_5", out alreadyCleared);
        if (alreadyCleared)
        {
            data.alreadyPlayedAnimationForNewlyOpenedStage.TryGetValue("LO_5_CLEAR", out result);
            if (!alreadyPlayingDialogue && !result)
            {
                afterLevelLO5.SetActive(true);
                data.alreadyPlayedAnimationForNewlyOpenedStage.Add("LO_5_CLEAR", true);
                alreadyPlayingDialogue = true;
            }
            librarianDialogue.openingDialogue.Add("Never take your anger on others. It'll only hurt you in the end, like what happened to Kapitan Lara and Joselito.");
            librarianDialogue.openingDialogue.Add("Even without knowing Maria Makiling's curse, just don't do nefarious deeds, alright?");
        }

        data.stageCompletionDictionary.TryGetValue("HO_1", out alreadyCleared);
        if (alreadyCleared)
        {
            data.alreadyPlayedAnimationForNewlyOpenedStage.TryGetValue("HO_1_CLEAR", out result);
            if (!alreadyPlayingDialogue && !result)
            {
                afterLevelHO1.SetActive(true);
                data.alreadyPlayedAnimationForNewlyOpenedStage.Add("HO_1_CLEAR", true);
                alreadyPlayingDialogue = true;
            }
            librarianDialogue.openingDialogue.Add("The moth story reminded me of Rizal. I hope I can also see Rizal's talent in you, iho!");
            librarianDialogue.openingDialogue.Add("Mothers and moths know best!");
        }

        data.stageCompletionDictionary.TryGetValue("HO_2", out alreadyCleared);
        if (alreadyCleared)
        {
            librarianDialogue.openingDialogue.Add("No journey's too long. Just take your time.");
            librarianDialogue.openingDialogue.Add("As long as you persevere, you'll succeed in the end.");
        }


        data.stageCompletionDictionary.TryGetValue("HO_3", out alreadyCleared);
        if (alreadyCleared)
        {
            data.alreadyPlayedAnimationForNewlyOpenedStage.TryGetValue("HO_3_CLEAR", out result);
            if (!alreadyPlayingDialogue && !result)
            {
                afterLevelHO3.SetActive(true);
                data.alreadyPlayedAnimationForNewlyOpenedStage.Add("HO_3_CLEAR", true);
                alreadyPlayingDialogue = true;
            }
            librarianDialogue.openingDialogue.Add("Do you think my jewelry can shine brighter than Maria's?");
            librarianDialogue.openingDialogue.Add("Could you even imagine a sky so low?");
        }


        data.stageCompletionDictionary.TryGetValue("HO_4", out alreadyCleared);
        if (alreadyCleared)
        {
            librarianDialogue.openingDialogue.Add("Never judge a book by its cover. Both literally and figuratively.");
            librarianDialogue.openingDialogue.Add("I'd say to just be kind. Who knows what the other person is going through.");
        }


        data.stageCompletionDictionary.TryGetValue("HO_5", out alreadyCleared);
        if (alreadyCleared)
        {
            data.alreadyPlayedAnimationForNewlyOpenedStage.TryGetValue("HO_5_CLEAR", out result);
            if (!alreadyPlayingDialogue && !result)
            {
                afterLevelHO5.SetActive(true);
                data.alreadyPlayedAnimationForNewlyOpenedStage.Add("HO_5_CLEAR", true);
                alreadyPlayingDialogue = true;
            }
            librarianDialogue.openingDialogue.Add("A tikbalang looks like a horse on two legs. I'd rather not encounter one.");
            librarianDialogue.openingDialogue.Add("Filipino mythological creatures are interesting, but scary. I would still want to meet one though.");
        }












    }
    public void SaveData(GameData data)
    {
        bool hasIntroBeenPlayed = false;

        if (lo_level_to_reveal != -1 && lo_level_has_played_animation)
        {
            bool temp;
            data.alreadyPlayedAnimationForNewlyOpenedStage.TryGetValue("LO_" + lo_level_to_reveal.ToString(), out temp);

            if (!temp)
                data.alreadyPlayedAnimationForNewlyOpenedStage.Add("LO_" + lo_level_to_reveal.ToString(), true);
        }

        if (ho_level_to_reveal != -1 && ho_level_has_played_animation)
        {
            bool temp;
            data.alreadyPlayedAnimationForNewlyOpenedStage.TryGetValue("HO_" + ho_level_to_reveal.ToString(), out temp);
            if (!temp)
                data.alreadyPlayedAnimationForNewlyOpenedStage.Add("HO_" + ho_level_to_reveal.ToString(), true);
        }

        data.alreadyPlayedAnimationForNewlyOpenedStage.TryGetValue("Intro", out hasIntroBeenPlayed);
        if (!hasIntroBeenPlayed && !introHandler.activeInHierarchy)
        {
            data.alreadyPlayedAnimationForNewlyOpenedStage.Add("Intro", true);
        }
    }

    IEnumerator FadeInAndOut(GameObject gameObject, bool fadeIn, float duration)
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


        Color current = gameObject.GetComponent<SpriteRenderer>().color;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            float alpha = Mathf.Lerp(a, b, counter / duration);
            gameObject.GetComponent<SpriteRenderer>().color = new Color(current.r, current.g, current.b, alpha);

            yield return null;
        }

    }
    IEnumerator FadeInAndOutLight(GameObject gameObject, bool fadeIn, float duration)
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


        Color current = gameObject.GetComponent<Light2D>().color;



        while (counter < duration)
        {
            counter += Time.deltaTime;
            float alpha = Mathf.Lerp(a, b, counter / duration);
            gameObject.GetComponent<Light2D>().color = new Color(current.r, current.g, current.b, alpha);

            yield return null;
        }

    }
}
