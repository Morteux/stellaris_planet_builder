using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DistrictManager : MonoBehaviour
{
    private Transform Planet;
    private Transform PlanetSizeSlider;
    private Transform CityDistrict;
    private Transform IndustrialDistrict;
    private Transform GeneratorDistrict;
    private Transform MiningDistrict;
    private Transform AgricultureDistrict;

    // Start is called before the first frame update
    void Start()
    {
        Planet = transform.parent.parent;

        InitializePlanetDistricts();

        PlanetSizeSlider = transform.Find("PlanetSizeSlider/Slider");
        PlanetSizeSlider.GetComponent<Slider>().onValueChanged.AddListener(UpdateDistrictsSize);

        CityDistrict = transform.Find("CityDistrict");
        IndustrialDistrict = transform.Find("IndustrialDistrict");
        GeneratorDistrict = transform.Find("GeneratorDistrict");
        MiningDistrict = transform.Find("MiningDistrict");
        AgricultureDistrict = transform.Find("AgricultureDistrict");

        CityDistrict.Find("SelectedDistrictSlot").GetComponent<Button>().onClick.AddListener(delegate{ChangeCityDistrict(1);});
        IndustrialDistrict.Find("SelectedDistrictSlot").GetComponent<Button>().onClick.AddListener(delegate{ChangeIndustrialDistrict(1);});
        GeneratorDistrict.Find("SelectedDistrictSlot").GetComponent<Button>().onClick.AddListener(delegate{ChangeGeneratorDistrict(1);});
        MiningDistrict.Find("SelectedDistrictSlot").GetComponent<Button>().onClick.AddListener(delegate{ChangeMiningDistrict(1);});
        AgricultureDistrict.Find("SelectedDistrictSlot").GetComponent<Button>().onClick.AddListener(delegate{ChangeAgricultureDistrict(1);});

        CityDistrict.Find("UnselectedDistrictSlot").GetComponent<Button>().onClick.AddListener(delegate{ChangeCityDistrict(-1);});
        IndustrialDistrict.Find("UnselectedDistrictSlot").GetComponent<Button>().onClick.AddListener(delegate{ChangeIndustrialDistrict(-1);});
        GeneratorDistrict.Find("UnselectedDistrictSlot").GetComponent<Button>().onClick.AddListener(delegate{ChangeGeneratorDistrict(-1);});
        MiningDistrict.Find("UnselectedDistrictSlot").GetComponent<Button>().onClick.AddListener(delegate{ChangeMiningDistrict(-1);});
        AgricultureDistrict.Find("UnselectedDistrictSlot").GetComponent<Button>().onClick.AddListener(delegate{ChangeAgricultureDistrict(-1);});

        UpdateDistrictsSize(20);
    }

    void UpdateDistrictsSize(float newPlanetSize)
    {
        // Debug.Log("UpdateDistrictsSize");
        
        PlanetSizeSlider.parent.Find("Counter").GetComponent<Text>().text = ((int) newPlanetSize).ToString();

        Planet.GetComponent<PlanetData>().planetSize_ = (int) newPlanetSize;
        
        UpdateDistrictsSlot();
    }

    void ChangeCityDistrict(int mode) 
    {
        ChangeDistrict(CityDistrict, mode);
    }

    void ChangeIndustrialDistrict(int mode) 
    {
        ChangeDistrict(IndustrialDistrict, mode);
    }

    void ChangeGeneratorDistrict(int mode) 
    {
        ChangeDistrict(GeneratorDistrict, mode);
    }

    void ChangeMiningDistrict(int mode) 
    {
        ChangeDistrict(MiningDistrict, mode);
    }

    void ChangeAgricultureDistrict(int mode) 
    {
        ChangeDistrict(AgricultureDistrict, mode);
    }

    void ChangeDistrict(Transform district, int mode) 
    {
        int selected = int.Parse(district.Find("SelectedDistrictSlot/Counter").GetComponent<Text>().text);
        int unselected = int.Parse(district.Find("UnselectedDistrictSlot/Counter").GetComponent<Text>().text);
        int planetSize = int.Parse(PlanetSizeSlider.parent.Find("Counter").GetComponent<Text>().text);

        // Less districts than planet size allow OR remove district
        if(Planet.GetComponent<PlanetData>().districtCounter_ < planetSize || mode == -1)
            // Selected and unselected in range [0, planetSize]
            if( selected + mode * 1 <= planetSize && selected + mode * 1 >= 0 && unselected - mode * 1 <= planetSize && unselected - mode * 1 >= 0)
            {
                district.Find("SelectedDistrictSlot/Counter").GetComponent<Text>().text = (selected + mode * 1).ToString();
                district.Find("UnselectedDistrictSlot/Counter").GetComponent<Text>().text = (unselected - mode * 1).ToString();

                Planet.GetComponent<PlanetData>().districtCounter_ += mode;
                Planet.GetComponent<PlanetData>().district_["Agriculture_District"] = selected + mode * 1;
                
                UpdateDistrictsSlot();

                Planet.GetComponentInChildren<PlanetData>().UpdatePlanetData();
            }
    }

    void UpdateDistrictsSlot()
    {
        int selectedCityDistrict        = int.Parse(CityDistrict.Find("SelectedDistrictSlot/Counter").GetComponent<Text>().text);
        int selectedIndustrialDistrict  = int.Parse(IndustrialDistrict.Find("SelectedDistrictSlot/Counter").GetComponent<Text>().text);
        int selectedGeneratorDistrict   = int.Parse(GeneratorDistrict.Find("SelectedDistrictSlot/Counter").GetComponent<Text>().text);
        int selectedMiningDistrict      = int.Parse(MiningDistrict.Find("SelectedDistrictSlot/Counter").GetComponent<Text>().text);
        int selectedAgricultureDistrict = int.Parse(AgricultureDistrict.Find("SelectedDistrictSlot/Counter").GetComponent<Text>().text);

        int unselectedCityDistrict        = int.Parse(CityDistrict.Find("UnselectedDistrictSlot/Counter").GetComponent<Text>().text);
        int unselectedIndustrialDistrict  = int.Parse(IndustrialDistrict.Find("UnselectedDistrictSlot/Counter").GetComponent<Text>().text);
        int unselectedGeneratorDistrict   = int.Parse(GeneratorDistrict.Find("UnselectedDistrictSlot/Counter").GetComponent<Text>().text);
        int unselectedMiningDistrict      = int.Parse(MiningDistrict.Find("UnselectedDistrictSlot/Counter").GetComponent<Text>().text);
        int unselectedAgricultureDistrict = int.Parse(AgricultureDistrict.Find("UnselectedDistrictSlot/Counter").GetComponent<Text>().text);

        // Update UnavailableDistrictSlot counter
        int total = selectedCityDistrict + selectedIndustrialDistrict + selectedGeneratorDistrict + selectedMiningDistrict + selectedAgricultureDistrict;

        PlanetSizeSlider.GetComponent<Slider>().minValue = total;

        CityDistrict.Find("UnavailableDistrictSlot/Counter").GetComponent<Text>().text          = Math.Abs(total - selectedCityDistrict).ToString();
        IndustrialDistrict.Find("UnavailableDistrictSlot/Counter").GetComponent<Text>().text    = Math.Abs(total - selectedIndustrialDistrict).ToString();
        GeneratorDistrict.Find("UnavailableDistrictSlot/Counter").GetComponent<Text>().text     = Math.Abs(total - selectedGeneratorDistrict).ToString();
        MiningDistrict.Find("UnavailableDistrictSlot/Counter").GetComponent<Text>().text        = Math.Abs(total - selectedMiningDistrict).ToString();
        AgricultureDistrict.Find("UnavailableDistrictSlot/Counter").GetComponent<Text>().text   = Math.Abs(total - selectedAgricultureDistrict).ToString();

        // Update UnselectedDistrictSlot counter
        int planetSize = Planet.GetComponent<PlanetData>().planetSize_;
        int absCityDistrict         = Math.Abs(planetSize - selectedCityDistrict);
        int absIndustrialDistrict   = Math.Abs(planetSize - selectedIndustrialDistrict);
        int absGeneratorDistrict    = Math.Abs(planetSize - selectedGeneratorDistrict);
        int absMiningDistrict       = Math.Abs(planetSize - selectedMiningDistrict);
        int absAgricultureDistrict  = Math.Abs(planetSize - selectedAgricultureDistrict);

        CityDistrict.Find("UnselectedDistrictSlot/Counter").GetComponent<Text>().text           = (absCityDistrict - Math.Abs(total - selectedCityDistrict)).ToString();
        IndustrialDistrict.Find("UnselectedDistrictSlot/Counter").GetComponent<Text>().text     = (absIndustrialDistrict - Math.Abs(total - selectedIndustrialDistrict)).ToString();
        GeneratorDistrict.Find("UnselectedDistrictSlot/Counter").GetComponent<Text>().text      = (absGeneratorDistrict - Math.Abs(total - selectedGeneratorDistrict)).ToString();
        MiningDistrict.Find("UnselectedDistrictSlot/Counter").GetComponent<Text>().text         = (absMiningDistrict - Math.Abs(total - selectedMiningDistrict)).ToString();
        AgricultureDistrict.Find("UnselectedDistrictSlot/Counter").GetComponent<Text>().text    = (absAgricultureDistrict - Math.Abs(total - selectedAgricultureDistrict)).ToString();
        }

    void InitializePlanetDistricts()
    {
        Planet.GetComponent<PlanetData>().district_.Add("City_District", 0);
        Planet.GetComponent<PlanetData>().district_.Add("Industrial_District", 0);
        Planet.GetComponent<PlanetData>().district_.Add("Generator_District", 0);
        Planet.GetComponent<PlanetData>().district_.Add("Mining_District", 0);
        Planet.GetComponent<PlanetData>().district_.Add("Agriculture_District", 0);
    }
}
