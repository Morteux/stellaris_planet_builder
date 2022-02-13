using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class VersionSelectionManager : MonoBehaviour
{
    public GameObject VersionButtonPrefab;
    [System.NonSerialized] public GameObject SelectedVersion;
    private string version;
    private int versionButtonCount;

    // Start is called before the first frame update
    void Start()
    {
        version = "";
        versionButtonCount = 0;
        transform.parent.Find("Accept").GetComponent<Button>().onClick.AddListener(AcceptButton);

        // Instantiate version buttons
        TextAsset versionsFile = Resources.Load<TextAsset>("Data/Versions"); // read Versions.txt from Resource folder

        foreach (string line in versionsFile.text.Split('\n'))
        {
            string[] lineArray = Regex.Split(line, ",\\s*");

            // Last version available is selected by default
            if (version == "" && lineArray[1] == "Yes")
            {
                version = lineArray[0];
                transform.parent.Find("Accept").GetChild(0).GetComponent<Text>().text = "Load: " + version;
            }

            GameObject NewVersionButtonPrefab = Instantiate(VersionButtonPrefab, transform.position, transform.rotation, transform);

            NewVersionButtonPrefab.GetComponent<Button>().onClick.AddListener(SelectVersion);
            NewVersionButtonPrefab.transform.Find("Version").GetComponent<Text>().text = lineArray[0];
            NewVersionButtonPrefab.transform.Find("Description").GetComponent<Text>().text = "Release: " + lineArray[3] + ". " + lineArray[5];
            NewVersionButtonPrefab.transform.Find("Wiki").GetComponent<Button>().onClick.AddListener(delegate { OpenWikiURL(lineArray[2]); });

            ++versionButtonCount;
        }

        Debug.Log("Versions loaded: " + versionButtonCount);
    }

    // Open wiki URL for each version
    void OpenWikiURL(string URL)
    {
        Application.OpenURL(URL);
    }

    // Store version value in this button
    void SelectVersion()
    {
        version = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.Find("Version").GetComponent<Text>().text;
        transform.parent.Find("Accept").GetChild(0).GetComponent<Text>().text = "Load: " + version;
    }

    void AcceptButton()
    {
        Data._version_ = version;
        // PlayerPrefs.SetString("version", version);
        SceneManager.LoadScene("PlanetView", LoadSceneMode.Single);
    }
}
