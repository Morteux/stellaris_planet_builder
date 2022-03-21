using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JobsButtonManager : MonoBehaviour
{
    public GameObject resourcePrefab_;
    private PlanetData planetData_;
    private GameObject jobsPanel_;
    private Transform jobsPanelContent_;

    private void Awake()
    {
        jobsPanel_ = transform.parent.parent.parent.parent.Find("ButtonPanels/JobsPanel").gameObject;
        jobsPanelContent_ = jobsPanel_.transform.Find("Scroll View/Viewport/Content");
        jobsPanel_.SetActive(false);
    }

    void Start()
    {
        planetData_ = transform.parent.parent.parent.parent.parent.GetComponent<PlanetData>();
        gameObject.GetComponent<Button>().onClick.AddListener(ShowJobPanel);
        jobsPanel_.transform.Find("Close").GetComponent<Button>().onClick.AddListener(ShowJobPanel);
    }

    void ShowJobPanel()
    {
        // Debug.Log("ShowJobPanel");
        jobsPanel_.SetActive(!jobsPanel_.activeSelf);

        if (jobsPanel_.activeSelf)
            UpdateJobs();
    }

    public void UpdateJobs()
    {
        // Remove all ResourcePrefab instantiated before
        if (jobsPanelContent_.childCount > 0)
            foreach (Transform child in jobsPanelContent_)
                GameObject.Destroy(child.gameObject);

        foreach (KeyValuePair<Job, int> pair in planetData_.jobs_)
        {
            // Debug.Log(pair);

            if (pair.Value != 0)
            {
                GameObject NewResourcePrefab = Instantiate(resourcePrefab_, jobsPanelContent_.position, jobsPanelContent_.rotation, jobsPanelContent_);
                // Debug.Log(Data.Resource_to_string(pair.Key));
                // NewResourcePrefab.transform.Find("Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Resources/" + Data.Resource_to_string(pair.Key));
                NewResourcePrefab.transform.Find("Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Jobs/" + pair.Key.name_);

                // NewResourcePrefab.transform.Find("Name").GetComponent<Text>().text = pair.Value.ToString() + " \t" + Data.Resource_to_string(pair.Key).Replace('_', ' ');
                NewResourcePrefab.transform.Find("Name").GetComponent<Text>().text = pair.Value.ToString() + " \t" + pair.Key.name_.Replace('_', ' ');
            }
        }
    }
}
