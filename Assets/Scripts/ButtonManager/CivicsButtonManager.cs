using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CivicsButtonManager : MonoBehaviour
{
    private PlanetData planetData;
    private Transform CivicsPanel;

    private void Awake()
    {
        CivicsPanel = transform.parent.parent.parent.parent.Find("ButtonPanels/CivicsPanel");
        CivicsPanel.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        planetData = transform.parent.parent.parent.parent.parent.GetComponent<PlanetData>();

        transform.GetComponent<Button>().onClick.AddListener(ShowCivicsPanel);
        CivicsPanel.Find("Close").GetComponent<Button>().onClick.AddListener(ShowCivicsPanel);
    }

    void ShowCivicsPanel()
    {
        // Debug.Log("ShowCivicsPanel");
        CivicsPanel.gameObject.SetActive(!CivicsPanel.gameObject.activeSelf);
    }
}
