using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class SlotsManager : MonoBehaviour
{
    [System.NonSerialized] public int lastFreeSlot;     // Next free slot available for add a new building
    [System.NonSerialized] public GameObject Slots;
    [System.NonSerialized] public GameObject SlotsBackground;
    [System.NonSerialized] public GameObject SlotsUpgrade;
    [System.NonSerialized] public GameObject SlotsDowngrade;
    [System.NonSerialized] public GameObject RemoveButton;
    private Transform[] childsSlots;
    private Transform[] childsSlotsBackground;
    private Transform[] childsSlotsUpgrade;
    private Transform[] childsSlotsDowngrade;
    private Building[] slotsBuildings;
    private Transform planet;
    private int selectedSlot;

    private void Awake()
    {
        planet = transform.parent.parent;

        lastFreeSlot = 1;
        selectedSlot = 0;

        Slots = transform.Find("Slots").gameObject;
        childsSlots = new Transform[Slots.transform.childCount];
        slotsBuildings = new Building[Slots.transform.childCount];
        RemoveButton = transform.Find("RemoveButton").gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        int children = Slots.transform.childCount;
        for (int i = 0; i < children; ++i)
        {
            childsSlots[i] = Slots.transform.GetChild(i);
            childsSlots[i].GetComponent<Button>().interactable = false;
            childsSlots[i].GetComponent<Button>().onClick.AddListener(HighlightSlot);
        }
        childsSlots[0].GetComponent<Button>().interactable = true;

        SlotsBackground = transform.Find("SlotsBackground").gameObject;
        childsSlotsBackground = new Transform[Slots.transform.childCount];

        children = SlotsBackground.transform.childCount;
        for (int i = 0; i < children; ++i)
            childsSlotsBackground[i] = SlotsBackground.transform.GetChild(i);

        SlotsUpgrade = transform.Find("SlotsUpgrade").gameObject;
        childsSlotsUpgrade = new Transform[Slots.transform.childCount];

        children = SlotsUpgrade.transform.childCount;
        for (int i = 0; i < children; ++i)
        {
            childsSlotsUpgrade[i] = SlotsUpgrade.transform.GetChild(i);
            childsSlotsUpgrade[i].GetComponent<Button>().onClick.AddListener(UpgradeBuilding);

            // Adjust clickable button area to actual image area
            childsSlotsUpgrade[i].GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
        }

        SlotsDowngrade = transform.Find("SlotsDowngrade").gameObject;
        childsSlotsDowngrade = new Transform[Slots.transform.childCount];

        children = SlotsDowngrade.transform.childCount;
        for (int i = 0; i < children; ++i)
        {
            childsSlotsDowngrade[i] = SlotsDowngrade.transform.GetChild(i);
            childsSlotsDowngrade[i].GetComponent<Button>().onClick.AddListener(DowngradeBuilding);

            // Adjust clickable button area to actual image area
            childsSlotsDowngrade[i].GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
        }

        RemoveButton.GetComponent<Button>().onClick.AddListener(RemoveBuilding);

        // Capital building
        slotsBuildings[0] = Building._buildings_["Reassembled_Ship_Shelter"];   // Temporal until multiple capital buildings are supported
        UpdatePlanetBuildings();
    }

    public void AddNewBuilding(Building building)
    {
        if (lastFreeSlot < Slots.transform.childCount)
        {

            childsSlots[lastFreeSlot].GetComponent<Button>().interactable = true;
            childsSlots[lastFreeSlot].GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Buildings/" + building.name_);

            if (building.unique_ == "Yes")
                childsSlotsBackground[lastFreeSlot].GetComponent<Image>().color = new Color32(22, 161, 231, 255);

            if (building.upgrade_ != "-")
                childsSlotsUpgrade[lastFreeSlot].gameObject.SetActive(true);

            slotsBuildings[lastFreeSlot] = building;

            lastFreeSlot++;
        }

        UpdatePlanetBuildings();
        planet.GetComponentInChildren<RequirementButtonManager>().UpdateRequirements();
        planet.GetComponentInChildren<CostButtonManager>().UpdateCosts();
        planet.GetComponentInChildren<EffectButtonManager>().UpdateEffects();
        planet.GetComponentInChildren<JobsButtonManager>().UpdateJobs();
    }

    public void LoadBuildings(Building[] buildings)
    {
        // Change capital building too
        lastFreeSlot = 0;

        foreach (Building building in buildings)
            if (building != null && lastFreeSlot < Slots.transform.childCount)
            {
                childsSlots[lastFreeSlot].GetComponent<Button>().interactable = true;
                childsSlots[lastFreeSlot].GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Buildings/" + building.name_);

                if (building.unique_ == "Yes")
                    childsSlotsBackground[lastFreeSlot].GetComponent<Image>().color = new Color32(22, 161, 231, 255);

                if (building.upgrade_ != "-")
                    childsSlotsUpgrade[lastFreeSlot].gameObject.SetActive(true);
                else
                    childsSlotsUpgrade[lastFreeSlot].gameObject.SetActive(false);

                if (building.downgrade_ != "-")
                    childsSlotsDowngrade[lastFreeSlot].gameObject.SetActive(true);
                else
                    childsSlotsDowngrade[lastFreeSlot].gameObject.SetActive(false);

                slotsBuildings[lastFreeSlot] = building;

                lastFreeSlot++;
            }

        UpdatePlanetBuildings();
        // planet.GetComponentInChildren<RequirementButtonManager>().UpdateRequirements();
        // planet.GetComponentInChildren<CostButtonManager>().UpdateCosts();
        // planet.GetComponentInChildren<EffectButtonManager>().UpdateEffects();
        // planet.GetComponentInChildren<JobsButtonManager>().UpdateJobs();
    }

    public void ChangeCapitalBuilding(Building building)
    {
        // Debug.Log("ChangeCapitalBuilding::building.name_:" + building.name_);

        childsSlots[0].GetComponent<Button>().interactable = true;
        childsSlots[0].GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Buildings/" + building.name_);

        // Is always unique
        childsSlotsBackground[0].GetComponent<Image>().color = new Color32(22, 161, 231, 255);

        // Show upgrade and hide downgrade
        childsSlotsUpgrade[0].gameObject.SetActive(true);
        childsSlotsDowngrade[0].gameObject.SetActive(false);

        slotsBuildings[0] = building;

        UpdatePlanetBuildings();
        planet.GetComponentInChildren<RequirementButtonManager>().UpdateRequirements();
        planet.GetComponentInChildren<CostButtonManager>().UpdateCosts();
        planet.GetComponentInChildren<EffectButtonManager>().UpdateEffects();
        planet.GetComponentInChildren<JobsButtonManager>().UpdateJobs();
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

        Building actualBuilding = slotsBuildings[slotID];
        Building upgradeBuilding = Building._buildings_[actualBuilding.upgrade_];

        childsSlots[slotID].GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Buildings/" + upgradeBuilding.name_);

        // if (upgradeBuilding.unique_ == "Yes")
        //     childsSlotsBackground[slotID].GetComponent<Image>().color = new Color32(22, 161, 231, 255);

        if (upgradeBuilding.upgrade_ != "-")
            childsSlotsUpgrade[slotID].gameObject.SetActive(true);
        else
            childsSlotsUpgrade[slotID].gameObject.SetActive(false);

        if (upgradeBuilding.downgrade_ != "-")
            childsSlotsDowngrade[slotID].gameObject.SetActive(true);
        else
            childsSlotsDowngrade[slotID].gameObject.SetActive(false);

        slotsBuildings[slotID] = upgradeBuilding;

        UpdatePlanetBuildings();
        planet.GetComponentInChildren<RequirementButtonManager>().UpdateRequirements();
        planet.GetComponentInChildren<CostButtonManager>().UpdateCosts();
        planet.GetComponentInChildren<EffectButtonManager>().UpdateEffects();
        planet.GetComponentInChildren<JobsButtonManager>().UpdateJobs();
    }

    public void DowngradeBuilding()
    {
        // Debug.Log("UpgradeBuilding");

        GameObject button = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

        // Debug.Log("UpgradeBuilding: " + button.name);

        int slotID = -1;
        string[] stringSplit = Regex.Split(button.name, @" ");
        int.TryParse(stringSplit[1], out slotID);

        // Debug.Log("UpgradeBuilding: " + slotID);

        slotID--;

        Building actualBuilding = slotsBuildings[slotID];
        Building downgradeBuilding = Building._buildings_[actualBuilding.downgrade_];

        childsSlots[slotID].GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Buildings/" + downgradeBuilding.name_);

        // if (downgradeBuilding.unique_ == "Yes")
        //     childsSlotsBackground[slotID].GetComponent<Image>().color = new Color32(22, 161, 231, 255);

        if (downgradeBuilding.upgrade_ != "-")
            childsSlotsUpgrade[slotID].gameObject.SetActive(true);
        else
            childsSlotsUpgrade[slotID].gameObject.SetActive(false);

        if (downgradeBuilding.downgrade_ != "-")
            childsSlotsDowngrade[slotID].gameObject.SetActive(true);
        else
            childsSlotsDowngrade[slotID].gameObject.SetActive(false);

        slotsBuildings[slotID] = downgradeBuilding;

        UpdatePlanetBuildings();
        planet.GetComponentInChildren<RequirementButtonManager>().UpdateRequirements();
        planet.GetComponentInChildren<CostButtonManager>().UpdateCosts();
        planet.GetComponentInChildren<EffectButtonManager>().UpdateEffects();
        planet.GetComponentInChildren<JobsButtonManager>().UpdateJobs();
    }

    public void HighlightSlot()
    {
        GameObject button = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        button.GetComponent<Button>().Select();

        string[] stringSplit = Regex.Split(button.name, @" ");
        int.TryParse(stringSplit[1], out selectedSlot);
        --selectedSlot;

        if (selectedSlot == 0)
            RemoveButton.GetComponent<Button>().interactable = false;
        else
            RemoveButton.GetComponent<Button>().interactable = true;

        RemoveButton.transform.Find("Text").GetComponent<Text>().text = "Remove " + (selectedSlot + 1);
    }

    public void RemoveBuilding()
    {
        // Debug.Log("RemoveBuilding: " + selectedSlot);

        // Reactivate building button in BuildPanel for unique builds
        if (slotsBuildings[selectedSlot].unique_ == "Yes")
            transform.parent.GetComponentInChildren<BuildManager>().ActivateBuildingButton(slotsBuildings[selectedSlot].name_);

        for (int i = selectedSlot; i < lastFreeSlot - 1; ++i)
        {
            slotsBuildings[i] = slotsBuildings[i + 1];
            childsSlots[i].GetComponent<Image>().sprite = childsSlots[i + 1].GetComponent<Image>().sprite;

            childsSlotsBackground[i].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            childsSlotsUpgrade[i].gameObject.SetActive(false);
            childsSlotsDowngrade[i].gameObject.SetActive(false);

            childsSlotsBackground[i + 1].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            childsSlotsUpgrade[i + 1].gameObject.SetActive(false);
            childsSlotsDowngrade[i + 1].gameObject.SetActive(false);

            if (slotsBuildings[i] != null)
            {
                if (slotsBuildings[i].unique_ == "Yes")
                    childsSlotsBackground[i].GetComponent<Image>().color = new Color32(22, 161, 231, 255);

                if (slotsBuildings[i].upgrade_ != "-")
                    childsSlotsUpgrade[i].gameObject.SetActive(true);

                if (slotsBuildings[i].downgrade_ != "-")
                    childsSlotsDowngrade[i].gameObject.SetActive(true);
            }
        }

        // Debug.Log(lastFreeSlot);

        if (lastFreeSlot > 1)
        {
            --lastFreeSlot;

            slotsBuildings[lastFreeSlot] = null;
            childsSlots[lastFreeSlot].GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/newBuildingButton");
            childsSlots[lastFreeSlot].GetComponent<Button>().interactable = false;
            childsSlotsBackground[lastFreeSlot].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            childsSlotsUpgrade[lastFreeSlot].gameObject.SetActive(false);
            childsSlotsDowngrade[lastFreeSlot].gameObject.SetActive(false);
        }

        if (selectedSlot >= lastFreeSlot)
            RemoveButton.GetComponent<Button>().interactable = false;

        UpdatePlanetBuildings();
        planet.GetComponentInChildren<RequirementButtonManager>().UpdateRequirements();
        planet.GetComponentInChildren<CostButtonManager>().UpdateCosts();
        planet.GetComponentInChildren<EffectButtonManager>().UpdateEffects();
        planet.GetComponentInChildren<JobsButtonManager>().UpdateJobs();
    }

    private void UpdatePlanetBuildings()
    {
        System.Array.Copy(slotsBuildings, planet.GetComponent<PlanetData>().buildings_, slotsBuildings.Length);
        planet.GetComponent<PlanetData>().UpdatePlanetData();
    }
}