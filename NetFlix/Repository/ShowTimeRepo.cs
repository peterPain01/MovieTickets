﻿using NetFlix.EnityModel;
using NetFlix.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace NetFlix.Repository
{
    public class ShowTimeRepo : RepositoryBase
    {
        public ObservableCollection<Showtime> GetShowTimeByMovieId(int movieId)
        {
            using (var context = new BookingMovieAppContext())
            {
                try
                {
                    var showtimes = context.Showtimes
                                           .Where(st => st.MovieId == movieId)
                                           .Select(st => new Showtime
                                           {
                                               ShowtimeId = st.ShowtimeId,
                                               ShowtimeDatetime = st.ShowtimeDatetime,
                                               Cinema = new Cinema
                                               {
                                                   CinemaId = st.Cinema.CinemaId,
                                                   Name = st.Cinema.Name
                                               }
                                           })
                                           .ToList();

                    return new ObservableCollection<Showtime>(showtimes);
                }
                catch (Exception ex)
                {
                    // Handle or log the exception
                    MessageBox.Show("An error occurred while fetching showtimes: " + ex.Message);
                    return new ObservableCollection<Showtime>();
                }
            }
        }

        public ObservableCollection<Showtime> GetAllShowTime()
        {
            using (var context = new BookingMovieAppContext())
            {
                try
                {
                    var showtimes = context.Showtimes.ToList();
                    return new ObservableCollection<Showtime>(showtimes);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while fetching all showtimes: " + ex.Message);
                    return new ObservableCollection<Showtime>();
                }
            }
        }

        public void InsertShowtime(Showtime showtime)
        {
            using (var context = new BookingMovieAppContext())
            {
                try
                {
                    context.Showtimes.Add(showtime);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while creating showtime: " + ex.Message);
                }
            }

        }
        public void DeleteShowtime(int id)
        {
            using (var context = new BookingMovieAppContext())
            {
                try
                {
                    var showtime = context.Showtimes.FirstOrDefault(st => st.ShowtimeId == id);
                    if (showtime != null)
                    {
                        context.Remove(showtime);
                        context.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while deleting showtime: " + ex.Message);
                }
            }
        }

        public void UpdateShowtime(Showtime newShowTime)
        {
            using (var context = new BookingMovieAppContext())
            {
                try
                {
                    var showtime = context.Showtimes.FirstOrDefault(st => st.ShowtimeId == newShowTime.ShowtimeId);
                    if (showtime != null)
                    {
                        showtime.MovieId = newShowTime.MovieId;
                        showtime.CinemaId = newShowTime.CinemaId;
                        showtime.ShowtimeDatetime = newShowTime.ShowtimeDatetime;
                        context.SaveChanges();

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while updating showtime: " + ex.Message);
                }
            }

        }

        public ObservableCollection<Seat> GetSeatByShowtimeId(int showtimeId)
        {
            using (var context = new BookingMovieAppContext())
            {
                try
                {
                    var seats = context.Seats
                                      .Where(s => s.ShowtimeId == showtimeId)
                                      .ToList();
                    return new ObservableCollection<Seat>(seats);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while fetching seats: " + ex.Message);
                    return new ObservableCollection<Seat>();
                }
            }
        }
    }
}
