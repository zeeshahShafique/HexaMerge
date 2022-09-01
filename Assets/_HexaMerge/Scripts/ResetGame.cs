using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour
{
    [SerializeField] private GridSystemSO Grid;
    public void ResetLevel()
    {
        Grid.Clear();
        SceneManager.LoadScene(0);
    }
}
