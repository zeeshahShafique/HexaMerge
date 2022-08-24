using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour
{
    public void ResetLevel()
    {
        SceneManager.LoadScene(0);
    }
}
