using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RequirementButtonManager : MonoBehaviour
{
    public GameObject ResourcePrefab;
    private PlanetData planetData_;
    private GameObject requirementPanel_;
    private Transform requirementPanelContent_;
    private HashSet<string> requirementSet;

    private void Awake()
    {
        requirementPanel_ = transform.parent.Find("RequirementPanel").gameObject;
        requirementPanelContent_ = requirementPanel_.transform.Find("Scroll View/Viewport/Content");
        requirementPanel_.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        requirementSet = new HashSet<string>();
        planetData_ = transform.parent.parent.GetComponent<PlanetData>();
        gameObject.GetComponent<Button>().onClick.AddListener(ShowRequirementPanel);
    }

    void ShowRequirementPanel()
    {
        // Debug.Log("ShowRequirementPanel");
        requirementPanel_.SetActive(!requirementPanel_.activeSelf);

        if (requirementPanel_.activeSelf)
            UpdateRequirements();
    }

    public void UpdateRequirements()
    {
        // Debug.Log("UpdateRequirements()");

        requirementSet = new HashSet<string>();
        bool isIncompatible = false;

        // Remove all ResourcePrefab instantiated before
        if (requirementPanelContent_.childCount > 0)
            foreach (Transform child in requirementPanelContent_)
                GameObject.Destroy(child.gameObject);

        // Actual government selected
        if (planetData_.government_ != "")
        {
            requirementSet.Add("{+\"" + planetData_.government_ + "\"}");

            foreach( string requirement in planetData_.governmentRequirement_)
                requirementSet.Add(requirement);
        }

        foreach (Building building in planetData_.buildings_)
        {
            if (building != null)
                foreach (string requirement in building.requirements_)
                    requirementSet.Add(requirement);
        }

        foreach (string requirement in requirementSet)
        {
            GameObject NewResourcePrefab = Instantiate(ResourcePrefab, requirementPanelContent_.position, requirementPanelContent_.rotation, requirementPanelContent_);

            if (requirement[1] == '+')
            {
                // Calculate negative requirement to check if there are two incompatible requirements
                string negativeRequirement = requirement.Substring(0, 1) + '-' + requirement.Substring(2, requirement.Length - 2);
                if (requirementSet.Contains(negativeRequirement))
                    isIncompatible = true;

                NewResourcePrefab.transform.Find("Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/tick");
            }
            else
            {
                // Calculate negative requirement to check if there are two incompatible requirements
                string negativeRequirement = requirement.Substring(0, 1) + '+' + requirement.Substring(2, requirement.Length - 2);
                if (requirementSet.Contains(negativeRequirement))
                    isIncompatible = true;

                NewResourcePrefab.transform.Find("Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/cross");
            }

            NewResourcePrefab.transform.Find("Name").GetComponent<Text>().text = requirement.Substring(3, requirement.Length - 5).Replace("_", " ");
        }

        transform.Find("Incompatible").gameObject.SetActive(isIncompatible);
    }
}