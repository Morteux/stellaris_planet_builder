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

    public IEnumerator LoadPlanetTab(string planetFileData)
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

        // Read file line by line and change planet data attributes
        PlanetData planetData = newPlanetTab.GetComponent<PlanetData>();

        // Wait until new planet is fully loaded
        yield return new WaitUntil(() => planetData.isInitialized_);

        // Debug.Log(planetData);
        string[] planetFileDataLines = planetFileData.Split('\n');

        // Planet name
        string planetName = planetFileDataLines[0].Split(':')[1];
        newPlanetTab.transform.Find("PlanetName").GetComponent<Text>().text = planetName;
        newPlanetTab.transform.Find("Panel/PlanetNameInputField").GetComponent<InputField>().text = planetName;

        // Buildings
        Building[] buildings = new Building[12];
        int i = 0;
        foreach (string building in planetFileDataLines[1].Split(':')[1].Split(','))
        {
            if(building != "")
                buildings[i] = Building._buildings_[building];
            ++i;
        }
        planetData.buildings_ = buildings;

        // Districs
        Dictionary<string, int> districts = new Dictionary<string, int>();
        foreach (string district in planetFileDataLines[2].Split(':')[1].Split(','))
            if(district != "")
            {
                string[] keyValuePair = district.Split('=');
                int.TryParse(keyValuePair[1], out int districtValue);
                districts.Add(keyValuePair[0], districtValue);
            }
        planetData.district_ = districts;

        // Custom jobs
        Dictionary<Job, int> customizeJobs = new Dictionary<Job, int>();
        foreach (string customizeJob in planetFileDataLines[3].Split(':')[1].Split(','))
            if(customizeJob != "")
            {
                string[] keyValuePair = customizeJob.Split('=');
                int.TryParse(keyValuePair[1], out int customizeJobsValue);
                customizeJobs.Add(Job._jobs_[keyValuePair[0]], customizeJobsValue);
            }
        planetData.customizeJobs_ = customizeJobs;

        // Custom resources
        Dictionary<Data.Resource, int> customizeResources = new Dictionary<Data.Resource, int>();
        foreach (string customizeJob in planetFileDataLines[3].Split(':')[1].Split(','))
            if(customizeJob != "")
            {
                string[] keyValuePair = customizeJob.Split('=');
                int.TryParse(keyValuePair[1], out int customizeResourcesValue);
                customizeResources.Add(Data.String_to_Resource(keyValuePair[0]), customizeResourcesValue);
            }
        planetData.customizeResources_ = customizeResources;

        // Percentage resources
        Dictionary<Data.Resource, float> percentageResources = new Dictionary<Data.Resource, float>();
        foreach (string customizeJob in planetFileDataLines[3].Split(':')[1].Split(','))
            if(customizeJob != "")
            {
                string[] keyValuePair = customizeJob.Split('=');
                float.TryParse(keyValuePair[1], out float percentageResourcesValue);
                percentageResources.Add(Data.String_to_Resource(keyValuePair[0]), percentageResourcesValue);
            }
        planetData.percentageResources_ = percentageResources;

        // Planet size
        int.TryParse(planetFileDataLines[6].Split(':')[1], out int planetSize);
        planetData.planetSize_ = planetSize;

        // Planet type
        planetData.planetType_ = planetFileDataLines[7].Split(':')[1];
        planetData.transform.GetComponentInChildren<PlanetTypeButtonManager>().LoadPlanetType(planetData.planetType_);

        // Government
        planetData.government_ = planetFileDataLines[8].Split(':')[1];
        planetData.transform.GetComponentInChildren<GovernmentButtonManager>().LoadGovernment(planetData.government_);

        // Update all planet information
        planetData.transform.GetComponentInChildren<BuildManager>().UpdateBuildingButtons();
        planetData.transform.GetComponentInChildren<BuildManager>().UpdateBuildingButtonsByRequirements();

        planetData.transform.GetComponentInChildren<DistrictManager>().LoadDistrictSlots(planetData.district_, planetData.planetSize_);

        planetData.transform.GetComponentInChildren<SlotsManager>().LoadBuildings(planetData.buildings_);

        planetData.UpdatePlanetData();

        // All called in LoadBuildings
        // planetData.transform.GetComponentInChildren<RequirementButtonManager>().UpdateRequirements();
        // planetData.transform.GetComponentInChildren<CostButtonManager>().UpdateCosts();
        // planetData.transform.GetComponentInChildren<EffectButtonManager>().UpdateEffects();
        // planetData.transform.GetComponentInChildren<JobsButtonManager>().UpdateJobs();
    }
}
