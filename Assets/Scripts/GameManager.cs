using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int lives = 3;

    public GameObject image;
    public GameObject canvas;
    public float fadeDuration = 1f;

    void Start()
    {
        // Ensure image is set initially (if not assigned, find it)
        if (image == null)
        {
            image = GameObject.Find("Image"); // Update with your actual image name
        }

        image.SetActive(false); // Hide image initially
    }

    void Awake()
    {
        // Singleton pattern: Ensure only one GameManager exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep GameManager across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate GameManagers
        }

        // Check for existing canvas
        if (canvas == null)
        {
            // Find the canvas in the scene if it wasn't assigned
            canvas = GameObject.Find("Canvas");
        }

        if (canvas != null)
        {
            // If canvas is already assigned, check if it's the one attached to GameManager
            GameObject existingCanvas = GameObject.Find("Canvas");
            if (existingCanvas != null && existingCanvas != canvas)
            {
                Destroy(existingCanvas); // Destroy any duplicate canvas objects
            }

            // Ensure that the canvas does not get destroyed between scenes
            DontDestroyOnLoad(canvas);
        }

        if(image == null)
        {
            image = GameObject.Find("Image"); // Find the image after the scene reload
        }

        if (image != null)
        {
            image.SetActive(false); // Hide image after scene reload
        }
    }

    public void DecreaseLives()
    {
        lives--;
    }

    public int GetLives()
    {
        return lives;
    }

    //added a fade coroutine like we did for mission demo to make the levels blend together better.

    //this is what the playerController will call to start the coroutine
    public void StartFadeAndLoad(int Level)
    {
        StartCoroutine(FadeAndLoad(Level));
    }

    private IEnumerator FadeAndLoad(int Level)
    {

        // Ensure image reference is valid before using it in the fade process
        if (image == null)
        {
            image = GameObject.Find("Image"); // Reassign the image if it was lost after scene load
        }

        Image block = image.GetComponent<Image>();
        image.SetActive(true);

        // Fade to black
        for (float ft = 0; ft <= 1; ft += 0.05f)
        {
            block.color = new Color(0, 0, 0, ft);
            yield return new WaitForSeconds(0.1f);
        }

        // Load the new scene
        SceneManager.LoadScene(Level);

        // Fade back in 
        for (float ft = 1; ft >= 0; ft -= 0.05f)
        {
            block.color = new Color(0, 0, 0, ft);
            yield return new WaitForSeconds(0.1f);
        }

        //make image false after coroutine is finished
        image.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
