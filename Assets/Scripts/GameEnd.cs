using UnityEngine;

public class GameEnding : MonoBehaviour
{
    public GameObject Player;
    public GameObject gameEnd;

    void Start()
    {
        //Canvas is hidden at the start
        gameEnd.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something entered the trigger: " + other.CompareTag("Player")); // Debugging

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the trigger. Showing end and quitting...");
            gameEnd.SetActive(true); // Show the Canvas
            Application.Quit();
        }
    }
}

