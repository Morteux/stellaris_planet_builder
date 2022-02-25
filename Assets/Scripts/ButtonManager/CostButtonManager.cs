using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CostButtonManager : MonoBehaviour
{
    public GameObject ResourcePrefab;
    private PlanetData planetData_;
    private GameObject costPanel_;
    private Transform costPanelContent_;

    private void Awake()
    {
        costPanel_ = transform.parent.parent.parent.parent.Find("ButtonPanels/CostPanel").gameObject;
        costPanelContent_ = costPanel_.transform.Find("Scroll View/Viewport/Content");
        costPanel_.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        planetData_ = transform.parent.parent.parent.parent.parent.GetComponent<PlanetData>();
        gameObject.GetComponent<Button>().onClick.AddListener(ShowCostPanel);
        costPanel_.transform.Find("Close").GetComponent<Button>().onClick.AddListener(ShowCostPanel);
    }

    void ShowCostPanel()
    {
        // Debug.Log("ShowCostPanel");
        costPanel_.SetActive(!costPanel_.activeSelf);

        if (costPanel_.activeSelf)
            UpdateCosts();
    }

    public void UpdateCosts()
    {
        costPanel_.transform.Find("Resource/Name").GetComponent<Text>().text = planetData_.timeCount + " days / " + (planetData_.timeCount / 365.0f).ToString("0.00") + " years";

        // Remove all ResourcePrefab instantiated before
        if (costPanelContent_.childCount > 0)
            foreach (Transform child in costPanelContent_)
                GameObject.Destroy(child.gameObject);

        foreach (KeyValuePair<Data.Resource, int> pair in planetData_.costResources_)
        {
            // Debug.Log(pair);

            if (pair.Value != 0)
            {
                GameObject NewResourcePrefab = Instantiate(ResourcePrefab, costPanelContent_.position, costPanelContent_.rotation, costPanelContent_);
                // Debug.Log(Data.Resource_to_string(pair.Key));
                // NewResourcePrefab.transform.Find("Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Resources/" + Data.Resource_to_string(pair.Key));
                NewResourcePrefab.transform.Find("Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Resources/" + pair.Key.ToString());

                // NewResourcePrefab.transform.Find("Name").GetComponent<Text>().text = pair.Value.ToString() + " \t" + Data.Resource_to_string(pair.Key).Replace('_', ' ');
                NewResourcePrefab.transform.Find("Name").GetComponent<Text>().text = pair.Value.ToString() + " \t" + pair.Key.ToString().Replace('_', ' ');
                NewResourcePrefab.transform.Find("Name").GetComponent<Text>().color = new Color32(255, 0, 0, 255);
            }
        }
    }
}
