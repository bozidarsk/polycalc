using System;
using System.Collections.Generic;
using System.Linq;

public static class Horner 
{
	private static Fraction CalculateValue(Fraction value, Fraction[] coefficients, out Fraction[] newCoefficients) 
	{
		List<Fraction> polynomial = new List<Fraction>();
		Fraction number = coefficients[0];

		polynomial.Add(number);
		for (int i = 0; i < coefficients.Length - 1; i++) 
		{
			number = number * value + coefficients[i + 1];
			polynomial.Add(number);
		}

		newCoefficients = polynomial.ToArray();
		return number;
	}

	private static Fraction[] GetValues(Fraction[] coefficients) 
	{
		Func<double, double[]> getFactors = (x) => 
		{
			List<double> list = new List<double>();
			for (double i = Math.Abs(x); i >= 1; i--) { if (x % i == 0) { list.Add(i); list.Add(-i); } }
			return list.ToArray();
		};

		List<Fraction> values = new List<Fraction>();
		// if (coefficients.Last() == 0) { values.Add(new Fraction(0, 1)); }

		double[] lasts = getFactors(coefficients.Last(x => x.Value != 0).Value);

		if (coefficients[0].Value != 1 && coefficients[0].Value != -1) 
		{
			double[] firsts = getFactors(coefficients[0].Value);
			Array.Sort(firsts);

			for (int i = 0; i < firsts.Length / 2; i++) { values.AddRange(lasts.Select(x => new Fraction(x, firsts[i]))); }
		}
		else { values.AddRange(lasts.Select(x => (Fraction)x)); }

		return values.ToArray();
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

	public static string Solve(Fraction[] input, Fraction[] values, out Fraction[] results) 
	{
		List<Fraction> answers = new List<Fraction>();
		List<List<string>> table = new List<List<string>>();
		List<string> row = new List<string>() { "-" };

		row.AddRange(input.Select(x => x.ToString()).ToArray());
		table.Add(row);

		bool all = values != null;
		values = values ?? GetValues(input);

		for (int i = 0; i < values.Length; i++) 
		{
			Fraction r = CalculateValue(values[i], input, out Fraction[] newCoefficients);

			if (all) 
			{
				answers.Add(values[i]);

				row = new List<string>() { values[i].ToString() };
				row.AddRange(newCoefficients.Select(x => x.ToString()).ToArray());
				table.Add(row);

				continue;
			}

			if (r.Value == 0 && !all) 
			{
				input = newCoefficients;
				answers.Add(values[i]);

				row = new List<string>() { values[i].ToString() };
				row.AddRange(newCoefficients.Select(x => x.ToString()).ToArray());
				table.Add(row);

				i--;
				continue;
			}
		}

		results = answers.ToArray();
		return ToString(table);
	}

	public static string Solve(string input, out Fraction[] results) => Solve(
		input
		.Split(' ', ',')
		.Where(x => !string.IsNullOrEmpty(x))
		.Select(x => (Fraction)double.Parse(x))
		.ToArray(),
		null,
		out results
	);

	public static string Solve(string input, string values = null) => Solve(
		input
		.Split(' ', ',')
		.Where(x => !string.IsNullOrEmpty(x))
		.Select(x => (Fraction)double.Parse(x))
		.ToArray(),
		(values != null)
		? values
		.Split(' ', ',')
		.Where(x => !string.IsNullOrEmpty(x))
		.Select(x => (Fraction)double.Parse(x))
		.ToArray()
		: null,
		out Fraction[] results
	);

	public static string Solve(string input, string values, out Fraction[] results) => Solve(input, values, out results);
	public static string Solve(Fraction[] input, out Fraction[] results) => Solve(input, null, out results);
	public static string Solve(Fraction[] input, Fraction[] values = null) => Solve(input, values, out Fraction[] results);
}
