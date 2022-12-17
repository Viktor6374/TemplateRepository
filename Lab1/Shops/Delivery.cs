using System.Collections.Generic;

namespace Shops
{
    public class Delivery
    {
        private List<Shipment> shipments = new List<Shipment>();

        public IReadOnlyList<Shipment> Shipments => shipments.AsReadOnly();
        public void AddShipment(Shipment shipment)
        {
            shipments.Add(shipment);
        }

        public void RemoveShipment(Shipment shipment)
        {
            shipments.Remove(shipment);
        }
    }
}
