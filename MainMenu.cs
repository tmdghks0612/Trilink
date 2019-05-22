using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject LevelButtonPrefab;
    public GameObject LevelButtonContainer;
    public GameObject ShopButtonPrefab;
    public GameObject ShopButtonContainer;

    private Transform cameraTransform;
    private Transform cameraDesiredLookAt;
    private const float CAMERA_TRANSITION_SPEED = 3.0f;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
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
        Sprite[] textures = Resources.LoadAll<Sprite>("Triangle_Texture");
        foreach(Sprite texture in textures)
        {
            GameObject container = Instantiate(ShopButtonPrefab) as GameObject;
            container.GetComponent<Image>().sprite = texture;
            container.transform.SetParent(ShopButtonContainer.transform, false);
        }
    }
    private void Update()
    {
        if(cameraDesiredLookAt != null)
        {
            cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, cameraDesiredLookAt.rotation,CAMERA_TRANSITION_SPEED*Time.deltaTime);
        }
    }
    private void LoadLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LookAtMenu(Transform menuTransform)
    {
        cameraDesiredLookAt = menuTransform;
    }
}
