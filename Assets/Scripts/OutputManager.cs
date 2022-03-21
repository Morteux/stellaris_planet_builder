using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutputManager : MonoBehaviour
{
    public GameObject resourcePrefab_;
    [System.NonSerialized] public Transform outcomeContent_;
    [System.NonSerialized] public Transform upkeepContent_;
    [System.NonSerialized] public Transform outputContent_;

    private void Awake()
    {
        outcomeContent_ = transform.Find("Outcome/Scroll View/Viewport/Content");
        upkeepContent_ = transform.Find("Upkeep/Scroll View/Viewport/Content");
        outputContent_ = transform.Find("Output/Scroll View/Viewport/Content");
    }

    public void AddOutcomeResource(KeyValuePair<Data.Resource, int> resource)
    {
        GameObject NewResourcePrefab = Instantiate(resourcePrefab_, outcomeContent_.position, outcomeContent_.rotation, outcomeContent_);
        
        // NewResourcePrefab.transform.Find("Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Resources/" + Data.Resource_to_string(resource.Key));
        NewResourcePrefab.transform.Find("Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Resources/" + resource.Key.ToString());
        // NewResourcePrefab.transform.Find("Name").GetComponent<Text>().text = "+" + resource.Value.ToString() + " \t" + Data.Resource_to_string(resource.Key).Replace('_', ' ');
        NewResourcePrefab.transform.Find("Name").GetComponent<Text>().text = "+" + resource.Value.ToString() + " \t" + resource.Key.ToString().Replace('_', ' ');
        NewResourcePrefab.transform.Find("Name").GetComponent<Text>().color = new Color32(0, 255, 0, 255);
    }

    public void AddUpkeepResource(KeyValuePair<Data.Resource, int> resource)
    {
        GameObject NewResourcePrefab = Instantiate(resourcePrefab_, upkeepContent_.position, upkeepContent_.rotation, upkeepContent_);
        
        // NewResourcePrefab.transform.Find("Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Resources/" + Data.Resource_to_string(resource.Key));
        NewResourcePrefab.transform.Find("Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Resources/" + resource.Key.ToString());
        // NewResourcePrefab.transform.Find("Name").GetComponent<Text>().text = resource.Value.ToString() + " \t" + Data.Resource_to_string(resource.Key).Replace('_', ' ');
        NewResourcePrefab.transform.Find("Name").GetComponent<Text>().text = resource.Value.ToString() + " \t" + resource.Key.ToString().Replace('_', ' ');
        NewResourcePrefab.transform.Find("Name").GetComponent<Text>().color = new Color32(255, 0, 0, 255);
    }

    public void AddOutputResource(KeyValuePair<Data.Resource, int> resource)
    {
        GameObject NewResourcePrefab = Instantiate(resourcePrefab_, outputContent_.position, outputContent_.rotation, outputContent_);
        
        // NewResourcePrefab.transform.Find("Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Resources/" + Data.Resource_to_string(resource.Key));
        NewResourcePrefab.transform.Find("Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Resources/" + resource.Key.ToString());

        if (resource.Value > 0)
        {
            // NewResourcePrefab.transform.Find("Name").GetComponent<Text>().text = "+" + resource.Value.ToString() + " \t" + Data.Resource_to_string(resource.Key).Replace('_', ' ');
            NewResourcePrefab.transform.Find("Name").GetComponent<Text>().text = "+" + resource.Value.ToString() + " \t" + resource.Key.ToString().Replace('_', ' ');
            NewResourcePrefab.transform.Find("Name").GetComponent<Text>().color = new Color32(0, 255, 0, 255);
        }
        else
        {
            // NewResourcePrefab.transform.Find("Name").GetComponent<Text>().text = resource.Value.ToString() + " \t" + Data.Resource_to_string(resource.Key).Replace('_', ' ');
            NewResourcePrefab.transform.Find("Name").GetComponent<Text>().text = resource.Value.ToString() + " \t" + resource.Key.ToString().Replace('_', ' ');
            NewResourcePrefab.transform.Find("Name").GetComponent<Text>().color = new Color32(255, 0, 0, 255);
        }
    }

    public void ResetResources()
    {
        if( outcomeContent_.childCount > 0)
            foreach (Transform child in outcomeContent_)
                GameObject.Destroy(child.gameObject);

        if( upkeepContent_.childCount > 0)
            foreach (Transform child in upkeepContent_)
                GameObject.Destroy(child.gameObject);

        if( outputContent_.childCount > 0)
            foreach (Transform child in outputContent_)
                GameObject.Destroy(child.gameObject);
    }
}
