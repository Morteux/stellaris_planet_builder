using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet
{
        // Constructor for invalid planet
        public Planet(string n)
        {
            name_ = n;
        }

        public Planet(string n, Dictionary<Data.Effects, int> e, Dictionary<Job, int> j)
        {
            name_ = n;
            effects_ = e;
            jobs_ = j;
        }

        public string name_;
        public Dictionary<Data.Effects, int> effects_;
        public Dictionary<Job, int> jobs_;

        public static Dictionary<string, Planet> _planets_;
}
