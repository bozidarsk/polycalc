using System;
using System.Collections;
using System.Collections.Generic;

public static class Program 
{
	private static void Main(string[] args) 
	{
		// Console.WriteLine(GaussainElimination.Solve("-2,-8,-11,-12,-14;0,1,-2,1,-4;-1,-9,10,11,2;-1,-6,-1,-6,0", out Fraction[] results));
		// Console.WriteLine(GaussainElimination.Solve("1,1,2,2,0;1,2,2,2,0;0,1,2,3,0;1,2,3,4,0", out Fraction[] results));
		// Console.WriteLine(GaussainElimination.Solve("2,1,1,2;3,3,2,5;,1,8,5,7;2,3,-3,14", out Fraction[] results));
		// Console.WriteLine(GaussainElimination.Solve("2,-1,1,3;1,2,-1,2;3,1,1,8", out Fraction[] results));
		// Console.WriteLine(GaussainElimination.Solve("2,1,2,1,0;2,-1,-1,2,2;2,2,1,2,3;2,-3,-1,3,1", out Fraction[] results));

		Console.WriteLine(NewtonsMethod.Solve("1,0,1,-1", out Fraction result));
	}
}