using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingButtonManager : MonoBehaviour
{
    [System.NonSerialized] public Building building;
    private GameObject BuildingsPanel;

    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log("BuildingButtonManager::Start");
        BuildingsPanel = transform.parent.parent.parent.parent.parent.Find("BuildingsPanel").gameObject;
        transform.Find("Disabled").gameObject.SetActive(false);

        GetComponent<Button>().onClick.AddListener(AddBuildingToNextSlot);
    }

    void AddBuildingToNextSlot()
    {
        // Debug.Log("BuildingButtonManager::AddBuildingToNextSlot");

        // Debug.Log(building.name_);
        SlotsManager slotManager = BuildingsPanel.GetComponent<SlotsManager>();

        if (slotManager.lastFreeSlot < slotManager.Slots.transform.childCount)
        {
            slotManager.AddNewBuilding(building);

            if (building.unique_ == "Yes")
            {
                // Debug.Log("is unique");
                transform.Find("Disabled").gameObject.SetActive(true);
                GetComponent<Button>().interactable = false;
            }
        }
    }

    public void ActivateBuildingButton()
    {
        // Debug.Log("BuildingButtonManager::ActivateBuildingButton");

        transform.Find("Disabled").gameObject.SetActive(false);
        GetComponent<Button>().interactable = true;
    }
}
