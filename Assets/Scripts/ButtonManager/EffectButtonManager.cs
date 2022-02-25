using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectButtonManager : MonoBehaviour
{
    public GameObject ResourcePrefab;
    private PlanetData planetData_;
    private GameObject effectPanel_;
    private Transform effectPanelContent_;

    private void Awake()
    {
        effectPanel_ = transform.parent.parent.parent.parent.Find("ButtonPanels/EffectPanel").gameObject;
        effectPanelContent_ = effectPanel_.transform.Find("Scroll View/Viewport/Content");
        effectPanel_.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        planetData_ = transform.parent.parent.parent.parent.parent.GetComponent<PlanetData>();
        gameObject.GetComponent<Button>().onClick.AddListener(ShowEffectsPanel);
        effectPanel_.transform.Find("Close").GetComponent<Button>().onClick.AddListener(ShowEffectsPanel);
    }

    void ShowEffectsPanel()
    {
        // Debug.Log("ShowEffectPanel");
        effectPanel_.SetActive(!effectPanel_.activeSelf);

        if (effectPanel_.activeSelf)
            UpdateEffects();
    }

    public void UpdateEffects()
    {
        // Remove all ResourcePrefab instantiated before
        if (effectPanelContent_.childCount > 0)
            foreach (Transform child in effectPanelContent_)
                GameObject.Destroy(child.gameObject);

        foreach (KeyValuePair<Data.Effects, int> pair in planetData_.planetaryEffects_)
        {
            // Debug.Log(pair);
            GameObject NewResourcePrefab = Instantiate(ResourcePrefab, effectPanelContent_.position, effectPanelContent_.rotation, effectPanelContent_);
            // NewResourcePrefab.transform.Find("Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Resources/" + Data.Effects_to_string(pair.Key));
            NewResourcePrefab.transform.Find("Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Planets/" + planetData_.planetType_);
            // NewResourcePrefab.transform.Find("Name").GetComponent<Text>().text = pair.Value.ToString() + "% \t" + Data.Effects_to_string(pair.Key).Replace('_', ' ');
            NewResourcePrefab.transform.Find("Name").GetComponent<Text>().text = pair.Value.ToString() + "% \t" + pair.Key.ToString().Replace('_', ' ');
        }

        foreach (KeyValuePair<Data.Effects, int> pair in planetData_.effects_)
        {
            // Debug.Log(pair);
            GameObject NewResourcePrefab = Instantiate(ResourcePrefab, effectPanelContent_.position, effectPanelContent_.rotation, effectPanelContent_);
            // NewResourcePrefab.transform.Find("Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Resources/" + Data.Effects_to_string(pair.Key));
            NewResourcePrefab.transform.Find("Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Resources/" + pair.Key.ToString());
            // NewResourcePrefab.transform.Find("Name").GetComponent<Text>().text = pair.Value.ToString() + "% \t" + Data.Effects_to_string(pair.Key).Replace('_', ' ');
            NewResourcePrefab.transform.Find("Name").GetComponent<Text>().text = pair.Value.ToString() + "% \t" + pair.Key.ToString().Replace('_', ' ');
        }
    }
}
