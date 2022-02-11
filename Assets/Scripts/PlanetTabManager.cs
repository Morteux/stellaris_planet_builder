using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Attached to each planet tab.

public class PlanetTabManager : MonoBehaviour
{
    [System.NonSerialized] public GameObject panel_;

    private string name_ = "1";
    private string type_;
    private Button btnPlanet_;

    void Start()
    {
        btnPlanet_ = gameObject.GetComponent<Button>();
        btnPlanet_.onClick.AddListener(ShowPlanetPanel);

        // Store childs references
        panel_ = btnPlanet_.transform.GetChild(1).gameObject;
    }

    void ShowPlanetPanel()
    {
        // Debug.Log("PlanetTabManager::ShowPlanetPanel");

        foreach(Transform child in gameObject.transform.parent)
        {
            child.GetChild(1).gameObject.SetActive(false);
        }
        panel_.SetActive(true);
    }

    public void StartInstantation()
    {
        // Debug.Log("PlanetTabManager::StartInstantation");
        btnPlanet_ = gameObject.GetComponent<Button>();
        btnPlanet_.onClick.AddListener(ShowPlanetPanel);

        // Store childs references
        panel_ = btnPlanet_.transform.GetChild(1).gameObject;
    }

    public void UpdateName(string name)
    {
        // Debug.Log("PlanetTabManager::UpdateName");

        name_ = name;

        transform.GetChild(1).GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = "" + name_;
        transform.GetChild(1).GetChild(1).GetChild(0).gameObject.GetComponent<Text>().text = "" + name_;
        transform.GetChild(1).GetChild(2).GetChild(0).gameObject.GetComponent<Text>().text = "" + name_;
        transform.GetChild(1).GetChild(3).GetChild(0).gameObject.GetComponent<Text>().text = "" + name_;
    }
}
