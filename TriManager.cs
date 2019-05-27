using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriManager : MonoBehaviour
{
    public TextAsset textfile;
    public enum Shape { Triangle = 0, Rectangle = 1 };
    public GameObject Tri;
    public GameObject PlayerPanel;
    public List<GameObject> CurTriList;
    public int TriCounter = 0;
    public int shape = 3;

    public class TriPoint
    {
        public bool exist;
        public Shape shape;
        public Vector3 coord;
    }
    public TriPoint[] TriArr;
    // Start is called before the first frame update
    void Start()
    {
        int PointNumber = 0;

        string content = textfile.text;
        var Lines = content.Split('\n');
        PointNumber = int.Parse(Lines[0]);
        TriArr = new TriPoint[PointNumber + 1];

        TriLocate(PointNumber, Lines);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void TriLocate(int PointNumber, string[] Lines)
    {
        int PointCounter = 0;

        for (PointCounter = 1; PointCounter < PointNumber + 1; ++PointCounter)
        {
            GameObject TestButton = Instantiate(Tri) as GameObject;
            var words = Lines[PointCounter].Split(' ');
            Debug.Log(PointCounter);
            //initialization of Tris
            TriArr[PointCounter] = new TriPoint();
            TriArr[PointCounter].exist = true;
            TriArr[PointCounter].shape = (Shape)int.Parse(words[0]);
            TriArr[PointCounter].coord = new Vector3(float.Parse(words[1]), float.Parse(words[2]), float.Parse(words[3]));
            TestButton.transform.Translate(TriArr[PointCounter].coord);
            Debug.Log(TriArr[PointCounter].coord);
            TestButton.transform.SetParent(PlayerPanel.transform, false);
        }
    }

    public void TriIncrease(GameObject CurPoint)
    {
        //Use TriArr with TriIncrease to let what shape current button is@@@@@@@@@@@@@@@@@@@@
        TriCounter += 1;
        CurTriList.Add(CurPoint);
        if(TriCounter == shape)
        {
            TriCounter = 0;
            HideTris(CurTriList);
        }
        Debug.Log(CurTriList);
    }

    public void HideTris(List<GameObject> TriList)
    {
        foreach(GameObject triObject in TriList)
        {
            triObject.SetActive(false);
        }
        TriList.Clear();
    }
    /*void TriCheck()
{
    self
    TriIncrease// 변수 하나 늘려주고 자료구조에 저장
    TriNumber , Shape // TriNumber가 Shape 와 같은지 확인.
        FadeTris// 없애주는 거 + 게임이 끝났는지 확인
}*/
}
