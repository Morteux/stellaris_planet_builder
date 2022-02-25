using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EthicsButtonManager : MonoBehaviour
{
    private PlanetData planetData;
    private Transform EthicsPanel;

    private void Awake()
    {
        EthicsPanel = transform.parent.parent.parent.parent.Find("ButtonPanels/EthicsPanel");
        EthicsPanel.gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        planetData = transform.parent.parent.parent.parent.parent.GetComponent<PlanetData>();

        transform.GetComponent<Button>().onClick.AddListener(ShowEthicsPanel);
        EthicsPanel.Find("Close").GetComponent<Button>().onClick.AddListener(ShowEthicsPanel);
    }

    void ShowEthicsPanel()
    {
        // Debug.Log("ShowEthicssPanel");
        EthicsPanel.gameObject.SetActive(!EthicsPanel.gameObject.activeSelf);
    }
}
