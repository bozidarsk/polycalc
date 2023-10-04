using System;
using System.Collections.Generic;
using System.Linq;

public static class GaussainElimination 
{
	private static string ToString(List<double[]> data) 
	{
		int maxLength = -1;
		foreach (double[] row in data) { foreach (double number in row) { maxLength = Math.Max(maxLength, Math.Round(number, 6).ToString().Length); } }

		Func<double, string> format = x => 
		{
			string o = Math.Round(x, 6).ToString();
			while (o.Length < maxLength + 1) { o = " " + o; }
			return o;
		}; 

		string output = "";
		foreach (double[] row in data) 
		{
			output += "|";
			for (int i = 0; i < row.Length; i++) 
			{
				if (i == row.Length - 1) { output += "|"; }
				output += format(row[i]);
			}
			output += "|\n";
		}

		return output;
	}

	private static bool CalculateSelected(ref List<double[]> input, int x, int y, out List<string> modifiedRows) 
	{
		modifiedRows = new List<string>();

		for (int i = 0; i < input.Count; i++) 
		{
			if (i == y || input[i][x] == 0) { continue; }

			// Rix` = Rix + n*Ryx
			// 0 = input[i][x] + n * input[y][x]
			// n = -input[i][x] / input[y][x]
			double n = -input[i][x] / input[y][x];
			modifiedRows.Add($"R{i+1}` = R{i+1} + R{y+1} * {Math.Round(-input[i][x], 6)}/{Math.Round(input[y][x], 6)}");

			for (int t = 0; t < input[i].Length; t++) { input[i][t] = input[i][t] + n * input[y][t]; }

			bool zeroes = true;
			for (int t = 0; t < input[i].Length - 1; t++) { zeroes = Math.Round(input[i][t], 6) == 0 && zeroes; }
			if (zeroes && Math.Round(input[i][input[i].Length - 1], 6) != 0) { return false; }
			if (zeroes && Math.Round(input[i][input[i].Length - 1], 6) == 0) { input.RemoveAt(i--); continue; }
		}

		return true;
	}

	private static string Solve(List<double[]> input, out double[] results) 
	{
		string output = "";
		output += ToString(input);

		for (int i = 0; i < Math.Min(input.Count, input[0].Length - 1); i++) 
		{
			bool success = CalculateSelected(ref input, i, i, out List<string> modifiedRows);

			foreach (string r in modifiedRows) { output += r + "\n"; }
			output += ToString(input);

			if (!success) { results = new double[0]; return output; }
		}

		if (input.Count != input[0].Length - 1) { results = null; return output; }

		results = new double[input.Count];
		for (int i = 0; i < input.Count; i++) 
		{
			results[i] = 
				input[i][input[i].Length - 1] / input[i][i]
			;
		}

		return output;
	}

	public static string Solve(string input, out double[] results) => Solve(
		input
		.Split(';', '\n', '\r', '|')
		.Where(r => !string.IsNullOrEmpty(r))
		.Select(r => r
			.Split(' ', ',')
			.Where(c => !string.IsNullOrEmpty(c))
			.Select(c => double.Parse(c))
			.ToArray()
		)
		.ToList(),
		out results
	);

	public static string Solve(double[,] input, out double[] results) 
	{
		List<double[]> list = new List<double[]>(input.GetLength(0));

		for (int row = 0; row < input.GetLength(0); row++) 
		{
			list.Add(new double[input.GetLength(1)]);
			for (int column = 0; column < input.GetLength(1); column++) { list[row][column] = input[row, column]; }
		}

		return Solve(list, out results);
	}
}