using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameEnding : MonoBehaviour
{
    public GameObject image;            
    public GameObject text;             
    public GameObject lives;            
    public float fadeDuration = 1f;

    void Start()
    {
        // Hide the Game Over initially
        image.SetActive(false);
        text.SetActive(false);

        // Ensure the lives is active at the start
        if (lives != null)
            lives.SetActive(true);
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        // Debug: Check if the trigger is detected and the player entered
        Debug.Log("Trigger entered by: " + other.gameObject.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player triggered the end zone!");
            
            // Optionally hide the lives UI when the game ends
            if (lives != null)
                lives.SetActive(false);

            // Show the Game Over image and text
            image.SetActive(true);
            text.SetActive(true);

            StartCoroutine(Fade());

            // Quit the application
            Application.Quit();
        }
    }

    IEnumerator Fade()
    {
        Image block = image.GetComponent<Image>();

        // Fade the image from fully transparent to opaque over a duration
        for (float ft = 0f; ft <= 1f; ft += 0.1f) // Fade in
        {
            block.color = new Color(0, 0, 0, ft); // Set alpha gradually
            yield return new WaitForSeconds(0.1f); // Wait for the next frame
        }

        block.color = new Color(0, 0, 0, 1f);

    }

}

