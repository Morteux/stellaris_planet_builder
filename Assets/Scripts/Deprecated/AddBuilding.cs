// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// public class AddBuilding : MonoBehaviour
// {
//     private GameObject BuildingsPanel;
//     private Button btnBuilding_;

//     // Start is called before the first frame update
//     void Start()
//     {
//         // Debug.Log("Start");
//         BuildingsPanel = transform.parent.parent.Find("BuildingsPanel").gameObject;

//         btnBuilding_ = gameObject.GetComponent<Button>();
//         btnBuilding_.onClick.AddListener(AddBuildingToNextSlot);
//     }

//     void AddBuildingToNextSlot()
//     {
//         // Debug.Log("AddBuildingToNextSlot");
//         // BuildingsPanel.GetComponent<SlotsManager>().AddNewBuilding(Building._buildings_["Research_Labs"]);

//         BuildingsPanel.GetComponent<SlotsManager>().AddNewBuilding(Building._buildings_["Research_Labs"]);
//     }
// }
