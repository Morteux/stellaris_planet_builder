using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorPanelManager : MonoBehaviour
{
    public GameObject errorPanel_;
    private InputField warningInputField_;

    void Start()
    {
        warningInputField_ = errorPanel_.transform.Find("Message").GetComponent<InputField>();
        errorPanel_.transform.Find("Close").GetComponent<Button>().onClick.AddListener(ShowPanel);

        errorPanel_.SetActive(false);

        Application.logMessageReceived += HandleLog;
    }

    private void OnDestroy()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        // Only show dangerous log message
        if (type == LogType.Assert || type == LogType.Error || type == LogType.Exception || type == LogType.Warning)
        {
            warningInputField_.text = logString;
            warningInputField_.text = logString + "\n\n" + stackTrace;
        }

        errorPanel_.SetActive(true);
    }

    void ShowPanel()
    {
        errorPanel_.SetActive(!errorPanel_.activeSelf);
    }
}
