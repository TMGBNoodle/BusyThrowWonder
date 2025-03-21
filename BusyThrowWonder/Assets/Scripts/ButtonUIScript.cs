using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonUIScript : MonoBehaviour
{
    [SerializeField] private string newGameLevel = "FightingZombies";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void NewGameButton(){
        SceneManager.LoadScene(newGameLevel);
    }
}
