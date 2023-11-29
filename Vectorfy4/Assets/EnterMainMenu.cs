using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterMainMenu : MonoBehaviour
{
    public void LoadMenu()
    {
        SceneManager.LoadScene("IntroScreen");
    }
}
