using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DistrictManager : MonoBehaviour
{
    public GameObject districtSlotPrefab;
    private PlanetData planetData_;
    private Transform planet_;
    private Transform districtContent_;
    private Transform planetSizeSlider_;
    private int districtNum_;

    // District names in order
    List<string> districtNames_;

    // District transforms in order
    List<Transform> districtTransform_;

    // Start is called before the first frame update
    void Start()
    {
        planet_ = transform.parent.parent;
        planetData_ = planet_.GetComponent<PlanetData>();
        districtContent_ = transform.Find("Scroll View/Viewport/Content");

        // Initialize districtNum by default
        districtNum_ = 5;

        // Initialize default districts
        districtNames_ = new List<string>();
        districtNames_.Add("City_District");
        districtNames_.Add("Industrial_District");
        districtNames_.Add("Generator_District");
        districtNames_.Add("Mining_District");
        districtNames_.Add("Agriculture_District");

        // Initialize planetSizeSlider attributes
        planetSizeSlider_ = transform.Find("PlanetSizeSlider/Slider");
        planetSizeSlider_.GetComponent<Slider>().onValueChanged.AddListener(UpdateDistrictsSize);

        // Initialize district slots
        InstantiateNewDistrictSlots();

        // Initialize district button listeners by default
        SetDistrictButtonListeners();

        // Initialize planet size by 20
        UpdateDistrictsSize(20);
    }

    void InstantiateNewDistrictSlots()
    {
        // Remove all BuildingButtonPrefab instantiated before
        if (districtContent_.childCount > 0)
            foreach (Transform child in districtContent_)
                GameObject.Destroy(child.gameObject);

        // Reset actual districts
        planetData_.district_ = new Dictionary<string, int>();

        // Initialize PlanetData districts
        foreach (string districtName in districtNames_)
            planetData_.district_.Add(districtName, 0);

        // Initialize district transforms
        districtTransform_ = new List<Transform>();
        foreach (string districtName in districtNames_)
        {
            // Instantiate new district slot
            GameObject NewDistrictSlotPrefab = Instantiate(districtSlotPrefab, districtContent_.position, districtContent_.rotation, districtContent_);
            NewDistrictSlotPrefab.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Districts/" + districtName);
            NewDistrictSlotPrefab.transform.Find("SelectedDistrictSlot").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Districts/Colors/" + District._districts_[districtName].color_ + "_Selected");
            NewDistrictSlotPrefab.transform.Find("UnselectedDistrictSlot").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Districts/Colors/" + District._districts_[districtName].color_ + "_Unselected");

            districtTransform_.Add(NewDistrictSlotPrefab.transform);
        }
    }

    // Set district buttons listeners
    void SetDistrictButtonListeners()
    {
        // Debug.Log("SetDistrictButtonListeners");

        for (int i = 0; i < districtNum_; ++i)
        {
            int ind = i;    // Make a copy of i instead use the reference to i
            districtTransform_[ind].Find("SelectedDistrictSlot").GetComponent<Button>().onClick.AddListener(delegate { ChangeDistrictCounter(districtTransform_[ind], districtNames_[ind], 1); });
            districtTransform_[ind].Find("UnselectedDistrictSlot").GetComponent<Button>().onClick.AddListener(delegate { ChangeDistrictCounter(districtTransform_[ind], districtNames_[ind], -1); });
        }
    }

    void UpdateDistrictsSize(float newPlanetSize)
    {
        // Debug.Log("UpdateDistrictsSize");

        planetSizeSlider_.parent.Find("Counter").GetComponent<Text>().text = ((int)newPlanetSize).ToString();

        planetData_.planetSize_ = (int)newPlanetSize;

        UpdateDistrictsSlot();
    }

    // Increment or decrement districtName build in this planet
    // Mode = 1, increment
    // Mode = -1, decrement
    void ChangeDistrictCounter(Transform district, string districtName, int mode)
    {
        // Debug.Log("ChangeDistrictCounter");
        // Debug.Log(district);
        // Debug.Log(districtName);

        int selected = int.Parse(district.Find("SelectedDistrictSlot/Counter").GetComponent<Text>().text);
        int unselected = int.Parse(district.Find("UnselectedDistrictSlot/Counter").GetComponent<Text>().text);
        int planetSize = int.Parse(planetSizeSlider_.parent.Find("Counter").GetComponent<Text>().text);

        // Less districts than planet size allow OR remove district
        if (planetData_.districtCounter_ < planetSize || mode == -1)
            // Selected and unselected in range [0, planetSize]
            if (selected + mode * 1 <= planetSize && selected + mode * 1 >= 0 && unselected - mode * 1 <= planetSize && unselected - mode * 1 >= 0)
            {
                district.Find("SelectedDistrictSlot/Counter").GetComponent<Text>().text = (selected + mode * 1).ToString();
                district.Find("UnselectedDistrictSlot/Counter").GetComponent<Text>().text = (unselected - mode * 1).ToString();

                planetData_.districtCounter_ += mode;
                planetData_.district_[districtName] = selected + mode * 1;

                UpdateDistrictsSlot();

                planetData_.UpdatePlanetData();
                // Planet.GetComponentInChildren<PlanetData>().UpdatePlanetData();
                planet_.GetComponentInChildren<CostButtonManager>().UpdateCosts();
                planet_.GetComponentInChildren<JobsButtonManager>().UpdateJobs();
            }
    }

    void UpdateDistrictsSlot()
    {
        List<int> selectedDistricts = new List<int>();
        int totalSelectedDistrict = 0;

        foreach (Transform districtTransform in districtTransform_)
        {
            int partialSelectedDistrict = int.Parse(districtTransform.Find("SelectedDistrictSlot/Counter").GetComponent<Text>().text);
            selectedDistricts.Add(partialSelectedDistrict);
            totalSelectedDistrict += partialSelectedDistrict;
        }

        // Update UnavailableDistrictSlot counter

        planetSizeSlider_.GetComponent<Slider>().minValue = totalSelectedDistrict;

        for (int i = 0; i < districtNum_; ++i)
            districtTransform_[i].Find("UnavailableDistrictSlot/Counter").GetComponent<Text>().text = Math.Abs(totalSelectedDistrict - selectedDistricts[i]).ToString();

        // Update UnselectedDistrictSlot counter
        int planetSize = planetData_.planetSize_;

        List<int> absDistricts = new List<int>();

        foreach (int selectedDistrict in selectedDistricts)
            absDistricts.Add(Math.Abs(planetSize - selectedDistrict));

        List<int> finalUnselectedDistricts = new List<int>();

        for (int i = 0; i < districtNum_; ++i)
            finalUnselectedDistricts.Add(absDistricts[i] - Math.Abs(totalSelectedDistrict - selectedDistricts[i]));

        for (int i = 0; i < districtNum_; ++i)
            districtTransform_[i].Find("UnselectedDistrictSlot/Counter").GetComponent<Text>().text = finalUnselectedDistricts[i].ToString();

        // Change districts icon color when you can't build new ones
        for (int i = 0; i < districtNum_; ++i)
            if (finalUnselectedDistricts[i] == 0)
                districtTransform_[i].GetComponent<Image>().color = new Color32(150, 150, 150, 255);
            else
                districtTransform_[i].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
    }

    // Change district type by government and planet type
    public void UpdateDistrictType()
    {
        Debug.Log("ChangeDistrictType");

        // Set new districts
        districtNames_ = new List<string>();
        switch (planetData_.planetType_)
        {
            case "Ecumenopolis":
                CheckRequirementsAndAddDistrictName("Residential_Arcology");
                CheckRequirementsAndAddDistrictName("Foundry_Arcology");
                CheckRequirementsAndAddDistrictName("Industrial_Arcology");
                CheckRequirementsAndAddDistrictName("Leisure_Arcology");
                CheckRequirementsAndAddDistrictName("Sanctuary_Arcology");
                CheckRequirementsAndAddDistrictName("Administrative_Arcology");
                CheckRequirementsAndAddDistrictName("Ecclesiastical_Arcology");
                break;
            case "Ringworld":
                CheckRequirementsAndAddDistrictName("City_Segment");
                CheckRequirementsAndAddDistrictName("Hive_Segment");
                CheckRequirementsAndAddDistrictName("Nexus_Segment");
                CheckRequirementsAndAddDistrictName("Commercial_Segment");
                CheckRequirementsAndAddDistrictName("Generator_Segment");
                CheckRequirementsAndAddDistrictName("Research_Segment");
                CheckRequirementsAndAddDistrictName("Agricultural_Segment");
                CheckRequirementsAndAddDistrictName("Industrial_Segment");
                break;
            case "Shattered_Ringworld":
                CheckRequirementsAndAddDistrictName("City_Segment");
                CheckRequirementsAndAddDistrictName("Hive_Segment");
                CheckRequirementsAndAddDistrictName("Nexus_Segment");
                CheckRequirementsAndAddDistrictName("Commercial_Segment");
                CheckRequirementsAndAddDistrictName("Generator_Segment");
                CheckRequirementsAndAddDistrictName("Research_Segment");
                CheckRequirementsAndAddDistrictName("Agricultural_Segment");
                CheckRequirementsAndAddDistrictName("Industrial_Segment");
                break;
            case "Habitat":
                CheckRequirementsAndAddDistrictName("Habitation_District");
                CheckRequirementsAndAddDistrictName("Trade_Habitat_District");
                CheckRequirementsAndAddDistrictName("Leisure_Habitat_District");
                CheckRequirementsAndAddDistrictName("Astro-Mining_Bay");
                CheckRequirementsAndAddDistrictName("Reactor_Habitat_District");
                CheckRequirementsAndAddDistrictName("Research_Habitat_District");
                CheckRequirementsAndAddDistrictName("Industrial_Habitat_District");
                break;
            case "Machine":
                CheckRequirementsAndAddDistrictName("Nexus_District");
                CheckRequirementsAndAddDistrictName("Industrial_District");
                CheckRequirementsAndAddDistrictName("Generator_District");
                CheckRequirementsAndAddDistrictName("Mining_District");
                CheckRequirementsAndAddDistrictName("Agriculture_District");
                break;
            case "Hive":
                CheckRequirementsAndAddDistrictName("Hive_District");
                CheckRequirementsAndAddDistrictName("Industrial_District");
                CheckRequirementsAndAddDistrictName("Generator_District");
                CheckRequirementsAndAddDistrictName("Mining_District");
                CheckRequirementsAndAddDistrictName("Agriculture_District");
                break;
            default:
                CheckRequirementsAndAddDistrictName("City_District");
                CheckRequirementsAndAddDistrictName("Industrial_District");
                CheckRequirementsAndAddDistrictName("Generator_District");
                CheckRequirementsAndAddDistrictName("Mining_District");
                CheckRequirementsAndAddDistrictName("Agriculture_District");
                break;
        }

        districtNum_ = districtNames_.Count;

        // Initialize district slots
        InstantiateNewDistrictSlots();

        // Initialize district button listeners by default
        SetDistrictButtonListeners();

        // Initialize planet size by 20
        UpdateDistrictsSize(20);
    }

    void CheckRequirementsAndAddDistrictName(string districtName)
    {
        // Debug.Log("CheckRequirementsAndAddDistrictName");
        Debug.Log(districtName);
        bool check = true;

        // Check if actually meets all the requirements
        foreach (string requirement in District._districts_[districtName].requirements_)
        {
            Debug.Log(requirement);

            string negativeRequirement = "";

            if (requirement[1] == '+')
                // Calculate negative requirement to check if there are two incompatible requirements
                negativeRequirement = requirement.Substring(0, 1) + '-' + requirement.Substring(2, requirement.Length - 2);
            else
                // Calculate negative requirement to check if there are two incompatible requirements
                negativeRequirement = requirement.Substring(0, 1) + '+' + requirement.Substring(2, requirement.Length - 2);

            if (planetData_.governmentRequirement_.Contains(negativeRequirement))
                check = false;
            if (planetData_.planetTypeRequirement_.Contains(negativeRequirement))
                check = false;

            Debug.Log(check);
        }

        if (check)
            districtNames_.Add(districtName);
    }
}
