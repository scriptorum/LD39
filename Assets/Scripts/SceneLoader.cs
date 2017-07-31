using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// TODO Merge with AutoLoad
public class SceneLoader : MonoBehaviour
{
	[TooltipAttribute("If blank, loads next scene")]
    public string scene;

    void OnTriggerEnter2D(Collider2D other)
    {
		if(scene == "")
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);			
        else SceneManager.LoadScene(scene);
    }
}