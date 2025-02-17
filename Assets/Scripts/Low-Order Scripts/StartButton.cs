using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public void LoadNextScene()
    {
        SceneManager.LoadScene("SortingScene"); 
    }
}