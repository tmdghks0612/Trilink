using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelControl : MonoBehaviour
{
    public GameObject Tri;
    public GameObject PlayerPanel;
    public GameObject ResetPopup;
    public GameObject EndLevelPopup;
    public GameObject UploadPopup;

    public TriManager TriManagerInst;
    public Transform CameraTransform;
    // Start is called before the first frame update

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
        Debug.Log("I'm here!!");
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
        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@ server job
    }
}
