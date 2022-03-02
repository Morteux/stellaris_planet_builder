using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class CustomizeButtonManager : MonoBehaviour
{
    public GameObject customizableItemPrefab_;
    public GameObject percentageCustomizableItemPrefab_;
    private Transform customizationPanel_;
    private GameObject valueButton_;
    private GameObject percentageButton_;
    private GameObject jobsButton_;
    private GameObject valueScrollView_;
    private GameObject percentageScrollView_;
    private GameObject jobsScrollView_;
    private PlanetData planetData_;

    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log("CustomizeButtonManager::Start");
        customizationPanel_ = transform.parent.parent.parent.parent.Find("ButtonPanels/CustomizationPanel");
        customizationPanel_.Find("Close").GetComponent<Button>().onClick.AddListener(ShowCustomizationPanel);

        GetComponent<Button>().onClick.AddListener(ShowCustomizationPanel);

        valueButton_ = customizationPanel_.transform.Find("ValueButton").gameObject;
        percentageButton_ = customizationPanel_.transform.Find("PercentageButton").gameObject;
        jobsButton_ = customizationPanel_.transform.Find("JobsButton").gameObject;

        valueScrollView_ = customizationPanel_.transform.Find("Value Scroll View").gameObject;
        percentageScrollView_ = customizationPanel_.transform.Find("Percentage Scroll View").gameObject;
        jobsScrollView_ = customizationPanel_.transform.Find("Jobs Scroll View").gameObject;

        valueButton_.GetComponent<Button>().onClick.AddListener(delegate { ShowScrollView(valueScrollView_); });
        percentageButton_.GetComponent<Button>().onClick.AddListener(delegate { ShowScrollView(percentageScrollView_); });
        jobsButton_.GetComponent<Button>().onClick.AddListener(delegate { ShowScrollView(jobsScrollView_); });

        planetData_ = transform.parent.parent.parent.parent.parent.GetComponent<PlanetData>();

        // Initialize resource values
        Transform valueContent = valueScrollView_.transform.Find("Viewport/Content");
        Dictionary<Data.Resource, int> customizeResources = new Dictionary<Data.Resource, int>();
        foreach (Data.Resource resource in Enum.GetValues(typeof(Data.Resource)))
        {
            // Debug.Log(resource);

            if (resource != Data.Resource.NULL && resource != Data.Resource.INVALID)
            {
                // Add resource to customizeResources
                customizeResources.Add(resource, 0);

                // Instantiate item
                GameObject NewCustomizableItemPrefab = Instantiate(customizableItemPrefab_, valueContent.position, valueContent.rotation, valueContent);
                NewCustomizableItemPrefab.transform.Find("Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Resources/" + resource.ToString());
                NewCustomizableItemPrefab.transform.Find("Name").GetComponent<Text>().text = resource.ToString().Replace('_', ' ');
                NewCustomizableItemPrefab.transform.Find("InputField").GetComponent<InputField>().onValueChanged.AddListener(delegate { ChangeValueResourcesInputField(); });
                NewCustomizableItemPrefab.transform.Find("Plus").GetComponent<Button>().onClick.AddListener(delegate { ChangeValueResourcesButton(1); });
                NewCustomizableItemPrefab.transform.Find("Minus").GetComponent<Button>().onClick.AddListener(delegate { ChangeValueResourcesButton(-1); });
            }
        }
        planetData_.customizeResources_ = customizeResources;

        // Initialize resource percentage
        Transform percentageContent = percentageScrollView_.transform.Find("Viewport/Content");
        Dictionary<Data.Resource, float> percentageResources = new Dictionary<Data.Resource, float>();
        foreach (Data.Resource resource in Enum.GetValues(typeof(Data.Resource)))
        {
            // Debug.Log(resource);

            if (resource != Data.Resource.NULL && resource != Data.Resource.INVALID)
            {
                // Add resource to percentageResources
                percentageResources.Add(resource, 0);

                // Instantiate item
                GameObject NewPercentageCustomizableItemPrefab = Instantiate(percentageCustomizableItemPrefab_, percentageContent.position, percentageContent.rotation, percentageContent);
                NewPercentageCustomizableItemPrefab.transform.Find("Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Resources/" + resource.ToString());
                NewPercentageCustomizableItemPrefab.transform.Find("Name").GetComponent<Text>().text = resource.ToString().Replace('_', ' ');
                NewPercentageCustomizableItemPrefab.transform.Find("Slider").GetComponent<Slider>().onValueChanged.AddListener(ChangePercentageResources);
            }
        }
        planetData_.percentageResources_ = percentageResources;

        // Initialize jobs values
        Transform jobsContent = jobsScrollView_.transform.Find("Viewport/Content");
        Dictionary<Job, int> customizeJobs = new Dictionary<Job, int>();
        foreach (KeyValuePair<string, Job> pair in Job._jobs_)
        {
            // Debug.Log(pair);

            // Add resource to percentageResources
            customizeJobs.Add(pair.Value, 0);

            // Instantiate item
            GameObject NewCustomizableItemPrefab = Instantiate(customizableItemPrefab_, jobsContent.position, jobsContent.rotation, jobsContent);
            NewCustomizableItemPrefab.transform.Find("Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Jobs/" + pair.Key);
            NewCustomizableItemPrefab.transform.Find("Name").GetComponent<Text>().text = pair.Key.Replace('_', ' ');
            NewCustomizableItemPrefab.transform.Find("InputField").GetComponent<InputField>().onValueChanged.AddListener(delegate { ChangeValueJobsInputField(); });
            NewCustomizableItemPrefab.transform.Find("Plus").GetComponent<Button>().onClick.AddListener(delegate { ChangeValueJobsButton(1); });
            NewCustomizableItemPrefab.transform.Find("Minus").GetComponent<Button>().onClick.AddListener(delegate { ChangeValueJobsButton(-1); });
        }

        // Remain active valueScrollView by default
        percentageScrollView_.SetActive(false);
        jobsScrollView_.SetActive(false);

        customizationPanel_.gameObject.SetActive(false);
    }

    void ShowCustomizationPanel()
    {
        // Debug.Log("CustomizeButtonManager::ShowCustomizationPanel");
        customizationPanel_.gameObject.SetActive(!customizationPanel_.gameObject.activeSelf);
    }

    void ShowScrollView(GameObject scrollView)
    {
        // Debug.Log("CustomizeButtonManager::ShowScrollView");
        valueScrollView_.SetActive(false);
        percentageScrollView_.SetActive(false);
        jobsScrollView_.SetActive(false);

        scrollView.SetActive(true);
    }

    // Function for plus and minus button. Increase or decrease actualValue by value
    void ChangeValueResourcesButton(int value)
    {
        // Debug.Log("CustomizeButtonManager::ChangeValueResourcesButton");

        GameObject item = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

        int.TryParse(item.transform.parent.Find("InputField").GetComponent<InputField>().text, out int actualValue);

        int totalValue = actualValue + value;

        planetData_.customizeResources_[Data.String_to_Resource(item.transform.parent.Find("Name").GetComponent<Text>().text.Replace(' ', '_'))] = totalValue;

        item.transform.parent.Find("InputField").GetComponent<InputField>().text = totalValue.ToString();

        planetData_.UpdatePlanetData();
    }

    // Function for plus and minus button. Increase or decrease actualValue by value
    void ChangeValueResourcesInputField()
    {
        // Debug.Log("CustomizeButtonManager::ChangeValueResourcesInputField");

        GameObject item = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

        int.TryParse(item.transform.parent.Find("InputField").GetComponent<InputField>().text, out int actualValue);

        planetData_.customizeResources_[Data.String_to_Resource(item.transform.parent.Find("Name").GetComponent<Text>().text.Replace(' ', '_'))] = actualValue;

        item.transform.parent.Find("InputField").GetComponent<InputField>().text = actualValue.ToString();

        planetData_.UpdatePlanetData();
    }

    // Function for plus and minus button. Increase or decrease actualValue by value
    void ChangeValueJobsButton(int value)
    {
        // Debug.Log("CustomizeButtonManager::ChangeValueJobsButton");

        GameObject item = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

        int.TryParse(item.transform.parent.Find("InputField").GetComponent<InputField>().text, out int actualValue);

        int totalValue = actualValue + value;

        planetData_.customizeJobs_[Job._jobs_[item.transform.parent.Find("Name").GetComponent<Text>().text.Replace(' ', '_')]] = totalValue;

        item.transform.parent.Find("InputField").GetComponent<InputField>().text = totalValue.ToString();

        planetData_.UpdatePlanetData();
        planetData_.transform.GetComponentInChildren<JobsButtonManager>().UpdateJobs();
    }

    // Function for plus and minus button. Increase or decrease actualValue by value
    void ChangeValueJobsInputField()
    {
        // Debug.Log("CustomizeButtonManager::ChangeValueJobsInputField");

        GameObject item = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

        int.TryParse(item.transform.parent.Find("InputField").GetComponent<InputField>().text, out int actualValue);

        planetData_.customizeJobs_[Job._jobs_[item.transform.parent.Find("Name").GetComponent<Text>().text.Replace(' ', '_')]] = actualValue;

        item.transform.parent.Find("InputField").GetComponent<InputField>().text = actualValue.ToString();

        planetData_.UpdatePlanetData();
        planetData_.transform.GetComponentInChildren<JobsButtonManager>().UpdateJobs();
    }

    // Function for percentage slider
    void ChangePercentageResources(float value)
    {
        // Debug.Log("CustomizeButtonManager::ChangePercentage");

        GameObject item = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

        planetData_.percentageResources_[Data.String_to_Resource(item.transform.parent.Find("Name").GetComponent<Text>().text.Replace(' ', '_'))] = value;

        item.transform.parent.Find("Value").GetComponent<Text>().text = (int)(value * 100) + "%";

        planetData_.UpdatePlanetData();
    }
}
