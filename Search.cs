using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FlightSeller
{
    class Search
    {
        public List<Flight> FindFlights(string cityFrom, string cityTo, DateTime flightDate)
        {
            List<Flight> flightList = new List<Flight>();
            XmlDocument flightsDoc = new XmlDocument();
            flightsDoc.Load(@"..\..\Flights.xml");

            XmlNodeList flights = flightsDoc.DocumentElement.SelectNodes("/flights/flight");

            foreach (XmlNode flight in flights)
            {
                Flight currentFlight = GetFlight(flight);
                bool isCitiesMatch = currentFlight.m_departureCity == cityFrom &&
                                                             currentFlight.m_arrivalCity == cityTo;

                bool isScheduleMatch = (currentFlight.m_schedule == "EVERY_DAY" ||
                           ConvertToWeekDay(currentFlight.m_schedule) == flightDate.DayOfWeek);

                if (isCitiesMatch && isScheduleMatch)
                    flightList.Add(currentFlight);
            }

            foreach (XmlNode flight in flights)
            {
                Flight currentFlight = GetFlight(flight);
                bool isCitiesMatch = currentFlight.m_departureCity == cityFrom && currentFlight.m_departureCity != currentFlight.m_arrivalCity;

                bool isScheduleMatch = (currentFlight.m_schedule == "EVERY_DAY" ||
                           ConvertToWeekDay(currentFlight.m_schedule) == flightDate.DayOfWeek);

                if (isCitiesMatch && isScheduleMatch)
                foreach (XmlNode tFlight in flights)
                {
                    Flight currentTFlight = GetFlight(tFlight);
                     isCitiesMatch = currentTFlight.m_departureCity == currentFlight.m_arrivalCity 
                                    && currentTFlight.m_arrivalCity == cityTo;

                    isScheduleMatch = (currentTFlight.m_schedule == "EVERY_DAY" ||
                               ConvertToWeekDay(currentTFlight.m_schedule) == flightDate.DayOfWeek);

                    if (isCitiesMatch && isScheduleMatch)
                    {
                        Flight resultFlight = new Flight(currentFlight.m_code + " " + currentTFlight.m_code, 
                      currentFlight.m_company + " " + currentTFlight.m_company,
                      currentFlight.m_departureCity, 
                      currentTFlight.m_arrivalCity, 
                      false, currentFlight.m_schedule,
                      currentFlight.m_departureTime, 
                      currentFlight.m_arrivalTime, 
                      currentFlight.m_economyPrice + currentTFlight.m_economyPrice,
                      currentTFlight.m_businessPrice + currentTFlight.m_businessPrice,
                      true, currentFlight.m_arrivalCity, 
                      currentTFlight.m_departureTime,
                      currentTFlight.m_arrivalTime);
                        flightList.Add(resultFlight);
                    }
                }
            }
            return flightList;
        }


        public List<Flight> FindFlights(string code)
        {
            List<Flight> flightList = new List<Flight>();
            XmlDocument flightsDoc = new XmlDocument();
            flightsDoc.Load(@"..\..\Flights.xml");

            XmlNodeList flights = flightsDoc.DocumentElement.SelectNodes("/flights/flight");
            foreach (XmlNode flight in flights)
            {
                Flight currentFlight = GetFlight(flight);
                if (currentFlight.m_code == code)
                    flightList.Add(currentFlight);
            }
            return flightList;
        }

        private Flight GetFlight(XmlNode flight)
        {
            string from = flight.SelectSingleNode("departureCity").InnerText;
            string to = flight.SelectSingleNode("arrivalCity").InnerText;
            bool isCharter = Convert.ToBoolean(flight.SelectSingleNode("charter").InnerText);
            string schedule = flight.SelectSingleNode("schedule").InnerText;
            DateTime departureTime = Convert.ToDateTime(flight.SelectSingleNode("departureTime").InnerText);
            DateTime arrivalTime = Convert.ToDateTime(flight.SelectSingleNode("arrivalTime").InnerText);

            string code = flight.SelectSingleNode("code").InnerText;
            string company = flight.SelectSingleNode("company").InnerText;
            double economyPrice = Convert.ToDouble(flight.SelectSingleNode("economyPrice").InnerText);
            double businessPrice = Convert.ToDouble(flight.SelectSingleNode("businessPrice").InnerText);
            return new Flight(code, company, from, to, isCharter, schedule, departureTime, arrivalTime, economyPrice, businessPrice, false, null, new DateTime(), new DateTime());
        }

        private DayOfWeek ConvertToWeekDay(string scheduleStr)
        {
            if (scheduleStr == "EVERY_MONDAY")
                return DayOfWeek.Monday;
            if (scheduleStr == "EVERY_TUESDAY")
                return DayOfWeek.Tuesday;
            if (scheduleStr == "EVERY_WEDNESDAY")
                return DayOfWeek.Wednesday;
            if (scheduleStr == "EVERY_THURSDAY")
                return DayOfWeek.Thursday;
            if (scheduleStr == "EVERY_FRIDAY")
                return DayOfWeek.Friday;
            if (scheduleStr == "EVERY_SATURDAY")
                return DayOfWeek.Saturday;
            if (scheduleStr == "EVERY_SUNDAY")
                return DayOfWeek.Sunday;
            throw new System.ArgumentException();
        }
    }
}
