using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Singular.Web;

namespace MEWeb.Movies
{
  public partial class Movie : MEPageBase<MovieVM>
  {
  }
  public class MovieVM : MEStatelessViewModel<MovieVM>
  {
        public MELib.Movies.MovieList MovieList { get; set; }
        public MELib.Movies.Movie Movie { get; set; }
        public MELib.Movies.UserMovieList UserMovielist { get; set; }

        public int movieID { get; set; }


    public MovieVM()
    {

    }
    protected override void Setup()
    {
      base.Setup();
            movieID = System.Convert.ToInt32(Page.Request.QueryString[0]);
            //UserMovielist = MELib.Movies.UserMovieList.GetUserMovieList();
            MovieList = MELib.Movies.MovieList.GetMovieList1(movieID);
            Movie = MovieList.GetItem(movieID);

    }
  }
}

