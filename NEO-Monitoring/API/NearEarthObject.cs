using System;
using System.Collections.Generic;
using System.Text;

namespace NEOMonitoring.API
{
    public class Links
    {
        public string Next { get; set; }
        public string Previous { get; set; }
        public string Self { get; set; }
    }

    public class EstimatedDiameter
    {
        public Kilometers Kilometers { get; set; }
        public Meters Meters { get; set; }
        public Miles Miles { get; set; }
        public Feet Feet { get; set; }
    }

    public class Kilometers
    {
        public double Estimated_diameter_min { get; set; }
        public double Estimated_diameter_max { get; set; }
    }

    public class Meters
    {
        public double Estimated_diameter_min { get; set; }
        public double Estimated_diameter_max { get; set; }
    }

    public class Miles
    {
        public double Estimated_diameter_min { get; set; }
        public double Estimated_diameter_max { get; set; }
    }

    public class Feet
    {
        public double Estimated_diameter_min { get; set; }
        public double Estimated_diameter_max { get; set; }
    }

    public class CloseApproachData
    {
        public string Close_approach_date { get; set; }
        public string Close_approach_date_full { get; set; }
        public long Epoch_date_close_approach { get; set; }
        public RelativeVelocity Relative_velocity { get; set; }
        public MissDistance Miss_distance { get; set; }
        public string Orbiting_body { get; set; }
    }

    public class RelativeVelocity
    {
        public string Kilometers_per_second { get; set; }
        public string Kilometers_per_hour { get; set; }
        public string Miles_per_hour { get; set; }
    }

    public class MissDistance
    {
        public string Astronomical { get; set; }
        public string Lunar { get; set; }
        public string Kilometers { get; set; }
        public string Miles { get; set; }
    }

    public class NearEarthObject
    {
        public Links Links { get; set; }
        public string Id { get; set; }
        public string Neo_reference_id { get; set; }
        public string Name { get; set; }
        public string Nasa_jpl_url { get; set; }
        public double Absolute_magnitude_h { get; set; }
        public EstimatedDiameter Estimated_diameter { get; set; }
        public bool Ispotentially_hazardous_asteroid { get; set; }
        public List<CloseApproachData> Close_approach_data { get; set; }
        public bool Is_sentry_object { get; set; }
        public string Name_displayed 
        {
            get => $"Name: {Name}";
        }
        public string Miss_distance_displayed
        {
            get => $"Miss distance: {Close_approach_data[0].Miss_distance.Kilometers} km";
        }
    }

    public class NearEarthObjects
    {
        public Links Links { get; set; }
        public int Element_count { get; set; }
        public Dictionary<string, List<NearEarthObject>> Near_earth_objects { get; set; }
    }

    public class FormatedApproachData
    {
        public string Date { get; set; }
        public string DateDisplayed { get => "Date: "+ Date; }
        public string MissDistance { get; set; }
        public string MissDistanceDisplayed { get => "Miss distance in km: " + MissDistance; }
    }
}
