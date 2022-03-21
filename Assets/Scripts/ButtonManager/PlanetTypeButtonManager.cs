using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetTypeButtonManager : MonoBehaviour
{
    public GameObject selectableItemButtonPrefab_;
    private GameObject planetTypePanel_;
    private Transform planetTypeContent_;
    private PlanetData planetData_;

    void Start()
    {
        planetTypePanel_ = transform.parent.parent.parent.parent.Find("ButtonPanels/PlanetTypePanel").gameObject;
        planetTypePanel_.transform.Find("Close").GetComponent<Button>().onClick.AddListener(ShowPlanetTypePanel);
        GetComponent<Button>().onClick.AddListener(ShowPlanetTypePanel);
        planetTypeContent_ = planetTypePanel_.transform.Find("Scroll View/Viewport/Content");
        planetData_ = transform.parent.parent.parent.parent.parent.GetComponent<PlanetData>();

        // Instantiate planets
        foreach (KeyValuePair<string, Planet> planet in Planet._planets_)
        {
            GameObject NewSelectableItemButtonPrefab = Instantiate(selectableItemButtonPrefab_, planetTypeContent_.position, planetTypeContent_.rotation, planetTypeContent_);
            NewSelectableItemButtonPrefab.GetComponent<Button>().onClick.AddListener(ChangePlanetType);
            NewSelectableItemButtonPrefab.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Planets/" + planet.Key);
            NewSelectableItemButtonPrefab.transform.Find("Text").GetComponent<Text>().text = planet.Key.Replace('_', ' ');
            NewSelectableItemButtonPrefab.transform.Find("SelectedFrame").gameObject.SetActive(false);
        }

        planetTypePanel_.SetActive(false);
    }

    public void ShowPlanetTypePanel()
    {
        // Debug.Log("ShowPlanetTypePanel");

        planetTypePanel_.SetActive(!planetTypePanel_.activeSelf);
    }

    public void ChangePlanetType()
    {
        // Debug.Log("PlanetTypeButtonManager::ChangePlanetType");

        GameObject button = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

        // Set active false all planet types SelectedFrame
        if (planetTypeContent_.childCount > 0)
            foreach (Transform child in planetTypeContent_)
                child.Find("SelectedFrame").gameObject.SetActive(false);

        button.transform.Find("SelectedFrame").gameObject.SetActive(true);

        // Save new planet type
        string planetType = button.GetComponentInChildren<Text>().text.Replace(' ', '_');

        // Cast from string[] requirements to HashSet<string> requirements
        planetData_.planetTypeRequirement_ = new HashSet<string>();
        foreach (string requirement in Planet._planets_[planetType].requirements_)
            planetData_.planetTypeRequirement_.Add(requirement);

        planetData_.planetTypeEffects_ = Planet._planets_[planetType].effects_;
        planetData_.planetType_ = planetType;
        planetData_.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Planets/" + planetType);

        // Update planet data
        planetData_.UpdatePlanetData();
        planetData_.transform.GetComponentInChildren<RequirementButtonManager>().UpdateRequirements();
        planetData_.transform.GetComponentInChildren<DistrictManager>().UpdateDistrictType();
        planetData_.transform.GetComponentInChildren<JobsButtonManager>().UpdateJobs();
        planetData_.transform.GetComponentInChildren<EffectButtonManager>().UpdateEffects();
    }

    public void LoadPlanetType(string planetType)
    {
        Transform planetTypeButton = null;

        // Store planet type button gameobject
        foreach (Transform child in planetTypeContent_)
            if (child.parent == planetTypeContent_ && child.Find("Text").GetComponent<Text>().text == planetType.Replace("_", " "))
                planetTypeButton = child;

        GameObject button = planetTypeButton.gameObject;

        // Set active false all planet types SelectedFrame
        if (planetTypeContent_.childCount > 0)
            foreach (Transform child in planetTypeContent_)
                child.Find("SelectedFrame").gameObject.SetActive(false);

        button.transform.Find("SelectedFrame").gameObject.SetActive(true);

        // Cast from string[] requirements to HashSet<string> requirements
        planetData_.planetTypeRequirement_ = new HashSet<string>();
        foreach (string requirement in Planet._planets_[planetType].requirements_)
            planetData_.planetTypeRequirement_.Add(requirement);

        planetData_.planetTypeEffects_ = Planet._planets_[planetType].effects_;
        planetData_.planetType_ = planetType;
        planetData_.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Planets/" + planetType);
    }
}
