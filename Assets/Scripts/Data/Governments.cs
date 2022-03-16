using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Government
{
        public Government(string n, string c, string[] r)
        {
            name_ = n;
            capitalBuilding_ = c;
            requirements_ = r;
        }

        public string name_;
        public string capitalBuilding_;
        public string[] requirements_;

        public static Dictionary<string, Government> _governments_;
}
