using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class LevelControl : MonoBehaviour
{
    public GameObject Tri;
    public GameObject PlayerPanel;
    public TriManager TriManagerInst;
    public Transform CameraTransform;
    // Start is called before the first frame update

    void Start()
    {

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
    
    public void ReturnScene()
    {
        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        Vector3 Direction = new Vector3(-928.0f, 0.0f, 0.0f);
        CameraTransform.rotation
    }
    
    public void PopupReset()
    {
        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        Vector3 Direction = new Vector3(928.0f, 0.0f, 0.0f);
        CameraTransform.Translate(Direction);
    }

}
