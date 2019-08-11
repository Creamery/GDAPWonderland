﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Script attached to the loading screen
/// </summary>
public class LoadingScreen : MonoBehaviour {

    /// <summary>
    /// Reference to the textbox that shows the loading progress
    /// </summary>
	[SerializeField] TextMeshProUGUI progressText;
    /// <summary>
    /// Reference to the slider that shows the loading progress
    /// </summary>
	[SerializeField] UnityEngine.UI.Slider progressBar;
    /// <summary>
    /// Reference to the canvas attached to this gameobject
    /// </summary>
	private Canvas canvas;
    /// <summary>
    /// Used to load a scene in background
    /// </summary>
	private AsyncOperation operation;

    /// <summary>
    /// Unity Function. Called once upon creation of the object 
    /// </summary>
	void Awake() {
        LoadingScreen[] screens = FindObjectsOfType<LoadingScreen>();
        //		Debug.LogError ("Screens length " + screens.Length);
        if (screens.Length > 1) {
            Destroy(screens[0].gameObject);
        }

        canvas = GetComponentInChildren<Canvas>();
        canvas.gameObject.SetActive(false);
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Updates the slider and text values 
    /// <param name="progress">Progress value</param>
    /// </summary>
	void UpdateProgressUI(float progress) {
        progressBar.value = progress;
        progressText.text = (int)(progress * 100f) + "%";
    }

    /// <summary>
    /// Loads the scene
    /// <param name="sceneName">Name of scene to be loaded</param>
    /// <param name="loadData">Flag to determine if data should be loaded after loading of scene</param>
    /// </summary>
	public void LoadScene(string sceneName, bool loadData) {
        UpdateProgressUI(0);
        canvas.gameObject.SetActive(true);
        StartCoroutine(LoadAsyncScene(sceneName, loadData));
    }

    /// <summary>
    /// Coroutine for loading a scene in background
    /// <param name="sceneName">Name of scene to be loaded</param>
    /// <param name="loadData">Flag to determine if data should be loaded after loading of scene</param>
    /// </summary>
    IEnumerator LoadAsyncScene(string sceneName, bool loadData) {
        Debug.Log("<color='blue'>STARTED LOAD ASYNC </color>");
        General.LogEntrance("Load Asynce Scene");

        operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

        while (!operation.isDone) {
            UpdateProgressUI(operation.progress);
            yield return null;
        }

        //		if (operation.isDone) {
        UpdateProgressUI(operation.progress);

        General.LogExit("Load Asynce Scene");

        operation = null;
        canvas.gameObject.SetActive(false);
        
        //		}
        yield return null;
    }
}
