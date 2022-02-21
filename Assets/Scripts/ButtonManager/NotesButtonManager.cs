using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotesButtonManager : MonoBehaviour
{
    private Transform notesPanel_;

    private void Awake()
    {
        notesPanel_ = transform.parent.Find("NotesPanel");
        notesPanel_.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.GetComponent<Button>().onClick.AddListener(ShowPanel);
    }

    void ShowPanel()
    {
        notesPanel_.gameObject.SetActive(!notesPanel_.gameObject.activeSelf);
    }
}
