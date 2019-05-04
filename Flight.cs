using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/* 
 *  <flight>
               <code>”something like S7 or TA"</code>
               <flightNo>”123"</flightNo>
               <company>”S7 for example"</company>
               <from>”City of departure"</from>
               <to>”city of destination”</to>
               <charter>”true or false"</charter>
               <date>”if charter date like 01/02/2014 in other case one of regular cases like 'every day’s every Thursday"</date>
               //about regular cases:
                    //do with day of week
                    //every day
                    //every month (by day num)
               <time>”Time in UTC0"</time>
               <economPrice>”7500"</economPrice>
               <bussinesPrice>”18000"</bussinesPrice>
     </flight>
*/
namespace FlightSeller
{
    class Flight
    {
        public string m_code { get; private set; }
        public string m_company { get; private set; }
        public string m_departureCity { get; private set; }
        public string  m_arrivalCity { get; private set; }
        public bool m_charter { get; private set; }
        public string m_schedule { get; private set; }
        public DateTime m_departureTime { get; set; }
        public DateTime m_arrivalTime { get; set; }
        public double m_economyPrice { get; private set; }
        public double m_businessPrice { get; private set; }
        public bool m_isTranfer { get; private set; }
        public string m_transferCity { get; private set; }
        public DateTime m_departureTimeTransit { get; set; }
        public DateTime m_arrivalTimeTransit { get; set; }

        public Flight(string code,
                       string company,
                       string cityFrom,
                       string cityTo,
                       bool charter,
                       string schedule,
                       DateTime departureTime,
                       DateTime arrivalTime,
                       double economyPrice,
                       double businessPrice, 
                       bool isTranfer,
                       string trasferCity,
                       DateTime departureTimeTransit,
                       DateTime arrivalTimeTransit)
        {
            this.m_code = code;
            this.m_company = company;
            this.m_departureCity = cityFrom;
            this.m_arrivalCity = cityTo;
            this.m_charter = charter;
            this.m_schedule = schedule;
            this.m_departureTime = departureTime;
            this.m_arrivalTime = arrivalTime;
            this.m_economyPrice = economyPrice;
            this.m_businessPrice = businessPrice;
            this.m_isTranfer = isTranfer;
            this.m_transferCity = trasferCity;
            this.m_departureTimeTransit = departureTimeTransit;
            this.m_arrivalTimeTransit = arrivalTimeTransit;
        }
    }
}
