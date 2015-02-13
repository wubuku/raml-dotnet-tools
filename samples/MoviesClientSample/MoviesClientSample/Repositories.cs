using System.Collections.Generic;
using MoviesClientSample.Movies.Models;

namespace MoviesClientSample
{
	public static class Repositories
	{
        public static ICollection<MoviesGetOKResponseContent> Movies { get; set; }
        public static ICollection<MoviesGetOKResponseContent> Wishlist { get; set; }
	}
}