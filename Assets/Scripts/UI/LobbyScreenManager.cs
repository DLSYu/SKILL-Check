using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    private float duration = 5f;
    private float elapsedTime;
    private Vector3 characterPosition;

    private void Start(){
        targetPosition = camera.transform.position;
    }

    private void Update(){
         showActiveButton();
          
        //GetMovemmentAmount of character when moving
        MovementAmount = mainCharacter.GetComponent<PlayerMovement>().MovementAmount;

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
          float percentageComplete = elapsedTime / duration;
          
          float characterPercentageComplete = elapsedTime / (duration + 10);
          camera.transform.position = Vector3.Lerp(camera.transform.position, targetPosition, Mathf.SmoothStep(0,1,percentageComplete));

          mainCharacter.transform.position = Vector3.Lerp(mainCharacter.transform.position, characterPosition, characterPercentageComplete);
    }
    public void clickLeftButton()
    {
        elapsedTime = 0;
        targetPosition = new Vector3(camera.transform.position.x - cameraMoveDistance, camera.transform.position.y, camera.transform.position.z);
        currentScreenIndex--;
    }

    public void clickRightButton()
    {
        elapsedTime = 0;
        targetPosition = new Vector3(camera.transform.position.x + cameraMoveDistance, camera.transform.position.y, camera.transform.position.z);
        currentScreenIndex++;
    }

    private void showActiveButton(){
        if (currentScreenIndex == -1){
            leftButton.SetActive(false);
            characterPosition = ruinsPosition;
            
        }
        else if (currentScreenIndex == 1){
            rightButton.SetActive(false);
            characterPosition = forestPosition;  
        }
        else if (currentScreenIndex == 0){
            characterPosition = libraryPosition;
            leftButton.SetActive(true);
            rightButton.SetActive(true);
        }
    }
}
