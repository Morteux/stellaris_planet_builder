using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

// Attached to each planet.

public class PlanetTabManager : MonoBehaviour, IPointerClickHandler
{

    [System.NonSerialized] public UnityEvent leftClick;
    // [System.NonSerialized] public UnityEvent middleClick;
    [System.NonSerialized] public UnityEvent rightClick;
    private GameObject planetPanel_;
    private GameObject planetTypePanel_;
    private string name_ = "1";
    private Button btnPlanet_;

    void Start()
    {
        // Debug.Log("PlanetTabManager::Start");

        // btnPlanet_ = gameObject.GetComponent<Button>();
        // btnPlanet_.onClick.AddListener(ShowPlanetPanel);

        // // Store panel references
        // panel_ = btnPlanet_.transform.Find("Panel").gameObject;
        leftClick = new UnityEvent();
        rightClick = new UnityEvent();

        leftClick.AddListener(ShowPlanetPanel);
        // rightClick.AddListener(ShowPlanetTypePanel);
        StartInstantation();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            leftClick.Invoke();
        // else if (eventData.button == PointerEventData.InputButton.Right)
        //     rightClick.Invoke();
    }

    void ShowPlanetPanel()
    {
        // Debug.Log("PlanetTabManager::ShowPlanetPanel");

        // transform.parent.parent.GetComponent<PlanetBarManager>().ChangeActivePlanet(panel_);

        foreach (Transform child in transform.parent)
            child.GetChild(1).gameObject.SetActive(false);

        planetPanel_.SetActive(true);
    }

    // public void ShowPlanetTypePanel()
    // {
    //     // Debug.Log("PlanetTabManager::ShowPlanetTypePanel");

    //     planetTypePanel_.SetActive(!planetTypePanel_.activeSelf);
    // }

    public void StartInstantation()
    {
        // Debug.Log("PlanetTabManager::StartInstantation");

        btnPlanet_ = gameObject.GetComponent<Button>();
        // btnPlanet_.onClick.AddListener(ShowPlanetPanel);

        // Store panel references
        planetPanel_ = btnPlanet_.transform.Find("Panel").gameObject;
        planetTypePanel_ = btnPlanet_.transform.Find("Panel/ButtonPanels/PlanetTypePanel").gameObject;
    }

    public void UpdateName(string name)
    {
        // Debug.Log("PlanetTabManager::UpdateName");

        name_ = name;

        transform.Find("Panel/DistrictPanel/Text").GetComponent<Text>().text = name_;
        transform.Find("Panel/BuildingsPanel/Text").GetComponent<Text>().text = name_;
        transform.Find("Panel/OutputPanel/Text").GetComponent<Text>().text = name_;
        transform.Find("Panel/BuildPanel/Text").GetComponent<Text>().text = name_;
    }
}
