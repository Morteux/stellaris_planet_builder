using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingButtonManager : MonoBehaviour
{
    [System.NonSerialized] public Building building_;
    private GameObject buildingsPanel_;

    void Start()
    {
        // Debug.Log("BuildingButtonManager::Start");
        
        buildingsPanel_ = transform.parent.parent.parent.parent.parent.Find("BuildingsPanel").gameObject;
        transform.Find("Disabled").gameObject.SetActive(false);

        GetComponent<Button>().onClick.AddListener(AddBuildingToNextSlot);
    }

    void AddBuildingToNextSlot()
    {
        // Debug.Log("BuildingButtonManager::AddBuildingToNextSlot");

        SlotsManager slotManager = buildingsPanel_.GetComponent<SlotsManager>();

        if (slotManager.lastFreeSlot_ < slotManager.slots_.transform.childCount)
        {
            slotManager.AddNewBuilding(building_);

            if (building_.unique_ == "Yes")
            {
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
