using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;



public class MainMenu : MonoBehaviour
{
    public int NUM_OF_SETTINGS=4;
    public float highlight = 1.2f;
    public float LevelButtonScale = 0.5f;
    public bool IsSettings = false;

    public GameObject LevelButtonPrefab;
    public GameObject LevelButtonContainer;

    public GameObject ShopColorPrefab;
    public GameObject TriColorContainer;
    public GameObject TriRectColorContainer;

    public GameObject ShopButtonPrefab;
    public GameObject TriButtonContainer;
    public GameObject TriRectButtonContainer;
    public GameObject CheckmarkPrefab;

    private Transform cameraTransform;
    private Transform cameraDesiredLookAt;

    public GameObject Tri;
    public GameObject TriRect;
    public GameObject TestButton;

    public GameObject[] CheckmarkList = null;

    public GameObject CurrentTriColor;
    public GameObject CurrentTriImage;
    public GameObject CurrentTriRectColor;
    public GameObject CurrentTriRectImage;
    
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

    public Color TriColor = new Color(255 / 255f, 48 / 255f, 49 / 255f, 255 / 255f);
    public Color TriRectColor = new Color(255 / 255f, 48 / 255f, 49 / 255f, 255 / 255f);

    private void Start()
    {
        int btnCounter = 0;

        CheckmarkList = new GameObject[NUM_OF_SETTINGS];
        cameraTransform = Camera.main.transform;

        Sprite[] thumbnails = Resources.LoadAll<Sprite>("Levels");
        //Sprite array to save all thumbnails in "Levels" in Resources
        foreach (Sprite thumbnail in thumbnails)
        {
            GameObject container = Instantiate(LevelButtonPrefab) as GameObject;
            container.transform.localScale = new Vector3(LevelButtonScale, LevelButtonScale, 1.0f);
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
            container.transform.SetParent(TriColorContainer.transform, false);

            int ColorNum = btnCounter;
            container.GetComponent<Button>().onClick.AddListener(() => TriChangeColor(btnColor[ColorNum]));
            btnCounter += 1;
        }
        btnCounter = 0;
        foreach (Sprite ColorTexture in textures)
        {
            GameObject container = Instantiate(ShopColorPrefab) as GameObject;
            container.GetComponent<Image>().sprite = ColorTexture;
            container.transform.SetParent(TriRectColorContainer.transform, false);

            int ColorNum = btnCounter;
            container.GetComponent<Button>().onClick.AddListener(() => TriRectChangeColor(btnColor[ColorNum]));
            btnCounter += 1;
        }
        //initialize

        Sprite[] tribtnImages = Resources.LoadAll<Sprite>("TriShape");

        //load button images from a folder
        foreach(Sprite btnImage in tribtnImages)
        {
            GameObject btncontainer = Instantiate(ShopButtonPrefab) as GameObject;
            btncontainer.GetComponent<Image>().sprite = btnImage;
            btncontainer.transform.SetParent(TriButtonContainer.transform, false);

            Sprite curImage = btnImage;
            btncontainer.GetComponent<Button>().onClick.AddListener(() => TriChangeImage(curImage));
        }

        Sprite[] rectbtnImages = Resources.LoadAll<Sprite>("TriRectShape");

        //load button images from a folder
        foreach (Sprite btnImage in rectbtnImages)
        {
            GameObject btncontainer = Instantiate(ShopButtonPrefab) as GameObject;
            btncontainer.GetComponent<Image>().sprite = btnImage;
            btncontainer.transform.SetParent(TriRectButtonContainer.transform, false);

            Sprite curImage = btnImage;
            btncontainer.GetComponent<Button>().onClick.AddListener(() => TriRectChangeImage(curImage));
        }

        //testing
        for(int i=0; i<NUM_OF_SETTINGS; ++i)
        {
            CheckmarkList[i] = Instantiate<GameObject>(CheckmarkPrefab);
        }

        //initialize buttons to default images @@@@@@change to saved settings later on
        if (IsSettings == false)
        {
            InitializeBtnImage();
        }

        SetCheckmarks();
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
        string path = "Assets/Resources/LevelsText/" + sceneName + ".txt";

        var reader = new StreamReader(path);
        var writer = new StreamWriter("Assets/Resources/SceneText.txt",false);

        writer.Write(reader.ReadToEnd());
        writer.Close();

        SceneManager.LoadScene("BaseScene");
    }

    public void LookAtMenu(Transform menuTransform)
    {
        cameraDesiredLookAt = menuTransform;
    }

    public void TriChangeColor(Color btnColor)
    {
        TriColor = btnColor;
        Tri.GetComponent<Image>().color = btnColor;
        //
        CurrentTriColor = EventSystem.current.currentSelectedGameObject;
        SetCheckmarks();
    }
    public void TriChangeImage(Sprite btnImage)
    {
        Tri.GetComponent<Image>().sprite = btnImage;
        CurrentTriImage = EventSystem.current.currentSelectedGameObject;
        SetCheckmarks();
    }

    public void TriRectChangeColor(Color btnColor)
    {
        TriRectColor = btnColor;
        TriRect.GetComponent<Image>().color = btnColor;

        CurrentTriRectColor = EventSystem.current.currentSelectedGameObject;
        SetCheckmarks();
    }
    public void TriRectChangeImage(Sprite btnImage)
    {
        TriRect.GetComponent<Image>().sprite = btnImage;

        CurrentTriRectImage = EventSystem.current.currentSelectedGameObject;
        SetCheckmarks();
    }

    public void LocateCheckmark(GameObject Checkmark, GameObject CurrentButton)
    {
        if(Checkmark == null)
        {
            Checkmark = Instantiate(CheckmarkPrefab);
        }
        Checkmark.transform.SetParent(CurrentButton.transform,false);
    }

    public void SetCheckmarks()
    {
        LocateCheckmark(CheckmarkList[0], CurrentTriColor);
        LocateCheckmark(CheckmarkList[1], CurrentTriImage);
        LocateCheckmark(CheckmarkList[2], CurrentTriRectColor);
        LocateCheckmark(CheckmarkList[3], CurrentTriRectImage);
    }

    public void InitializeBtnImage()
    {
        Sprite TriRectDefaultImage = Resources.Load<Sprite>("TriRectShape/default");
        Sprite TriDefaultImage = Resources.Load<Sprite>("TriShape/default");
        GameObject DefaultContainer;

        TriRect.GetComponent<Image>().sprite = TriRectDefaultImage;
        Tri.GetComponent<Image>().sprite = TriDefaultImage;


        DefaultContainer = GameObject.Find("TriColorContainer");
        CurrentTriColor = DefaultContainer.transform.GetChild(0).gameObject;
        LocateCheckmark(CheckmarkList[0], CurrentTriColor);

        DefaultContainer = GameObject.Find("TriButtonContainer");
        CurrentTriImage = DefaultContainer.transform.GetChild(0).gameObject;
        LocateCheckmark(CheckmarkList[0], CurrentTriImage);

        DefaultContainer = GameObject.Find("TriRectColorContainer");
        CurrentTriRectColor = DefaultContainer.transform.GetChild(0).gameObject;
        LocateCheckmark(CheckmarkList[0], CurrentTriRectColor);

        DefaultContainer = GameObject.Find("TriRectButtonContainer");
        CurrentTriRectImage = DefaultContainer.transform.GetChild(0).gameObject;
        LocateCheckmark(CheckmarkList[0], CurrentTriRectImage);
    }

    public void EditLevel()
    {
        SceneManager.LoadScene("EditScene");
    }
}
