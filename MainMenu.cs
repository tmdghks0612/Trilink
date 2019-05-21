using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject LevelButtonPrefab;
    public GameObject LevelButtonContainer;

    private void Start()
    {
        Sprite[] thumbnails = Resources.LoadAll<Sprite>("Levels");
        //Sprite array to save all thumbnails in "Levels" in Resources
        foreach (Sprite thumbnail in thumbnails)
        {
            GameObject container = Instantiate(LevelButtonPrefab) as GameObject;
            container.GetComponent<Image>().sprite = thumbnail;
            container.transform.SetParent(LevelButtonContainer.transform, false);
            //overload to spawn object in parent location

            string sceneName = thumbnail.name;
            //change scene name here
            container.GetComponent<Button>().onClick.AddListener(() => LoadLevel(sceneName));
            //fetch a function on click

        }
    }

    private void LoadLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
