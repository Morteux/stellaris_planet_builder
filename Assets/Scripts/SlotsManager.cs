using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class SlotsManager : MonoBehaviour
{
    [System.NonSerialized] public int lastFreeSlot_;     // Next free slot available for add a new building
    [System.NonSerialized] public GameObject slots_;
    [System.NonSerialized] public GameObject slotsBackground_;
    [System.NonSerialized] public GameObject slotsUpgrade_;
    [System.NonSerialized] public GameObject slotsDowngrade_;
    [System.NonSerialized] public GameObject removeButton_;
    private Transform[] childsSlots_;
    private Transform[] childsSlotsBackground_;
    private Transform[] childsSlotsUpgrade_;
    private Transform[] childsSlotsDowngrade_;
    private Building[] slotsBuildings_;
    private Transform planet_;
    private Transform buildingName_;
    private int selectedSlot_;

    private void Awake()
    {
        planet_ = transform.parent.parent;

        lastFreeSlot_ = 1;
        selectedSlot_ = 0;

        slots_ = transform.Find("Slots").gameObject;
        childsSlots_ = new Transform[slots_.transform.childCount];
        slotsBuildings_ = new Building[slots_.transform.childCount];
        removeButton_ = transform.Find("RemoveButton").gameObject;

        buildingName_ = transform.Find("BuildingName");
    }

    void Start()
    {
        int children = slots_.transform.childCount;
        for (int i = 0; i < children; ++i)
        {
            childsSlots_[i] = slots_.transform.GetChild(i);
            childsSlots_[i].GetComponent<Button>().interactable = false;
            childsSlots_[i].GetComponent<Button>().onClick.AddListener(HighlightSlot);
        }
        childsSlots_[0].GetComponent<Button>().interactable = true;

        slotsBackground_ = transform.Find("SlotsBackground").gameObject;
        childsSlotsBackground_ = new Transform[slots_.transform.childCount];

        children = slotsBackground_.transform.childCount;
        for (int i = 0; i < children; ++i)
            childsSlotsBackground_[i] = slotsBackground_.transform.GetChild(i);

        slotsUpgrade_ = transform.Find("SlotsUpgrade").gameObject;
        childsSlotsUpgrade_ = new Transform[slots_.transform.childCount];

        children = slotsUpgrade_.transform.childCount;
        for (int i = 0; i < children; ++i)
        {
            childsSlotsUpgrade_[i] = slotsUpgrade_.transform.GetChild(i);
            childsSlotsUpgrade_[i].GetComponent<Button>().onClick.AddListener(UpgradeBuilding);

            // Adjust clickable button area to actual image area
            childsSlotsUpgrade_[i].GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
        }

        slotsDowngrade_ = transform.Find("SlotsDowngrade").gameObject;
        childsSlotsDowngrade_ = new Transform[slots_.transform.childCount];

        children = slotsDowngrade_.transform.childCount;
        for (int i = 0; i < children; ++i)
        {
            childsSlotsDowngrade_[i] = slotsDowngrade_.transform.GetChild(i);
            childsSlotsDowngrade_[i].GetComponent<Button>().onClick.AddListener(DowngradeBuilding);

            // Adjust clickable button area to actual image area
            childsSlotsDowngrade_[i].GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
        }

        removeButton_.GetComponent<Button>().onClick.AddListener(RemoveBuilding);

        // Capital building
        slotsBuildings_[0] = Building._buildings_["Reassembled_Ship_Shelter"];   // Temporal until multiple capital buildings are supported
        UpdatePlanetBuildings();
    }

    public void AddNewBuilding(Building building)
    {
        if (lastFreeSlot_ < slots_.transform.childCount)
        {

            childsSlots_[lastFreeSlot_].GetComponent<Button>().interactable = true;
            childsSlots_[lastFreeSlot_].GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Buildings/" + building.name_);

            if (building.unique_ == "Yes")
                childsSlotsBackground_[lastFreeSlot_].GetComponent<Image>().color = new Color32(22, 161, 231, 255);

            if (building.upgrade_ != "-")
                childsSlotsUpgrade_[lastFreeSlot_].gameObject.SetActive(true);

            slotsBuildings_[lastFreeSlot_] = building;

            lastFreeSlot_++;
        }

        UpdatePlanetBuildings();
        planet_.GetComponentInChildren<RequirementButtonManager>().UpdateRequirements();
        planet_.GetComponentInChildren<CostButtonManager>().UpdateCosts();
        planet_.GetComponentInChildren<EffectButtonManager>().UpdateEffects();
        planet_.GetComponentInChildren<JobsButtonManager>().UpdateJobs();
    }

    public void LoadBuildings(Building[] buildings)
    {
        // Change capital building too
        lastFreeSlot_ = 0;

        foreach (Building building in buildings)
            if (building != null && lastFreeSlot_ < slots_.transform.childCount)
            {
                childsSlots_[lastFreeSlot_].GetComponent<Button>().interactable = true;
                childsSlots_[lastFreeSlot_].GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Buildings/" + building.name_);

                if (building.unique_ == "Yes")
                    childsSlotsBackground_[lastFreeSlot_].GetComponent<Image>().color = new Color32(22, 161, 231, 255);

                if (building.upgrade_ != "-")
                    childsSlotsUpgrade_[lastFreeSlot_].gameObject.SetActive(true);
                else
                    childsSlotsUpgrade_[lastFreeSlot_].gameObject.SetActive(false);

                if (building.downgrade_ != "-")
                    childsSlotsDowngrade_[lastFreeSlot_].gameObject.SetActive(true);
                else
                    childsSlotsDowngrade_[lastFreeSlot_].gameObject.SetActive(false);

                slotsBuildings_[lastFreeSlot_] = building;

                lastFreeSlot_++;
            }

        UpdatePlanetBuildings();
    }

    public void ChangeCapitalBuilding(Building building)
    {
        // Debug.Log("ChangeCapitalBuilding::building.name_:" + building.name_);

        childsSlots_[0].GetComponent<Button>().interactable = true;
        childsSlots_[0].GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Buildings/" + building.name_);

        // Is always unique
        childsSlotsBackground_[0].GetComponent<Image>().color = new Color32(22, 161, 231, 255);

        // Show upgrade and hide downgrade
        childsSlotsUpgrade_[0].gameObject.SetActive(true);
        childsSlotsDowngrade_[0].gameObject.SetActive(false);

        slotsBuildings_[0] = building;

        UpdatePlanetBuildings();
        planet_.GetComponentInChildren<RequirementButtonManager>().UpdateRequirements();
        planet_.GetComponentInChildren<CostButtonManager>().UpdateCosts();
        planet_.GetComponentInChildren<EffectButtonManager>().UpdateEffects();
        planet_.GetComponentInChildren<JobsButtonManager>().UpdateJobs();
    }

    public void UpgradeBuilding()
    {
        // Debug.Log("UpgradeBuilding");

        GameObject button = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

        // Debug.Log("UpgradeBuilding: " + button.name);

        int slotID = -1;
        string[] stringSplit = Regex.Split(button.name, @" ");
        int.TryParse(stringSplit[1], out slotID);

        // Debug.Log("UpgradeBuilding: " + slotID);

        slotID--;

        Building actualBuilding = slotsBuildings_[slotID];
        Building upgradeBuilding = Building._buildings_[actualBuilding.upgrade_];

        childsSlots_[slotID].GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Buildings/" + upgradeBuilding.name_);

        if (upgradeBuilding.upgrade_ != "-")
            childsSlotsUpgrade_[slotID].gameObject.SetActive(true);
        else
            childsSlotsUpgrade_[slotID].gameObject.SetActive(false);

        if (upgradeBuilding.downgrade_ != "-")
            childsSlotsDowngrade_[slotID].gameObject.SetActive(true);
        else
            childsSlotsDowngrade_[slotID].gameObject.SetActive(false);

        slotsBuildings_[slotID] = upgradeBuilding;

        UpdatePlanetBuildings();
        planet_.GetComponentInChildren<RequirementButtonManager>().UpdateRequirements();
        planet_.GetComponentInChildren<CostButtonManager>().UpdateCosts();
        planet_.GetComponentInChildren<EffectButtonManager>().UpdateEffects();
        planet_.GetComponentInChildren<JobsButtonManager>().UpdateJobs();
    }

    public void DowngradeBuilding()
    {
        // Debug.Log("UpgradeBuilding");

        GameObject button = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

        int slotID = -1;
        string[] stringSplit = Regex.Split(button.name, @" ");
        int.TryParse(stringSplit[1], out slotID);

        slotID--;

        Building actualBuilding = slotsBuildings_[slotID];
        Building downgradeBuilding = Building._buildings_[actualBuilding.downgrade_];

        childsSlots_[slotID].GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Buildings/" + downgradeBuilding.name_);

        if (downgradeBuilding.upgrade_ != "-")
            childsSlotsUpgrade_[slotID].gameObject.SetActive(true);
        else
            childsSlotsUpgrade_[slotID].gameObject.SetActive(false);

        if (downgradeBuilding.downgrade_ != "-")
            childsSlotsDowngrade_[slotID].gameObject.SetActive(true);
        else
            childsSlotsDowngrade_[slotID].gameObject.SetActive(false);

        slotsBuildings_[slotID] = downgradeBuilding;

        UpdatePlanetBuildings();
        planet_.GetComponentInChildren<RequirementButtonManager>().UpdateRequirements();
        planet_.GetComponentInChildren<CostButtonManager>().UpdateCosts();
        planet_.GetComponentInChildren<EffectButtonManager>().UpdateEffects();
        planet_.GetComponentInChildren<JobsButtonManager>().UpdateJobs();
    }

    public void HighlightSlot()
    {
        GameObject button = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        button.GetComponent<Button>().Select();

        string[] stringSplit = Regex.Split(button.name, @" ");
        int.TryParse(stringSplit[1], out selectedSlot_);
        --selectedSlot_;

        if (selectedSlot_ == 0)
            removeButton_.GetComponent<Button>().interactable = false;
        else
            removeButton_.GetComponent<Button>().interactable = true;

        removeButton_.transform.Find("Text").GetComponent<Text>().text = "Remove " + (selectedSlot_ + 1);

        // Update showed name
        buildingName_.GetComponent<Text>().text = slotsBuildings_[selectedSlot_].name_.Replace("_", " ");
    }

    public void RemoveBuilding()
    {
        // Debug.Log("RemoveBuilding");

        // Reactivate building button in BuildPanel for unique builds
        if (slotsBuildings_[selectedSlot_].unique_ == "Yes")
            transform.parent.GetComponentInChildren<BuildManager>().ActivateBuildingButton(slotsBuildings_[selectedSlot_].name_);

        for (int i = selectedSlot_; i < lastFreeSlot_ - 1; ++i)
        {
            slotsBuildings_[i] = slotsBuildings_[i + 1];
            childsSlots_[i].GetComponent<Image>().sprite = childsSlots_[i + 1].GetComponent<Image>().sprite;

            childsSlotsBackground_[i].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            childsSlotsUpgrade_[i].gameObject.SetActive(false);
            childsSlotsDowngrade_[i].gameObject.SetActive(false);

            childsSlotsBackground_[i + 1].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            childsSlotsUpgrade_[i + 1].gameObject.SetActive(false);
            childsSlotsDowngrade_[i + 1].gameObject.SetActive(false);

            if (slotsBuildings_[i] != null)
            {
                if (slotsBuildings_[i].unique_ == "Yes")
                    childsSlotsBackground_[i].GetComponent<Image>().color = new Color32(22, 161, 231, 255);

                if (slotsBuildings_[i].upgrade_ != "-")
                    childsSlotsUpgrade_[i].gameObject.SetActive(true);

                if (slotsBuildings_[i].downgrade_ != "-")
                    childsSlotsDowngrade_[i].gameObject.SetActive(true);
            }
        }

        if (lastFreeSlot_ > 1)
        {
            --lastFreeSlot_;

            slotsBuildings_[lastFreeSlot_] = null;
            childsSlots_[lastFreeSlot_].GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/newBuildingButton");
            childsSlots_[lastFreeSlot_].GetComponent<Button>().interactable = false;
            childsSlotsBackground_[lastFreeSlot_].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            childsSlotsUpgrade_[lastFreeSlot_].gameObject.SetActive(false);
            childsSlotsDowngrade_[lastFreeSlot_].gameObject.SetActive(false);
        }

        if (selectedSlot_ >= lastFreeSlot_)
            removeButton_.GetComponent<Button>().interactable = false;

        UpdatePlanetBuildings();
        planet_.GetComponentInChildren<RequirementButtonManager>().UpdateRequirements();
        planet_.GetComponentInChildren<CostButtonManager>().UpdateCosts();
        planet_.GetComponentInChildren<EffectButtonManager>().UpdateEffects();
        planet_.GetComponentInChildren<JobsButtonManager>().UpdateJobs();
    }

    private void UpdatePlanetBuildings()
    {
        System.Array.Copy(slotsBuildings_, planet_.GetComponent<PlanetData>().buildings_, slotsBuildings_.Length);
        planet_.GetComponent<PlanetData>().UpdatePlanetData();
    }
}