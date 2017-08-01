using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoLoad : MonoBehaviour
{
    public bool additive;
    public string scene;
	public string additiveMarker;
    void Awake()
    {
		if(additive)
		{
			if(transform.Find(additiveMarker) == null)
        		SceneManager.LoadScene(scene, LoadSceneMode.Additive);
		}
        else SceneManager.LoadScene(scene);
    }
}