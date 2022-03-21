using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

// Attached to each planet.

public class PlanetTabManager : MonoBehaviour, IPointerClickHandler
{
    [System.NonSerialized] public UnityEvent leftClick_;
    private GameObject planetPanel_;
    private GameObject planetTypePanel_;
    private string name_ = "1";
    private Button btnPlanet_;

    void Start()
    {
        // Debug.Log("PlanetTabManager::Start");

        // // Store panel references
        leftClick_ = new UnityEvent();

        leftClick_.AddListener(ShowPlanetPanel);
        StartInstantation();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            leftClick_.Invoke();
    }

    void ShowPlanetPanel()
    {
        // Debug.Log("PlanetTabManager::ShowPlanetPanel");

        foreach (Transform child in transform.parent)
            child.GetChild(1).gameObject.SetActive(false);

        planetPanel_.SetActive(true);
    }
    public void StartInstantation()
    {
        // Debug.Log("PlanetTabManager::StartInstantation");

        btnPlanet_ = gameObject.GetComponent<Button>();

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
