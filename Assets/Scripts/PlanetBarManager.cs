using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Attached to planet bar, which manage all planet tabs

public class PlanetBarManager : MonoBehaviour
{
    public GameObject planetTabPrefab_;
    [System.NonSerialized] public GameObject activePlanet_;
    private int planetButtonDistance_;

    void Awake()
    {
        transform.Find("NewPlanet").GetComponent<Button>().onClick.AddListener(AddPlanetTab);
        planetButtonDistance_ = 80;
    }

    void Start()
    {
        activePlanet_ = transform.Find("Planets/Planet/Panel").gameObject;
    }

    void AddPlanetTab()
    {
        // Debug.Log("PlanetBarManager::AddPlanetTab");

        // Hide all planet panel
        Transform planets = transform.Find("Planets");
        foreach (Transform child in planets)
            if (child.parent == planets)
                child.Find("Panel").gameObject.SetActive(false);

        // Copy last planet tab position and add it planetButtonDistance_ at x axis. This is the position for the new planet tab.
        Vector3 buttonPos = transform.Find("Planets").GetChild(transform.Find("Planets").childCount - 1).position;
        buttonPos.x += planetButtonDistance_;

        // Copy positions from child's last planet tab and copy them to the new planet tab.
        Vector3 panelPos = transform.Find("Planets").GetChild(transform.Find("Planets").childCount - 1).GetChild(1).position;

        // Instantiate a new planet tab
        GameObject newPlanetTab = Instantiate(planetTabPrefab_, buttonPos, transform.rotation);
        newPlanetTab.transform.SetParent(transform.Find("Planets"));

        // Adjust new planet tab button position correctly
        newPlanetTab.transform.GetChild(0).GetComponent<Text>().text = transform.Find("Planets").childCount.ToString();
        newPlanetTab.transform.GetChild(1).position = panelPos;

        // Update new planet name with default name
        newPlanetTab.GetComponent<PlanetTabManager>().StartInstantation();
        newPlanetTab.GetComponent<PlanetTabManager>().UpdateName(transform.Find("Planets").childCount.ToString());
    }

    // public void ChangeActivePlanet(GameObject panel)
    // {
    //     // Debug.Log("PlanetBarManager::ChangeActivePlanet");

    //     if (activePlanet_ != panel)
    //     {
    //         activePlanet_.SetActive(false);
    //         activePlanet_ = panel;
    //         activePlanet_.SetActive(true);
    //     }
    // }
}
