using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CivicsButtonManager : MonoBehaviour
{
    private PlanetData planetData_;
    private Transform civicsPanel_;

    private void Awake()
    {
        civicsPanel_ = transform.parent.parent.parent.parent.Find("ButtonPanels/CivicsPanel");
        civicsPanel_.gameObject.SetActive(false);
    }

    void Start()
    {
        planetData_ = transform.parent.parent.parent.parent.parent.GetComponent<PlanetData>();

        transform.GetComponent<Button>().onClick.AddListener(ShowCivicsPanel);
        civicsPanel_.Find("Close").GetComponent<Button>().onClick.AddListener(ShowCivicsPanel);
    }

    void ShowCivicsPanel()
    {
        // Debug.Log("ShowCivicsPanel");
        civicsPanel_.gameObject.SetActive(!civicsPanel_.gameObject.activeSelf);
    }
}
