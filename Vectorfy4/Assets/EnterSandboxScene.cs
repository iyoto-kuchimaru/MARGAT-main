using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterSandboxScene : MonoBehaviour
{
    public void LoadARSandbox()
    {
        SceneManager.LoadScene("Level3");
    }
}