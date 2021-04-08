﻿using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public class HoursSqlDAO : IHoursDAO
    {
        private readonly string connectionString;
        private const string SQL_GET_HOURS = "SELECT * FROM hours;";
        private const string SQL_CREATE_HOURS = @"INSERT INTO hours (brewery_id, day_of_week, open_time, close_time) VALUES (@breweryId, @dayOfWeek, @openTime, @closeTime);
                                                    SELECT * FROM hours WHERE hours_id = @@IDENTITY;";
        private const string SQL_UPDATE_HOURS = @"UPDATE hours SET brewery_id = @breweryId, day_of_week = @dayOfWeek, open_time = @openTime, close_time = @closeTime;
                                                    SELECT * FROM hours WHERE hours_id = @hoursId;";

        public HoursSqlDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Hours> GetHours()
        {
            List<Hours> hoursList = new List<Hours>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GET_HOURS, conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Hours hours = RowToObject(reader);
                        hoursList.Add(hours);
                    }

                    return hoursList;
                }
            }
            catch (SqlException ex)
            {

                throw;
            }
        }


        public Hours CreateHours(Hours hours)
        {
            Hours createdHours = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_CREATE_HOURS, conn);
                    cmd.Parameters.AddWithValue("@breweryId", hours.BreweryId);
                    cmd.Parameters.AddWithValue("@dayOfWeek", hours.DayOfWeek);
                    cmd.Parameters.AddWithValue("@openTime", hours.OpenTime);
                    cmd.Parameters.AddWithValue("@closeTime", hours.CloseTime);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        createdHours = RowToObject(reader);
                    }

                    return createdHours;

                }
            }
            catch (SqlException ex)
            {

                throw;
            }
        }


        public Hours UpdateHours(Hours hours)
        {
            Hours updatedHours = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_UPDATE_HOURS, conn);
                    cmd.Parameters.AddWithValue("@hoursId", hours.HoursId);
                    cmd.Parameters.AddWithValue("@breweryId", hours.BreweryId);
                    cmd.Parameters.AddWithValue("@dayOfWeek", hours.DayOfWeek);
                    cmd.Parameters.AddWithValue("@openTime", hours.OpenTime);
                    cmd.Parameters.AddWithValue("@closeTime", hours.CloseTime);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        updatedHours = RowToObject(reader);
                    }

                    return updatedHours;

                }
            }
            catch (SqlException ex)
            {

                throw;
            }
        }

        private static Hours RowToObject(SqlDataReader reader)
        {
            Hours hours = new Hours();

            hours.HoursId = Convert.ToInt32(reader["hours_id"]);
            hours.BreweryId = Convert.ToInt32(reader["brewery_id"]);
            hours.DayOfWeek = Convert.ToInt32(reader["day_of_week"]);
            hours.OpenTime = Convert.ToDateTime(reader["open_time"]);
            hours.CloseTime = Convert.ToDateTime(reader["close_time"]);

            return hours;
        }
    }
}