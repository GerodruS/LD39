using UnityEngine;
using UnityEngine.SceneManagement;


public class RestartButton : MonoBehaviour
{
    public void Activate()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}