namespace OOP_2
{
   
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



        public struct Shipment
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
            public decimal EstimatedCost
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
        //Delivery Center Struct
        public struct DeliveryCenter
        {
            Shipment[] shipment;
            public DeliveryCenter()
            {
                shipment = new Shipment[10];
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
                        if (shipment[i].TrackingCode == trackingcode)
                        {
                            return shipment[i];
                        }

                    }
                    return default;
                }
            }
            //AddShipment Method
            public bool AddShipment(Shipment newshipment)
            {
                for (int i = 0; i < shipment.Length; i++)
                {
                    if (shipment[i].TrackingCode == null)
                    {
                        shipment[i] = newshipment;
                        return true;
                    }
                }
                return false;
            }
        }
        internal class Program
        {
            static void Main(string[] args)
            {
                DeliveryCenter center = new DeliveryCenter();
                //Reading Shipment Data and adding it to delivery center
                for (int i = 0; i < 3; i++)
                {
                    Console.WriteLine($"Enter Shipment{i + 1} data");
                    Console.Write("Tracking Code:");
                    string trackingcode = Console.ReadLine();
                    Console.Write("Description:");
                    string description = Console.ReadLine();
                    Console.Write("Weight:");
                    int weight = int.Parse(Console.ReadLine());
                    Console.Write("Delivery Fee:");
                    decimal deliveryfee = decimal.Parse(Console.ReadLine());
                    Shipment shipment = new Shipment(trackingcode, description, weight, deliveryfee);
                    center.AddShipment(shipment);
                    Console.WriteLine("\n\nShipment Added Successfully");

                }
                //Printing Shipments using integer indexer
                Console.WriteLine("---All Shipments");

                for (int i = 0; i < 3; i++)
                {
                    Console.WriteLine($"Shipment {i + 1} data");
                    Console.WriteLine(center[i].PrintShipmentDetails());
                    Console.WriteLine("////////////////////////////");
                }
                //Searching using shipment tracking code
                bool isShipmentfound = false;
                Console.WriteLine("Enter a Tracking Code to search:");
                string searchcode = Console.ReadLine();
                for (int i = 0; i < 3; i++)
                {
                    if (center[i].TrackingCode == searchcode)
                    {
                        isShipmentfound = true;
                        Console.WriteLine($"Shipment Found: {center[i].TrackingCode}");
                    }

                }
                if (!isShipmentfound)
                    Console.WriteLine("No Shipment Found!!");
                //Struct Copy Behaviour
                DeliveryAddress address01 = new DeliveryAddress("Cairo", "Mostafa Al Nahas", 53);
                DeliveryAddress address02 = address01;
                address02 = new DeliveryAddress("Freedom St.");
                Console.WriteLine(address01.GetFullAddress());
                Console.WriteLine(address02.GetFullAddress());
            }
        }
    }

