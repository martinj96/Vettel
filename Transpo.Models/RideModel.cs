using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Transpo.Models
{
    public class Point
    {
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Name { get; set; }
    }
    public class Time
    {
        public int Hour { get; set; }
        public int Minutes { get; set; }
    }

    public class RideModel
    {
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }
        public decimal? PricePerPassenger { get; set; }
		public string PricePerPassengerText
		{
			get
			{
				return "Цена: " + PricePerPassenger + " MKD";
			}
		}
        public int? SeatsLeft { get; set; }
		public string SeatsLeftText
		{
			get
			{
				return "Места: " + SeatsLeft;
			}
		}
        public decimal? Length { get; set; }
        public int? Detour { get; set; }
        public DateTime DepartureDate { get; set; }
		public string DepartureDateText
		{
			get
			{
				return "Поаѓање: " + DepartureDate.ToString("dd-MMM-yyyy HH:mm", new CultureInfo("mk-MK"));
			}
		}

		public string DepartureDateSimpleText
		{
			get
			{
				return DepartureDate.ToString("dd-MMM-yyyy HH:mm", new CultureInfo("mk-MK"));
			}
		}

        public Time TimeDeparture { get; set; }
        public string Description { get; set; }
		public string DescriptionText
		{
			get
			{
				return "Опис: " + Description;
			}
		}
        public ICollection<Point> Waypoints { get; set; }
        public DateTime? ReturnDepartureDate { get; set; }
        public Time ReturnTimeDeparture { get; set; }
        public UserViewModel Driver { get; set; }
        public int RideId { get; set; }
        public string ReturnRide { get; set; }

        public RideModel() { }

    }
}