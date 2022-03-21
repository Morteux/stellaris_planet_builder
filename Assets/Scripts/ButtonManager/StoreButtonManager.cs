using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class StoreButtonManager : MonoBehaviour
{
    private Transform storePanel_;
    private string defaultFilePath_;

    void Start()
    {
        storePanel_ = transform.parent.parent.Find("Panels/StorePanel");

        defaultFilePath_ = Application.dataPath;

        GetComponent<Button>().onClick.AddListener(ShowPanel);

        // Set default filePath
        storePanel_.Find("InputField").GetComponent<InputField>().text = defaultFilePath_;
        storePanel_.Find("Store").GetComponent<Button>().onClick.AddListener(StorePlanet);

        //Hide panel
        storePanel_.gameObject.SetActive(false);
    }

    void ShowPanel()
    {
        storePanel_.gameObject.SetActive(!storePanel_.gameObject.activeSelf);
    }

    void StorePlanet()
    {
        // Debug.Log("StorePlanet");

        // Get active planet tab
        Transform planets = transform.parent.parent.parent.Find("PlanetBar/Planets");
        Transform firstActivePlanet = null;

        int i = 0;
        while (i < planets.childCount && firstActivePlanet == null)
        {
            if (planets.GetChild(i).parent == planets && planets.GetChild(i).Find("Panel").gameObject.activeSelf == true)
                firstActivePlanet = planets.GetChild(i);
            ++i;
        }

        // Write relevant planet data to string
        PlanetData planetData = firstActivePlanet.GetComponent<PlanetData>();
        string fileData = "";

        // Planet name
        fileData += "Name:" + planetData.planetName_.GetComponent<Text>().text + "\n";

        // Buildings
        fileData += "Buildings:";
        foreach (Building building in planetData.buildings_)
            if (building != null)
                fileData += building.name_ + ",";
        fileData += "\n";

        // Districs
        fileData += "Districs:";
        foreach (KeyValuePair<string, int> district in planetData.district_)
            fileData += district.Key + "=" + district.Value + ",";
        fileData += "\n";

        // Custom jobs
        fileData += "CustomJobs:";
        foreach (KeyValuePair<Job, int> job in planetData.customizeJobs_)
            if(job.Value != 0)
            fileData += job.Key.name_ + "=" + job.Value + ",";
        fileData += "\n";

        // Custom resources
        fileData += "CustomResources:";
        foreach (KeyValuePair<Data.Resource, int> resource in planetData.customizeResources_)
            if(resource.Value != 0)
            fileData += resource.Key + "=" + resource.Value + ",";
        fileData += "\n";

        // Percentage resources
        fileData += "CustomPercentage:";
        foreach (KeyValuePair<Data.Resource, float> resource in planetData.percentageResources_)
            if(resource.Value != 0.0f)
            fileData += resource.Key + "=" + resource.Value.ToString().Replace(",", ".") + ",";
        fileData += "\n";

        // Planet size
        fileData += "Size:" + planetData.planetSize_ + "\n";

        // Planet type
        fileData += "Type:" + planetData.planetType_ + "\n";

        // Government
        fileData += "Government:" + planetData.government_ + "\n";

        // Store notes
        fileData += planetData.transform.Find("Panel/ButtonPanels/NotesPanel/InputField").GetComponent<InputField>().text;

        // Create or overwrite planetName_.txt file
        File.WriteAllText(storePanel_.Find("InputField").GetComponent<InputField>().text + "\\" + planetData.planetName_.GetComponent<Text>().text + ".spbs", fileData);

        ShowPanel();
    }
}
