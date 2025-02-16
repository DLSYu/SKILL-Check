using UnityEngine;

public class LobbyScreenManager : MonoBehaviour
{   
    [SerializeField] Camera camera;
    [SerializeField] GameObject leftButton;
    [SerializeField] GameObject rightButton;
    [SerializeField] int cameraMoveDistance;
    [SerializeField] int cameraMoveSpeed;
    [SerializeField] GameObject mainCharacter;
    [SerializeField] Animator animator;
    private Vector2 MovementAmount;
    [SerializeField] Vector3 libraryPosition;
    [SerializeField] Vector3 ruinsPosition;
    [SerializeField] Vector3 forestPosition;

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

    private void Start(){
        prevCharacterPos = mainCharacter.transform.position;
        prevCameraPos = camera.transform.position;
        targetPosition = camera.transform.position;
    }

    private void Update(){
        checkCharacterPosition();

        //GetMovemmentAmount of character when moving
        //MovementAmount = mainCharacter.GetComponent<PlayerMovement>().MovementAmount;

        animator.SetFloat("Speed", MovementAmount.magnitude);
        
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
        prevCharacterPos = mainCharacter.transform.position;
        prevCameraPos = camera.transform.position;
        MovementAmount.x = -1f;
        elapsedTime = 0;
        targetPosition = new Vector3(camera.transform.position.x - cameraMoveDistance, camera.transform.position.y, camera.transform.position.z);
        currentScreenIndex--;
    }

    public void clickRightButton()
    {
        leftButton.SetActive(false);
        rightButton.SetActive(false);
        prevCharacterPos = mainCharacter.transform.position;
        prevCameraPos = camera.transform.position;
        MovementAmount.x = 1f;
        elapsedTime = 0;
        targetPosition = new Vector3(camera.transform.position.x + cameraMoveDistance, camera.transform.position.y, camera.transform.position.z);
        currentScreenIndex++;
    }

    private void showActiveButton(){
        if (currentScreenIndex == -1){
            leftButton.SetActive(false);
            rightButton.SetActive(true);
            
        }
        else if (currentScreenIndex == 1){
            rightButton.SetActive(false);
            leftButton.SetActive(true);
        }
        else if (currentScreenIndex == 0){
            leftButton.SetActive(true);
            rightButton.SetActive(true);
        }
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
}
