using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotesButtonManager : MonoBehaviour
{
    private Transform notesPanel_;

    private void Awake()
    {
        notesPanel_ = transform.parent.parent.parent.parent.Find("ButtonPanels/NotesPanel");
        notesPanel_.gameObject.SetActive(false);
    }

    void Start()
    {
        transform.GetComponent<Button>().onClick.AddListener(ShowNotesPanel);
        notesPanel_.Find("Close").GetComponent<Button>().onClick.AddListener(ShowNotesPanel);
    }

    void ShowNotesPanel()
    {
        notesPanel_.gameObject.SetActive(!notesPanel_.gameObject.activeSelf);
    }
}
