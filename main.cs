using System;
using System.Collections;
using System.Collections.Generic;

public static class Program 
{
	private static void Main(string[] args) 
	{
		// string str = GaussainElimination.Solve("1,2,5,-9;1,-1,3,2;3,-6,-1,25", out double[] results);
		// string str = GaussainElimination.Solve("1,0,3,4,2;4,1,12,14,14;3,-5,8,25,-33;-3,-2,-11,-3,-34", out double[] results);
		// string str = GaussainElimination.Solve("1,1,2,4;1,0,1,2;1,2,3,7", out double[] results);
		// string str = GaussainElimination.Solve("1,0,-1,2;2,5,0,7;3,5,-1,9", out double[] results);
		// string str = GaussainElimination.Solve("4,3,-9,-9,43;-1,0,10,9,-39;2,0,-14,0,30;0,-1,-10,-8,35", out double[] results);
		// string str = GaussainElimination.Solve("1,1,0,2,4;2,1,-1,1,3;1,3,0,1,0;3,2,1,3,7;1,2,-2,2,3", out double[] results);
		string str = Horner.Solve("2,-7,0,8,3", "1,-1"); double[] results = {0};
		// string str = Horner.Solve("3,4,-4,3,4,-4"); double[] results = {0};
		// string str = Horner.Solve("8,-5,5,3"); double[] results = {0};

		Console.WriteLine(str);
		if (results == null) { Console.WriteLine("bezkrai resheniq"); }
		if (results?.GetLength(0) == 0) { Console.WriteLine("nqma resheniq"); }
		for (int i = 0; i < results?.GetLength(0); i++) { Console.WriteLine($"x{i+1} = {results[i]}"); }
	}
}