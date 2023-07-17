namespace NeurofyMesh.Models
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public class DecodedPayload
    {
        public Gps_Coordinates Gps_Coordinates { get; set; }
        public Measurement Measurements { get; set; }
        public bool touchSensorPressed { get; set; }

    }

    public class Gps_Coordinates
    {
        public string latitude { get; set; }
        public string longitude { get; set; }
    }

    public class Measurement
    {
        public int temperature { get; set; }
        public int humidity { get; set; }
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}