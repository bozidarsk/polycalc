using System;
using System.Collections.Generic;
using System.Linq;

public static class NewtonsMethod 
{
	private static Fraction[] SolveDerivative(Fraction[] coefficients) 
	{
		Fraction[] derivative = new Fraction[coefficients.Length - 1];

		for (int i = 0; i < coefficients.Length - 1; i++) 
		{
			int power = coefficients.Length - i - 1;
			derivative[i] = coefficients[i] * power;
		}

		return derivative;
	}

	public static string Solve(Fraction[] coefficients, out Fraction result, double value = 0) 
	{
		Fraction[] derivative = SolveDerivative(coefficients);
		Fraction diff = (Fraction)1;
		result = (Fraction)value;

		string output = "";
		int index = 0;

		while (diff.Value != 0) 
		{
			Fraction f = (Fraction)0;
			Fraction fprim = (Fraction)0;

			for (int i = 0; i < coefficients.Length; i++) 
			{
				int power = coefficients.Length - i - 1;
				f += coefficients[i] * Math.Pow(result.Value, power);
			}

			for (int i = 0; i < derivative.Length; i++) 
			{
				int power = derivative.Length - i - 1;
				fprim += derivative[i] * Math.Pow(result.Value, power);
			}

			Fraction current = result - (f / fprim);

			if (double.IsNaN(current.Value) || double.IsPositiveInfinity(current.Value) || double.IsNegativeInfinity(current.Value)) 
			{
				if (index != 0)
					throw new InvalidOperationException();

				result = (Fraction)(double)(++value);
				diff = (Fraction)1;
				continue;
			}

			diff = result - current;
			result = current;

			output += $"x{index++} = {current.Value}\n";
		}

		return $"value = {value}\n" + output;
	}

	public static string Solve(string input, out Fraction result, double value = 0) => Solve(
		input
		.Split(' ', ',')
		.Where(x => !string.IsNullOrEmpty(x))
		.Select(x => (Fraction)double.Parse(x))
		.ToArray(),
		out result,
		value
	);

	public static string Solve(Fraction[] coefficients, double value = 0) => Solve(coefficients, out Fraction result, value);
	public static string Solve(string input, double value = 0) => Solve(input, out Fraction result, value);
}
