using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building
{
        public Building(string n, Dictionary<Data.Effects, int> e, Dictionary<Data.Resource, int> p, Dictionary<Job, int> j, Dictionary<Data.Resource, int> u, int t, Dictionary<Data.Resource, int> c, string up, string d, string b, string un, string[] r, string desc)
        {
            name_ = n;
            effects_ = e;
            produces_ = p;
            jobs_ = j;
            upkeep_ = u;
            time_ = t;
            cost_ = c;
            upgrade_ = up;
            downgrade_ = d;
            buildable_ = b;
            unique_ = un;
            requirements_ = r;

            description_ = desc;
        }

        public string name_;
        public Dictionary<Data.Effects, int> effects_;
        public Dictionary<Data.Resource, int> produces_;
        public Dictionary<Job, int> jobs_;
        public Dictionary<Data.Resource, int> upkeep_;
        public int time_;
        public Dictionary<Data.Resource, int> cost_;
        public string upgrade_;
        public string downgrade_;
        public string buildable_;
        public string unique_;
        public string[] requirements_;

        public string description_;

        public static Dictionary<string, Building> _buildings_;
}
