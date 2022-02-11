using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Attached to planet bar, which manage all planet tabs

public class PlanetBarManager : MonoBehaviour
{
    public GameObject planetTabPrefab_;

    [System.NonSerialized] public static GameObject PlanetBar_;
    [System.NonSerialized] public static Button btnNewPlanet_;

    void Awake()
    {
        PlanetBar_ = gameObject;
        btnNewPlanet_ = gameObject.transform.GetChild(0).gameObject.GetComponent<Button>();
        btnNewPlanet_.onClick.AddListener(AddPlanetTab);
    }

    void AddPlanetTab()
    {
        // Debug.Log("PlanetBarManager::AddPlanetTab");

        // Copy last planet tab position and add it +100 at x axis. This is the position for the new planet tab.
        Vector3 buttonPos = gameObject.transform.GetChild(1).GetChild(gameObject.transform.GetChild(1).childCount - 1).gameObject.transform.position;
        buttonPos.x += 100;

        // Copy positions from child's last planet tab and copy them to the new planet tab.
        Vector3 panelPos = gameObject.transform.GetChild(1).GetChild(gameObject.transform.GetChild(1).childCount - 1).transform.GetChild(1).transform.position;

        // Instantiate a new planet tab
        GameObject newPlanetTab = Instantiate(planetTabPrefab_, buttonPos, gameObject.transform.rotation);
        newPlanetTab.transform.SetParent(PlanetBar_.transform.GetChild(1));
        
        // Adjust new planet tab button position correctly
        newPlanetTab.transform.GetChild(0).gameObject.GetComponent<Text>().text = gameObject.transform.GetChild(1).childCount.ToString();
        newPlanetTab.transform.GetChild(1).transform.position = panelPos;

        // Update new planet name with default name
        newPlanetTab.GetComponent<PlanetTabManager>().StartInstantation();
        newPlanetTab.GetComponent<PlanetTabManager>().UpdateName((gameObject.transform.GetChild(1).childCount).ToString());
    }
}
