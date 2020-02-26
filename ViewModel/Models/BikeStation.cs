using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;

namespace BikeWatcher.Models
{



    public class BikeStation
    {

        public BikeStation()
        {

        }

        public BikeStation(BikeStationBdx bikeStationBdx)
        {
            this.gid = bikeStationBdx.id.ToString();
            this.lat = bikeStationBdx.latitude;
            this.lng = bikeStationBdx.longitude;
            this.address = "Pas d'adresse dispo";
            this.name = bikeStationBdx.name;
            this.available_bikes = bikeStationBdx.bike_count.ToString();
            this.bike_stands = bikeStationBdx.slot_count.ToString();

            if (bikeStationBdx.is_online)
            {
                this.status = "OPEN";
            }
            else
            {
                this.status = "CLOSE";

            }


        }





        public string number { get; set; }
        public string pole { get; set; }
        public string available_bikes { get; set; }
        public string code_insee { get; set; }
        public string lng { get; set; }
        public string availability { get; set; }
        public string availabilitycode { get; set; }
        public string etat { get; set; }
        public string startdate { get; set; }
        public string langue { get; set; }
        public string bike_stands { get; set; }
        public string last_update { get; set; }
        public string available_bike_stands { get; set; }
        public string gid { get; set; }
        public string titre { get; set; }
        public string status { get; set; }
        public string commune { get; set; }
        public string description { get; set; }
        public string nature { get; set; }
        public string bonus { get; set; }
        public string address2 { get; set; }
        public string address { get; set; }
        public string lat { get; set; }
        public string last_update_fme { get; set; }
        public string enddate { get; set; }
        public string name { get; set; }
        public string banking { get; set; }
        public string nmarrond { get; set; }
    }


    
    

}
