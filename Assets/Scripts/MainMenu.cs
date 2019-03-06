using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public string levelOne;
    public string levelSelect;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Play()
    {
        SceneManager.LoadScene(levelOne);
    }

    public void LevelSelect()
    {
        SceneManager.LoadScene(levelSelect);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
