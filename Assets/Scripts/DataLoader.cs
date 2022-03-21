using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoader : MonoBehaviour
{
    public GameObject PlanetBar;
    
    void Start()
    {   
        // Debug.Log("Started Data.loadJobs()");
        Data.loadJobs();
        // Debug.Log("Ended Data.loadJobs()");
        // Debug.Log("Started Data.loadBuildings()");
        Data.loadBuildings();
        // Debug.Log("Ended Data.loadBuildings()");
        // Debug.Log("Started Data.loadDistrics()");
        Data.loadDistricts();
        // Debug.Log("Ended Data.loadDistrics()");
        // Debug.Log("Started Data.loadPlanets()");
        Data.loadPlanets();
        // Debug.Log("Ended Data.loadPlanets()");
        // Debug.Log("Started Data.loadGovernments()");
        Data.loadGovernments();
        // Debug.Log("Ended Data.loadGovernments()");

        PlanetBar.SetActive(true);
    }
}
