using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float nextLevelDelay = 1f;

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                Debug.Log(" ");
                break;

            case "Friendly":
                Debug.Log("Launch Pad");
                break;

            case "Finish":
                Debug.Log("Complete!");
                LevelComplete();
                break;

            default:
                Debug.Log("You died!");
                CrashSequence();
                break;
        }

    }

    void CrashSequence()
    {
        //TODO add sfx upon crash
        //TODO add particle effect upon crash
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", nextLevelDelay);
    }

    void LevelComplete()
    {
        //TODO add sfx upon crash
        //TODO add particle effect upon crash
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", nextLevelDelay);
    }

    void ReloadLevel()
    {

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int NextSceneIndex = currentSceneIndex + 1;
        if (NextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            NextSceneIndex = 0;
        }
        SceneManager.LoadScene(NextSceneIndex);


    }
}
