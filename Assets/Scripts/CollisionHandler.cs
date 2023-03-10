using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    //parameters
    [SerializeField] float nextLevelDelay = 1f;
    [SerializeField] AudioClip explosion;
    [SerializeField] AudioClip success;

    //cache
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    void OnCollisionEnter(Collision collision)
    {
        // switch statement for when player obj collides with different tagged objs
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

    // prevent player input and reload level when player dies
    // play explosion audio when player collides with obstacle
    void CrashSequence()
    {
        //TODO add particle effect upon crash
        audioSource.PlayOneShot(explosion);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", nextLevelDelay);
    }

    // prevent player input and load next level when player completes level
    // play success audio when player collides with landing pad
    void LevelComplete()
    {
        //TODO add particle effect upon crash
        audioSource.PlayOneShot(success);
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", nextLevelDelay);

    }

    // reload active level in build index
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    // load next level in build index
    // if next level is equal to amount of open scenes then load first level
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


