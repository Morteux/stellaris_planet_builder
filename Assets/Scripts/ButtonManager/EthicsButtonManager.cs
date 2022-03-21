using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EthicsButtonManager : MonoBehaviour
{
    private PlanetData planetData_;
    private Transform ethicsPanel_;

    private void Awake()
    {
        ethicsPanel_ = transform.parent.parent.parent.parent.Find("ButtonPanels/EthicsPanel");
        ethicsPanel_.gameObject.SetActive(false);
    }
    
    void Start()
    {
        planetData_ = transform.parent.parent.parent.parent.parent.GetComponent<PlanetData>();

        transform.GetComponent<Button>().onClick.AddListener(ShowEthicsPanel);
        ethicsPanel_.Find("Close").GetComponent<Button>().onClick.AddListener(ShowEthicsPanel);
    }

    void ShowEthicsPanel()
    {
        // Debug.Log("ShowEthicssPanel");
        
        ethicsPanel_.gameObject.SetActive(!ethicsPanel_.gameObject.activeSelf);
    }
}
