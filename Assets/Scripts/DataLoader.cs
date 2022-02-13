using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoader : MonoBehaviour
{
    public GameObject PlanetBar;
    
    // Start is called before the first frame update
    void Start()
    {
        // Data._version_ = PlayerPrefs.GetString("version");
        
        // Debug.Log("Started Data.loadJobs()");
        Data.loadJobs();
        // Debug.Log("Ended Data.loadJobs()");
        // Debug.Log("Started Data.loadBuildings()");
        Data.loadBuildings();
        // Debug.Log("Ended Data.loadBuildings()");
        // Debug.Log("Started Data.loadDistrics()");
        Data.loadDistricts();
        // Debug.Log("Ended Data.loadDistrics()");

        PlanetBar.SetActive(true);
    }
}
