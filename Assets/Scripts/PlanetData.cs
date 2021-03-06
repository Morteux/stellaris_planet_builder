using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetData : MonoBehaviour
{
    [System.NonSerialized] public Building[] buildings_;
    [System.NonSerialized] public int districtCounter_;
    [System.NonSerialized] public Dictionary<string, int> district_;
    [System.NonSerialized] public Dictionary<Job, int> jobs_;
    [System.NonSerialized] public Dictionary<Job, int> customizeJobs_;
    [System.NonSerialized] public Dictionary<Data.Resource, int> costResources_;
    [System.NonSerialized] public Dictionary<Data.Resource, int> outcomeResources_;
    [System.NonSerialized] public Dictionary<Data.Resource, int> upkeepResources_;
    [System.NonSerialized] public Dictionary<Data.Resource, int> outputResources_;
    [System.NonSerialized] public Dictionary<Data.Resource, int> customizeResources_;
    [System.NonSerialized] public Dictionary<Data.Resource, float> percentageResources_;
    [System.NonSerialized] public Dictionary<Data.Effects, int> effects_;
    [System.NonSerialized] public Dictionary<Data.Effects, int> planetTypeEffects_;
    [System.NonSerialized] public int planetSize_;
    [System.NonSerialized] public int jobCount_;
    [System.NonSerialized] public int timeCount_;
    [System.NonSerialized] public string planetType_;
    [System.NonSerialized] public string government_;
    [System.NonSerialized] public HashSet<string> planetTypeRequirement_;
    [System.NonSerialized] public HashSet<string> governmentRequirement_;
    [System.NonSerialized] public HashSet<string> requirements_;
    [System.NonSerialized] public Transform planetName_;
    [System.NonSerialized] public Transform planetNameInputField_;
    [System.NonSerialized] public bool isInitialized_;
    private OutputManager outputManager_;

    private void Awake() {
        isInitialized_ = false;
        
        outputManager_ = transform.GetComponentInChildren<OutputManager>();

        districtCounter_ = 0;
        district_ = new Dictionary<string, int>();

        jobs_ = new Dictionary<Job, int>();
        customizeJobs_ = new Dictionary<Job, int>();
        costResources_ = new Dictionary<Data.Resource, int>();
        outcomeResources_ = new Dictionary<Data.Resource, int>();
        upkeepResources_ = new Dictionary<Data.Resource, int>();
        outputResources_ = new Dictionary<Data.Resource, int>();
        customizeResources_ = new Dictionary<Data.Resource, int>();
        percentageResources_ = new Dictionary<Data.Resource, float>();
        effects_ = new Dictionary<Data.Effects, int>();
        planetTypeEffects_ = new Dictionary<Data.Effects, int>();

        jobCount_ = 0;
        timeCount_ = 0;

        planetType_ = "Continental";
        government_ = "";
        planetTypeRequirement_ = new HashSet<string>();
        governmentRequirement_ = new HashSet<string>();
        requirements_ = new HashSet<string>();
    }

    void Start()
    {
        buildings_ = new Building[transform.Find("Panel/BuildingsPanel").GetComponent<SlotsManager>().slots_.transform.childCount];

        planetName_ = transform.Find("PlanetName");
        planetNameInputField_ = transform.Find("Panel/PlanetNameInputField");
        planetNameInputField_.GetComponent<InputField>().onValueChanged.AddListener(delegate { ChangePlanetName(); });
        
        isInitialized_ = true;
    }

    public void UpdatePlanetData()
    {
        outputManager_.ResetResources();

        jobs_ = new Dictionary<Job, int>();
        costResources_ = new Dictionary<Data.Resource, int>();
        outcomeResources_ = new Dictionary<Data.Resource, int>();
        upkeepResources_ = new Dictionary<Data.Resource, int>();
        outputResources_ = new Dictionary<Data.Resource, int>();
        effects_ = new Dictionary<Data.Effects, int>();

        jobCount_ = 0;
        timeCount_ = 0;

        // Calculate planetary job
        foreach (KeyValuePair<Job, int> job in Planet._planets_[planetType_].jobs_)
        {
            if (job.Value != 0)
            {
                if (jobs_.ContainsKey(job.Key))
                    jobs_[job.Key] += job.Value;
                else
                    jobs_.Add(job.Key, job.Value);
            }
        }

        // Add customize job
        foreach (KeyValuePair<Job, int> job in customizeJobs_)
        {
            if (job.Value != 0)
            {
                if (jobs_.ContainsKey(job.Key))
                    jobs_[job.Key] += job.Value;
                else
                    jobs_.Add(job.Key, job.Value);
            }
        }

        // Calculate district outcome
        foreach (KeyValuePair<string, int> district in district_)
        {
            foreach (KeyValuePair<Data.Resource, int> pair in District._districts_[district.Key].production_)
                if (outcomeResources_.ContainsKey(pair.Key))
                    outcomeResources_[pair.Key] += pair.Value * district.Value;
                else
                    outcomeResources_.Add(pair.Key, pair.Value * district.Value);

            foreach (KeyValuePair<Data.Resource, int> pair in District._districts_[district.Key].upkeep_)
                if (upkeepResources_.ContainsKey(pair.Key))
                    upkeepResources_[pair.Key] += pair.Value * district.Value;
                else
                    upkeepResources_.Add(pair.Key, pair.Value * district.Value);

            foreach (KeyValuePair<Job, int> pair in District._districts_[district.Key].jobs_)
                if (jobs_.ContainsKey(pair.Key))
                    jobs_[pair.Key] += pair.Value * district.Value;
                else
                    jobs_.Add(pair.Key, pair.Value * district.Value);

            foreach (KeyValuePair<Data.Resource, int> pair in District._districts_[district.Key].cost_)
                if (costResources_.ContainsKey(pair.Key))
                    costResources_[pair.Key] += pair.Value * district.Value;
                else
                    costResources_.Add(pair.Key, pair.Value * district.Value);

            timeCount_ += District._districts_[district.Key].time_ * district.Value;
        }

        // Calculate building outcome
        foreach (Building building in buildings_)
        {
            if (building != null)
            {
                // Reinitialize all data
                foreach (KeyValuePair<Data.Resource, int> pair in building.produces_)
                    if (outcomeResources_.ContainsKey(pair.Key))
                        outcomeResources_[pair.Key] += pair.Value;
                    else
                        outcomeResources_.Add(pair.Key, pair.Value);

                foreach (KeyValuePair<Data.Resource, int> pair in building.upkeep_)
                    if (upkeepResources_.ContainsKey(pair.Key))
                        upkeepResources_[pair.Key] += pair.Value;
                    else
                        upkeepResources_.Add(pair.Key, pair.Value);

                foreach (KeyValuePair<Job, int> pair in building.jobs_)
                    if (jobs_.ContainsKey(pair.Key))
                        jobs_[pair.Key] += pair.Value;
                    else
                        jobs_.Add(pair.Key, pair.Value);

                foreach (KeyValuePair<Data.Effects, int> pair in building.effects_)
                {
                    // Debug.Log(pair);

                    if (effects_.ContainsKey(pair.Key))
                        effects_[pair.Key] += pair.Value;
                    else
                        effects_.Add(pair.Key, pair.Value);
                }

                // Calculate cost for this building and all its downgraded version
                Building downgraded = building;
                bool isDowngradable = true;

                while (isDowngradable)
                {
                    // Debug.Log(downgraded.name_);

                    foreach (KeyValuePair<Data.Resource, int> pair in downgraded.cost_)
                        if (costResources_.ContainsKey(pair.Key))
                            costResources_[pair.Key] += pair.Value;
                        else
                            costResources_.Add(pair.Key, pair.Value);

                    timeCount_ += downgraded.time_;

                    isDowngradable = Building._buildings_.TryGetValue(downgraded.downgrade_, out downgraded);
                }
            }
        }

        // Calculate final outcome
        foreach (KeyValuePair<Job, int> pair in jobs_)
        {
            jobCount_ += pair.Value; // Count all jobs in the planet

            foreach (KeyValuePair<Data.Resource, int> pair2 in pair.Key.produces_)
                if (outcomeResources_.ContainsKey(pair2.Key))
                    outcomeResources_[pair2.Key] += pair2.Value * pair.Value;
                else
                    outcomeResources_.Add(pair2.Key, pair2.Value * pair.Value);
        }

        // Add customize resources
        foreach (KeyValuePair<Data.Resource, int> pair in customizeResources_)
        {
            if (pair.Value != 0)
                if (outcomeResources_.ContainsKey(pair.Key))
                    outcomeResources_[pair.Key] += pair.Value;
                else
                    outcomeResources_.Add(pair.Key, pair.Value);
        }

        // Calculate percentage resources
        foreach (KeyValuePair<Data.Resource, float> pair in percentageResources_)
            if (pair.Value != 0 && outcomeResources_.ContainsKey(pair.Key))
                outcomeResources_[pair.Key] += (int)(outcomeResources_[pair.Key] * pair.Value);

        // Jobs count == housing for working pop
        if (upkeepResources_.ContainsKey(Data.Resource.Housing))
            upkeepResources_[Data.Resource.Housing] = -1 * jobCount_;
        else
            upkeepResources_.Add(Data.Resource.Housing, -1 * jobCount_);

        // Calculate final output
        foreach (KeyValuePair<Data.Resource, int> pair in outcomeResources_)
            if (outputResources_.ContainsKey(pair.Key))
                outputResources_[pair.Key] += pair.Value;
            else
                outputResources_.Add(pair.Key, pair.Value);

        foreach (KeyValuePair<Data.Resource, int> pair in upkeepResources_)
            if (outputResources_.ContainsKey(pair.Key))
                outputResources_[pair.Key] += pair.Value;
            else
                outputResources_.Add(pair.Key, pair.Value);

        // Print final resources
        foreach (KeyValuePair<Data.Resource, int> pair in outcomeResources_)
            if (pair.Value != 0)
                outputManager_.AddOutcomeResource(pair);

        foreach (KeyValuePair<Data.Resource, int> pair in upkeepResources_)
            if (pair.Value != 0)
                outputManager_.AddUpkeepResource(pair);

        foreach (KeyValuePair<Data.Resource, int> pair in outputResources_)
            if (pair.Value != 0)
                outputManager_.AddOutputResource(pair);
    }

    void ChangePlanetName()
    {
        planetName_.GetComponent<Text>().text = planetNameInputField_.GetComponent<InputField>().text;
    }
}
