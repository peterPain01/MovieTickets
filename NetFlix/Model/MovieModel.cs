using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Model
{
    public class MovieModel
    {
        private int _id; 
        private string _title;
        private int _genre_id;
        private int _duration_minutes;
        private DateTime _release_date;
        private double _rating;
        private int _certification;
        private string _plot_summary; 
        private string _poster_url; 
        private string _poster_vertical_url;
        private string _trailer_url; 
        private int _director_id;

        public MovieModel() { }

        public MovieModel(int id, string title, int genre_id,
            int duration_minutes, DateTime release_date, double rating, 
            int certification, string plot_summary, string poster_url, string poster_vertical_url,
            string trailer_url, int director_id)
        {
            _id = id;
            _title = title;
            _genre_id = genre_id;
            _duration_minutes = duration_minutes;
            _release_date = release_date;
            _rating = rating;
            _certification = certification;
            _plot_summary = plot_summary;
            _poster_url = poster_url;
            _poster_vertical_url = poster_vertical_url;
            _trailer_url = trailer_url;
            _director_id = director_id;
        }

        public int Id { get => _id; set => _id = value; }
        public string Title { get => _title; set => _title = value; }
        public int Genre_id { get => _genre_id; set => _genre_id = value; }
        public int Duration_minutes { get => _duration_minutes; set => _duration_minutes = value; }
        public DateTime Release_date { get => _release_date; set => _release_date = value; }
        public double Rating { get => _rating; set => _rating = value; }
        public int Certification { get => _certification; set => _certification = value; }
        public string Plot_summary { get => _plot_summary; set => _plot_summary = value; }
        public string Poster_url { get => _poster_url; set => _poster_url = value; }
        public string Poster_vertical_url { get => _poster_vertical_url; set => _poster_vertical_url = value; }
        public string Trailer_url { get => _trailer_url; set => _trailer_url = value; }
        public int Director_id { get => _director_id; set => _director_id = value; }
    }
}

