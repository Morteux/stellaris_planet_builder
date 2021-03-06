using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RequirementButtonManager : MonoBehaviour
{
    public GameObject resourcePrefab_;
    private PlanetData planetData_;
    private GameObject requirementPanel_;
    private GameObject buildPanel_;
    private Transform requirementPanelContent_;
    private HashSet<string> requirementSet_;

    private void Awake()
    {
        buildPanel_ = transform.parent.parent.parent.parent.Find("BuildPanel").gameObject;
        requirementPanel_ = transform.parent.parent.parent.parent.Find("ButtonPanels/RequirementPanel").gameObject;
        requirementPanelContent_ = requirementPanel_.transform.Find("Scroll View/Viewport/Content");
        requirementPanel_.SetActive(false);
    }

    void Start()
    {
        requirementSet_ = new HashSet<string>();
        planetData_ = transform.parent.parent.parent.parent.parent.GetComponent<PlanetData>();
        gameObject.GetComponent<Button>().onClick.AddListener(ShowRequirementPanel);
        requirementPanel_.transform.Find("Close").GetComponent<Button>().onClick.AddListener(ShowRequirementPanel);
    }

    void ShowRequirementPanel()
    {
        // Debug.Log("ShowRequirementPanel");
        requirementPanel_.SetActive(!requirementPanel_.activeSelf);

        if (requirementPanel_.activeSelf)
            UpdateRequirements();
    }

    // Instantiate a child ResourcePrefab in requirement panel for each requirement
    public void UpdateRequirements()
    {
        // Debug.Log("UpdateRequirements()");

        requirementSet_ = new HashSet<string>();
        bool isIncompatible = false;

        // Remove all ResourcePrefab instantiated before
        if (requirementPanelContent_.childCount > 0)
            foreach (Transform child in requirementPanelContent_)
                GameObject.Destroy(child.gameObject);

        // Requirements for actual government selected
        if (planetData_.government_ != "")
        {
            // Format actual government requirement
            requirementSet_.Add("{+\"" + planetData_.government_ + "\"}");

            foreach (string requirement in planetData_.governmentRequirement_)
                requirementSet_.Add(requirement);
        }

        // Requirements for actual planet type selected
        if (planetData_.planetType_ != "")
        {
            // Format actual government requirement
            requirementSet_.Add("{+\"" + planetData_.planetType_ + "\"}");

            foreach (string requirement in planetData_.planetTypeRequirement_)
                requirementSet_.Add(requirement);
        }

        // Requirements for each building builded
        foreach (Building building in planetData_.buildings_)
        {
            if (building != null)
                foreach (string requirement in building.requirements_)
                    requirementSet_.Add(requirement);
        }

        // Update requirement panel
        foreach (string requirement in requirementSet_)
        {
            GameObject NewResourcePrefab = Instantiate(resourcePrefab_, requirementPanelContent_.position, requirementPanelContent_.rotation, requirementPanelContent_);

            if (requirement[1] == '+')
            {
                // Calculate negative requirement to check if there are two incompatible requirements
                string negativeRequirement = requirement.Substring(0, 1) + '-' + requirement.Substring(2, requirement.Length - 2);
                if (requirementSet_.Contains(negativeRequirement))
                    isIncompatible = true;

                NewResourcePrefab.transform.Find("Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/tick");
            }
            else
            {
                // Calculate negative requirement to check if there are two incompatible requirements
                string negativeRequirement = requirement.Substring(0, 1) + '+' + requirement.Substring(2, requirement.Length - 2);
                if (requirementSet_.Contains(negativeRequirement))
                    isIncompatible = true;

                NewResourcePrefab.transform.Find("Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/cross");
            }

            NewResourcePrefab.transform.Find("Name").GetComponent<Text>().text = requirement.Substring(3, requirement.Length - 5).Replace("_", " ");
        }

        planetData_.requirements_ = requirementSet_;
        buildPanel_.GetComponent<BuildManager>().UpdateBuildingButtonsByRequirements();

        transform.Find("Incompatible").gameObject.SetActive(isIncompatible);
    }
}