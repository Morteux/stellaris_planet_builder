using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text.RegularExpressions;

public class AddVersionButtonManager : MonoBehaviour
{
    public GameObject versionButtonPrefab_;
    private Transform gridLayout_;
    private Transform warningText_;
    private GameObject newVersionPanel_;
    private InputField jobsInputField_;
    private InputField districtsInputField_;
    private InputField buildingsInputField_;
    private InputField planetsInputField_;
    private InputField governmentsInputField_;
    private string defaultWarningText_;

    void Start()
    {
        gridLayout_ = transform.parent.Find("GridLayout");
        transform.parent.Find("NewVersionPanel/Close").GetComponent<Button>().onClick.AddListener(ShowNewVersionPanel);
        transform.GetComponent<Button>().onClick.AddListener(ShowNewVersionPanel);
        transform.parent.Find("NewVersionPanel/Accept").GetComponent<Button>().onClick.AddListener(AddNewVersion);
        warningText_ = transform.parent.Find("NewVersionPanel/Warning");
        newVersionPanel_ = transform.parent.Find("NewVersionPanel").gameObject;

        // Get all path input fields
        jobsInputField_ = transform.parent.Find("NewVersionPanel/Scroll View/Viewport/Content/Jobs/InputField").GetComponent<InputField>();
        districtsInputField_ = transform.parent.Find("NewVersionPanel/Scroll View/Viewport/Content/Districts/InputField").GetComponent<InputField>();
        buildingsInputField_ = transform.parent.Find("NewVersionPanel/Scroll View/Viewport/Content/Buildings/InputField").GetComponent<InputField>();
        planetsInputField_ = transform.parent.Find("NewVersionPanel/Scroll View/Viewport/Content/Planets/InputField").GetComponent<InputField>();
        governmentsInputField_ = transform.parent.Find("NewVersionPanel/Scroll View/Viewport/Content/Governments/InputField").GetComponent<InputField>();

        // Set default warning text
        defaultWarningText_ = "Incorrect name or path.";

        // Create custom directory for new versions
        if (!Directory.Exists(Application.persistentDataPath + "/Custom/"))
            Directory.CreateDirectory(Application.persistentDataPath + "/Custom");

        // Create new versions dataset
        if (!File.Exists(Application.persistentDataPath + "/Custom/NewVersions.txt"))
            File.Create(Application.persistentDataPath + "/Custom/NewVersions.txt");
        else
            // Instantiate older custom versions
            LoadCustomVersion();

        // Show Application.persistentDataPath content
        transform.parent.Find("NewVersionPanel/StorePath/InputField").GetComponent<InputField>().text = Application.persistentDataPath;

        // Hide warning text by default
        warningText_.gameObject.SetActive(false);

        // Hide new version panel by default
        newVersionPanel_.SetActive(false);
    }

    void ShowNewVersionPanel()
    {
        // Debug.Log("ShowNewVersionPanel");

        newVersionPanel_.SetActive(!newVersionPanel_.activeSelf);

        // Hide warning text
        warningText_.gameObject.SetActive(false);
    }

    // Instantiate older custom versions
    void LoadCustomVersion()
    {
        foreach (string line in File.ReadAllLines(Application.persistentDataPath + "/Custom/NewVersions.txt"))
        {
            string[] lineArray = Regex.Split(line, ",\\s*");

            if (lineArray[0] != "")
            {
                GameObject NewVersionButtonPrefab = Instantiate(versionButtonPrefab_, gridLayout_.position, gridLayout_.rotation, gridLayout_);

                NewVersionButtonPrefab.GetComponent<Button>().onClick.AddListener(transform.parent.GetComponentInChildren<VersionSelectionManager>().SelectCustomVersion);
                NewVersionButtonPrefab.transform.Find("Version").GetComponent<Text>().text = lineArray[0];
                NewVersionButtonPrefab.transform.Find("Description").GetComponent<Text>().text = lineArray[1];
                NewVersionButtonPrefab.transform.Find("Wiki").gameObject.SetActive(false);
            }
        }
    }

    void AddNewVersion()
    {
        string customVersionName = transform.parent.Find("NewVersionPanel/Name/InputField").GetComponent<InputField>().text;

        if (customVersionName != "" && jobsInputField_.text != "" && districtsInputField_.text != "" && buildingsInputField_.text != "" && planetsInputField_.text != "" && governmentsInputField_.text != "")
        {
            // Hide warning text
            warningText_.gameObject.SetActive(false);

            string description = transform.parent.Find("NewVersionPanel/Description/InputField").GetComponent<InputField>().text;

            // Create directory for this new version
            Directory.CreateDirectory(Application.persistentDataPath + "/Custom/" + customVersionName);

            // Copy each file to Application.persistentDataPath given for this new version
            File.Copy(jobsInputField_.text, Application.persistentDataPath + "/Custom/" + customVersionName + "/" + Path.GetFileName(jobsInputField_.text), true);
            File.Copy(districtsInputField_.text, Application.persistentDataPath + "/Custom/" + customVersionName + "/" + Path.GetFileName(districtsInputField_.text), true);
            File.Copy(buildingsInputField_.text, Application.persistentDataPath + "/Custom/" + customVersionName + "/" + Path.GetFileName(buildingsInputField_.text), true);
            File.Copy(planetsInputField_.text, Application.persistentDataPath + "/Custom/" + customVersionName + "/" + Path.GetFileName(planetsInputField_.text), true);
            File.Copy(governmentsInputField_.text, Application.persistentDataPath + "/Custom/" + customVersionName + "/" + Path.GetFileName(governmentsInputField_.text), true);

            // Create new line version
            string line = customVersionName + "," + description + "\n";

            string[] lineArray = Regex.Split(line, ",\\s*");

            if (lineArray[0] != "")
            {
                GameObject NewVersionButtonPrefab = Instantiate(versionButtonPrefab_, gridLayout_.position, gridLayout_.rotation, gridLayout_);

                NewVersionButtonPrefab.GetComponent<Button>().onClick.AddListener(transform.parent.GetComponentInChildren<VersionSelectionManager>().SelectVersion);
                NewVersionButtonPrefab.transform.Find("Version").GetComponent<Text>().text = lineArray[0];
                NewVersionButtonPrefab.transform.Find("Description").GetComponent<Text>().text = lineArray[1];
                NewVersionButtonPrefab.transform.Find("Wiki").gameObject.SetActive(false);
            }

            // Store new line version in custom versions dataset
            File.AppendAllText(Application.persistentDataPath + "/Custom/NewVersions.txt", line);
            
            // Set accept button interactable to force select a new version
            transform.parent.Find("Accept").GetComponent<Button>().interactable = false;
        }
        else
        {
            // Show warning text
            if (customVersionName == "")
                warningText_.GetComponent<Text>().text = "Incorrect name format.";
            else if (jobsInputField_.text == "")
                warningText_.GetComponent<Text>().text = "Incorrect jobs path format.";
            else if (districtsInputField_.text == "")
                warningText_.GetComponent<Text>().text = "Incorrect districts path format.";
            else if (buildingsInputField_.text == "")
                warningText_.GetComponent<Text>().text = "Incorrect buildings path format.";
            else if (planetsInputField_.text == "")
                warningText_.GetComponent<Text>().text = "Incorrect planets path format.";
            else if (governmentsInputField_.text == "")
                warningText_.GetComponent<Text>().text = "Incorrect government path format.";
            else
                warningText_.GetComponent<Text>().text = defaultWarningText_;

            warningText_.gameObject.SetActive(true);
        }
    }
}
