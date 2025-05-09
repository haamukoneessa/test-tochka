using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;


class HotelCapacity
{
    static bool CheckCapacity(int maxCapacity, List<Guest> guests)
    {
        SortedDictionary<DateTime, int> changesOccupation = new SortedDictionary<DateTime, int>();
        for (int i = 0; i < guests.Count; i++)
        {
            DateTime checkInDate = DateTime.Parse(guests[i].CheckIn);
            DateTime checkOutDate = DateTime.Parse(guests[i].CheckOut);

            if (changesOccupation.ContainsKey(checkInDate)) changesOccupation[checkInDate]++;
            else changesOccupation[checkInDate] = 1;

            if (changesOccupation.ContainsKey(checkOutDate)) changesOccupation[checkOutDate]--;
            else changesOccupation[checkOutDate] = -1;

            int countOccupiedRoom = 0;

            foreach (var item in changesOccupation)
            {
                countOccupiedRoom += item.Value;
                if (countOccupiedRoom > maxCapacity) return false;
            }
        }
        return true; // или false
    }


    class Guest
    {
        public string Name { get; set; }
        public string CheckIn { get; set; }
        public string CheckOut { get; set; }
    }


    static void Main()
    {
        int maxCapacity = int.Parse(Console.ReadLine());
        int n = int.Parse(Console.ReadLine());


        List<Guest> guests = new List<Guest>();


        for (int i = 0; i < n; i++)
        {
            string line = Console.ReadLine();
            Guest guest = ParseGuest(line);
            guests.Add(guest);
        }


        bool result = CheckCapacity(maxCapacity, guests);


        Console.WriteLine(result ? "True" : "False");
    }


    // Простой парсер JSON-строки для объекта Guest
    static Guest ParseGuest(string json)
    {
        var guest = new Guest();


        // Извлекаем имя
        Match nameMatch = Regex.Match(json, "\"name\"\\s*:\\s*\"([^\"]+)\"");
        if (nameMatch.Success)
            guest.Name = nameMatch.Groups[1].Value;


        // Извлекаем дату заезда
        Match checkInMatch = Regex.Match(json, "\"check-in\"\\s*:\\s*\"([^\"]+)\"");
        if (checkInMatch.Success)
            guest.CheckIn = checkInMatch.Groups[1].Value;


        // Извлекаем дату выезда
        Match checkOutMatch = Regex.Match(json, "\"check-out\"\\s*:\\s*\"([^\"]+)\"");
        if (checkOutMatch.Success)
            guest.CheckOut = checkOutMatch.Groups[1].Value;


        return guest;
    }
}