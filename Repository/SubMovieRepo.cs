using Microsoft.EntityFrameworkCore;
using Netflix.Repository;
using NetFlix.EnityModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetFlix.Repository
{
    public class SubMovieRepo
    {
        // Get Movie
        public Movie GetMovie(int id)
        {
            using (var context = new BookingMovieAppContext())
            {
                var movie = context.Movies.Where(mv => mv.MovieId == id)
                    .Include(mv => mv.Stars)
                    .Include(mv => mv.Director)
                    .Include(mv => mv.Genre)
                    .FirstOrDefault();
                return movie == null ? null : movie;
            }
        }

        // Get All Genres 
        public ObservableCollection<Genre> GetAllGenres()
        {
            using (var context = new BookingMovieAppContext())
            {
                var genres = context.Genres.ToList();
                return new ObservableCollection<Genre>(genres);
            }
        }
        public void UpdateGenreForMovie(int movieId, int GenreId)
        {
            using (var context = new BookingMovieAppContext())
            {
                var mv = context.Movies.Where(mv => mv.MovieId == movieId).FirstOrDefault();

                if (mv != null)
                {
                    mv.GenreId = GenreId;
                    context.SaveChanges();
                }
            }
        }

        // Get All Stars of particlar Movie 
        public ObservableCollection<Star> GetAllStarMovie(int movieId)
        {
            using (var context = new BookingMovieAppContext())
            {
                var stars = context.Stars.Where(s => s.Movies.Any(m => m.MovieId == movieId)).ToList();
                return new ObservableCollection<Star>(stars);
            }
        }

        // Get all stars 
        public ObservableCollection<Star> GetAllStar()
        {
            using (var context = new BookingMovieAppContext())
            {
                var stars = context.Stars.ToList();
                return new ObservableCollection<Star>(stars);
            }
        } 
        
        // Get all director
        public ObservableCollection<Director> GetAllDirector()
        {
            using (var context = new BookingMovieAppContext())
            {
                var dir = context.Directors.ToList();
                return new ObservableCollection<Director>(dir);
            }
        }

        // Get Particular Star
        public Star GetStar(int StarId)
        {
            using (var context = new BookingMovieAppContext())
            {
                var star = context.Stars.FirstOrDefault(s => s.StarId == StarId);
                return star;
            }
        }



        // Add Star
        public Star AddStar(Star newStar)
        {
            using (var context = new BookingMovieAppContext())
            {
                var star = new Star { Name =  newStar.Name };
                context.Stars.Add(star);
                context.SaveChanges(); 
                return star;
            }
        }
        // Add Existing Star (cover later) 
        // Add New Star 
        public void AddStarToMovie(int movieId, Star newStar)
        {
            using (var context = new BookingMovieAppContext())
            {
                Star star = context.Stars.FirstOrDefault(s => s.StarId == newStar.StarId);
                Movie mv = context.Movies.FirstOrDefault(mv => mv.MovieId == movieId);
                if (star != null && mv != null)
                {
                    star.Movies.Add(mv);
                    context.Stars.Update(star);
                    context.SaveChanges();
                }
            }
        }

        // Add Director
        // Add Existing Director (cover later) 
        // Add New Star 
        public void AddDirectorToMovie(int movieId, Director newDirector)
        {
            using (var context = new BookingMovieAppContext())
            {
                context.Directors.Add(newDirector);
                context.SaveChanges();
            }
        }

        // Delete Star
        public void DeleteStarFromMovie(int movieId, int starId)
        {
            using (var context = new BookingMovieAppContext())
            {
                var mv = context.Movies.Where(mv => mv.MovieId == movieId).Include(mv => mv.Stars).FirstOrDefault();
                if (mv != null)
                {
                    var star = mv.Stars.FirstOrDefault(s => s.StarId == starId);
                    if (star != null)
                    {
                        mv.Stars.Remove(star);
                        context.SaveChanges();
                    }
                }
            }
        }

        // Delete Director
        public void DeleteDirectorFromMovie(int movieId, int directorId)
        {
            using (var context = new BookingMovieAppContext())
            {
                var director = context.Directors.FirstOrDefault(d => d.DirectorId == directorId && d.MoviesNavigation.Any(mv => mv.MovieId == movieId));
                if (director != null)
                {
                    context.Directors.Remove(director);
                    context.SaveChanges();
                }
            }
        }

        public Star AddNewStarToMovie(int movieId, int StarId)
        {
            using (var context = new BookingMovieAppContext())
            {
                var movie = context.Movies.Where(mv => mv.MovieId == movieId).FirstOrDefault();
                var star = context.Stars.Where(s => s.StarId == StarId).FirstOrDefault();
                if (movie != null && star != null)
                {
                    movie.Stars.Add(star);
                    context.SaveChanges();
                }
                return star;
            }
        }
        // Update Star (only update name)
        public void EditStar(Star newStar)
        {
            using (var context = new BookingMovieAppContext())
            {
                var star = context.Stars.FirstOrDefault(s => s.StarId == newStar.StarId);
                if (star != null)
                {
                    star.Name = newStar.Name;
                }
                context.SaveChanges();
            }
        }

        // Update Director (only update name)
        public void EditDirector(int movieId, Director newDirector)
        {
            using (var context = new BookingMovieAppContext())
            {
                var director = context.Directors.FirstOrDefault(d => d.DirectorId == newDirector.DirectorId && d.Movies.Any(mv => mv.MovieId == movieId));
                if (director != null)
                {
                    director.Name = newDirector.Name;
                }
                context.SaveChanges();
            }
        }

        // Genres 

        // Delete Star 
        public void DeleteStar(Star starDelete)
        {
            using (var context = new BookingMovieAppContext())
            {
                var star = context.Stars.FirstOrDefault(st => st.StarId == starDelete.StarId); 
                if(star != null)
                {
                    context.Stars.Remove(star); 
                    context.SaveChanges() ;
                }
            }
        }

        public void DeleteDirector(Director dirDelete)
        {
            using (var context = new BookingMovieAppContext())
            {
                var dir = context.Directors.FirstOrDefault(dir => dir.DirectorId == dirDelete.DirectorId);
                if (dir != null)
                {
                    context.Directors.Remove(dir);
                    context.SaveChanges();
                }
            }
        }
    }
}
