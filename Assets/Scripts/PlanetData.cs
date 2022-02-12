using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetData : MonoBehaviour
{
    [System.NonSerialized] public Building[] buildings_;
    [System.NonSerialized] public int districtCounter_;
    [System.NonSerialized] public Dictionary<string, int> district_;
    [System.NonSerialized] public Dictionary<Job, int> jobs_;
    [System.NonSerialized] public Dictionary<Data.Resource, int> costResources_;
    [System.NonSerialized] public Dictionary<Data.Resource, int> outcomeResources_;
    [System.NonSerialized] public Dictionary<Data.Resource, int> upkeepResources_;
    [System.NonSerialized] public Dictionary<Data.Resource, int> outputResources_;
    [System.NonSerialized] public Dictionary<Data.Effects, int> effects_;
    [System.NonSerialized] public int planetSize_;
    [System.NonSerialized] public int jobCount;
    [System.NonSerialized] public int timeCount;
    [System.NonSerialized] public string government_;
    [System.NonSerialized] public List<string> governmentRequirement_;

    private OutputManager outputManager;

    // Start is called before the first frame update
    void Start()
    {
        outputManager = transform.GetComponentInChildren<OutputManager>();

        buildings_ = new Building[transform.Find("Panel/BuildingsPanel").GetComponent<SlotsManager>().Slots.transform.childCount];

        districtCounter_ = 0;
        district_ = new Dictionary<string, int>();

        jobs_ = new Dictionary<Job, int>();
        costResources_ = new Dictionary<Data.Resource, int>();
        outcomeResources_ = new Dictionary<Data.Resource, int>();
        upkeepResources_ = new Dictionary<Data.Resource, int>();
        outputResources_ = new Dictionary<Data.Resource, int>();
        effects_ = new Dictionary<Data.Effects, int>();

        jobCount = 0;
        timeCount = 0;

        government_ = "";
        governmentRequirement_ = new List<string>();
    }

    public void UpdatePlanetData()
    {
        outputManager.ResetResources();

        jobs_ = new Dictionary<Job, int>();
        costResources_ = new Dictionary<Data.Resource, int>();
        outcomeResources_ = new Dictionary<Data.Resource, int>();
        upkeepResources_ = new Dictionary<Data.Resource, int>();
        outputResources_ = new Dictionary<Data.Resource, int>();
        effects_ = new Dictionary<Data.Effects, int>();

        jobCount = 0;
        timeCount = 0;

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

            // Debug.Log(district);

            foreach (KeyValuePair<Data.Resource, int> pair in District._districts_[district.Key].cost_)
                if (costResources_.ContainsKey(pair.Key))
                    costResources_[pair.Key] += pair.Value * district.Value;
                else
                    costResources_.Add(pair.Key, pair.Value * district.Value);

            timeCount += District._districts_[district.Key].time_ * district.Value;
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

                    timeCount += downgraded.time_;

                    isDowngradable = Building._buildings_.TryGetValue(downgraded.downgrade_, out downgraded);
                }
            }
        }

        // Calculate final output
        foreach (KeyValuePair<Job, int> pair in jobs_)
        {
            jobCount += pair.Value; // Count all jobs in the planet

            foreach (KeyValuePair<Data.Resource, int> pair2 in pair.Key.produces_)
                if (outcomeResources_.ContainsKey(pair2.Key))
                    outcomeResources_[pair2.Key] += pair2.Value * pair.Value;
                else
                    outcomeResources_.Add(pair2.Key, pair2.Value * pair.Value);
        }

        // Jobs count == housing for working pop
        if (upkeepResources_.ContainsKey(Data.Resource.Housing))
            upkeepResources_[Data.Resource.Housing] = -1 * jobCount;
        else
            upkeepResources_.Add(Data.Resource.Housing, -1 * jobCount);

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
                outputManager.AddOutcomeResource(pair);

        foreach (KeyValuePair<Data.Resource, int> pair in upkeepResources_)
            if (pair.Value != 0)
                outputManager.AddUpkeepResource(pair);

        foreach (KeyValuePair<Data.Resource, int> pair in outputResources_)
            if (pair.Value != 0)
                outputManager.AddOutputResource(pair);
    }
}
