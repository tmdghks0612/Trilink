using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControl : MonoBehaviour
{
    public enum Shape { Triangle = 0, Rectangle = 1 };
    public GameObject Tri;
    public GameObject PlayerPanel;
    // Start is called before the first frame update

    public TextAsset textfile;

    public struct TriPoint
    {
        public bool exist;
        public Shape shape;
        public Vector3 coord;
    }
    public TriPoint[] TriArr;
    
    void Start()
    {
        int PointNumber = 0;
        int PointCounter = 0;
        //input PointNumber and PointCounter by File in the 
        
        string content = textfile.text;
        var Lines = content.Split('\n');
        PointNumber = int.Parse(Lines[0]);
        TriArr = new TriPoint[PointNumber+1];

        for (PointCounter = 1; PointCounter < PointNumber+1; ++PointCounter)
        {
            GameObject TestButton = Instantiate(Tri) as GameObject;
            var words = Lines[PointCounter].Split(' ');
            Debug.Log(PointCounter);
            TriArr[PointCounter] = new TriPoint();
            TriArr[PointCounter].exist = true;
            TriArr[PointCounter].shape = (Shape)int.Parse(words[0]);
            TriArr[PointCounter].coord = new Vector3(float.Parse(words[1]), float.Parse(words[2]), float.Parse(words[3]));
            TestButton.transform.Translate(TriArr[PointCounter].coord);
            Debug.Log(TriArr[PointCounter].coord);
            TestButton.transform.SetParent(PlayerPanel.transform, false);
        }
        
        //used when file read enabled
    }

    // Update is called once per frame
    void Update()
    {
    }
}
