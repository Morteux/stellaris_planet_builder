using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GovernmentButtonManager : MonoBehaviour
{
    private PlanetData planetData;
    private Transform GovernmentPanel;

    // Button references
    private Transform DemocraticButton;
    private Transform OligarchicButton;
    private Transform DictatorialButton;
    private Transform ImperialButton;
    private Transform HiveMindButton;
    private Transform MachineIntelligenceButton;
    private Transform CorporateButton;

    // SelectedFrame references
    private Transform DemocraticFrame;
    private Transform OligarchicFrame;
    private Transform DictatorialFrame;
    private Transform ImperialFrame;
    private Transform HiveMindFrame;
    private Transform MachineIntelligenceFrame;
    private Transform CorporateFrame;

    // Requirements for each government
    private HashSet<string> DemocraticRequirement;
    private HashSet<string> OligarchicRequirement;
    private HashSet<string> DictatorialRequirement;
    private HashSet<string> ImperialRequirement;
    private HashSet<string> HiveMindRequirement;
    private HashSet<string> MachineIntelligenceRequirement;
    private HashSet<string> CorporateRequirement;

    // Capital for each government
    private string DemocraticCapitalBuilding;
    private string OligarchicCapitalBuilding;
    private string DictatorialCapitalBuilding;
    private string ImperialCapitalBuilding;
    private string HiveMindCapitalBuilding;
    private string MachineIntelligenceCapitalBuilding;
    private string CorporateCapitalBuilding;

    private void Awake()
    {
        GovernmentPanel = transform.parent.parent.parent.parent.Find("ButtonPanels/GovernmentPanel");
        GovernmentPanel.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        planetData = transform.parent.parent.parent.parent.parent.GetComponent<PlanetData>();

        transform.GetComponent<Button>().onClick.AddListener(ShowGovernmentPanel);
        GovernmentPanel.Find("Close").GetComponent<Button>().onClick.AddListener(ShowGovernmentPanel);

        // Get buttons
        DemocraticButton = GovernmentPanel.Find("DemocraticButton");
        OligarchicButton = GovernmentPanel.Find("OligarchicButton");
        DictatorialButton = GovernmentPanel.Find("DictatorialButton");
        ImperialButton = GovernmentPanel.Find("ImperialButton");
        HiveMindButton = GovernmentPanel.Find("HiveMindButton");
        MachineIntelligenceButton = GovernmentPanel.Find("MachineIntelligenceButton");
        CorporateButton = GovernmentPanel.Find("CorporateButton");

        // Add listeners to buttons
        DemocraticButton.GetComponent<Button>().onClick.AddListener(delegate { SelectGovernment("Democratic", DemocraticRequirement, DemocraticFrame, DemocraticCapitalBuilding); });
        OligarchicButton.GetComponent<Button>().onClick.AddListener(delegate { SelectGovernment("Oligarchic", OligarchicRequirement, OligarchicFrame, OligarchicCapitalBuilding); });
        DictatorialButton.GetComponent<Button>().onClick.AddListener(delegate { SelectGovernment("Dictatorial", DictatorialRequirement, DictatorialFrame, DictatorialCapitalBuilding); });
        ImperialButton.GetComponent<Button>().onClick.AddListener(delegate { SelectGovernment("Imperial", ImperialRequirement, ImperialFrame, ImperialCapitalBuilding); });
        HiveMindButton.GetComponent<Button>().onClick.AddListener(delegate { SelectGovernment("Hive_Mind", HiveMindRequirement, HiveMindFrame, HiveMindCapitalBuilding); });
        MachineIntelligenceButton.GetComponent<Button>().onClick.AddListener(delegate { SelectGovernment("Machine_Intelligence", MachineIntelligenceRequirement, MachineIntelligenceFrame, MachineIntelligenceCapitalBuilding); });
        CorporateButton.GetComponent<Button>().onClick.AddListener(delegate { SelectGovernment("Corporate", CorporateRequirement, CorporateFrame, CorporateCapitalBuilding); });

        // Get SelectedFrame for each button
        DemocraticFrame = DemocraticButton.Find("SelectedFrame");
        OligarchicFrame = OligarchicButton.Find("SelectedFrame");
        DictatorialFrame = DictatorialButton.Find("SelectedFrame");
        ImperialFrame = ImperialButton.Find("SelectedFrame");
        HiveMindFrame = HiveMindButton.Find("SelectedFrame");
        MachineIntelligenceFrame = MachineIntelligenceButton.Find("SelectedFrame");
        CorporateFrame = CorporateButton.Find("SelectedFrame");

        // Hide all SelectedFrame
        DemocraticFrame.gameObject.SetActive(false);
        OligarchicFrame.gameObject.SetActive(false);
        DictatorialFrame.gameObject.SetActive(false);
        ImperialFrame.gameObject.SetActive(false);
        HiveMindFrame.gameObject.SetActive(false);
        MachineIntelligenceFrame.gameObject.SetActive(false);
        CorporateFrame.gameObject.SetActive(false);

        // Set requirements for each government
        DemocraticRequirement = new HashSet<string>();
        OligarchicRequirement = new HashSet<string>();
        DictatorialRequirement = new HashSet<string>();
        ImperialRequirement = new HashSet<string>();
        HiveMindRequirement = new HashSet<string>();
        MachineIntelligenceRequirement = new HashSet<string>();
        CorporateRequirement = new HashSet<string>();

        DemocraticRequirement.Add("{+\"Democratic\"}");
        DemocraticRequirement.Add("{-\"Authoritarian\"}");
        DemocraticRequirement.Add("{-\"Fanatic_Authoritarian\"}");
        DemocraticRequirement.Add("{-\"Gestalt_Consciousness\"}");
        DemocraticRequirement.Add("{-\"Hive_Mind\"}");
        DemocraticRequirement.Add("{-\"Machine_Intelligence\"}");
        DemocraticRequirement.Add("{-\"Rogue_Servitor\"}");

        OligarchicRequirement.Add("{+\"Oligarchic\"}");
        OligarchicRequirement.Add("{-\"Fanatic_Authoritarian\"}");
        OligarchicRequirement.Add("{-\"Fanatic_Egalitarian\"}");
        OligarchicRequirement.Add("{-\"Gestalt_Consciousness\"}");
        OligarchicRequirement.Add("{-\"Hive_Mind\"}");
        OligarchicRequirement.Add("{-\"Machine_Intelligence\"}");
        OligarchicRequirement.Add("{-\"Rogue_Servitor\"}");

        DictatorialRequirement.Add("{+\"Dictatorial\"}");
        DictatorialRequirement.Add("{-\"Egalitarian\"}");
        DictatorialRequirement.Add("{-\"Fanatic_Egalitarian\"}");
        DictatorialRequirement.Add("{-\"Gestalt_Consciousness\"}");
        DictatorialRequirement.Add("{-\"Hive_Mind\"}");
        DictatorialRequirement.Add("{-\"Machine_Intelligence\"}");
        DictatorialRequirement.Add("{-\"Rogue_Servitor\"}");

        ImperialRequirement.Add("{+\" Imperial\"}");
        ImperialRequirement.Add("{-\"Egalitarian\"}");
        ImperialRequirement.Add("{-\"Fanatic_Egalitarian\"}");
        ImperialRequirement.Add("{-\"Gestalt_Consciousness\"}");
        ImperialRequirement.Add("{-\"Hive_Mind\"}");
        ImperialRequirement.Add("{-\"Machine_Intelligence\"}");
        ImperialRequirement.Add("{-\"Rogue_Servitor\"}");

        HiveMindRequirement.Add("{+\"Gestalt_Consciousness\"}");
        HiveMindRequirement.Add("{+\"Hive_Mind\"}");
        HiveMindRequirement.Add("{-\"Machine_Intelligence\"}");
        HiveMindRequirement.Add("{-\"Democratic\"}");
        HiveMindRequirement.Add("{-\"Oligarchic\"}");
        HiveMindRequirement.Add("{-\"Dictatorial\"}");
        HiveMindRequirement.Add("{-\"Imperial\"}");
        HiveMindRequirement.Add("{-\"Corporate\"}");
        HiveMindRequirement.Add("{-\"Spiritualist\"}");
        HiveMindRequirement.Add("{-\"Rogue_Servitor\"}");

        MachineIntelligenceRequirement.Add("{+\"Gestalt_Consciousness\"}");
        MachineIntelligenceRequirement.Add("{+\"Machine_Intelligence\"}");
        MachineIntelligenceRequirement.Add("{-\"Hive_Mind\"}");
        MachineIntelligenceRequirement.Add("{-\"Democratic\"}");
        MachineIntelligenceRequirement.Add("{-\"Oligarchic\"}");
        MachineIntelligenceRequirement.Add("{-\"Dictatorial\"}");
        MachineIntelligenceRequirement.Add("{-\"Imperial\"}");
        MachineIntelligenceRequirement.Add("{-\"Corporate\"}");
        MachineIntelligenceRequirement.Add("{-\"Spiritualist\"}");

        CorporateRequirement.Add("{+\"Corporate\"}");
        CorporateRequirement.Add("{-\"Fanatic_Authoritarian\"}");
        CorporateRequirement.Add("{-\"Fanatic_Egalitarian\"}");
        CorporateRequirement.Add("{-\"Gestalt_Consciousness\"}");
        CorporateRequirement.Add("{-\"Hive_Mind\"}");
        CorporateRequirement.Add("{-\"Machine_Intelligence\"}");
        CorporateRequirement.Add("{-\"Rogue_Servitor\"}");

        // Set capital building for each government
        DemocraticCapitalBuilding = "Reassembled_Ship_Shelter";
        OligarchicCapitalBuilding = "Reassembled_Ship_Shelter";
        DictatorialCapitalBuilding = "Reassembled_Ship_Shelter";
        ImperialCapitalBuilding = "Reassembled_Ship_Shelter";
        HiveMindCapitalBuilding = "Hive_Core";
        MachineIntelligenceCapitalBuilding = "Deployment_Post";
        CorporateCapitalBuilding = "Reassembled_Ship_Shelter";
    }

    void ShowGovernmentPanel()
    {
        // Debug.Log("ShowGovernmentPanel");
        GovernmentPanel.gameObject.SetActive(!GovernmentPanel.gameObject.activeSelf);
    }

    void SelectGovernment(string government, HashSet<string> requirements, Transform governmentButton, string capitalBuilding)
    {
        // Debug.Log(government);
        // Debug.Log("SelectGovernment::capitalBuilding: " + capitalBuilding);

        if (governmentButton.gameObject.activeSelf)
        {
            // Hide current selected frame
            governmentButton.gameObject.SetActive(false);

            // Set all government attributes by default
            planetData.government_ = "";
            planetData.governmentRequirement_ = new HashSet<string>();
            transform.parent.parent.parent.parent.GetComponentInChildren<SlotsManager>().ChangeCapitalBuilding(Building._buildings_["Reassembled_Ship_Shelter"]);
        }
        else
        {
            // Hide all frames
            DemocraticFrame.gameObject.SetActive(false);
            OligarchicFrame.gameObject.SetActive(false);
            DictatorialFrame.gameObject.SetActive(false);
            ImperialFrame.gameObject.SetActive(false);
            HiveMindFrame.gameObject.SetActive(false);
            MachineIntelligenceFrame.gameObject.SetActive(false);
            CorporateFrame.gameObject.SetActive(false);

            // Show current selected frame
            governmentButton.gameObject.SetActive(true);

            planetData.government_ = government;

            planetData.governmentRequirement_ = requirements;

            transform.parent.parent.parent.parent.GetComponentInChildren<SlotsManager>().ChangeCapitalBuilding(Building._buildings_[capitalBuilding]);
        }

        planetData.UpdatePlanetData();
        transform.parent.GetComponentInChildren<RequirementButtonManager>().UpdateRequirements();
        planetData.transform.GetComponentInChildren<DistrictManager>().UpdateDistrictType();
        transform.parent.GetComponentInChildren<JobsButtonManager>().UpdateJobs();
        transform.parent.GetComponentInChildren<EffectButtonManager>().UpdateEffects();
    }
}
