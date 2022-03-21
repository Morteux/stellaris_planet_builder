using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuBarManager : MonoBehaviour
{
    void Start()
    {
        // Debug.Log("MenuBarManager:Start");

        transform.Find("Version").GetComponent<Text>().text = Data._version_;
    }
}
