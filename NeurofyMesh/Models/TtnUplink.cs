namespace NeurofyMesh.Models
{
    public class TtnUplink
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public EndDeviceIds end_device_ids { get; set; }

        public UplinkMessage uplink_message { get; set; }
         


    }

    public class UplinkMessage
    {
        public DecodedPayload decoded_payload { get; set; }
    }

    public class EndDeviceIds
    {
        public string device_id { get; set; } = "";
        public ApplicationIds application_ids { get; set; }
        public string dev_eui { get; set; } = "";
        public string join_eui { get; set; } = "";
        public string dev_addr { get; set; } = "";
    }

    public class ApplicationIds
    {
        public string application_id { get; set; }
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
