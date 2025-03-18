using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToLobby_SequenceEnd : MonoBehaviour
{
    [SerializeField] private LoadingScreen loadingScreen;
    public void ReturnToLobby()
    {
        loadingScreen.LoadScene("Lobby");
    }
}
