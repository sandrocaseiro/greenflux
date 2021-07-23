namespace Greenflux.Models.Connectors
{
    public class SConnector
    {
        public int Id { get; set; }
        public decimal MaxCurrent { get; set; }
        public int ChargeStationId { get; set; }
    }
}