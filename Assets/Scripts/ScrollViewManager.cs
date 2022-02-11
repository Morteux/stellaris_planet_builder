using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewManager : MonoBehaviour
{
    public GameObject BuildingButtonPrefab;
    public GameObject ResourcePrefab;
    private Dictionary<string, GameObject> BuildingButtons;
    private Transform Content;

    // Start is called before the first frame update
    void Start()
    {
        Content = transform.Find("Scroll View/Viewport/Content");

        InitializeScrollView();
    }

    void InitializeScrollView()
    {
        foreach (KeyValuePair<string, Building> pair in Building._buildings_)
        {
            if (pair.Value.buildable_ == "Yes")
            {
                GameObject NewBuildingButtonPrefab = Instantiate(BuildingButtonPrefab, Content.position, Content.rotation, Content);
                NewBuildingButtonPrefab.transform.Find("Name").GetComponent<Text>().text = pair.Key.Replace('_', ' ');
                NewBuildingButtonPrefab.transform.Find("Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Buildings/" + pair.Key);
                NewBuildingButtonPrefab.transform.GetComponent<BuildingButtonManager>().building = pair.Value;

                if (pair.Value.unique_ == "Yes")
                {
                    NewBuildingButtonPrefab.transform.Find("NameBackground").GetComponent<Image>().color = new Color32(10, 13, 173, 240);
                    NewBuildingButtonPrefab.transform.Find("DescriptionBackground").GetComponent<Image>().color = new Color32(10, 13, 173, 240);
                    NewBuildingButtonPrefab.transform.Find("BuildingBackground").GetComponent<Image>().color = new Color32(22, 161, 231, 255);
                }

                // Debug.Log(pair.Value.requirements_);

                Transform ButtonContent = NewBuildingButtonPrefab.transform.Find("Scroll View/Viewport/Content");
                // foreach (string requirement in pair.Value.requirements_)
                // {
                //     GameObject NewResourcePrefab = Instantiate(ResourcePrefab, ButtonContent.position, ButtonContent.rotation, ButtonContent);

                //     if(requirement[1] == '+')
                //         NewResourcePrefab.transform.Find("Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/tick");
                //     else
                //         NewResourcePrefab.transform.Find("Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/cross");
                    
                //     NewResourcePrefab.transform.Find("Name").GetComponent<Text>().text = requirement;
                // }

                foreach (KeyValuePair<Job, int> job in pair.Value.jobs_)
                {
                    GameObject NewResourcePrefab = Instantiate(ResourcePrefab, ButtonContent.position, ButtonContent.rotation, ButtonContent);
                    NewResourcePrefab.transform.Find("Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Jobs/" + job.Key.name_);
                    NewResourcePrefab.transform.Find("Name").GetComponent<Text>().text = job.Value + " " + job.Key.name_.Replace('_', ' ');
                }

                foreach (KeyValuePair<Data.Effects, int> effect in pair.Value.effects_)
                {
                    GameObject NewResourcePrefab = Instantiate(ResourcePrefab, ButtonContent.position, ButtonContent.rotation, ButtonContent);
                    NewResourcePrefab.transform.Find("Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Resources/" + Data.Effects_to_string(effect.Key));
                    NewResourcePrefab.transform.Find("Name").GetComponent<Text>().text = effect.Value + "% " + Data.Effects_to_string(effect.Key).Replace('_', ' ');
                }
                    
                foreach (KeyValuePair<Data.Resource, int> pairUpkeep in pair.Value.upkeep_)
                {
                    if( pairUpkeep.Value < 0)
                    {
                        NewBuildingButtonPrefab.transform.Find("Description/UpkeepCounter").GetComponent<Text>().text = (-1 * pairUpkeep.Value).ToString();
                        NewBuildingButtonPrefab.transform.Find("Description/UpkeepIcon").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Resources/" + Data.Resource_to_string(pairUpkeep.Key));
                        NewBuildingButtonPrefab.transform.Find("Description/UpkeepIcon").GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                    }
                }
            }
        }
    }

    public void ActivateBuildingButton(string buildingName)
    {
        // BuildingButtons[buildingName].GetComponent<BuildingButtonManager>().ActivateBuildingButton();
        foreach (Transform button in transform.Find("Scroll View/Viewport/Content"))
        {
            if (button.name == "BuildingButton(Clone)" && button.Find("Name").GetComponent<Text>().text == buildingName.Replace('_', ' '))
            {
                // Debug.Log ("Child found. Mame: " + button.name);
                button.GetComponent<BuildingButtonManager>().ActivateBuildingButton();
            }
        }
    }
}
