using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingButtonManager : MonoBehaviour
{
    [System.NonSerialized] public Building building;
    private GameObject BuildingsPanel;
    private Button btnBuilding_;

    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log("Start");
        BuildingsPanel = transform.parent.parent.parent.parent.parent.Find("BuildingsPanel").gameObject;

        btnBuilding_ = gameObject.GetComponent<Button>();
        btnBuilding_.onClick.AddListener(AddBuildingToNextSlot);
    }

    void AddBuildingToNextSlot()
    {
        // Debug.Log("AddBuildingToNextSlot");
        // BuildingsPanel.GetComponent<SlotsManager>().AddNewBuilding(Building._buildings_["Research_Labs"]);

        // Debug.Log(building.name_);
        SlotsManager slotManager = BuildingsPanel.GetComponent<SlotsManager>();

        if (slotManager.lastFreeSlot < slotManager.Slots.transform.childCount)
        {
            slotManager.AddNewBuilding(building);

            if (building.unique_ == "Yes")
            {
                transform.Find("Disabled").gameObject.SetActive(true);
                btnBuilding_.interactable = false;
            }
        }
    }

    public void ActivateBuildingButton()
    {
        transform.Find("Disabled").gameObject.SetActive(false);
        btnBuilding_.interactable = true;
    }
}
