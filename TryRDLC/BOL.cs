using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TryRDLC
{
    public class BillOfLadingHeader
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string ShipToName { get; set; }
        public string ShipToAddress { get; set; }
        public string BillOfLadingNumber { get; set; }
        public string SCAC { get; set; }
        public string ProNumber { get; set; }
        public byte[] BillOfLadingNumberBarcodeImage { get; set; }
    }

    public class BillOfLadingCustomerOrderInformation
    {
        public string CustomerOrderNumber { get; set; }
        public string NumberOfPackages { get; set; }
        public decimal Weight { get; set; }
        public string AdditionalShipperInfo { get; set; }
    }

    public class BillOfLadingCarrierInformation
    {
        public int HandingUnitQty { get; set; }
        public string HandlingUnitType { get; set; }
        public int PackageQty { get; set; }
        public string PackageType { get; set; }
        public decimal Weight { get; set; }
    }
}
