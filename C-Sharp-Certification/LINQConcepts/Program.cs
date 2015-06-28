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
			// NOTE: The "ToList()" calls below are intentional for to immediately execute the otherwise deffered execution of the query for the sake of being able to use the "ForEach" extension of the IEnumerable interface to reduce output code.

			// Setup
			List<Pseudo.StateCityData> pseudoData = Pseudo.GetPseudoStateCityData();
			List<Pseudo.CityData> pseudoCityData = Pseudo.GetPseudoCityData();
			List<Pseudo.StateData> pseudoStateData = Pseudo.GetPseudoStateData();
			Console.WriteLine("Pseudo Data:");
			pseudoData.ForEach(data => Console.WriteLine(data.State + " " + data.City));
			Console.WriteLine();

			// Projection Example 1 - Method Syntax
			Console.WriteLine("Projected City Data - Method Syntax:");
			var pseudoCityDataCollection = pseudoData.Select(data => data.City);
			pseudoCityDataCollection.ToList().ForEach(cityData => Console.WriteLine(cityData));
			Console.WriteLine();

			// Projection Example 1 - Query Syntax
			Console.WriteLine("Projected City Data - Query Syntax:");
			var pseudoCityDataQuerySyntax = (from data in pseudoData select data.City);
			pseudoCityDataQuerySyntax.ToList().ForEach(cityData => Console.WriteLine(cityData));
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

			// Orderby Clause - Method Syntax
			Console.WriteLine("OrderBy - Method Syntax:");
			var pseudoOrderByCity = pseudoData.OrderBy(data => data.City).Select(data => data.City);
			pseudoOrderByCity.ToList().ForEach(data => Console.WriteLine(data));
			Console.WriteLine();

			// Orderby Clause - Query Syntax
			Console.WriteLine("OrderBy - Query Syntax:");
			var pseudoOrderByCityQuerySyntax = from data in pseudoData orderby data.City ascending select data.City;
			pseudoOrderByCityQuerySyntax.ToList().ForEach(data => Console.WriteLine(data));
			Console.WriteLine();

			// Group Clause - Method Syntax
			Console.WriteLine("Group - Method Syntax:");
			var pseudoCityStateGroup = pseudoData.GroupBy(data => data.City[0]);
			pseudoCityStateGroup.ToList().ForEach(displayGroupData);

			// Group Clause - Query Syntax
			Console.WriteLine("Group - Query Syntax:");
			var pseudoCityStateGroupQuerySyntax = from data in pseudoData group data by data.City[0];
			pseudoCityStateGroupQuerySyntax.ToList().ForEach(displayGroupData);

			// Join Clause - Method Syntax
			Console.WriteLine("Join - Method Syntax:");
			var pseudoCityStateJoin = pseudoCityData.Join(pseudoStateData, cityData => cityData.StateId, stateData => stateData.StateId, (cityData, stateData) => new { CityAndState = cityData.City + ", " + stateData.State });
			pseudoCityStateJoin.ToList().ForEach(data => Console.WriteLine(data.CityAndState));
			Console.WriteLine();

			// Join Clause - Query Syntax
			Console.WriteLine("Join - Query Syntax:");
			var pseudoCityStateJoinQuerySyntax = from cityData in pseudoCityData
												 join stateData in pseudoStateData on cityData.StateId equals stateData.StateId
												 select new { CityAndState = cityData.City + ", " + stateData.State };

			pseudoCityStateJoinQuerySyntax.ToList().ForEach(data => Console.WriteLine(data.CityAndState));
			Console.WriteLine();

			// Let Clause
			// NOTE: Could just write "from data in pseudoData select cityInReverse"... "Let" is more useful when the result needs to be used in a subsequent "from" clauses (i.e. from using String.Split()).
			Console.WriteLine("Let Clause - Cities in Reverse:");
			var letClauseQuery = from data in pseudoData
								 let cityInReverse = data.City.Reverse()
								 select cityInReverse;

			letClauseQuery.ToList().ForEach(cityInReverse => Console.WriteLine(cityInReverse.ToArray()));
			Console.WriteLine();

			// Deffered Query Execution
			int iterations = 100000;
			Console.WriteLine("Deffered Query Execution - Query does not execute until it is enumerated:");
			ActionTimer.Time("Declaring The Query", () =>
			{
				for (int i = 0; i < iterations; i++)
				{
					var defferedExecutionAnonymousScope = from data in pseudoData group data by data.City into g orderby g.Key ascending select g;
				}
			});

			var defferedExecution = from data in pseudoData
									group data by data.City into g
									orderby g.Key ascending
									select g;

			ActionTimer.Time("Executing the Query", () =>
			{
				for (int i = 0; i < iterations; i++)
				{
					// NOTE: Compiler might optimize this out... If it does, save it to a non-anonymously scoped List<IGrouping<string, Pseudo.StateCityData>> variable and display it so the enumeration is actually used.
					defferedExecution.ToList();
				}
			});

			Console.ReadLine();
		}

		private static void displayGroupData(IGrouping<char, Pseudo.StateCityData> group)
		{
			Console.WriteLine("Group: " + group.Key);

			foreach (Pseudo.StateCityData data in group)
			{
				Console.WriteLine(data.City);
			}

			Console.WriteLine();
		}

		private static void displayGroupData(IGrouping<string, Pseudo.StateCityData> group)
		{
			Console.WriteLine("Group: " + group.Key);

			foreach (Pseudo.StateCityData data in group)
			{
				Console.WriteLine(data.City);
			}

			Console.WriteLine();
		}
	}
}
