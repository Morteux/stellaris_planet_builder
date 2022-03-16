using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GovernmentButtonManager : MonoBehaviour
{
    public GameObject selectableItemButtonPrefab_;
    private PlanetData planetData_;
    private Transform governmentPanel_;
    private Transform governmentContent_;

    // Start is called before the first frame update
    void Start()
    {
        governmentPanel_ = transform.parent.parent.parent.parent.Find("ButtonPanels/GovernmentPanel");
        governmentContent_ = governmentPanel_.Find("Scroll View/Viewport/Content");
        planetData_ = transform.parent.parent.parent.parent.parent.GetComponent<PlanetData>();

        transform.GetComponent<Button>().onClick.AddListener(ShowGovernmentPanel);
        governmentPanel_.Find("Close").GetComponent<Button>().onClick.AddListener(ShowGovernmentPanel);

        // Instantiate governments
        foreach (KeyValuePair<string, Government> government in Government._governments_)
        {
            GameObject NewSelectableItemButtonPrefab = Instantiate(selectableItemButtonPrefab_, governmentContent_.position, governmentContent_.rotation, governmentContent_);
            NewSelectableItemButtonPrefab.GetComponent<Button>().onClick.AddListener(delegate { SelectGovernment(government.Value, NewSelectableItemButtonPrefab.transform.Find("SelectedFrame")); });
            NewSelectableItemButtonPrefab.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Government/" + government.Key);
            NewSelectableItemButtonPrefab.transform.Find("Text").GetComponent<Text>().text = government.Key.Replace('_', ' ');
            NewSelectableItemButtonPrefab.transform.Find("SelectedFrame").gameObject.SetActive(false);
        }

        governmentPanel_.gameObject.SetActive(false);
    }

    void ShowGovernmentPanel()
    {
        // Debug.Log("ShowGovernmentPanel");
        governmentPanel_.gameObject.SetActive(!governmentPanel_.gameObject.activeSelf);
    }

    void SelectGovernment(Government government, Transform governmentFrame)
    {
        // Debug.Log("SelectGovernment");

        if (governmentFrame.gameObject.activeSelf)
        {
            // Hide current selected frame
            governmentFrame.gameObject.SetActive(false);

            // Set all government attributes by default
            planetData_.government_ = "";
            planetData_.governmentRequirement_ = new HashSet<string>();
            planetData_.transform.GetComponentInChildren<SlotsManager>().ChangeCapitalBuilding(Building._buildings_["Reassembled_Ship_Shelter"]);
        }
        else
        {
            // Hide all frames
            foreach (Transform child in governmentContent_)
                if (child.parent == governmentContent_)
                    child.Find("SelectedFrame").gameObject.SetActive(false);

            // Show current selected frame
            governmentFrame.gameObject.SetActive(true);

            planetData_.government_ = government.name_;

            planetData_.governmentRequirement_ = new HashSet<string>();
            foreach (string requirement in government.requirements_)
                planetData_.governmentRequirement_.Add(requirement);

            planetData_.transform.GetComponentInChildren<SlotsManager>().ChangeCapitalBuilding(Building._buildings_[government.capitalBuilding_]);
        }

        planetData_.UpdatePlanetData();
        transform.parent.GetComponentInChildren<RequirementButtonManager>().UpdateRequirements();
        planetData_.transform.GetComponentInChildren<DistrictManager>().UpdateDistrictType();
        transform.parent.GetComponentInChildren<JobsButtonManager>().UpdateJobs();
        transform.parent.GetComponentInChildren<EffectButtonManager>().UpdateEffects();
    }

    public void LoadGovernment(string government)
    {
        Transform governmentButton = null;

        // Store planet type button gameobject
        foreach (Transform child in governmentContent_)
            if (child.parent == governmentContent_ && child.Find("Text").GetComponent<Text>().text == government.Replace("_", " "))
                governmentButton = child;

        // Hide all frames
        foreach (Transform child in governmentContent_)
            if (child.parent == governmentContent_)
                child.Find("SelectedFrame").gameObject.SetActive(false);

        // Show current selected frame
        governmentButton.Find("SelectedFrame").gameObject.SetActive(true);

        // Store loaded government name
        planetData_.government_ = government;

        // Store loaded government requirements
        planetData_.governmentRequirement_ = new HashSet<string>();
        foreach (string requirement in Government._governments_[government].requirements_)
            planetData_.governmentRequirement_.Add(requirement);
    }
}
