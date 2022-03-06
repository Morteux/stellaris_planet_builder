using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{
    public GameObject BuildingButtonPrefab;
    public GameObject ResourcePrefab;
    public GameObject IconResourcePrefab;
    private PlanetData planetData;
    private Dictionary<string, GameObject> BuildingButtons;
    private Transform Content;

    // Start is called before the first frame update
    void Start()
    {
        planetData = transform.parent.parent.GetComponent<PlanetData>();
        Content = transform.Find("Scroll View/Viewport/Content");

        UpdateBuildingButtons();
    }

    public void UpdateBuildingButtons()
    {
        // Remove all BuildingButtonPrefab instantiated before
        if (Content.childCount > 0)
            foreach (Transform child in Content)
                GameObject.Destroy(child.gameObject);

        foreach (KeyValuePair<string, Building> pair in Building._buildings_)
        {
            bool isCompatible = true;
            string negativeRequirement = "";

            foreach (string requirement in pair.Value.requirements_)
            {
                if (requirement[1] == '+')
                    // Calculate negative requirement to check if there are two incompatible requirements
                    negativeRequirement = requirement.Substring(0, 1) + '-' + requirement.Substring(2, requirement.Length - 2);
                else
                    // Calculate negative requirement to check if there are two incompatible requirements
                    negativeRequirement = requirement.Substring(0, 1) + '+' + requirement.Substring(2, requirement.Length - 2);

                // If negativeRequirement belongs to planetData.requirements_, this building is incompatible with actual requirements
                isCompatible = !planetData.requirements_.Contains(negativeRequirement);
            }

            if (isCompatible && pair.Value.buildable_ == "Yes")
            {
                // Instantiate new building button
                GameObject NewBuildingButtonPrefab = Instantiate(BuildingButtonPrefab, Content.position, Content.rotation, Content);
                NewBuildingButtonPrefab.transform.Find("Name").GetComponent<Text>().text = pair.Key.Replace('_', ' ');
                NewBuildingButtonPrefab.transform.Find("Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Buildings/" + pair.Key);
                NewBuildingButtonPrefab.transform.GetComponent<BuildingButtonManager>().building = pair.Value;

                // Change button color for unique buildings
                if (pair.Value.unique_ == "Yes")
                {
                    NewBuildingButtonPrefab.transform.Find("NameBackground").GetComponent<Image>().color = new Color32(10, 13, 173, 240);
                    NewBuildingButtonPrefab.transform.Find("DescriptionBackground").GetComponent<Image>().color = new Color32(10, 13, 173, 240);
                    NewBuildingButtonPrefab.transform.Find("BuildingBackground").GetComponent<Image>().color = new Color32(22, 161, 231, 255);
                }

                // Get tranform where instantiate info for this building
                Transform ButtonContent = NewBuildingButtonPrefab.transform.Find("Scroll View/Viewport/Content");

                foreach (KeyValuePair<Job, int> job in pair.Value.jobs_)
                {
                    GameObject NewResourcePrefab = Instantiate(ResourcePrefab, ButtonContent.position, ButtonContent.rotation, ButtonContent);
                    NewResourcePrefab.transform.Find("Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Jobs/" + job.Key.name_);
                    NewResourcePrefab.transform.Find("Name").GetComponent<Text>().text = job.Value + " " + job.Key.name_.Replace('_', ' ');
                }

                foreach (KeyValuePair<Data.Effects, int> effect in pair.Value.effects_)
                {
                    GameObject NewResourcePrefab = Instantiate(ResourcePrefab, ButtonContent.position, ButtonContent.rotation, ButtonContent);
                    NewResourcePrefab.transform.Find("Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Effects/" + effect.Key.ToString());
                    NewResourcePrefab.transform.Find("Name").GetComponent<Text>().text = effect.Value + "% " + effect.Key.ToString().Replace('_', ' ');
                }

                // Transform UpkeepContent = NewBuildingButtonPrefab.transform.Find("Description/Upkeep/Scroll View/Viewport/Content");
                Transform UpkeepContent = NewBuildingButtonPrefab.transform.Find("Description/Upkeep/Content");
                foreach (KeyValuePair<Data.Resource, int> pairUpkeep in pair.Value.upkeep_)
                    if (pairUpkeep.Value < 0)
                    {
                        GameObject NewIconResourcePrefab = Instantiate(IconResourcePrefab, UpkeepContent.position, UpkeepContent.rotation, UpkeepContent);
                        NewIconResourcePrefab.transform.Find("UpkeepCounter").GetComponent<Text>().text = (-1 * pairUpkeep.Value).ToString();
                        NewIconResourcePrefab.transform.Find("UpkeepIcon").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Resources/" + pairUpkeep.Key.ToString());
                        NewIconResourcePrefab.transform.Find("UpkeepIcon").GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                    }
            }
        }
    }

    public void UpdateBuildingButtonsByRequirements()
    {
        foreach (Transform buildButton in Content)
        {
            if (buildButton.parent == Content)
            {
                bool isCompatible = true;
                string negativeRequirement = "";
                Building building = Building._buildings_[buildButton.Find("Name").GetComponent<Text>().text.Replace(" ", "_")];

                // Iterate through building requirements
                foreach (string requirement in building.requirements_)
                {
                    if (requirement[1] == '+')
                        // Calculate negative requirement to check if there are two incompatible requirements
                        negativeRequirement = requirement.Substring(0, 1) + '-' + requirement.Substring(2, requirement.Length - 2);
                    else
                        // Calculate negative requirement to check if there are two incompatible requirements
                        negativeRequirement = requirement.Substring(0, 1) + '+' + requirement.Substring(2, requirement.Length - 2);

                    // If negativeRequirement belongs to planetData.requirements_, this building is incompatible with actual requirements
                    isCompatible = !planetData.requirements_.Contains(negativeRequirement);
                }

                if (!isCompatible || building.buildable_ != "Yes")
                    buildButton.gameObject.SetActive(false);
                else
                    buildButton.gameObject.SetActive(true);
            }
        }
    }

    // Activate an unique building button
    public void ActivateBuildingButton(string buildingName)
    {
        // Get the most downgrade building
        while (Building._buildings_[buildingName].downgrade_ != "-")
            buildingName = Building._buildings_[buildingName].downgrade_;

        // Look for the most downgrade building button and activate it
        foreach (Transform button in transform.Find("Scroll View/Viewport/Content"))
            if (button.name == "BuildingButton(Clone)" && button.Find("Name").GetComponent<Text>().text == buildingName.Replace('_', ' '))
                button.GetComponent<BuildingButtonManager>().ActivateBuildingButton();
    }
}
