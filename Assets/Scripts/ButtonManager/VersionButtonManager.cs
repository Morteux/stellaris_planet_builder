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
        versionPanel_ = transform.parent.Find("VersionPanel");
        versionPanel_.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.GetComponent<Button>().onClick.AddListener(ShowPanel);
        versionPanel_.Find("Accept").GetComponent<Button>().onClick.AddListener(ChangeScene);
        versionPanel_.Find("Reject").GetComponent<Button>().onClick.AddListener(HidePanel);
    }

    void ShowPanel()
    {
        versionPanel_.gameObject.SetActive(!versionPanel_.gameObject.activeSelf);
    }

    void ChangeScene()
    {
        SceneManager.LoadScene("VersionSelection", LoadSceneMode.Single);
    }

    void HidePanel()
    {
        versionPanel_.gameObject.SetActive(false);
    }
}
