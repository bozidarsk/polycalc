using System;
using System.Collections.Generic;
using System.Linq;

public static class Horner 
{
	private static double CalculateValue(double value, double[] coefficients, out double[] newCoefficients) 
	{
		List<double> polynomial = new List<double>();
		double number = coefficients[0];
		polynomial.Add(number);

		for (int i = 0; i < coefficients.Length - 1; i++) 
		{
			number = number * value + coefficients[i + 1];
			polynomial.Add(number);
		}

		newCoefficients = polynomial.ToArray();
		return number;
	}

	private static double[] GetValues(double first, double last) 
	{
		Func<double, double[]> getFactors = (x) => 
		{
			List<double> list = new List<double>();
			for (double i = Math.Abs(x); i >= 1; i--) { if (x % i == 0) { list.Add(i); list.Add(-i); } }
			return list.ToArray();
		};

		List<double> factors = new List<double>();
		double[] firsts = getFactors(first);
		double[] lasts = getFactors(last);

		Array.Sort(firsts);
		for (int i = 0; i < firsts.Length / 2; i++) { factors.AddRange(lasts.Select(x => x / firsts[i])); }

		return factors.ToArray();
	}

	private static string ToString(List<List<string>> data) 
	{
		string output = "";
		int max = -1;

		for (int i = 0; i < data.Count; i++) 
		{
			for (int t = 0; t < data[i].Count; t++) 
			{
				max = Math.Max(max, data[i][t].Length);
			}
		}

		max++;
		for (int i = 0; i < data.Count; i++) 
		{
			for (int t = 0; t < data[i].Count; t++) 
			{
				if (i == 0 && t == 0) { output += new string(' ', max) + "|"; continue; }

				string number = data[i][t];
				if (number.Length < max) { output += new string(' ', max - number.Length); }
				output += number;

				if (t == 0) { output += "|"; }
				if (t == data[i].Count - 1) { output += "\n"; }
			}
		}

		return output;
	}

	public static string Solve(string input, string values, out double[] results) 
	{
		double[] coefficients = input
			.Split(' ', ',')
			.Where(x => !string.IsNullOrEmpty(x))
			.Select(x => double.Parse(x))
			.ToArray()
		;

		List<double> answers = new List<double>();
		List<List<string>> table = new List<List<string>>();
		List<string> row = new List<string>() { "-" };

		row.AddRange(coefficients.Select(x => x.ToString()).ToArray());
		table.Add(row);

		double[] numbers = (values != null) 
			? values
			.Split(' ', ',')
			.Where(x => !string.IsNullOrEmpty(x))
			.Select(x => double.Parse(x))
			.ToArray()
			: GetValues(coefficients[0], coefficients[coefficients.Length - 1])
		;

		for (int i = 0; i < numbers.Length; i++) 
		{
			double r = CalculateValue(numbers[i], coefficients, out double[] newCoefficients);

			if (r == 0 && values == null) 
			{
				coefficients = newCoefficients;
				answers.Add(numbers[i]);

				row = new List<string>() { Math.Round(numbers[i], 6).ToString() };
				row.AddRange(newCoefficients.Select(x => Math.Round(x, 6).ToString()).ToArray());
				table.Add(row);

				i--;
				continue;
			}

			if (values != null) 
			{
				coefficients = newCoefficients;

				row = new List<string>() { Math.Round(numbers[i], 6).ToString() };
				row.AddRange(newCoefficients.Select(x => Math.Round(x, 6).ToString()).ToArray());
				table.Add(row);

				continue;
			}
		}

		results = answers.ToArray();
		return ToString(table);
	}

	public static string Solve(string input, out double[] results) => Solve(input, null, out results);
	public static string Solve(string input, string values = null) => Solve(input, values, out double[] results);
}