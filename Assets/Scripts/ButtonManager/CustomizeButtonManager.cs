using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomizeButtonManager : MonoBehaviour
{
    Transform customizationPanel_;
    GameObject valueButton_;
    GameObject percentageButton_;
    GameObject jobsButton_;
    GameObject valueScrollView_;
    GameObject percentageScrollView_;
    GameObject jobsScrollView_;

    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log("CustomizeButtonManager::Start");
        customizationPanel_ = transform.parent.parent.parent.parent.Find("ButtonPanels/CustomizationPanel");
        customizationPanel_.Find("Close").GetComponent<Button>().onClick.AddListener(ShowCustomizationPanel);

        GetComponent<Button>().onClick.AddListener(ShowCustomizationPanel);

        valueButton_ = customizationPanel_.transform.Find("ValueButton").gameObject;
        percentageButton_ = customizationPanel_.transform.Find("PercentageButton").gameObject;
        jobsButton_ = customizationPanel_.transform.Find("JobsButton").gameObject;

        valueScrollView_ = customizationPanel_.transform.Find("Value Scroll View").gameObject;
        percentageScrollView_ = customizationPanel_.transform.Find("Percentage Scroll View").gameObject;
        jobsScrollView_ = customizationPanel_.transform.Find("Jobs Scroll View").gameObject;

        valueButton_.GetComponent<Button>().onClick.AddListener(delegate { ShowScrollView(valueScrollView_); });
        percentageButton_.GetComponent<Button>().onClick.AddListener(delegate { ShowScrollView(percentageScrollView_); });
        jobsButton_.GetComponent<Button>().onClick.AddListener(delegate { ShowScrollView(jobsScrollView_); });

        percentageScrollView_.SetActive(false);
        jobsScrollView_.SetActive(false);

        customizationPanel_.gameObject.SetActive(false);
    }

    void ShowCustomizationPanel()
    {
        // Debug.Log("CustomizeButtonManager::ShowCustomizationPanel");
        customizationPanel_.gameObject.SetActive(!customizationPanel_.gameObject.activeSelf);
    }

    void ShowScrollView(GameObject scrollView)
    {
        // Debug.Log("CustomizeButtonManager::ShowScrollView");
        valueScrollView_.SetActive(false);
        percentageScrollView_.SetActive(false);
        jobsScrollView_.SetActive(false);

        scrollView.SetActive(true);
    }
}
