using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class District
{
        // Constructor for invalid disctrict
        public District(string n)
        {
            name_ = n;
        }

        public District(string n, Dictionary<Data.Resource, int> p, Dictionary<Job, int> j, Dictionary<Data.Resource, int> u, int t, Dictionary<Data.Resource, int> c, string[] r, string co)
        {
            name_ = n;
            production_ = p;
            jobs_ = j;
            upkeep_ = u;
            time_ = t;
            cost_ = c;
            requirements_ = r;
            color_ = co;
        }

        public string name_;
        public Dictionary<Data.Resource, int> production_;
        public Dictionary<Job, int> jobs_;
        public Dictionary<Data.Resource, int> upkeep_;
        public int time_;
        public Dictionary<Data.Resource, int> cost_;
        public string[] requirements_;
        public string color_;

        public static Dictionary<string, District> _districts_;
}
