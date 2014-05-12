using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace CodeFirst
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (var movieDbObj = new MovieDBContext())
                {
                    var movieObj = new Movie();
                    movieObj.Title = "The Dark KNight Rises";
                    movieObj.ReleaseYear = 2012;
                    movieObj.Plot = " Eight years after the Joker's reign of anarchy, " +
                                    " the Dark Knight must return to defend Gotham City" +
                                    " against the enigmatic jewel thief Catwoman and the ruthless mercenary" + 
                                    " Bane as the city teeters on the brink of complete annihilation.";
                    Genre movieGen = new Genre() { GenreName = "Action | Thriller" };
                    Country movieCountry = new Country() { CountryName = "USA" };
                    Producer prod = new Producer() { Name = "Christopher Nolan" };
                    
                    
                    Actor actorBale = new Actor(){ Name = "Christian Bale" , Bio= "Christian Charles Philip Bale was born in Pembrokeshire, Wales, UK on January 30, 1974, to a English parents Jennifer James and David Bale. Christian's father was a commercial pilot, and the family lived in different countries throughout Bale's childhood, including England, Portugal, and the United States.",
                                                Dob = new DateTime(1974,01,30)};

                    Actor actorGary = new Actor(){ Name = "Gary Oldman" , Bio= "Gary Oldman was born on March 21, 1958 in London, England, the son of Kathleen (Cheriton), a homemaker, and Leonard Bertram Oldman, a welder. For most of his career he was best-known for playing over-the-top antagonists, though he has recently reached a new audience with heroic roles in the Harry Potter and Dark Knight franchises.",
                                                Dob = new DateTime(1958,03,21)};

                    
                    movieObj.MovieProducer = prod;
                    movieObj.MovieGenre = movieGen;
                    movieObj.MovieCountry = movieCountry;
                    movieObj.Actors.Add(actorBale);
                    movieObj.Actors.Add(actorGary);

                    movieDbObj.Movies.Add(movieObj);


                    movieDbObj.SaveChanges();
                                                
                    
                }
            }
            catch (DbEntityValidationException exp)
            {
                Console.WriteLine("Error occured in the Transaction Message :- " + exp.Message);

                foreach (var eve in exp.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
            }

            Console.ReadLine();
        }
    }



    // Database Classes

    public class Genre
    {

        public string GenreID { get; set; }
        public string GenreName { get; set; }
    }

    public class Country
    {
        public string countryID { get; set; }
        public string CountryName { get; set; }
    }

    public class Actor
    {
        public int ActorID { get; set; }
        public string Name { get; set; }
        public DateTime Dob { get; set; }
        public string Bio { get; set; }

        public virtual List<Movie> Movies { get; set; }

        public Actor()
        {
            this.Movies = new List<Movie>();
            
        }
    }

    public class Producer
    {
        public int ProducerID { get; set; }
        public string Name { get; set; }
        public DateTime Dob { get; set; }
        public string Bio { get; set; }

        public List<Movie> ProducedMovies { get; set; }

        public Producer()
        {
            this.ProducedMovies = new List<Movie>();
        }
    }

    public class Movie
    {
        public string MovieID { get; set; }
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public string Plot { get; set; }
        public Genre MovieGenre { get; set; }
        public Country MovieCountry { get; set; }

        public virtual List<Actor> Actors { get; set; }

        public Producer MovieProducer { get; set; }

        public Movie()
        {
            this.Actors = new List<Actor>();
            this.MovieGenre = new Genre();
            this.MovieCountry = new Country();
        }
    }


    public class MovieDBContext:DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Country> Countries { get; set; }
    }

    
}
