using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public GameObject LevelButtonPrefab;
    public GameObject LevelButtonContainer;

    public GameObject ShopColorPrefab;
    public GameObject ShopColorContainer;

    public GameObject ShopButtonPrefab;
    public GameObject ShopButtonContainer;

    private Transform cameraTransform;
    private Transform cameraDesiredLookAt;

    public GameObject Tri;
    public GameObject TestButton;

    private const float CAMERA_TRANSITION_SPEED = 3.0f;


    Color[] btnColor =
    {
        new Color(255/255f,48/255f,49/255f,255/255f),
        new Color(255/255f,165/255f,46/255f,255/255f),
        new Color(206/255f,46/255f,255/255f,255/255f),
        new Color(255/255f,47/255f,253/255f,255/255f),
        new Color(225/255f,255/255f,46/255f,255/255f),
        new Color(255/255f,42/255f,46/255f,255/255f),
        new Color(46/255f,255/255f,189/255f,255/255f),
        new Color(104/255f,47/255f,255/255f,255/255f),
        new Color(255/255f,255/255f,46/255f,255/255f),
        new Color(159/255f,255/255f,46/255f,255/255f),
        new Color(46/255f,255/255f,140/255f,255/255f),
        new Color(132/255f,46/255f,255/255f,255/255f),
        new Color(242/255f,255/255f,46/255f,255/255f),
        new Color(46/255f,255/255f,129/255f,255/255f),
        new Color(46/255f,137/255f,255/255f,255/255f),
        new Color(57/255f,106/255f,255/255f,255/255f),
    };

    private void Start()
    {
        int btnCounter = 0;
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
        foreach(Sprite ColorTexture in textures)
        {
            GameObject container = Instantiate(ShopColorPrefab) as GameObject;
            container.GetComponent<Image>().sprite = ColorTexture;
            container.transform.SetParent(ShopColorContainer.transform, false);

            int ColorNum = btnCounter;
            container.GetComponent<Button>().onClick.AddListener(() => ChangeColor(btnColor[ColorNum]));
            btnCounter += 1;
        }
        btnCounter = 0;
        //initialize

        Sprite[] btnImages = Resources.LoadAll<Sprite>("Buttons");

        //load button images from a folder
        foreach(Sprite btnImage in btnImages)
        {
            GameObject btncontainer = Instantiate(ShopButtonPrefab) as GameObject;
            btncontainer.GetComponent<Image>().sprite = btnImage;
            btncontainer.transform.SetParent(ShopButtonContainer.transform, false);

            Sprite curImage = btnImage;
            btncontainer.GetComponent<Button>().onClick.AddListener(() => ChangeImage(curImage));
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

    public void ChangeColor(Color btnColor)
    {
        Tri.GetComponent<Image>().color = btnColor;
    }
    public void ChangeImage(Sprite btnImage)
    {
        Tri.GetComponent<Image>().sprite = btnImage;
    }
}
