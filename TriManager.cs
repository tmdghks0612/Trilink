using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine;

public class TriManager : MonoBehaviour
{
    public GameObject Tri;
    public GameObject TriRect;
    public GameObject PlayerPanel;

    public GameObject[] TriRectList;
    public List<GameObject> CurTriList;
    public List<GameObject> DelTriList;
    public int TriCounter = 0;
    public int TriTotal = 0;
    private bool rectangleFlag = false;

    public Text textScore;
    public Text textbestScore;

    public float score;
    public float bestscore;

    public LevelControl LevelControlInstance;

    public Color TriColor;
    public Color TriRectColor;

    public Color highlight = new Color(1.0f, 1.0f, 1.0f);
    
    public TriControl.TriPoint[] TriArr;
    // Start is called before the first frame update

    string urlWeb = "http://ec2-3-15-131-103.us-east-2.compute.amazonaws.com:8081";

    void Start()
    {
        int PointNumber = 0;
        //change to load leveltext from server@@@@@@@@@@@@@@@@@@@@@@@@@
        /*string path = "Assets/Resources/SceneText.txt";

        var reader = new StreamReader(path);

        
        string content = reader.ReadToEnd();*/
        string content = PublicLevel.sceneText;
        string bestscore = PublicLevel.highScore.ToString();

        textbestScore.text = "Best : " + bestscore.ToString();

        TriColor = Tri.GetComponent<Image>().color;
        TriRectColor = TriRect.GetComponent<Image>().color;
        var Lines = content.Split('\n');
        PointNumber = int.Parse(Lines[0]);
        TriArr = new TriControl.TriPoint[PointNumber + 1];
        InitPanel(PointNumber, Lines);

        TriRectList = GameObject.FindGameObjectsWithTag("ShapeRect");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ClearLevel()
    {
        Debug.Log("Clear!");
        LevelControlInstance.EndLevel();
        if(score > bestscore)
        {
            //send update request by PublicLevel.id on highscore@@@@@@@@@@@@@@@@@@@@@@@@
            bestscore = score;
            StartCoroutine(PutRequest(urlWeb));
        }
    }

    void InitPanel(int PointNumber, string[] Lines)
    {
        int PointCounter = 0;

        TriTotal = 0;

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
            {
                currButton = Instantiate(Tri) as GameObject;
                TriTotal += 1;
            }
            else
            { currButton = Instantiate(TriRect) as GameObject; }

            currButton.GetComponent<TriControl>().triPoint.exist = true;
            currButton.GetComponent<TriControl>().triPoint.shape = currShape;
            currButton.GetComponent<TriControl>().triPoint.coord = currVector;
            currButton.transform.Translate(currVector);
            currButton.transform.SetParent(PlayerPanel.transform, false);

            //Debug.Log(PointCounter);
            //Debug.Log(currVector);
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

        score += AddScore(TriList);
        TriList.Clear();

        textScore.text = "Score : " + score.ToString();

        if(DelTriList.Count == TriTotal)
        {
            ClearLevel();
        }
    }

    public float AddScore(List<GameObject> TriList)
    {
        //calculate score here!@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        int scoreCurrent = 0;

        foreach (GameObject ShapeRect in TriRectList)
        {
            if (Poly.ContainsPoint(TriList, ShapeRect.transform.position))
            {
                scoreCurrent += 1;
            }
        }
        return scoreCurrent;
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

    IEnumerator PutRequest(string url)
    {
        WWWForm formWWW = new WWWForm();
        //add fields between here

        formWWW.AddField("highscore", bestscore.ToString());
        //byte[] formData = System.Text.Encoding.UTF8.GetBytes(bestscore.ToString());
        //string myString = System.Text.Encoding.UTF8.GetString(formData);

        MyClass classData = new MyClass();
        classData.highscore = bestscore;

        Debug.Log(JsonUtility.ToJson(classData));

        urlWeb = url + "/" + PublicLevel.id.ToString();
        //add fields between here!
        using (UnityWebRequest webRequest = UnityWebRequest.Put(urlWeb, JsonUtility.ToJson(classData)))
        {
            //webRequest header of request to HTTP server should be set to json
            webRequest.SetRequestHeader("Content-type", "application/json");

            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                Debug.Log(":Request error:");
            }
            else
            {
                Debug.Log(":Request Sent:");
                Debug.Log(urlWeb);
            }
        }
    }

    public static class Poly
    {
        public static bool ContainsPoint(List<GameObject> polyPoints, Vector2 p)
        {
            var j = polyPoints.Count - 1;
            var inside = false;
            for (int i = 0; i < polyPoints.Count; j = i++)
            {
                var pi = polyPoints[i].transform.position;
                var pj = polyPoints[j].transform.position;
                if (((pi.y <= p.y && p.y < pj.y) || (pj.y <= p.y && p.y < pi.y)) &&
                    (p.x < (pj.x - pi.x) * (p.y - pi.y) / (pj.y - pi.y) + pi.x))
                    inside = !inside;
            }
            return inside;
        }
    }

    public class MyClass
    {
        public float highscore;
    }

}
