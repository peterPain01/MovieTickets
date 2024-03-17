use master
DROP DATABASE IF EXISTS BookingMovieApp;
CREATE DATABASE BookingMovieApp;
GO
use BookingMovieApp
GO
CREATE TABLE Users (
    id INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    username VARCHAR(50) NOT NULL,
    email VARCHAR(100),
    password VARCHAR(100) NOT NULL,
    full_name VARCHAR(100),
    birth_date DATE,
	is_admin bool, 
    gender VARCHAR(10) CHECK (gender IN ('Male', 'Female', 'Other')),
);

CREATE TABLE Genres (
    genre_id INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    name VARCHAR(100) NOT NULL
);

CREATE TABLE Directors (
    director_id INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    name VARCHAR(100) NOT NULL
);

CREATE TABLE Stars (
    star_id INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    name VARCHAR(100) NOT NULL
);

CREATE TABLE Movies (
    movie_id INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    title NVARCHAR(255) NOT NULL,
    genre_id INT,
    duration_minutes INT,
    release_date DATE,
    rating DECIMAL(3, 1),
	Certification int, 
    plot_summary TEXT,
    poster_url VARCHAR(255),
    poster_vertical_url VARCHAR(255),
    trailer_url VARCHAR(255), 
	director_id INT,
    FOREIGN KEY (director_id) REFERENCES Directors(director_id),
	FOREIGN KEY (genre_id) REFERENCES Genres(genre_id)
);

CREATE TABLE MovieStars (
    movie_id INT,
    star_id INT,
    PRIMARY KEY (movie_id, star_id),
    FOREIGN KEY (movie_id) REFERENCES Movies(movie_id),
    FOREIGN KEY (star_id) REFERENCES Stars(star_id)
);

CREATE TABLE Cinema(
	cinema_id INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	name varchar(32), 
)

-- to upgrade: cinema fields, custom chairs for each show time
-- PRIMARY KEY(movie_id,cinema_id, showtime_datetime )
CREATE TABLE Showtimes (
    showtime_id INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    movie_id INT,
	cinema_id INT, 
    showtime_datetime DATETIME,
    FOREIGN KEY (movie_id) REFERENCES Movies(movie_id),
    FOREIGN KEY (cinema_id) REFERENCES Cinema(cinema_id)
);

CREATE TABLE Seats(
	row char(1), 
	number int, 
	cinema_id int, 
	showtime_id int, 
	status VARCHAR(12) CHECK (status IN ('Available', 'Sold')), 
	price int, -- decimal to upgrade
	PRIMARY KEY (row, number, cinema_id, showtime_id), 
    FOREIGN KEY (cinema_id) REFERENCES Cinema(cinema_id), 
    FOREIGN KEY (showtime_id) REFERENCES Showtimes(showtime_id)
)

CREATE TABLE Bookings (
    booking_id INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    user_id INT,
    showtime_id INT,
    booking_datetime DATETIME,
	transfer int, 
    FOREIGN KEY (user_id) REFERENCES Users(id),
    FOREIGN KEY (showtime_id) REFERENCES Showtimes(showtime_id)
);

INSERT INTO Users (username, email, password, full_name, birth_date, gender)
VALUES
('huypeter', 'john@example.com', '123', 'Huy Pham', '2003-01-01', 'Male'),
('txb', 'jane@example.com', '123', 'Bach Xuan', '1985-08-2', 'Female'),
('alex_jones', 'alex@example.com', '789', 'Alex Jones', '1978-12-10', 'Other');

-- Genres
INSERT INTO Genres (name)
VALUES
('Action'),
('Comedy'),
('Drama');

-- Directors
INSERT INTO Directors (name)
VALUES
('Director 1'),
('Director 2'),
('Director 3');

-- Stars
INSERT INTO Stars (name)
VALUES
('Star 1'),
('Star 2'),
('Star 3');

-- Movies
INSERT INTO Movies (title, genre_id, duration_minutes, release_date, rating, Certification, plot_summary, poster_url, trailer_url, director_id)
VALUES
('Movie 1', 1, 120, '2023-01-01', 8.5, 1, 'Plot summary for Movie 1', 'poster1.jpg', 'trailer1.mp4', 1),
('Movie 2', 2, 110, '2023-02-01', 7.8, 2, 'Plot summary for Movie 2', 'poster2.jpg', 'trailer2.mp4', 2),
('Movie 3', 3, 130, '2023-03-01', 6.9, 3, 'Plot summary for Movie 3', 'poster3.jpg', 'trailer3.mp4', 3),
('Fast and Furious 11', 1, 120, '2023-01-01', 8.5, 1, 'Plot summary for Movie 1', '/Images/slider-faf.jpg','/Images/mv-faf.jpg', 'trailer1.mp4', 1);
-- MovieStars
INSERT INTO MovieStars (movie_id, star_id)
VALUES
(1, 1),
(1, 2),
(2, 2),
(3, 3);

-- Cinema
INSERT INTO Cinema (name)
VALUES
('Cinema 1'),
('Cinema 2'),
('Cinema 3');

-- Showtimes
INSERT INTO Showtimes (movie_id, cinema_id, showtime_datetime)
VALUES
(1, 1, '2023-01-01 15:00:00'),
(2, 2, '2023-02-01 16:00:00'),
(3, 3, '2023-03-01 17:00:00');

-- Seats
INSERT INTO Seats (row, number, cinema_id, showtime_id, status, price)
VALUES
('A', 1, 1, 1, 'Available', 10),
('A', 2, 1, 1, 'Available', 10),
('B', 1, 2, 2, 'Available', 12),
('B', 2, 2, 2, 'Available', 12),
('C', 1, 3, 3, 'Available', 15),
('C', 2, 3, 3, 'Available', 15);

-- Bookings
INSERT INTO Bookings (user_id, showtime_id, booking_datetime, transfer)
VALUES
(1, 1, '2023-01-01 14:00:00', 0),
(2, 2, '2023-02-01 15:00:00', 0),
(1, 3, '2023-03-01 16:00:00', 0);



