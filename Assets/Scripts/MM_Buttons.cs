using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void SwitchToMainScene()
    {
        SceneManager.LoadScene("SCN_Main");
    }
}