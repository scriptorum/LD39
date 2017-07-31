using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Config : MonoBehaviour
{
    public static Config instance;

    public bool gamePaused = false;
    public bool gameOver = false;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
#if DEBUG
        if (Input.GetKeyDown(KeyCode.N))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
#endif
    }
}