using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class VersionSelectionManager : MonoBehaviour
{
    public GameObject versionButtonPrefab_;
    private string version_;
    private string customPath_;
    private int versionButtonCount_;
    private Transform loadingScreen_;
    private Transform progressBar_;
    private AsyncOperation loadingOperation_;

    void Start()
    {
        version_ = "";
        versionButtonCount_ = 0;
        loadingScreen_ = transform.parent.parent.Find("LoadingScreen");
        progressBar_ = loadingScreen_.Find("Slider");
        loadingScreen_.gameObject.SetActive(false);

        transform.parent.Find("Accept").GetComponent<Button>().onClick.AddListener(AcceptButton);

        // Instantiate version buttons
        TextAsset versionsFile = Resources.Load<TextAsset>("Data/Versions"); // read Versions.txt from Resource folder

        foreach (string line in versionsFile.text.Split('\n'))
        {
            string[] lineArray = Regex.Split(line, ",\\s*");

            // Last version available is selected by default
            if (version_ == "" && lineArray[1] == "Yes")
            {
                version_ = lineArray[0];
                transform.parent.Find("Accept").GetChild(0).GetComponent<Text>().text = "Load: " + version_;
            }

            GameObject NewVersionButtonPrefab = Instantiate(versionButtonPrefab_, transform.position, transform.rotation, transform);

            NewVersionButtonPrefab.GetComponent<Button>().onClick.AddListener(SelectVersion);
            NewVersionButtonPrefab.transform.Find("Version").GetComponent<Text>().text = lineArray[0];
            NewVersionButtonPrefab.transform.Find("Description").GetComponent<Text>().text = "Release: " + lineArray[3] + ". " + lineArray[5];
            NewVersionButtonPrefab.transform.Find("Wiki").GetComponent<Button>().onClick.AddListener(delegate { OpenWikiURL(lineArray[2]); });

            if (lineArray[1] == "No")
                NewVersionButtonPrefab.GetComponent<Button>().interactable = false;

            ++versionButtonCount_;
        }

        Debug.Log("Versions loaded: " + versionButtonCount_);
    }

    private void Update()
    {
        if (loadingScreen_.gameObject.activeSelf)
        {
            float progressValue = Mathf.Clamp01(loadingOperation_.progress / 0.9f);
            progressBar_.GetComponent<Slider>().value = progressValue;
            progressBar_.Find("Value").GetComponent<Text>().text = Mathf.Round(progressValue * 100).ToString("0.0") + "%";
        }
    }

    // Open wiki URL for each version
    void OpenWikiURL(string URL)
    {
        Application.OpenURL(URL);
    }

    // Store version value in this button
    public void SelectVersion()
    {
        // Debug.Log("SelectVersion");

        Data._isCustomVersion_ = false;
        version_ = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.Find("Version").GetComponent<Text>().text;
        transform.parent.Find("Accept").GetChild(0).GetComponent<Text>().text = "Load: " + version_;
        transform.parent.Find("Accept").GetComponent<Button>().interactable = true;
    }

    // Store version value in this button
    public void SelectCustomVersion()
    {
        // Debug.Log("SelectCustomVersion");

        Data._isCustomVersion_ = true;
        version_ = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.Find("Version").GetComponent<Text>().text;
        customPath_ = Application.persistentDataPath + "/Custom/" + version_;
        transform.parent.Find("Accept").GetChild(0).GetComponent<Text>().text = "Load: " + version_;
        transform.parent.Find("Accept").GetComponent<Button>().interactable = true;
    }

    void AcceptButton()
    {
        Data._version_ = version_;

        if (Data._isCustomVersion_)
            Data._customPath_ = customPath_;

        loadingScreen_.gameObject.SetActive(true);

        loadingScreen_.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/LoadingScreens/" + Random.Range(0, 17));

        loadingOperation_ = SceneManager.LoadSceneAsync("PlanetView");
    }
}
