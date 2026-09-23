namespace OOP_2
{
    //Part1: Theoritical
    //Question1:
    //a)Class is a reference type while struct is a value type
    //Class is stored in heap while struct is stored in stack
    //Class support Inheritance while struct doesn't
    //Class provides parameterless constructor if none defined while struct always provide it
    //Class can be null while struct can't
    //Class is best for complex data with behaviour,inheritance,shared state while struct is best for small,simple data,better performance
    //b)Classes work best for large applications as they avoid unecessary copying,work better with shared objects
    //Question2:
    //a)Shipment
    //b)ExpressShipment
    //c)Shipment members which is a property here(TrackingCode)
    //d)Saving code memory
        public struct DeliveryAddress
        {
            string City;
            string Street;
            int BuildingNumber;
            public DeliveryAddress(string city, string street, int buildingNumber)
            {
                City = city;
                Street = street;
                BuildingNumber = buildingNumber;
            }
            public DeliveryAddress(string street)
            {
                Street = street;
                City = "New York";
                BuildingNumber = 1;
            }
            public string GetFullAddress()
            {
                return $"{BuildingNumber} {Street} ,{City}";
            }


        }


        //Shipment Class(Parent Class)
        public class Shipment
        {
            string trackingCode;
            string description;
            int weight;
            decimal deliveryFee;
            DeliveryAddress destination;


            //Properties
            //1.Tracking Code Property
            public string TrackingCode
            {
                get
                {
                    return trackingCode;
                }
                set
                {
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        trackingCode = value;
                    }
                    else
                    {
                        throw new ArgumentException("Tracking Code can't be null or empty");
                    }
                }
            }
            //2.Description Property
            public string Description
            {
                get
                {
                    return description;
                }
                set
                {
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        throw new ArgumentException("Description cannot be null or empty.");
                    }
                    else
                    {
                        description = value;
                    }
                }
            }
            //3.Weight Property
            public int Weight
            {
                get
                {
                    return weight;
                }
                set
                {
                    if (value <= 0)
                    {
                        throw new ArgumentException("Weight must be a positive number.");
                    }
                    weight = value;
                }

            }
            //4.Delivery Fee Property
            public decimal DeliveryFee
            {
                get
                {
                    return deliveryFee;
                }
                private set
                {
                    if (value <= 0)
                    {
                        throw new ArgumentException("Delivery fee must be a positive number.");
                    }
                    else
                    {
                        deliveryFee = value;
                    }
                }
            }
            //5.Destination Property
            public DeliveryAddress Destination
            {
                get
                {
                    return destination;
                }
                set
                {
                    destination = value;
                }
            }
            public virtual decimal EstimatedCost
            {
                get
                {
                    return deliveryFee + (weight * 5);
                }
            }

            //////////////Constructors
            //1st Constructor
            public Shipment(string trackingCode)
            {
                this.trackingCode = trackingCode;
                description = "Unknown";
                weight = 1;
                deliveryFee = 50;
                destination = new DeliveryAddress("Nasr city", "Al Nahas", 15);
            }
            //2nd Constructor
            public Shipment(string TrackingCode, string Description, int Weight, decimal DeliveryFee)
            {
                trackingCode = TrackingCode;
                description = Description;
                weight = Weight;
                deliveryFee = DeliveryFee;
            }
            //////////////Methods
            //Update DeliveryFee Method
            public void UpdateDeilveryFee(decimal newFee)
            {
                if (newFee > 0)
                {
                    deliveryFee = newFee;
                }
                else
                {
                    throw new ArgumentException("Delivery fee must be a positive number.");
                }
            }
            //Print Shipment Method
            public string PrintShipmentDetails()
            {
                return $"Tracking Code:\n{TrackingCode}\nDescription:\n{Description}\nWeight:\n{Weight}kg\nDelivery Fee:\n{deliveryFee}\nEstimated Cost:\n{EstimatedCost}";
        }
    }
    //Standard Shipment Class(Child Class)
    public class StandardShipment : Shipment
    {
        //Chaining Constructor
        public StandardShipment(string TrackingCode, string Description, int Weight, decimal DeliveryFee) : base(TrackingCode, Description, Weight, DeliveryFee)
        {
        }
    }
    //Express Shipment Class(Child Class)
    public class ExpressShipment : Shipment
    {
        decimal extrafee;
        //ExtraFee Property
        public decimal ExtraFee
        {
            get
            {
                return extrafee;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Extra fee must be greater than or equal to 0");
                else
                    extrafee = value;
            }
        }
        //Override Estimated cost property
        public override decimal EstimatedCost
        { 
            get
            {
                return base.EstimatedCost + extrafee;
            }
            
        }
        public ExpressShipment(string TrackingCode, string Description, int Weight, decimal DeliveryFee,decimal extrafee) : base(TrackingCode, Description, Weight, DeliveryFee)
        {
            this.ExtraFee = extrafee;
        }
    }
    //International Shipment Class(parent class)
    public class InternationalShipment : Shipment
    {
        string destinationcountry;
        decimal customsfee;
        //Destination Country property
        public string DestinationCountry
        {
            get
            {
                return destinationcountry;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Description cannot be null or empty.");
                }
                else
                {
                    destinationcountry = value;
                }


            }
        }
        //Customs Fee property
        public decimal CustomsFee
        {
            get
            {
                return customsfee;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Extra fee must be greater than or equal to 0");
                else
                    customsfee = value;
            }
        }
        //Override Estimated cost property
        public override decimal EstimatedCost
        {
            get
            {
                return base.EstimatedCost + customsfee;
            }

        }
        //Constructor chaining
        public InternationalShipment(string TrackingCode, string Description, int Weight, decimal DeliveryFee, decimal customsfee,string destinationcountry) : base(TrackingCode, Description, Weight, DeliveryFee)
        {
            this.CustomsFee = customsfee;
            this.DestinationCountry = destinationcountry;
        }
    }

    //Delivery Center Class
    public class DeliveryCenter
        {
            public string CenterName;
            Shipment[] shipment;
            public DeliveryCenter()
            {
                shipment = new Shipment[20];
            }
            //Integer Indexer
            public Shipment this[int index]
            {
                get
                {
                    if (index >= 0 && index < shipment.Length)
                    {
                        return shipment[index];
                    }
                    return default;
                }
                set
                {
                    if (index >= 0 && index < shipment.Length)
                    {
                        shipment[index] = value;
                    }
                }
            }
        //String Indexer
        public Shipment this[string trackingcode]
        {
            get
            {
                for (int i = 0; i < shipment.Length; i++)
                {
                    if (shipment[i] != null &&
                        shipment[i].TrackingCode == trackingcode)
                    {
                        return shipment[i];
                    }
                }

                return null;
            }
        }
        //AddShipment Method
        public bool AddShipment(Shipment newshipment)
        {
            for (int i = 0; i < shipment.Length; i++)
            {
                if (shipment[i] == null)
                {
                    shipment[i] = newshipment;
                    return true;
                }
            }

            return false;
        }
        //RemoveShipment Method
        public bool RemoveShipment(string trackingcode)
        {
            for (int i = 0; i < shipment.Length; i++)
            {
                if (shipment[i].TrackingCode != null && shipment[i].TrackingCode == trackingcode)
                {
                    shipment[i] = null;
                    return true;
                }
            }
            return false;
        }
        //Print all shipments method
        public void PrintAllShipments()
        {
            for (int i = 0; i < shipment.Length; i++)
            {

                if(shipment[i] != null)
                {
                    Console.WriteLine(shipment[i].PrintShipmentDetails());
                }
            }
        }
        }
    internal class Program
    {
        static void Main(string[] args)
        {
            // 1. Create a DeliveryCenter
            Console.WriteLine("Enter Delivery Center Name:");
            string centerName = Console.ReadLine();

            DeliveryCenter center = new DeliveryCenter();
            center.CenterName = centerName;

            // 2. Create Standard Shipment
            Console.WriteLine("\n--- Standard Shipment ---");

            Console.WriteLine("Tracking Code:");
            string standardTrackingCode = Console.ReadLine();

            Console.WriteLine("Description:");
            string standardDescription = Console.ReadLine();

            Console.WriteLine("Weight:");
            int standardWeight = int.Parse(Console.ReadLine());

            Console.WriteLine("Delivery Fee:");
            decimal standardDeliveryFee = decimal.Parse(Console.ReadLine());

            StandardShipment standardShipment =
                new StandardShipment(
                    standardTrackingCode,
                    standardDescription,
                    standardWeight,
                    standardDeliveryFee
                );


            // 3. Create Express Shipment
            Console.WriteLine("\n--- Express Shipment ---");

            Console.WriteLine("Tracking Code:");
            string expressTrackingCode = Console.ReadLine();

            Console.WriteLine("Description:");
            string expressDescription = Console.ReadLine();

            Console.WriteLine("Weight:");
            int expressWeight = int.Parse(Console.ReadLine());

            Console.WriteLine("Delivery Fee:");
            decimal expressDeliveryFee = decimal.Parse(Console.ReadLine());

            Console.WriteLine("Extra Fee:");
            decimal extraFee = decimal.Parse(Console.ReadLine());

            ExpressShipment expressShipment =
                new ExpressShipment(
                    expressTrackingCode,
                    expressDescription,
                    expressWeight,
                    expressDeliveryFee,
                    extraFee
                );


            // 4. Create International Shipment
            Console.WriteLine("\n--- International Shipment ---");

            Console.WriteLine("Tracking Code:");
            string internationalTrackingCode = Console.ReadLine();

            Console.WriteLine("Description:");
            string internationalDescription = Console.ReadLine();

            Console.WriteLine("Weight:");
            int internationalWeight = int.Parse(Console.ReadLine());

            Console.WriteLine("Delivery Fee:");
            decimal internationalDeliveryFee = decimal.Parse(Console.ReadLine());

            Console.WriteLine("Destination Country:");
            string destinationCountry = Console.ReadLine();

            Console.WriteLine("Customs Fee:");
            decimal customsFee = decimal.Parse(Console.ReadLine());

            InternationalShipment internationalShipment =
                new InternationalShipment(
                    internationalTrackingCode,
                    internationalDescription,
                    internationalWeight,
                    internationalDeliveryFee,
                    customsFee,
                    destinationCountry
                    
                );


            // 5. Add shipments to Delivery Center
            center.AddShipment(standardShipment);
            center.AddShipment(expressShipment);
            center.AddShipment(internationalShipment);


            // 6. Print all shipments
            Console.WriteLine("\n========== ALL SHIPMENTS ==========");
            center.PrintAllShipments();


            // 7. Search using the tracking-code indexer
            Console.WriteLine("\nEnter Tracking Code to Search:");
            string searchCode = Console.ReadLine();

            Shipment foundShipment = center[searchCode];

            if (foundShipment != null)
            {
                Console.WriteLine("\nShipment Found:");
                Console.WriteLine(foundShipment.PrintShipmentDetails());
            }
            else
            {
                Console.WriteLine("No Shipment Found!!");
            }


            // 8. Remove shipment
            Console.WriteLine("\nEnter Tracking Code to Remove:");
            string removeCode = Console.ReadLine();

            bool removed = center.RemoveShipment(removeCode);

            if (removed)
            {
                Console.WriteLine("Shipment Removed Successfully.");
            }
            else
            {
                Console.WriteLine("Shipment Not Found.");
            }


            // 9. Print remaining shipments
            Console.WriteLine("\n========== REMAINING SHIPMENTS ==========");
            center.PrintAllShipments();
        }
    }
}

