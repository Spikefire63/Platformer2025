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
        if (other.CompareTag("Player"))
        {
            Debug.Log("player triggered zone");
            
            //hide the lives at the end of the game
            if (lives != null)
                lives.SetActive(false);

            // show the game over text
            image.SetActive(true);
            text.SetActive(true);

            //calling the ending coroutine
            StartCoroutine(Fade());
        }
    }

    //creating the ending coroutine
    IEnumerator Fade()
    {
        Image block = image.GetComponent<Image>();

        //fade to black
        for (float ft = 0f; ft <= 1f; ft += 0.1f)
        {
            block.color = new Color(0, 0, 0, ft);
            yield return new WaitForSeconds(0.1f);
        }
        //make sure screen is completely black by the end of the fade
        block.color = new Color(0, 0, 0, 1f);

        yield return new WaitForSeconds(2f);

        UnityEditor.EditorApplication.isPlaying = false;
    }

}

