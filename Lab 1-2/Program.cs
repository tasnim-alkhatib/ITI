using System;
using System.Collections.Generic;

namespace AviationTicketPricingEngine
{
    class FlightTicket
    {
        public int TicketId { get; set; }
        public string PassengerName { get; set; }
        public decimal BasePrice { get; set; }
        public DateTime FlightTime { get; set; }
        public bool IsDelayed { get; set; }
    }

    class DiscountRules
    {
        public static decimal DelayedFlightDiscount(FlightTicket ticket)
        {
            if (ticket.IsDelayed)
                return ticket.BasePrice * 0.80m;
            return ticket.BasePrice;
        }

        public decimal VipPassengerDiscount(FlightTicket ticket)
        {
            return ticket.BasePrice * 0.85m;
        }
    }

    class Program
    {
        static void ProcessTickets(List<FlightTicket> tickets, Func<FlightTicket, decimal> priceCalculator)
        {
            foreach (var ticket in tickets)
            {
                decimal finalPrice = priceCalculator(ticket);
                Console.WriteLine(
                    $"Passenger: {ticket.PassengerName} | Original: {ticket.BasePrice} | Final: {finalPrice}");
            }
        }

        static List<string> GetTicketSummaries(List<FlightTicket> tickets, Func<FlightTicket, string> formatter)
        {
            List<string> summaries = new List<string>();
            foreach (var ticket in tickets)
            {
                summaries.Add(formatter(ticket));
            }
            return summaries;
        }

        static List<FlightTicket> FilterTickets(List<FlightTicket> tickets, Predicate<FlightTicket> condition)
        {
            List<FlightTicket> result = new List<FlightTicket>();
            foreach (var ticket in tickets)
            {
                if (condition(ticket))
                    result.Add(ticket);
            }
            return result;
        }

        static void BroadcastNotification(FlightTicket ticket, Action<FlightTicket> notificationAction)
        {
            notificationAction(ticket);
        }

        static void Main(string[] args)
        {
            List<FlightTicket> tickets = new List<FlightTicket>
            {
                new FlightTicket { TicketId = 1, PassengerName = "Ahmed",  BasePrice = 500m,  FlightTime = new DateTime(2026, 8, 1, 9, 30, 0),  IsDelayed = true  },
                new FlightTicket { TicketId = 2, PassengerName = "Sara",   BasePrice = 1200m, FlightTime = new DateTime(2026, 8, 2, 14, 0, 0),  IsDelayed = false },
                new FlightTicket { TicketId = 3, PassengerName = "Omar",   BasePrice = 800m,  FlightTime = new DateTime(2026, 8, 3, 6, 45, 0),  IsDelayed = false },
                new FlightTicket { TicketId = 4, PassengerName = "Mona",   BasePrice = 1500m, FlightTime = new DateTime(2026, 8, 4, 20, 15, 0), IsDelayed = true  },
                new FlightTicket { TicketId = 5, PassengerName = "Karim",  BasePrice = 300m,  FlightTime = new DateTime(2026, 8, 5, 11, 0, 0),  IsDelayed = false },
            };

            Console.WriteLine("-- Broadcast Notification --");
            foreach (var ticket in tickets)
            {
                BroadcastNotification(ticket, t =>
                    Console.WriteLine($"[System Log] Processing ticket for {t.PassengerName}."));
            }

            Console.WriteLine("\n-- Delayed Flights Only --");
            List<FlightTicket> delayedTickets = FilterTickets(tickets, t => t.IsDelayed);
            foreach (var ticket in delayedTickets)
            {
                Console.WriteLine($"Delayed -> TicketId: {ticket.TicketId}, Passenger: {ticket.PassengerName}");
            }

            Console.WriteLine("\n-- Delayed Flight Discount (static method reference) --");
            ProcessTickets(tickets, DiscountRules.DelayedFlightDiscount);

            Console.WriteLine("\n-- VIP Passenger Discount (instance method reference) --");
            DiscountRules rules = new DiscountRules();
            ProcessTickets(tickets, rules.VipPassengerDiscount);

            Console.WriteLine("\n-- Holiday Special (10% off, inline lambda) --");
            ProcessTickets(tickets, t => t.BasePrice * 0.90m);

            Console.WriteLine("\n-- Ticket Summaries via GetTicketSummaries (like LINQ .Select()) --");
            List<string> summaries = GetTicketSummaries(tickets, t => $"{t.TicketId} - {t.PassengerName}");
            foreach (var summary in summaries)
            {
                Console.WriteLine(summary);
            }
        }
    }
}