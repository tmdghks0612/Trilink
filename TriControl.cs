using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TriControl : MonoBehaviour
{
    public enum Shape { Triangle = 0, Rectangle = 1 };
    public class TriPoint
    {
        public bool exist;
        public Shape shape;
        public Vector3 coord;
    }
    public TriPoint triPoint = new TriPoint();
    public void TriClicked()
    {
        if(triPoint.exist)
        {
            triPoint.exist = false;
            GameObject triManagerObject = GameObject.Find("TriManager");
            TriManager triManager = triManagerObject.GetComponent<TriManager>();
            triManager.TriIncrease(this.gameObject);
            Debug.Log(triManager.TriCounter);
        }
        else
        {
            triPoint.exist = true;
            GameObject triManagerObject = GameObject.Find("TriManager");
            TriManager triManager = triManagerObject.GetComponent<TriManager>();
            triManager.TriRemove(this.gameObject);
            Debug.Log(triManager.TriCounter);
        }
    }
}
