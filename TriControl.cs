using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TriControl : MonoBehaviour
{
    public void TriClicked()
    {
        GameObject triManagerObject = GameObject.Find("TriManager");
        TriManager triManager = triManagerObject.GetComponent<TriManager>();
        triManager.TriIncrease(this.gameObject);
        Debug.Log(triManager.TriCounter);
    }
}
