using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TriManager : MonoBehaviour
{
    public TextAsset textfile;
    public GameObject Tri;
    public GameObject TriRect;
    public GameObject PlayerPanel;
    public List<GameObject> CurTriList;
    public List<GameObject> DelTriList;
    public int TriCounter = 0;
    private bool rectangleFlag = false;

    public Color TriColor;
    public Color TriRectColor;

    public Color highlight = new Color(1.0f, 1.0f, 1.0f);

    public TriControl.TriPoint[] TriArr;
    // Start is called before the first frame update
    void Start()
    {
        int PointNumber = 0;

        TriColor = Tri.GetComponent<Image>().color;
        TriRectColor = TriRect.GetComponent<Image>().color;
        string content = textfile.text;
        var Lines = content.Split('\n');
        PointNumber = int.Parse(Lines[0]);
        TriArr = new TriControl.TriPoint[PointNumber + 1];

        InitPanel(PointNumber, Lines);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void InitPanel(int PointNumber, string[] Lines)
    {
        int PointCounter = 0;

        for (PointCounter = 1; PointCounter < PointNumber + 1; ++PointCounter)
        {
            var words = Lines[PointCounter].Split(' ');
            GameObject currButton;
            Vector3 currVector;
            TriControl.Shape currShape;

            //initialization of Tris
            currShape = (TriControl.Shape)int.Parse(words[0]);
            currVector = new Vector3(float.Parse(words[1]), float.Parse(words[2]), float.Parse(words[3]));

            if (currShape == TriControl.Shape.Triangle)
            { currButton = Instantiate(Tri) as GameObject; }
            else
            { currButton = Instantiate(TriRect) as GameObject; }

            currButton.GetComponent<TriControl>().triPoint.exist = true;
            currButton.GetComponent<TriControl>().triPoint.shape = currShape;
            currButton.GetComponent<TriControl>().triPoint.coord = currVector;
            currButton.transform.Translate(currVector);
            currButton.transform.SetParent(PlayerPanel.transform, false);

            Debug.Log(PointCounter);
            Debug.Log(currVector);
        }
    }

    

    public void TriIncrease(GameObject CurPoint)
    {
        //Use TriArr with TriIncrease to let what shape current button is@@@@@@@@@@@@@@@@@@@@
        int ClearNumber = 3;
        TriCounter += 1;
        CurTriList.Add(CurPoint);
        SetTriHighlight(CurPoint);

        Debug.Log("Highlighted!");
        //if clicked button is a rectangle

        if (CurPoint.GetComponent<TriControl>().triPoint.shape == TriControl.Shape.Rectangle)
        {
            //if another rectangle was chosen beforehand
            if (rectangleFlag == true)
            {
                ResetList(CurTriList);
                Debug.Log("Two rectangles chosen");
            }
            //raise rectangleFlag
            else { rectangleFlag = true; }
        }
        //if a rectangle was involved in the list, clear with 4 TriPoints
        if (rectangleFlag) { ClearNumber = 4; }

        if (TriCounter == ClearNumber)
        {
            TriCounter = 0;
            ClearList(CurTriList);
        }
        Debug.Log(CurTriList);
    }

    public void TriRemove(GameObject CurPoint)
    {
        Debug.Log("TriRemove");
        TriCounter -= 1;
        SetTriNoHighlight(CurPoint);
        CurPoint.GetComponent<TriControl>().triPoint.exist = true;
        if (CurPoint.GetComponent<TriControl>().triPoint.shape == TriControl.Shape.Rectangle)
        {
            rectangleFlag = false;
        }
        CurTriList.Remove(CurPoint);
    }

    public void ResetList(List<GameObject> TriList)
    {
        TriCounter = 0;
        rectangleFlag = false;
        TriList.Clear();
    }

    public void ResetAllTris()
    {
        GameObject[] respawns = GameObject.FindGameObjectsWithTag("Respawn");
        foreach (GameObject respawn in respawns)
        {
            SetTriNoHighlight(respawn);
        }
        foreach(GameObject deleted in DelTriList)
        {
            deleted.GetComponent<TriControl>().triPoint.exist = true;
            deleted.SetActive(true);
            deleted.GetComponent<TriControl>().triPoint.exist = true;
        }
        DelTriList.Clear();
        ResetList(CurTriList);
    }

    public void ClearList(List<GameObject> TriList)
    {
        foreach (GameObject triObject in TriList)
        {
            triObject.GetComponent<TriControl>().triPoint.exist = false;
            DelTriList.Add(triObject);
            triObject.SetActive(false);
            SetTriNoHighlight(triObject);
        }
        TriCounter = 0;
        rectangleFlag = false;
        TriList.Clear();
    }

    public void SetTriHighlight(GameObject CurObject)
    {
        CurObject.GetComponent<Image>().color = Color.white;
    }
    public void SetTriNoHighlight(GameObject CurObject)
    {
        Color prevColor;
        if (CurObject.GetComponent<TriControl>().triPoint.shape == TriControl.Shape.Triangle)
        { prevColor = TriColor; }
        else
        { prevColor = TriRectColor; }
        CurObject.GetComponent<Image>().color = prevColor;
    }

}
