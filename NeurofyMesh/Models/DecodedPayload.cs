namespace NeurofyMesh.Models
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public class DecodedPayload
    {
        public Gps_Coordinates? Gps_Coordinates { get; set; } = null;
        public Measurement? Measurements { get; set; } = null;
        public bool? touchSensorPressed { get; set; } = null;

    }

    public class Gps_Coordinates
    {
        public string latitude { get; set; } = "";
        public string longitude { get; set; } = "";
    }

    public class Measurement
    {
        public double? temperature { get; set; } = null;
        public double? humidity { get; set; } = null;
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}