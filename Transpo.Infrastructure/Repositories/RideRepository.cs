﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transpo.Infrastructure.Data.Entities;
using Transpo.Infrastructure.Data.Interfaces;

namespace Transpo.Infrastructure.Data.Repositories
{
    public class RideRepository : BaseRepository<Ride>, IRideRepository
    {
        public RideRepository(TranspoDbContext context)
            : base(context)
        {

        }

        public ICollection<Ride> GetRides(User user)
        {
            return _context.Rides.Where(r => r.Driver.id == user.id && r.Active == true).ToList();
        }

        public ICollection<Ride> GetRides(ICollection<CriticalPoint> criticalPoints, decimal radius)
        {
            ArrayList parameters = new ArrayList();
            
            string selectPoints = "select id from CriticalPoints where ";
            string selectRides = "select r.*, oc.[Order] from Rides as r join OrderedCriticalPoints as oc on r.id=oc.RideId where r.Departure>@Now AND r.Active=1 AND ";
            string finalSelect = "select r0.id as id, r0.PricePerPassenger as PricePerPassenger, r0.SeatsLeft as SeatsLeft, r0.Length as Length, r0.MinPrice as MinPrice, r0.MaxPrice as MaxPrice,  r0.Detour as Detour, r0.Departure as Departure, r0.Description as Description, r0.DateCreated as DateCreated, r0.Active as Active, r0.DriverId as DriverId from ";
            string join = "";
            string where = " where";
            parameters.Add(new SqlParameter("@Now", DateTime.Now));
            parameters.Add(new SqlParameter("@Radius", radius));

            for (int i = 0; i < criticalPoints.Count; i++)
            {
                CriticalPoint point = criticalPoints.ElementAt(i);

                string points = selectPoints +
                                "(6371 * acos(" +
                                      "cos(radians(@Latitude" + i + "))" +
                                      "* cos(radians(Latitude))" +
                                      "* cos(radians(Longitude) - radians(@Longitude" + i + "))" +
                                      "+ sin(radians(@Latitude" + i + "))" +
                                      "* sin(radians(Latitude))" +
                                      ") <= @Radius )";

                //string points = selectPoints + "(Longitude-@Longitude" + i + ")*(Longitude-@Longitude" + i +
                //    ")+(Latitude-@Latitude" + i + ")*(Latitude-@Latitude" + i + ")<=@Radius*@Radius";
                parameters.Add(new SqlParameter("@Longitude" + i, point.Longitude));
                parameters.Add(new SqlParameter("@Latitude" + i, point.Latitude));

                string rides = "(" + selectRides + "oc.CriticalPointId in (" + points + "))";
                if (i != 0)
                    join += " join ";
                join += rides + " as r" + i;
                if (i != 0)
                {
                    join += " on r" + i + ".id=r" + (i - 1) + ".id";
                    where += " r" + (i - 1) + ".[Order]<r" + i + ".[Order]";
                    if (i != criticalPoints.Count - 1)
                        where += " AND ";
                }
            }

            string query = finalSelect + join + where;
            return _context.Database.SqlQuery<Ride>(query, parameters.ToArray()).ToList();
        }
    }
}
