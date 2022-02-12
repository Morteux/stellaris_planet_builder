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
    private List<string> DemocraticRequirement;
    private List<string> OligarchicRequirement;
    private List<string> DictatorialRequirement;
    private List<string> ImperialRequirement;
    private List<string> HiveMindRequirement;
    private List<string> MachineIntelligenceRequirement;
    private List<string> CorporateRequirement;

    private void Awake()
    {
        GovernmentPanel = transform.parent.Find("GovernmentPanel");
        GovernmentPanel.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        planetData = transform.parent.parent.GetComponent<PlanetData>();

        transform.GetComponent<Button>().onClick.AddListener(ShowGovernmentPanel);

        // Get buttons
        DemocraticButton = GovernmentPanel.Find("DemocraticButton");
        OligarchicButton = GovernmentPanel.Find("OligarchicButton");
        DictatorialButton = GovernmentPanel.Find("DictatorialButton");
        ImperialButton = GovernmentPanel.Find("ImperialButton");
        HiveMindButton = GovernmentPanel.Find("HiveMindButton");
        MachineIntelligenceButton = GovernmentPanel.Find("MachineIntelligenceButton");
        CorporateButton = GovernmentPanel.Find("CorporateButton");

        // Add listeners to buttons
        DemocraticButton.GetComponent<Button>().onClick.AddListener(delegate { SelectGovernment("Democratic", DemocraticRequirement, DemocraticFrame); });
        OligarchicButton.GetComponent<Button>().onClick.AddListener(delegate { SelectGovernment("Oligarchic", OligarchicRequirement, OligarchicFrame); });
        DictatorialButton.GetComponent<Button>().onClick.AddListener(delegate { SelectGovernment("Dictatorial", DictatorialRequirement, DictatorialFrame); });
        ImperialButton.GetComponent<Button>().onClick.AddListener(delegate { SelectGovernment("Imperial", ImperialRequirement, ImperialFrame); });
        HiveMindButton.GetComponent<Button>().onClick.AddListener(delegate { SelectGovernment("Hive_Mind", HiveMindRequirement, HiveMindFrame); });
        MachineIntelligenceButton.GetComponent<Button>().onClick.AddListener(delegate { SelectGovernment("Machine_Intelligence", MachineIntelligenceRequirement, MachineIntelligenceFrame); });
        CorporateButton.GetComponent<Button>().onClick.AddListener(delegate { SelectGovernment("Corporate", CorporateRequirement, CorporateFrame); });

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
        DemocraticRequirement = new List<string>();
        OligarchicRequirement = new List<string>();
        DictatorialRequirement = new List<string>();
        ImperialRequirement = new List<string>();
        HiveMindRequirement = new List<string>();
        MachineIntelligenceRequirement = new List<string>();
        CorporateRequirement = new List<string>();

        DemocraticRequirement.Add("{-\"Authoritarian\"}");
        DemocraticRequirement.Add("{-\"Fanatic_Authoritarian\"}");
        DemocraticRequirement.Add("{-\"Gestalt_Consciousness\"}");

        OligarchicRequirement.Add("{-\"Fanatic_Authoritarian\"}");
        OligarchicRequirement.Add("{-\"Fanatic_Egalitarian\"}");
        OligarchicRequirement.Add("{-\"Gestalt_Consciousness\"}");

        DictatorialRequirement.Add("{-\"Egalitarian\"}");
        DictatorialRequirement.Add("{-\"Fanatic_Egalitarian\"}");
        DictatorialRequirement.Add("{-\"Gestalt_Consciousness\"}");

        ImperialRequirement.Add("{-\"Egalitarian\"}");
        ImperialRequirement.Add("{-\"Fanatic_Egalitarian\"}");
        ImperialRequirement.Add("{-\"Gestalt_Consciousness\"}");

        HiveMindRequirement.Add("{+\"Gestalt_Consciousness\"}");

        MachineIntelligenceRequirement.Add("{+\"Gestalt_Consciousness\"}");

        CorporateRequirement.Add("{-\"Fanatic_Authoritarian\"}");
        CorporateRequirement.Add("{-\"Fanatic_Egalitarian\"}");
        CorporateRequirement.Add("{-\"Gestalt_Consciousness\"}");
    }

    void ShowGovernmentPanel()
    {
        // Debug.Log("ShowGovernmentPanel");
        GovernmentPanel.gameObject.SetActive(!GovernmentPanel.gameObject.activeSelf);
    }

    void SelectGovernment(string government, List<string> requirements, Transform governmentButton)
    {
        // Debug.Log(government);

        if (governmentButton.gameObject.activeSelf)
        {
            governmentButton.gameObject.SetActive(false);

            planetData.government_ = "";
            planetData.governmentRequirement_ = new List<string>();
        }
        else
        {
            DemocraticFrame.gameObject.SetActive(false);
            OligarchicFrame.gameObject.SetActive(false);
            DictatorialFrame.gameObject.SetActive(false);
            ImperialFrame.gameObject.SetActive(false);
            HiveMindFrame.gameObject.SetActive(false);
            MachineIntelligenceFrame.gameObject.SetActive(false);
            CorporateFrame.gameObject.SetActive(false);

            governmentButton.gameObject.SetActive(true);

            planetData.government_ = government;

            planetData.governmentRequirement_ = requirements;
        }

        transform.parent.GetComponentInChildren<RequirementButtonManager>().UpdateRequirements();
    }
}
