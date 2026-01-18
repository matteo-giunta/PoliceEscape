using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKeyDown || Input.touchCount > 0)
        {
            SceneManager.LoadScene("MainScene");
        }
    }
}