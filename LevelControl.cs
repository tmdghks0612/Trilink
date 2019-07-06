using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class LevelControl : MonoBehaviour
{
    public float CheckmarkScale = 3.0f;
    public float LevelImageScale = 0.5f;

    public InputField InputFieldInstance;

    public GameObject Tri;
    public GameObject PlayerPanel;
    public GameObject ResetPopup;
    public GameObject EndLevelPopup;
    public GameObject UploadPopup;
    
    public GameObject CheckmarkPrefab;
    public GameObject CheckmarkLevelImage;

    public GameObject LevelImage;
    public GameObject LevelButtonPrefab;
    public GameObject LevelButtonContainer;

    public TriManager TriManagerInst;
    public Transform CameraTransform;

    public string NameLevelText = "Editted";

    public List<GameObject> TriEditList;
    public List<GameObject> TriRectEditList;

    public Sprite UploadSprite;

    void Start()
    {
        /*Vector3 UpVector = new Vector3(0.0f, 1.0f, 0.0f);
        Vector3 FrontVector = new Vector3(0.0f, 0.0f, 350.0f);
        CameraTransform.position = PlayerPanel.GetComponent<RectTransform>().position - FrontVector;
        CameraTransform.LookAt(PlayerPanel.GetComponent<RectTransform>().position, UpVector);*/
        //input PointNumber and PointCounter by File in the 

        //Select textfile to read @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        //TriManagerInst.TriIncrease(EventSystem.current.currentSelectedGameObject);


        //used when file read enabled
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetScene()
    {
        GameObject[] Tris = GameObject.FindGameObjectsWithTag("Respawn");
        foreach(GameObject Tri in Tris)
        {
            Tri.SetActive(false);
        }
        ReturnScene();
        //reset score and etc
    }
    
    public void ReturnScene()
    {
        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        Vector3 Direction = new Vector3(675.0f, 0.0f, 0.0f);
        ResetPopup.transform.position = PlayerPanel.gameObject.GetComponent<RectTransform>().position+Direction;
    }
    
    public void PopupReset()
    {
        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        Vector3 Direction = new Vector3(443.0f, 194.0f, 0.0f);
        ResetPopup.transform.position = PlayerPanel.transform.position;
    }

    public void PopupEndLevel()
    {
        EndLevelPopup.transform.position = PlayerPanel.transform.position;
    }
    
    public void EndLevel()
    {
        PopupEndLevel();
    }

    public void ReturnToMainmenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void PopupUpload()
    {
        UploadPopup.transform.position = PlayerPanel.transform.position;
    }

    public void ReturnUploadScene()
    {
        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        Vector3 Direction = new Vector3(675.0f, 0.0f, 0.0f);
        UploadPopup.transform.position = PlayerPanel.gameObject.GetComponent<RectTransform>().position + Direction;
    }

    public void UploadLevel()
    {
        WriteLevelText();
        WriteLevelImage();

        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@ server job

    }

    public void WriteLevelText()
    {
        NameLevelText = InputFieldInstance.text;

        // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@user input to change NameLevelText

        string path = "Assets/Resources/LevelsText/" + NameLevelText + ".txt";

        if (CheckIdenticalName(NameLevelText))
        {
            File.WriteAllText(path,string.Format("{0}\n",TriEditList.Count+TriRectEditList.Count));
        }

        foreach(GameObject TriEdit in TriEditList)
        {
            Vector3 position = TriEdit.transform.position;
            File.AppendAllText(path, string.Format("0 {0} {1} 0.0\n",position.x, position.y));
        }

        foreach (GameObject TriRectEdit in TriRectEditList)
        {
            Vector3 position = TriRectEdit.transform.position;
            File.AppendAllText(path, string.Format("1 {0} {1} 0.0\n", position.x, position.y));
        }
    }

    public void WriteLevelImage()
    { 
        Texture2D texture = UploadSprite.texture;
        byte[] texAsByte = texture.EncodeToPNG();
        string path = "Assets/Resources/Levels/" + NameLevelText + ".png";

        File.WriteAllBytes(path, texAsByte);
    }

    public bool CheckIdenticalName(string Name)
    {
        //@@@@@@@@@@@@@@@@@@@@check if identical name exists in server
        return true;
    }

    public void PopupLevelImage()
    {
        LevelImage.transform.position = PlayerPanel.transform.position;
    }

    public void ReturnLevelImage()
    {
        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        Vector3 Direction = new Vector3(675.0f, 0.0f, 0.0f);
        LevelImage.transform.position = PlayerPanel.gameObject.GetComponent<RectTransform>().position + Direction;
    }

    public void ChooseLevelImage()
    {
        Sprite[] thumbnails = Resources.LoadAll<Sprite>("LevelImage");
        //Sprite array to save all thumbnails in "Levels" in Resources
        foreach (Sprite thumbnail in thumbnails)
        {
            GameObject container = Instantiate(LevelButtonPrefab) as GameObject;
            container.transform.localScale = new Vector3(LevelImageScale, LevelImageScale, 1.0f);
            container.GetComponent<Image>().sprite = thumbnail;
            container.transform.SetParent(LevelButtonContainer.transform, false);
            //overload to spawn object in parent location

            string sceneName = thumbnail.name;
            //change scene name here
            container.GetComponent<Button>().onClick.AddListener(() => ClickLevelImage());
            //fetch a function on click

        }

        LocateCheckmark(LevelButtonContainer.transform.GetChild(0).gameObject);
    }

    public void ClickLevelImage()
    {
        GameObject ClickedObject = EventSystem.current.currentSelectedGameObject;
        UploadSprite = ClickedObject.GetComponent<Image>().sprite;
        LocateCheckmark(ClickedObject);
    }

    public void LocateCheckmark(GameObject CurrentButton)
    {
        if (CheckmarkLevelImage == null)
        {
            CheckmarkLevelImage = Instantiate(CheckmarkPrefab);
            CheckmarkLevelImage.transform.localScale = new Vector3(CheckmarkScale, CheckmarkScale, 1.0f);
        }
        CheckmarkLevelImage.transform.SetParent(CurrentButton.transform, false);
    }
    /*
    public void SetCheckmarks()
    {
        LocateCheckmark(CheckmarkList[0], CurrentTriColor);
        LocateCheckmark(CheckmarkList[1], CurrentTriImage);
        LocateCheckmark(CheckmarkList[2], CurrentTriRectColor);
        LocateCheckmark(CheckmarkList[3], CurrentTriRectImage);
    }*/
}
