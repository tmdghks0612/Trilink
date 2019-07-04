using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnControl : MonoBehaviour
{
    public GameObject PlayerPanel;

    public GameObject Tri;
    public GameObject TriRect;

    public GameObject SpawnTriButton;
    public GameObject SpawnTriRectButton;

    public GameObject TriEdit;
    public GameObject TriRectEdit;

    // Start is called before the first frame update
    void Start()
    {
        TriEdit.GetComponent<Image>().sprite = Tri.GetComponent<Image>().sprite;
        TriRectEdit.GetComponent<Image>().sprite = TriRect.GetComponent<Image>().sprite;

        SpawnTriButton.GetComponent<Image>().sprite = Tri.GetComponent<Image>().sprite;
        SpawnTriRectButton.GetComponent<Image>().sprite = TriRect.GetComponent<Image>().sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnTri()
    {
        GameObject TriEditInstance = Instantiate(TriEdit) as GameObject;
        TriEditInstance.transform.position = PlayerPanel.transform.position;
        TriEditInstance.transform.SetParent(PlayerPanel.transform, false);
    }
}
