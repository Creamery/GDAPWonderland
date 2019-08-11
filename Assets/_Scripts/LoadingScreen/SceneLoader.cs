using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    [SerializeField] public string destination;

    /// <summary>
    /// Inspector trigger variable.
    /// </summary>
    [SerializeField] public bool triggerLoad;


    [SerializeField] LoadingScreen loadingScreen;
    [SerializeField] GameObject prefabLoadingScreen;


    void Update() {
        if (triggerLoad) {
            triggerLoad = false;
            this.LoadScene();
        }
    }

    /// <summary>
    /// Load a scene. To be used for Inspector-based trigger.
    /// </summary>
    public void LoadScene() {
        // If loading screen is not null, load screen through that.
        if (this.GetLoadingScreen() != null) {
            loadingScreen.LoadScene(destination, false);
        }
        // Else, try to instantiate and reload.
        else {
            this.InstantiatingReload();
            //			Instantiate (loadingScreenPrefab);
        }
    }

    /// <summary>
    /// Load a scene based on the passed destination name. To be used for Inspector-based trigger.
    /// </summary>
    public void LoadScene(string sceneDestination) {
        this.destination = sceneDestination;
        this.LoadScene();
    }

    /// <summary>
    /// Try to Instantiate a loading screen and reload.
    /// If instantiation fails, load normally.
    /// </summary>
    public void InstantiatingReload() {
        if(this.prefabLoadingScreen != null) {
            LoadingScreen newScreen = GameObject.Instantiate(prefabLoadingScreen).GetComponent<LoadingScreen>();
            this.loadingScreen = newScreen;
            loadingScreen.LoadScene(destination, false);
        }
        else {
            SceneManager.LoadScene(destination);
        }
    }

    /// <summary>
    /// Returns the string name of the destination scene.
    /// </summary>
    /// <returns></returns>
    public string GetDestination() {
        return this.destination;
    }

    /// <summary>
    /// Loading screen getter.z
    /// </summary>
    /// <returns></returns>
    public LoadingScreen GetLoadingScreen() {
        if(this.loadingScreen == null) {
            this.loadingScreen = FindObjectOfType<LoadingScreen>();
        }
        return this.loadingScreen;
    }
}
