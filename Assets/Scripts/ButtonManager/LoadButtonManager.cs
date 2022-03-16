using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class LoadButtonManager : MonoBehaviour
{
    private Transform loadPanel_;
    private string defaultFilePath_;

    // Start is called before the first frame update
    void Start()
    {
        loadPanel_ = transform.parent.parent.Find("Panels/LoadPanel");

        defaultFilePath_ = Application.dataPath;

        GetComponent<Button>().onClick.AddListener(ShowPanel);

        // Set default filePath
        loadPanel_.Find("InputField").GetComponent<InputField>().text = defaultFilePath_;
        loadPanel_.Find("Load").GetComponent<Button>().onClick.AddListener(LoadPlanet);

        //Hide panel
        loadPanel_.gameObject.SetActive(false);
    }

    void ShowPanel()
    {
        loadPanel_.gameObject.SetActive(!loadPanel_.gameObject.activeSelf);
    }

    void LoadPlanet()
    {
        string path = loadPanel_.Find("InputField").GetComponent<InputField>().text;

        // Read only if file exists
        if (File.Exists(path))
        {
            string fileData = File.ReadAllText(path);
            StartCoroutine(transform.parent.parent.parent.GetComponentInChildren<PlanetBarManager>().LoadPlanetTab(fileData));
        }
        else
            Debug.Log("File does not exists");

        ShowPanel();
    }
}
