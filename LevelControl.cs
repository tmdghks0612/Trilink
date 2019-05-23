using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControl : MonoBehaviour
{
    public GameObject Tri;
    public GameObject PlayerPanel;
    // Start is called before the first frame update
    void Start()
    {
        GameObject TestButton = Instantiate(Tri) as GameObject;
        TestButton.transform.SetParent(PlayerPanel.transform,true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
