using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlButton : MonoBehaviour
{
    [SerializeField] private string newGameLevel = "Controls";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void NewGameButton(){
        SceneManager.LoadScene(newGameLevel);
    }
}

