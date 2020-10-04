using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebCoreAPI.Models;

namespace WebCoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuyBookController : ControllerBase
    {
        public Double GetDeliveryCost(Double baseCost, string deliveryType)
        {

            Double deliveryCost = 0;
            DateTime current = DateTime.Now;
            switch (deliveryType)
            {
                case "Motobike":
                    if (current.Month >= 6 && current.Month <= 8)
                    {
                        deliveryCost = baseCost * 0.8;

                    }
                    else if (current.Month == 9)
                    {
                        deliveryCost = baseCost * 1.5;
                    }
                    else
                    {
                        deliveryCost = baseCost * 1;
                    }
                    return deliveryCost;

                case "Train":
                    if (current.Month >= 6 && current.Month <= 8)
                    {
                        deliveryCost = baseCost * 0.8;

                    }
                    else if (current.Month == 9)
                    {
                        deliveryCost = baseCost * 1.8;
                    }
                    else
                    {
                        deliveryCost = baseCost * 1;
                    }
                    return deliveryCost;
                case "Aircraft":
                    if (current.Month >= 6 && current.Month <= 8)
                    {
                        deliveryCost = baseCost * 0.8;

                    }
                    else if (current.Month == 9)
                    {
                        deliveryCost = baseCost * 2;
                    }
                    else
                    {
                        deliveryCost = baseCost * 1;
                    }
                    return deliveryCost;
                default:
                    break;
            }
            return deliveryCost;
        }

        [HttpGet]
        public List<BuyBookModel> GetDeliveryServices()
        {
            var services = new List<BuyBookModel>();
            services.Add(new BuyBookModel
            {
                DeliveryService = "Motobike",
                DeliveryCost = GetDeliveryCost(5, "Motobike")

            });
            services.Add(new BuyBookModel
            {
                DeliveryService = "Train",
                DeliveryCost = GetDeliveryCost(10, "Train")

            });
            services.Add(new BuyBookModel
            {
                DeliveryService = "Aircraft",
                DeliveryCost = GetDeliveryCost(20, "Aircraft")

            });
            return services;

        }

        public string DeliveryInfoGenerator(BuyBookModel submitData)
        {
            List<DriverModel> drivers = new List<DriverModel>();
            drivers.Add(new DriverModel { DriverName="David", PhoneNo="021020102"});
            drivers.Add(new DriverModel { DriverName = "Peter", PhoneNo = "021029902" });
            drivers.Add(new DriverModel { DriverName = "Tom", PhoneNo = "021340102" });
            drivers.Add(new DriverModel { DriverName = "John", PhoneNo = "021880102" });

            string [] stations = { "Newmarket", "Manukau", "Papatoetoe", "Otahuhu" };
            string [] gates = { "Auckland", "Christchurch", "Queenstown", "Tauranga" };

            DateTime currentDate = DateTime.Now;

            Random rand = new Random();
            string info = "";
            int randomIndex = 1;
            switch (submitData.DeliveryService)
            {
            
                case "Motobike":
                    randomIndex = rand.Next(drivers.Count);
                    info = String.Join(Environment.NewLine,
                        "Driver name: " + drivers[randomIndex].DriverName + ", ",
                        "Phone no: " + drivers[randomIndex].PhoneNo + ", ",
                        "Delevery date: " + currentDate.AddDays(1).ToString("dd/MM/yyyy" + ", "),
                        "Delivery cost: $" +submitData.DeliveryCost);
                    return info;

                case "Train":
                    string trainNo = "TRN"+ rand.Next(1000).ToString();
                    randomIndex = rand.Next(stations.Length);
                    info = String.Join(Environment.NewLine,
                        "Train no: " + trainNo + ", ",
                        "Station of arrival: " + stations[randomIndex] + ", ",
                        "Delevery date: " + currentDate.AddDays(2).ToString("dd/MM/yyyy") + ", ",
                        "Delivery cost: $" + submitData.DeliveryCost);
                    return info;

                case "Aircraft":
                    string flightNo = "AIR" + rand.Next(1000).ToString();
                    randomIndex = rand.Next(gates.Length);
                    info = String.Join(Environment.NewLine,
                        "Flight no: " + flightNo +", ",
                        "Gate of arrival: " + gates[randomIndex] + ", ",
                        "Delevery date: " + currentDate.AddDays(3).ToString("dd/MM/yyyy") + ", ",
                        "Delivery cost: $" + submitData.DeliveryCost);
                    return info;

                default:
                    break;

            }
            return info;
        }

        [HttpPost]
        public string BuyBook([FromBody] BuyBookModel submitData)
        {           
            var deleveryInfo = DeliveryInfoGenerator(submitData);
            return deleveryInfo;
        }
    }
}