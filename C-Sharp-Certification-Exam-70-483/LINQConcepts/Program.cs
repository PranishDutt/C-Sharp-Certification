using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helpers;

namespace LINQConcepts
{
	class Program
	{
		static void Main(string[] args)
		{
			// Setup
			List<Pseudo.StateCityData> pseudoData = Pseudo.GetPseudoStateCityData();
			Console.WriteLine("Pseudo Data:");
			pseudoData.ForEach(data => Console.WriteLine(data.State + " " + data.City));
			Console.WriteLine();

			// Projection Example 1 - Method Syntax
			Console.WriteLine("Projected City Data - Method Syntax:");
			List<string> pseudoCityData = pseudoData.Select(data => data.City).ToList();
			pseudoCityData.ForEach(cityData => Console.WriteLine(cityData));
			Console.WriteLine();

			// Projection Example 1 - Query Syntax
			Console.WriteLine("Projected City Data - Query Syntax:");
			List<string> pseudoCityDataQuerySyntax = (from data in pseudoData select data.City).ToList();
			pseudoCityDataQuerySyntax.ForEach(cityData => Console.WriteLine(cityData));
			Console.WriteLine();

			// Projection Example 2 - Method Syntax (Anonymous Type Projection)
			Console.WriteLine("Projected Anonymous State & City Data - Method Syntax:");
			var pseudoStateCityDataCollection = pseudoData.Select(data => new { CityAndState = data.City + ", " + data.State });
			pseudoStateCityDataCollection.ToList().ForEach(data => Console.WriteLine(data.CityAndState));
			Console.WriteLine();

			// Projection Example 2 - Query Syntax (Anonymous Type Projection)
			Console.WriteLine("Projected Anonymous State & City Data - Query Syntax:");
			var pseudoStateCityDataCollectionQuerySyntax = (from data in pseudoData select new { CityAndState = data.City + ", " + data.State });
			pseudoStateCityDataCollectionQuerySyntax.ToList().ForEach(data => Console.WriteLine(data.CityAndState));
			Console.WriteLine();

			Console.ReadLine();
		}
	}
}
