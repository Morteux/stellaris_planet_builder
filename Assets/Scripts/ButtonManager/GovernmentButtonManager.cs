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
        DemocraticRequirement = new HashSet<string>();
        OligarchicRequirement = new HashSet<string>();
        DictatorialRequirement = new HashSet<string>();
        ImperialRequirement = new HashSet<string>();
        HiveMindRequirement = new HashSet<string>();
        MachineIntelligenceRequirement = new HashSet<string>();
        CorporateRequirement = new HashSet<string>();

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

    void SelectGovernment(string government, HashSet<string> requirements, Transform governmentButton)
    {
        // Debug.Log(government);

        if (governmentButton.gameObject.activeSelf)
        {
            governmentButton.gameObject.SetActive(false);

            planetData.government_ = "";
            planetData.governmentRequirement_ = new HashSet<string>();
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

            // Add government requirement in all planetary requirements set
            foreach(string requirement in requirements)
                planetData.governmentRequirement_.Add(requirement);
        }

        transform.parent.GetComponentInChildren<RequirementButtonManager>().UpdateRequirements();
    }
}
