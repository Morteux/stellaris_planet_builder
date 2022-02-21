using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Job
{
        // Constructor for invalid job
        public Job(string n)
        {
            name_ = n;
        }

        public Job(string n, Data.Stratums s, Dictionary<Data.Effects, int> e, Dictionary<Data.Resource, int> p, Dictionary<Data.Resource, int> u, int dp)
        {
            name_ = n;
            stratum_ = s;
            effects_ = e;
            produces_ = p;
            upkeep_ = u;
            defaultPriority_ = dp;
        }

        public string name_;
        public Data.Stratums stratum_;
        public Dictionary<Data.Effects, int> effects_;
        public Dictionary<Data.Resource, int> produces_;
        public Dictionary<Data.Resource, int> upkeep_;
        public int defaultPriority_;

        public static Dictionary<string, Job> _jobs_;
}
