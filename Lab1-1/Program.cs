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

    delegate decimal DiscountCalculator(FlightTicket ticket);

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
        static void ProcessTickets(List<FlightTicket> tickets, DiscountCalculator calculator)
        {
            foreach (var ticket in tickets)
            {
                decimal finalPrice = calculator(ticket);
                Console.WriteLine(
                    $"Passenger: {ticket.PassengerName,-10} | Original: {ticket.BasePrice,8:C} | Final: {finalPrice,8:C}");
            }
        }

        delegate void NotificationSender(FlightTicket ticket);

        static void SendEmail(FlightTicket ticket)
        {
            Console.WriteLine($"[Email] Dear {ticket.PassengerName}, your ticket (ID {ticket.TicketId}) has been processed.");
        }
        static void SendSMS(FlightTicket ticket)
        {
            Console.WriteLine($"[SMS] Hi {ticket.PassengerName}, ticket #{ticket.TicketId} update available.");
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


            Console.WriteLine("-- Delayed Flight Discount (static method) --");
            ProcessTickets(tickets, DiscountRules.DelayedFlightDiscount);

            Console.WriteLine("\n-- VIP Passenger Discount (instance method) --");
            DiscountRules rules = new DiscountRules();
            ProcessTickets(tickets, rules.VipPassengerDiscount);

            Console.WriteLine("\n-- Holiday Special (10% off) --");
            ProcessTickets(tickets, delegate (FlightTicket t)
            {
                return t.BasePrice * 0.90m;
            });

            Console.WriteLine("\n-- Morning Flight Discount (5% before noon) --");
            ProcessTickets(tickets, t => t.FlightTime.Hour < 12 ? t.BasePrice * 0.95m : t.BasePrice);

            Console.WriteLine("\n-- Premium Ticket Discount ($50 off if price > $1000) --");
            ProcessTickets(tickets, t => t.BasePrice > 1000m ? t.BasePrice - 50m : t.BasePrice);

            Console.WriteLine("\n-- Multicast Delegate Notifications --");
            NotificationSender notifier = SendEmail;
            notifier += SendSMS;

            notifier.Invoke(tickets[0]);
        }
    }
}