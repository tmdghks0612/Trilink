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
        GameObject triManagerObject = GameObject.Find("TriManager");
        TriManager triManager = triManagerObject.GetComponent<TriManager>();
        if (!triManager.CurTriList.Contains(this.gameObject))
        {
            
            triManager.TriIncrease(this.gameObject);
            Debug.Log(triManager.TriCounter);
        }
        else
        {
            triManager.TriRemove(this.gameObject);
            Debug.Log(triManager.TriCounter);
        }
    }
}
