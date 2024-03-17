using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Model
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int DurationMinutes { get; set; }
        public DateTime ReleaseDate { get; set; }
        public double Rating { get; set; }
        public int Certification { get; set; }
        public string PlotSummary { get; set; }
        public string PosterUrl { get; set; }
        public string PosterVerticalUrl { get; set; }
        public string TrailerUrl { get; set; }
        public int DirectorId { get; set; }
        public Movie() { }
        public Movie(int id, string title, int genreId,
            int durationMinutes, DateTime releaseDate, double rating,
            int certification, string plotSummary, string posterUrl, string posterVerticalUrl,
            string trailerUrl, int directorId)
        {
            Id = id;
            Title = title;
            GenreId = genreId;
            DurationMinutes = durationMinutes;
            ReleaseDate = releaseDate;
            Rating = rating;
            Certification = certification;
            PlotSummary = plotSummary;
            PosterUrl = posterUrl;
            PosterVerticalUrl = posterVerticalUrl;
            TrailerUrl = trailerUrl;
            DirectorId = directorId;
        }
    }
}

