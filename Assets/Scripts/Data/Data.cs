using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class Data {
    static public string _version_ = "3.2.1";

    /////////////////////////////////////////////////////////////////////////////////////////////////////
    // Resource
    /////////////////////////////////////////////////////////////////////////////////////////////////////

    public enum Resource { INVALID, NULL, Energy_Credits, Minerals, Food, Alloys, Consumer_Goods,
                Trade_Value, Influence, Unity,
                Research, Physics_Research, Society_Research, Engineering_Research,
                Exotic_Gases, Rare_Crystals, Volatile_Motes,
                Zro, Dark_Matter, Living_Metal, Nanites,
                Envoys, Storage_Capacity,
                Housing, Amenities, Stability, Crime, Happiness, Building_Slot,
                Administrative_Capacity, Defense_Army, Naval_Capacity, Army_Experience, Deviancy
    };

    public static Resource String_to_Resource(string r)
    {
        switch(r)
        {
            case "NULL":                    return Resource.NULL;
            case "Energy_Credits":          return Resource.Energy_Credits;
            case "Minerals":                return Resource.Minerals;
            case "Food":                    return Resource.Food;
            case "Alloys":                  return Resource.Alloys;
            case "Consumer_Goods":          return Resource.Consumer_Goods;
            case "Trade_Value":             return Resource.Trade_Value;
            case "Influence":               return Resource.Influence;
            case "Unity":                   return Resource.Unity;
            case "Research":                return Resource.Research;
            case "Physics_Research":        return Resource.Physics_Research;
            case "Society_Research":        return Resource.Society_Research;
            case "Engineering_Research":    return Resource.Engineering_Research;
            case "Exotic_Gases":            return Resource.Exotic_Gases;
            case "Rare_Crystals":           return Resource.Rare_Crystals;
            case "Volatile_Motes":          return Resource.Volatile_Motes;
            case "Zro":                     return Resource.Zro;
            case "Dark_Matter":             return Resource.Dark_Matter;
            case "Living_Metal":            return Resource.Living_Metal;
            case "Nanites":                 return Resource.Nanites;
            case "Envoys":                  return Resource.Envoys;
            case "Storage_Capacity":        return Resource.Storage_Capacity;
            case "Housing":                 return Resource.Housing;
            case "Amenities":               return Resource.Amenities;
            case "Stability":               return Resource.Stability;
            case "Crime":                   return Resource.Crime;
            case "Happiness":               return Resource.Happiness;
            case "Building_Slot":           return Resource.Building_Slot;
            case "Administrative_Capacity": return Resource.Administrative_Capacity;
            case "Defense_Army":            return Resource.Defense_Army;
            case "Naval_Capacity":          return Resource.Naval_Capacity;
            case "Army_Experience":         return Resource.Army_Experience;
            case "Deviancy":                return Resource.Deviancy;

            default: {Debug.Log("INVALID RESOURCE: " + r); return Resource.INVALID;}
        }
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////
    // Effects
    /////////////////////////////////////////////////////////////////////////////////////////////////////

    public enum Effects { INVALID, NULL, Jobs_Energy, Jobs_Minerals, Jobs_Food, Jobs_Alloy, 
                Jobs_Consumer_Goods, Jobs_Unity, Jobs_Trade_Value,
                Research_Output, Slave_Output, Technicians_Output, 
                Miners_Output, Farmers_Output, Worker_Output,
                Complex_Drone_Output, Menial_Drone_Output,
                Pop_Growth_Speed, Monthly_Pop_Assembly, 
                Bombardment_Damage, Slave_Political_Power, 
                Spiritualist_Ethics, Militarist_Ethics, Xenophobe_Ethics, 
                Authoritarian_Ethics, Egalitarian_Ethics, Xenophile_Ethics, 
                Pacifist_Ethics, Materialist_Ethics,
                Generator_District_Build_Speed, Mining_District_Build_Speed, City_District_Build_Speed,
                Research_Lab_District_Build_Speed, Foundry_District_Build_Speed,
                Civilian_Industries_District_Build_Speed, Agriculture_District_Build_Speed,
                Chemical_Plant_Build_Speed, Crystal_Plant_Build_Speed, Refinery_Build_Speed,
                Upkeep_from_Jobs, Minerals_upkeep_Metallurgists, Food_upkeep_Catalytic_Technicians, Minerals_upkeep_Artisans,
                Researcher_Upkeep, Metallurgist_Upkeep, Artisan_Upkeep, Chemist_Upkeep, Translucer_Upkeep, Gas_Refiner_Upkeep,
                Fortress_Cost, City_District_Cost, Hive_District_Cost, Nexus_District_Cost, Defense_Army_Damage,
                Artisan_Output, Unemployed_Slaves_automatically_resettle, Happiness_for_6_months, Immigration_Pull_for_6_months,
                Pop_Primary_Species, Organic_Pop_Assembly_Speed, Complex_Drone_Output_when_above_50_Happiness, Criminal_Branch_Office_Trade_Value,
                Resources_from_Jobs, Energy_from_Technicians, Minerals_from_Miners, Food_from_Farmers, Alloys_from_Metallurgists, Consumer_Goods_from_Artisans, 
                Culture_Workers_Output, Spiritualist_Ethics_attraction, Managers_Output, Evaluators_Output, Metallurgist_Output, Trade_Value, 
                Automatic_resettlement_chance, Monthly_Organic_Pop_Assembly, Envoy, Habitability, Planet_becomes_Gaia_World, FTL_Inhibitor, 
                Spiritualist_Ethics_Attraction, Unity_from_Jobs, Biological_Pop_Happiness, Lithoid_Pop_Happiness,
                Planet_Capacity_per_Unblocked_District_Slot, Housing_from_Hive_Districts, Pop_Housing_Usage, Colony_Development_Speed
    };

    public static Effects String_to_Effects(string e)
    {
        switch(e)
        {
            case "[]":                                          return Effects.NULL;
            case "Jobs_Energy":                                 return Effects.Jobs_Energy;
            case "Jobs_Minerals":                               return Effects.Jobs_Minerals;
            case "Jobs_Food":                                   return Effects.Jobs_Food;
            case "Jobs_Alloy":                                  return Effects.Jobs_Alloy;
            case "Jobs_Consumer_Goods":                         return Effects.Jobs_Consumer_Goods;
            case "Jobs_Unity":                                  return Effects.Jobs_Unity;
            case "Jobs_Trade_Value":                            return Effects.Jobs_Trade_Value;
            case "Research_Output":                             return Effects.Research_Output;
            case "Slave_Output":                                return Effects.Slave_Output;
            case "Technicians_Output":                          return Effects.Technicians_Output;
            case "Miners_Output":                               return Effects.Miners_Output;
            case "Farmers_Output":                              return Effects.Farmers_Output;
            case "Worker_Output":                               return Effects.Worker_Output;
            case "Complex_Drone_Output":                        return Effects.Complex_Drone_Output;
            case "Menial_Drone_Output":                         return Effects.Menial_Drone_Output;
            case "Pop_Growth_Speed":                            return Effects.Pop_Growth_Speed;
            case "Monthly_Pop_Assembly":                        return Effects.Monthly_Pop_Assembly;
            case "Bombardment_Damage":                          return Effects.Bombardment_Damage;
            case "Slave_Political_Power":                       return Effects.Slave_Political_Power;
            case "Spiritualist_Ethics":                         return Effects.Spiritualist_Ethics;
            case "Militarist_Ethics":                           return Effects.Militarist_Ethics;
            case "Xenophobe_Ethics":                            return Effects.Xenophobe_Ethics;
            case "Authoritarian_Ethics":                        return Effects.Authoritarian_Ethics;
            case "Egalitarian_Ethics":                          return Effects.Egalitarian_Ethics;
            case "Xenophile_Ethics":                            return Effects.Xenophile_Ethics;
            case "Pacifist_Ethics":                             return Effects.Pacifist_Ethics;
            case "Materialist_Ethics":                          return Effects.Materialist_Ethics;
            case "Generator_District_Build_Speed":              return Effects.Generator_District_Build_Speed;
            case "Mining_District_Build_Speed":                 return Effects.Mining_District_Build_Speed;
            case "City_District_Build_Speed":                   return Effects.City_District_Build_Speed;
            case "Research_Lab_District_Build_Speed":           return Effects.Research_Lab_District_Build_Speed;
            case "Foundry_District_Build_Speed":                return Effects.Foundry_District_Build_Speed;
            case "Civilian_Industries_District_Build_Speed":    return Effects.Civilian_Industries_District_Build_Speed;
            case "Agriculture_District_Build_Speed":            return Effects.Agriculture_District_Build_Speed;
            case "Chemical_Plant_Build_Speed":                  return Effects.Chemical_Plant_Build_Speed;
            case "Crystal_Plant_Build_Speed":                   return Effects.Crystal_Plant_Build_Speed;
            case "Refinery_Build_Speed":                        return Effects.Refinery_Build_Speed;
            case "Upkeep_from_Jobs":                            return Effects.Upkeep_from_Jobs;
            case "Minerals_upkeep_Metallurgists":               return Effects.Minerals_upkeep_Metallurgists;
            case "Food_upkeep_Catalytic_Technicians":           return Effects.Food_upkeep_Catalytic_Technicians;
            case "Minerals_upkeep_Artisans":                    return Effects.Minerals_upkeep_Artisans;
            case "Researcher_Upkeep":                           return Effects.Researcher_Upkeep;
            case "Metallurgist_Upkeep":                         return Effects.Metallurgist_Upkeep;
            case "Artisan_Upkeep":                              return Effects.Artisan_Upkeep;
            case "Chemist_Upkeep":                              return Effects.Chemist_Upkeep;
            case "Translucer_Upkeep":                           return Effects.Translucer_Upkeep;
            case "Gas_Refiner_Upkeep":                          return Effects.Gas_Refiner_Upkeep;
            case "Fortress_Cost":                               return Effects.Fortress_Cost;
            case "City_District_Cost":                          return Effects.City_District_Cost;
            case "Hive_District_Cost":                          return Effects.Hive_District_Cost;
            case "Nexus_District_Cost":                         return Effects.Nexus_District_Cost;
            case "Defense_Army_Damage":                         return Effects.Defense_Army_Damage;
            case "Artisan_Output":                              return Effects.Artisan_Output;
            case "Unemployed_Slaves_automatically_resettle":    return Effects.Unemployed_Slaves_automatically_resettle;
            case "Happiness_for_6_months":                      return Effects.Happiness_for_6_months;
            case "Immigration_Pull_for_6_months":               return Effects.Immigration_Pull_for_6_months;
            case "Pop_Primary_Species":                         return Effects.Pop_Primary_Species;
            case "Organic_Pop_Assembly_Speed":                  return Effects.Organic_Pop_Assembly_Speed;
            case "Complex_Drone_Output_when_above_50_Happiness":return Effects.Complex_Drone_Output_when_above_50_Happiness;
            case "Criminal_Branch_Office_Trade_Value":          return Effects.Criminal_Branch_Office_Trade_Value;
            case "Resources_from_Jobs":                         return Effects.Resources_from_Jobs;
            case "Energy_from_Technicians":                     return Effects.Energy_from_Technicians;
            case "Minerals_from_Miners":                        return Effects.Minerals_from_Miners;
            case "Food_from_Farmers":                           return Effects.Food_from_Farmers;
            case "Alloys_from_Metallurgists":                   return Effects.Alloys_from_Metallurgists;
            case "Consumer_Goods_from_Artisans":                return Effects.Consumer_Goods_from_Artisans;
            case "Culture_Workers_Output":                      return Effects.Culture_Workers_Output;
            case "Spiritualist_Ethics_attraction":              return Effects.Spiritualist_Ethics_attraction;
            case "Managers_Output":                             return Effects.Managers_Output;
            case "Evaluators_Output":                           return Effects.Evaluators_Output;
            case "Metallurgist_Output":                         return Effects.Metallurgist_Output;
            case "Trade_Value":                                 return Effects.Trade_Value;
            case "Automatic_resettlement_chance":               return Effects.Automatic_resettlement_chance;
            case "Monthly_Organic_Pop_Assembly":                return Effects.Monthly_Organic_Pop_Assembly;
            case "Envoy":                                       return Effects.Envoy;
            case "Habitability":                                return Effects.Habitability;
            case "Planet_becomes_Gaia_World":                   return Effects.Planet_becomes_Gaia_World;
            case "FTL_Inhibitor":                               return Effects.FTL_Inhibitor;
            case "Spiritualist_Ethics_Attraction":              return Effects.Spiritualist_Ethics_Attraction;
            case "Unity_from_Jobs":                             return Effects.Unity_from_Jobs;
            case "Biological_Pop_Happiness":                    return Effects.Biological_Pop_Happiness;
            case "Lithoid_Pop_Happiness":                       return Effects.Lithoid_Pop_Happiness;
            case "Planet_Capacity_per_Unblocked_District_Slot": return Effects.Planet_Capacity_per_Unblocked_District_Slot;
            case "Housing_from_Hive_Districts":                 return Effects.Housing_from_Hive_Districts;
            case "Pop_Housing_Usage":                           return Effects.Pop_Housing_Usage;
            case "Colony_Development_Speed":                    return Effects.Colony_Development_Speed;
            
            default: {Debug.Log("INVALID EFFECT: " + e); return Effects.INVALID;}
        }
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////
    // Stratums
    /////////////////////////////////////////////////////////////////////////////////////////////////////

    public enum Stratums {INVALID, NULL, Ruler, Specialist, Worker, Menial_Drone, Complex_Drone, Bio_Trophy};

    public static Stratums String_to_Stratums(string s)
    {
        switch(s)
        {
            case "-":               return Stratums.NULL;
            case "Ruler":           return Stratums.Ruler;
            case "Specialist":      return Stratums.Specialist;
            case "Worker":          return Stratums.Worker;
            case "Menial_Drone":    return Stratums.Menial_Drone;
            case "Complex_Drone":   return Stratums.Complex_Drone;
            case "Bio_Trophy":      return Stratums.Bio_Trophy;

            default: {Debug.Log("INVALID STRATUMS: " + s); return Stratums.INVALID;}
        }
    }
























    /////////////////////////////////////////////////////////////////////////////////////////////////////
    // loadJobs
    /////////////////////////////////////////////////////////////////////////////////////////////////////

    public static void loadJobs()
    {
        TextAsset jobsFile = Resources.Load<TextAsset>("Data/" + _version_ + "/Jobs"); // read Jobs.txt from Resource folder
        Job._jobs_ = new Dictionary<string, Job>();

        foreach(string line in jobsFile.text.Split('\n'))
        {
            string[] lineArray = Regex.Split(line, "\\s+");

            string name = lineArray[0];
            Data.Stratums stratum = String_to_Stratums(lineArray[1]);
            Dictionary<Data.Effects, int> effects = new Dictionary<Data.Effects, int>();
            Dictionary<Data.Resource, int> produces = new Dictionary<Data.Resource, int>();
            Dictionary<Data.Resource, int> upkeep = new Dictionary<Data.Resource, int>();
            int defaultPriority = 0;
            
            // Initialize effects
            foreach(string field in Regex.Split(lineArray[2].Substring(1, lineArray[2].Length - 2), @"(?<=}),(?={)"))
            {
                if( field.Length > 0 )
                {
                    // Debug.Log("field: " + field);
                    string[] subfields = field.Substring(1, field.Length - 2).Split(',');  // Remove brackets
                    
                    int.TryParse(subfields[1], out int index);

                    effects.Add(String_to_Effects(subfields[0]), index);
                }
            }

            // Initialize produces
            // Debug.Log("lineArray[3]: " + lineArray[3]);
            string dictionaryString = lineArray[3].Substring(1, lineArray[3].Length - 2);  // Remove square brackets
            
            // Debug.Log("dictionaryString: " + dictionaryString);
            if( dictionaryString.Length > 0 )
                foreach(string field in Regex.Split(dictionaryString, @"(?<=}),(?={)"))
                {
                    if( field.Length > 0 )
                    {
                        // Debug.Log("field: " + field);
                        string[] subfields = field.Substring(1, field.Length - 2).Split(',');  // Remove brackets
                        
                        int.TryParse(subfields[1], out int index);

                        produces.Add(String_to_Resource(subfields[0]), index);
                    }
                }
            
            // Initialize upkeep
            // Debug.Log("lineArray[4]: " + lineArray[4]);
            dictionaryString = lineArray[4].Substring(1, lineArray[4].Length - 2);  // Remove square brackets

            // Debug.Log("dictionaryString: " + dictionaryString);
            if( dictionaryString.Length > 0 )
                foreach(string field in Regex.Split(dictionaryString, @"(?<=}),(?={)"))
                {
                    if( field.Length > 0 )
                    {
                        // Debug.Log(field);
                        string[] subfields = field.Substring(1, field.Length - 2).Split(',');  // Remove brackets
                        
                        int.TryParse(subfields[1], out int index);

                        upkeep.Add(String_to_Resource(subfields[0]), index);
                    }
                }

            // Initialize defaultPriority
            int.TryParse(lineArray[5], out defaultPriority);

            Job._jobs_.Add(name, new Job(name, stratum, effects, produces, upkeep, defaultPriority));
        }

        Debug.Log("Jobs loaded: " + Job._jobs_.Count);
    }























    /////////////////////////////////////////////////////////////////////////////////////////////////////
    // loadBuildings
    /////////////////////////////////////////////////////////////////////////////////////////////////////

    public static void loadBuildings()
    {
        TextAsset buildingsFile = Resources.Load<TextAsset>("Data/" + _version_ + "/Buildings");
        Building._buildings_ = new Dictionary<string, Building>();

        foreach(string line in buildingsFile.text.Split('\n'))
        {
            string[] lineArray = Regex.Split(line, "\\s+");
            
            // string lineString = "";
            // foreach(string field in lineArray)
            //     lineString += field + " ";
            // Debug.Log(lineString);

            string name = lineArray[0];
            Dictionary<Data.Effects, int> effects = new Dictionary<Effects, int>();
            Dictionary<Data.Resource, int> produces = new Dictionary<Resource, int>();
            Dictionary<Job, int> jobs = new Dictionary<Job, int>();
            Dictionary<Data.Resource, int> upkeep = new Dictionary<Resource, int>();
            int time = 0;
            Dictionary<Data.Resource, int> cost = new Dictionary<Resource, int>();
            string upgrade;
            string downgrade;
            string buildable;
            string unique;
            string[] requirements = new string[0];

            string description = "Example description...";
            
            // Initialize effects
            // Debug.Log("lineArray[1]: " + lineArray[1]);
            string dictionaryString = lineArray[1].Substring(1, lineArray[1].Length - 2);  // Remove square brackets
            
            // Debug.Log("dictionaryString: " + dictionaryString);
            if( dictionaryString.Length > 0 )
                foreach(string field in Regex.Split(dictionaryString, @"(?<=}),(?={)"))
                {
                    if( field.Length > 0 )
                    {
                        // Debug.Log("field: " + field);
                        string[] subfields = field.Substring(1, field.Length - 2).Split(',');  // Remove brackets
                        
                        // Debug.Log(subfields[1]);
                        int.TryParse(subfields[1], out int index);

                        // Debug.Log(subfields[0]);
                        effects.Add(String_to_Effects(subfields[0]), index);
                    }
                }
            
            // Initialize produces
            // Debug.Log("lineArray[2]: " + lineArray[2]);
            dictionaryString = lineArray[2].Substring(1, lineArray[2].Length - 2);  // Remove square brackets
            
            // Debug.Log("dictionaryString: " + dictionaryString);
            if( dictionaryString.Length > 0 )
                foreach(string field in Regex.Split(dictionaryString, @"(?<=}),(?={)"))
                {
                    if( field.Length > 0 )
                    {
                        // Debug.Log("field: " + field);
                        string[] subfields = field.Substring(1, field.Length - 2).Split(',');  // Remove brackets
                        
                        int.TryParse(subfields[1], out int index);

                        produces.Add(String_to_Resource(subfields[0]), index);
                    }
                }
            
            // Initialize jobs
            // Debug.Log("lineArray[3]: " + lineArray[3]);
            dictionaryString = lineArray[3].Substring(1, lineArray[3].Length - 2);  // Remove square brackets
            
            // Debug.Log("dictionaryString: " + dictionaryString);
            if( dictionaryString.Length > 0 )
                foreach(string field in Regex.Split(dictionaryString, @"(?<=}),(?={)"))
                {
                    if( field.Length > 0 )
                    {
                        // Debug.Log("field: " + field);
                        string[] subfields = field.Substring(1, field.Length - 2).Split(',');  // Remove brackets
                        
                        int.TryParse(subfields[1], out int index);

                        // jobs.Add(String_to_Jobs(subfields[0]), index);
                        try{
                            jobs.Add(Job._jobs_[subfields[0]], index);
                        }
                        catch(KeyNotFoundException) {
                            jobs.Add(new Job("INVALID_" + subfields[0]), index);
                        }
                    }
                }
            
            // Initialize upkeep
            // Debug.Log("lineArray[4]: " + lineArray[4]);
            dictionaryString = lineArray[4].Substring(1, lineArray[4].Length - 2);  // Remove square brackets
            
            // Debug.Log("dictionaryString: " + dictionaryString);
            if( dictionaryString.Length > 0 )
                foreach(string field in Regex.Split(dictionaryString, @"(?<=}),(?={)"))
                {
                    if( field.Length > 0 )
                    {
                        // Debug.Log("field: " + field);
                        string[] subfields = field.Substring(1, field.Length - 2).Split(',');  // Remove brackets
                        
                        int.TryParse(subfields[1], out int index);

                        upkeep.Add(String_to_Resource(subfields[0]), index);
                    }
                }

            // Initialize defaultPriority
            int.TryParse(lineArray[5], out time);
            
            // Initialize cost
            // Debug.Log("lineArray[6]: " + lineArray[6]);
            dictionaryString = lineArray[6].Substring(1, lineArray[6].Length - 2);  // Remove square brackets
            
            // Debug.Log("dictionaryString: " + dictionaryString);
            if( dictionaryString.Length > 0 )
                foreach(string field in Regex.Split(dictionaryString, @"(?<=}),(?={)"))
                {
                    if( field.Length > 0 )
                    {
                        // Debug.Log("field: " + field);
                        string[] subfields = field.Substring(1, field.Length - 2).Split(',');  // Remove brackets
                        
                        int.TryParse(subfields[1], out int index);

                        cost.Add(String_to_Resource(subfields[0]), index);
                    }
                }

            // Initialize upgrade
            upgrade = lineArray[7];
            // Debug.Log("upgrade: " + upgrade);

            // Initialize downgrade
            downgrade = lineArray[8];
            // Debug.Log("downgrade: " + downgrade);

            // Initialize buildable
            buildable = lineArray[9];
            // Debug.Log("buildable: " + buildable);

            // Initialize unique
            unique = lineArray[10];
            // Debug.Log("unique: " + unique);

            // Initialize requirements
            string arrayString = lineArray[11].Substring(1, lineArray[11].Length - 2);  // Remove square brackets
            
            // Debug.Log("arrayString: " + arrayString);
            if( arrayString.Length > 0 )
                requirements = Regex.Split(arrayString, @"(?<=}),(?={)");

                
            
            // string lineString = "";
            // foreach(string field in requirements)
            //     lineString += field + " ";
            // Debug.Log("lineString: " + lineString);

            Building._buildings_.Add(name, new Building(name, effects, produces, jobs, upkeep, time, cost, upgrade, downgrade, buildable, unique, requirements, description));
        }

        Debug.Log("Buildings loaded: " + Building._buildings_.Count);
    }






















    /////////////////////////////////////////////////////////////////////////////////////////////////////
    // loadDistricts
    /////////////////////////////////////////////////////////////////////////////////////////////////////

    public static void loadDistricts()
    {
        TextAsset districtsFile = Resources.Load<TextAsset>("Data/" + _version_ + "/Districts"); // read Districs.txt from Resource folder
        District._districts_ = new Dictionary<string, District>();

        foreach(string line in districtsFile.text.Split('\n'))
        {
            string[] lineArray = Regex.Split(line, "\\s+");

            string name = lineArray[0];
            Dictionary<Data.Resource, int> production = new Dictionary<Data.Resource, int>();
            Dictionary<Job, int> jobs = new Dictionary<Job, int>();
            Dictionary<Data.Resource, int> upkeep = new Dictionary<Data.Resource, int>();
            int time = 0;
            Dictionary<Data.Resource, int> cost = new Dictionary<Data.Resource, int>();
            string[] requirements = new string[0];
            string color = "";
            
            // Initialize production
            // Debug.Log("lineArray[1]: " + lineArray[1]);
            string dictionaryString = lineArray[1].Substring(1, lineArray[1].Length - 2);  // Remove square brackets
            
            // Debug.Log("dictionaryString: " + dictionaryString);
            if( dictionaryString.Length > 0 )
                foreach(string field in Regex.Split(dictionaryString, @"(?<=}),(?={)"))
                {
                    if( field.Length > 0 )
                    {
                        // Debug.Log("field: " + field);
                        string[] subfields = field.Substring(1, field.Length - 2).Split(',');  // Remove brackets
                        
                        int.TryParse(subfields[1], out int index);

                        production.Add(String_to_Resource(subfields[0]), index);
                    }
                }
            
            // Initialize jobs
            // Debug.Log("lineArray[2]: " + lineArray[2]);
            dictionaryString = lineArray[2].Substring(1, lineArray[2].Length - 2);  // Remove square brackets
            
            // Debug.Log("dictionaryString: " + dictionaryString);
            if( dictionaryString.Length > 0 )
                foreach(string field in Regex.Split(dictionaryString, @"(?<=}),(?={)"))
                {
                    if( field.Length > 0 )
                    {
                        // Debug.Log("field: " + field);
                        string[] subfields = field.Substring(1, field.Length - 2).Split(',');  // Remove brackets
                        
                        int.TryParse(subfields[1], out int index);

                        try{
                            jobs.Add(Job._jobs_[subfields[0]], index);
                        }
                        catch(KeyNotFoundException) {
                            jobs.Add(new Job("INVALID_" + subfields[0]), index);
                        }
                    }
                }
            
            // Initialize upkeep
            // Debug.Log("lineArray[3]: " + lineArray[3]);
            dictionaryString = lineArray[3].Substring(1, lineArray[3].Length - 2);  // Remove square brackets
            
            // Debug.Log("dictionaryString: " + dictionaryString);
            if( dictionaryString.Length > 0 )
                foreach(string field in Regex.Split(dictionaryString, @"(?<=}),(?={)"))
                {
                    if( field.Length > 0 )
                    {
                        // Debug.Log("field: " + field);
                        string[] subfields = field.Substring(1, field.Length - 2).Split(',');  // Remove brackets
                        
                        int.TryParse(subfields[1], out int index);

                        upkeep.Add(String_to_Resource(subfields[0]), index);
                    }
                }

            time = int.Parse(lineArray[4]);
            
            // Initialize cost
            // Debug.Log("lineArray[5]: " + lineArray[5]);
            dictionaryString = lineArray[5].Substring(1, lineArray[5].Length - 2);  // Remove square brackets
            
            // Debug.Log("dictionaryString: " + dictionaryString);
            if( dictionaryString.Length > 0 )
                foreach(string field in Regex.Split(dictionaryString, @"(?<=}),(?={)"))
                {
                    if( field.Length > 0 )
                    {
                        // Debug.Log("field: " + field);
                        string[] subfields = field.Substring(1, field.Length - 2).Split(',');  // Remove brackets
                        
                        int.TryParse(subfields[1], out int index);

                        cost.Add(String_to_Resource(subfields[0]), index);
                    }
                }

            // Initialize requirements
            string arrayString = lineArray[6].Substring(1, lineArray[6].Length - 2);  // Remove square brackets
            
            // Debug.Log("arrayString: " + arrayString);
            if( arrayString.Length > 0 )
                requirements = Regex.Split(arrayString, @"(?<=}),(?={)");
                
            // foreach( string requirement in requirements)
            //     Debug.Log(requirement);

            // Initialize color
            color = lineArray[7];

            District._districts_.Add(name, new District(name, production, jobs, upkeep, time, cost, requirements, color));
        }

        Debug.Log("Districts loaded: " + District._districts_.Count);
    }






















    /////////////////////////////////////////////////////////////////////////////////////////////////////
    // loadPlanets
    /////////////////////////////////////////////////////////////////////////////////////////////////////

    public static void loadPlanets()
    {
        TextAsset planetsFile = Resources.Load<TextAsset>("Data/" + _version_ + "/Planets"); // read Planets.txt from Resource folder
        Planet._planets_ = new Dictionary<string, Planet>();

        foreach(string line in planetsFile.text.Split('\n'))
        {
            string[] lineArray = Regex.Split(line, "\\s+");

            string name = lineArray[0];
            Dictionary<Data.Effects, int> effects = new Dictionary<Data.Effects, int>();
            Dictionary<Job, int> jobs = new Dictionary<Job, int>();
            string[] requirements = new string[0];
            
            // Initialize effects
            // Debug.Log("lineArray[1]: " + lineArray[1]);
            string dictionaryString = lineArray[1].Substring(1, lineArray[1].Length - 2);  // Remove square brackets
            
            // Debug.Log("dictionaryString: " + dictionaryString);
            if( dictionaryString.Length > 0 )
                foreach(string field in Regex.Split(dictionaryString, @"(?<=}),(?={)"))
                {
                    if( field.Length > 0 )
                    {
                        // Debug.Log("field: " + field);
                        string[] subfields = field.Substring(1, field.Length - 2).Split(',');  // Remove brackets
                        
                        // Debug.Log(subfields[1]);
                        int.TryParse(subfields[1], out int index);

                        // Debug.Log(subfields[0]);
                        effects.Add(String_to_Effects(subfields[0]), index);
                    }
                }
            
            // Initialize jobs
            // Debug.Log("lineArray[2]: " + lineArray[2]);
            dictionaryString = lineArray[2].Substring(1, lineArray[2].Length - 2);  // Remove square brackets
            
            // Debug.Log("dictionaryString: " + dictionaryString);
            if( dictionaryString.Length > 0 )
                foreach(string field in Regex.Split(dictionaryString, @"(?<=}),(?={)"))
                    if( field.Length > 0 )
                    {
                        // Debug.Log("field: " + field);
                        string[] subfields = field.Substring(1, field.Length - 2).Split(',');  // Remove brackets
                        
                        int.TryParse(subfields[1], out int index);

                        try{
                            jobs.Add(Job._jobs_[subfields[0]], index);
                        }
                        catch(KeyNotFoundException) {
                            jobs.Add(new Job("INVALID_" + subfields[0]), index);
                        }
                    }
            
            // Initialize requirements
            string arrayString = lineArray[3].Substring(1, lineArray[3].Length - 2);  // Remove square brackets
            
            // Debug.Log("arrayString: " + arrayString);
            if( arrayString.Length > 0 )
                requirements = Regex.Split(arrayString, @"(?<=}),(?={)");

            Planet._planets_.Add(name, new Planet(name, effects, jobs, requirements));
        }

        Debug.Log("Planets loaded: " + Planet._planets_.Count);
    }
}