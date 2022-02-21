using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetTypeButtonManager : MonoBehaviour
{
    public GameObject selectableItemButtonPrefab_;
    private GameObject planetTypePanel_;
    private Transform planetTypeContent_;

    // Start is called before the first frame update
    void Start()
    {
        planetTypePanel_ = gameObject.GetComponent<Button>().transform.Find("Panel/PlanetTypePanel").gameObject;
        planetTypeContent_ = planetTypePanel_.transform.Find("Scroll View/Viewport/Content");

        // Instantiate Planets
        foreach (KeyValuePair<string, Planet> pair in Planet._planets_)
        {
            // Debug.Log(pair);
            GameObject NewSelectableItemButtonPrefab = Instantiate(selectableItemButtonPrefab_, planetTypeContent_.position, planetTypeContent_.rotation, planetTypeContent_);
            NewSelectableItemButtonPrefab.GetComponent<Button>().onClick.AddListener(ChangePlanetType);
            NewSelectableItemButtonPrefab.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Planets/" + pair.Key);
            NewSelectableItemButtonPrefab.transform.Find("Text").GetComponent<Text>().text = pair.Key.Replace('_', ' ');
            NewSelectableItemButtonPrefab.transform.Find("SelectedFrame").gameObject.SetActive(false);
        }

        planetTypePanel_.SetActive(false);
    }

    public void ChangePlanetType()
    {
        // Debug.Log("PlanetTypeButtonManager::ChangePlanetType");

        GameObject button = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

        // Set active false all planet types SelectedFrame
        if (planetTypeContent_.childCount > 0)
            foreach (Transform child in planetTypeContent_)
                child.Find("SelectedFrame").gameObject.SetActive(false);
        
        button.transform.Find("SelectedFrame").gameObject.SetActive(true);

        // Save new planet type
        string planetType = button.GetComponentInChildren<Text>().text.Replace(' ', '_');
        GetComponent<PlanetData>().planetaryEffects_ = Planet._planets_[planetType].effects_;
        GetComponent<PlanetData>().planetType_ = planetType;
        GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Planets/" + planetType);
        GetComponent<PlanetData>().UpdatePlanetData();
        GetComponentInChildren<JobsButtonManager>().UpdateJobs();
        GetComponentInChildren<EffectButtonManager>().UpdateEffects();
    }
}
