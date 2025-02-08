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
    [SerializeField] Vector3 libraryPosition;
    [SerializeField] Vector3 ruinsPosition;
    [SerializeField] Vector3 forestPosition;

    private int currentScreenIndex = 0; // -1 is left, 0 is center, 1 is right
    private Vector3 targetPosition;

    private void Start(){
        targetPosition = camera.transform.position;
    }

    private void Update(){
          showActiveButton();
          camera.transform.position = Vector3.Lerp(camera.transform.position, targetPosition, cameraMoveSpeed * Time.deltaTime);
    }
    public void clickLeftButton()
    {
        targetPosition = new Vector3(camera.transform.position.x - cameraMoveDistance, camera.transform.position.y, camera.transform.position.z);
        currentScreenIndex--;
    }

    public void clickRightButton()
    {
        targetPosition = new Vector3(camera.transform.position.x + cameraMoveDistance, camera.transform.position.y, camera.transform.position.z);
        currentScreenIndex++;
    }

    private void showActiveButton(){
        if (currentScreenIndex == -1){
            leftButton.SetActive(false);
            mainCharacter.transform.position = ruinsPosition;
        }
        else if (currentScreenIndex == 1){
            rightButton.SetActive(false);
            mainCharacter.transform.position = forestPosition;  
        }
        else if (currentScreenIndex == 0){
            mainCharacter.transform.position = libraryPosition;
            leftButton.SetActive(true);
            rightButton.SetActive(true);
        }
    }
}
