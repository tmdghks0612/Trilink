using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.Networking;
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

    //WWWForm variables
    public int Id = 405;
    public string Name = "Editted";
    public string ImageUrl;

    public string[] ImageUrlarr =
    {
        "https://lh3.googleusercontent.com/TdOjHNDtUO3BBFTg3FFJZz47VOcsodslejs4ogEhZPxug5fXcSA8k0uanJ6qQ0KdOYYV=s85",
        "http://lh3.googleusercontent.com/0_RtOnwpnViZLOgnGKWZKQCy5775Bhdue27EiYOBuOQ2FQ3gzpqSKxdi1cVR05Ptef-9UeY=s50",
        "http://lh3.googleusercontent.com/LNLR_6nbYxCFwhUMvwoWaUO1G0LjZG4tar2YSLt0Yht4NyieoqZuKLavaqg7BpTt2x-s=s50",
        "http://lh3.googleusercontent.com/1ZSeoDvC-G9rDDW8JhrDdfNvce4-E2E9vjayHLa2TOTzFiuQohPIIHUiIjWofQypbuWR7Zg=s50",
        "http://lh3.googleusercontent.com/tlTEdaQTcPETAxJxeNnOtVMpB6KVKHnNkAlgZJbswY1OF9Xjls6LSRE4QD9_sDVkZXFY=s50",
        "http://lh3.googleusercontent.com/48oHZKWZsSzk-CszwNeLoIhy0f2KK85O9fXIcXujohBLYrsRYolmXoCAAlM1w8eoq4vyTQU=s85"
    };

    public string imageUrl;
    public string LevelText = "";
    public float highscore = 0.00f;
    
    public List<GameObject> TriEditList;
    public List<GameObject> TriRectEditList;

    public Sprite UploadSprite;

    public string urlWeb = "http://ec2-3-15-131-103.us-east-2.compute.amazonaws.com:8081";


    void Start()
    {
        Camera.main.backgroundColor = Color.black;
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
        TriManagerInst.ResetAllTris();
        ReturnScene();
        //reset score and etc
    }

    public void ClearScene()
    {
        GameObject[] Tris = GameObject.FindGameObjectsWithTag("Respawn");
        foreach(GameObject Tri in Tris)
        {
            Tri.SetActive(false);
        }
        ReturnScene();
        //reset score and etc
    }

    public void ReturnObject(GameObject PanelCurrent)
    {
        Vector3 Direction = new Vector3(675.0f, 0.0f, 0.0f);
        PanelCurrent.transform.position = PlayerPanel.gameObject.GetComponent<RectTransform>().position + Direction;
    }
    
    public void ReturnScene()
    {
        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        Vector3 Direction = new Vector3(675.0f, 0.0f, 0.0f);
        ResetPopup.transform.position = PlayerPanel.gameObject.GetComponent<RectTransform>().position+Direction;
    }
    
    public void PopupObject(GameObject PanelCurrent)
    {
        PanelCurrent.transform.position = PlayerPanel.transform.position;
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

    public void ReturnUploadScene()
    {
        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        Vector3 Direction = new Vector3(675.0f, 0.0f, 0.0f);
        UploadPopup.transform.position = PlayerPanel.gameObject.GetComponent<RectTransform>().position + Direction;
    }

    public void UploadLevel()
    {

        WriteIdLevel();
        WriteNameLevel();
        WriteImageUrlLevel();
        WriteLevelTextLevel();
        WriteHighscoreLevel();

        StartCoroutine(PostRequest(urlWeb));
        SceneManager.LoadScene("Menu");

    }

    public void WriteIdLevel()
    {
        //Id = GetIdLevel()@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        Id = 405;
        //get id function needed!!
    }

    public void WriteNameLevel()
    {
        Name = InputFieldInstance.text;
    }

    public void WriteImageUrlLevel()
    {
        Texture2D texture = UploadSprite.texture;

        imageUrl = ImageUrl;

        //get url function needed!
    }

    public void WriteLevelTextLevel()
    {
        //network error will be checked before executing this function.

        LevelText = string.Format("{0}\n", TriEditList.Count + TriRectEditList.Count);

        foreach(GameObject TriEdit in TriEditList)
        {
            Vector3 position = TriEdit.transform.position;
            LevelText = LevelText + string.Format("0 {0} {1} 0.0\n",position.x, position.y);
        }

        foreach (GameObject TriRectEdit in TriRectEditList)
        {
            Vector3 position = TriRectEdit.transform.position;
            LevelText = LevelText + string.Format("1 {0} {1} 0.0\n", position.x, position.y);
        }
    }

    public void WriteHighscoreLevel()
    {
        //highscore = GetHighscore()@@@@@@@@@@@@@@@@@@@@@@@@@@@@

        //get highscore function needed!!
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
        int index = 0;
        Sprite[] thumbnails = Resources.LoadAll<Sprite>("LevelImage");
        //Sprite array to save all thumbnails in "Levels" in Resources
        foreach (Sprite thumbnail in thumbnails)
        {
            GameObject container = Instantiate(LevelButtonPrefab) as GameObject;
            container.transform.localScale = new Vector3(LevelImageScale, LevelImageScale, 1.0f);
            container.GetComponent<Image>().sprite = thumbnail;
            container.transform.SetParent(LevelButtonContainer.transform, false);
            //overload to spawn object in parent location
            int tempindex = index;
            string sceneName = thumbnail.name;
            //change scene name here
            container.GetComponent<Button>().onClick.AddListener(() => ClickLevelImage(tempindex));
            //fetch a function on click

            index++;
        }

        LocateCheckmark(LevelButtonContainer.transform.GetChild(0).gameObject);
        UploadSprite = LevelButtonContainer.transform.GetChild(0).GetComponent<Image>().sprite;
    }

    public void ClickLevelImage(int index)
    {
        GameObject ClickedObject = EventSystem.current.currentSelectedGameObject;
        UploadSprite = ClickedObject.GetComponent<Image>().sprite;
        Debug.Log(index);
        ImageUrl = ImageUrlarr[index];
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

    // Start is called before the first frame update

    IEnumerator PostRequest(string url)
    {
        WWWForm formData = new WWWForm();
        //add fields between here

        //formData.AddField("id", Id.ToString());
        formData.AddField("name", Name);
        formData.AddField("imageurl", ImageUrl);
        formData.AddField("leveltext", LevelText);
        formData.AddField("highscore", highscore.ToString());

        //add fields between here!
        using (UnityWebRequest webRequest = UnityWebRequest.Post(urlWeb, formData))
        {
            //webRequest.chunkedTransfer = false;
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                Debug.Log(":Request error:");
            }
            else
            {
                Debug.Log(":Request Sent:");
            }
        }
    }

    public class levelInfo
    {
        public int id;
        public string name;
        public string imageurl;
        public string leveltext;
        public float highscore;

        public static levelInfo CreateFromJson(string jsonString)
        {
            return JsonUtility.FromJson<levelInfo>(jsonString);
        }
    }

}
