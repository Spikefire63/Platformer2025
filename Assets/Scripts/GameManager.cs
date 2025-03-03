using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int lives = 3;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

    }

    public void DecreaseLives()
    {
        lives--;

        if(lives <= 0)

            UnityEditor.EditorApplication.isPlaying = false;
    }

    public int GetLives()
    {
        return lives;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
