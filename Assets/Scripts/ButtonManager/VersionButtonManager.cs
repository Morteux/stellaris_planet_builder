using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VersionButtonManager : MonoBehaviour
{
    private Transform versionPanel_;

    private void Awake()
    {
        versionPanel_ = transform.parent.parent.Find("Panels/VersionPanel");
        versionPanel_.gameObject.SetActive(false);
    }

    void Start()
    {
        transform.GetComponent<Button>().onClick.AddListener(ShowPanel);
        versionPanel_.Find("Accept").GetComponent<Button>().onClick.AddListener(ChangeScene);
        versionPanel_.Find("Reject").GetComponent<Button>().onClick.AddListener(HidePanel);
    }

    void ChangeScene()
    {
        SceneManager.LoadScene("VersionSelection", LoadSceneMode.Single);
    }

    void ShowPanel()
    {
        versionPanel_.gameObject.SetActive(!versionPanel_.gameObject.activeSelf);
    }

    void HidePanel()
    {
        versionPanel_.gameObject.SetActive(false);
    }
}
