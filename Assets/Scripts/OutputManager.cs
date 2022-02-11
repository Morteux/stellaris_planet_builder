using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutputManager : MonoBehaviour
{
    public GameObject ResourcePrefab;
    [System.NonSerialized] public Transform outcomeContent;
    [System.NonSerialized] public Transform upkeepContent;
    [System.NonSerialized] public Transform outputContent;

    private void Awake()
    {
        outcomeContent = transform.Find("Outcome/Scroll View/Viewport/Content");
        upkeepContent = transform.Find("Upkeep/Scroll View/Viewport/Content");
        outputContent = transform.Find("Output/Scroll View/Viewport/Content");
    }

    public void AddOutcomeResource(KeyValuePair<Data.Resource, int> resource)
    {
        GameObject NewResourcePrefab = Instantiate(ResourcePrefab, outcomeContent.position, outcomeContent.rotation, outcomeContent);
        // Debug.Log(Data.Resource_to_string(resource.Key));
        NewResourcePrefab.transform.Find("Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Resources/" + Data.Resource_to_string(resource.Key));
        NewResourcePrefab.transform.Find("Name").GetComponent<Text>().text = "+" + resource.Value.ToString() + " \t" + Data.Resource_to_string(resource.Key).Replace('_', ' ');
        NewResourcePrefab.transform.Find("Name").GetComponent<Text>().color = new Color32(0, 255, 0, 255);
    }

    public void AddUpkeepResource(KeyValuePair<Data.Resource, int> resource)
    {
        GameObject NewResourcePrefab = Instantiate(ResourcePrefab, upkeepContent.position, upkeepContent.rotation, upkeepContent);
        // Debug.Log(Data.Resource_to_string(resource.Key));
        NewResourcePrefab.transform.Find("Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Resources/" + Data.Resource_to_string(resource.Key));
        NewResourcePrefab.transform.Find("Name").GetComponent<Text>().text = resource.Value.ToString() + " \t" + Data.Resource_to_string(resource.Key).Replace('_', ' ');
        NewResourcePrefab.transform.Find("Name").GetComponent<Text>().color = new Color32(255, 0, 0, 255);
    }

    public void AddOutputResource(KeyValuePair<Data.Resource, int> resource)
    {
        GameObject NewResourcePrefab = Instantiate(ResourcePrefab, outputContent.position, outputContent.rotation, outputContent);
        // Debug.Log(Data.Resource_to_string(resource.Key));
        NewResourcePrefab.transform.Find("Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Resources/" + Data.Resource_to_string(resource.Key));

        if (resource.Value > 0)
        {
            NewResourcePrefab.transform.Find("Name").GetComponent<Text>().text = "+" + resource.Value.ToString() + " \t" + Data.Resource_to_string(resource.Key).Replace('_', ' ');
            NewResourcePrefab.transform.Find("Name").GetComponent<Text>().color = new Color32(0, 255, 0, 255);
        }
        else
        {
            NewResourcePrefab.transform.Find("Name").GetComponent<Text>().text = resource.Value.ToString() + " \t" + Data.Resource_to_string(resource.Key).Replace('_', ' ');
            NewResourcePrefab.transform.Find("Name").GetComponent<Text>().color = new Color32(255, 0, 0, 255);
        }
    }

    public void ResetResources()
    {
        if( outcomeContent.childCount > 0)
            foreach (Transform child in outcomeContent)
                GameObject.Destroy(child.gameObject);

        if( upkeepContent.childCount > 0)
            foreach (Transform child in upkeepContent)
                GameObject.Destroy(child.gameObject);

        if( outputContent.childCount > 0)
            foreach (Transform child in outputContent)
                GameObject.Destroy(child.gameObject);
    }
}
