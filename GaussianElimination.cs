using System;
using System.Collections.Generic;
using System.Linq;

public static class GaussainElimination 
{
	private static string ToString(List<Fraction[]> data) 
	{
		int max = -1;
		foreach (Fraction[] row in data) { foreach (Fraction number in row) { max = Math.Max(max, number.ToString().Length); } }

		string output = "";
		foreach (Fraction[] row in data) 
		{
			output += "|";
			for (int i = 0; i < row.Length; i++) 
			{
				if (i == row.Length - 1) { output += "|"; }
				string str = row[i].ToString();
				output += new string(' ', max - str.Length + 1) + str;
			}
			output += "|\n";
		}

		return output;
	}

	private static bool CalculateSelected(ref List<Fraction[]> input, int x, int y, out List<string> modifiedRows) 
	{
		modifiedRows = new List<string>();

		for (int i = 0; i < input.Count; i++) 
		{
			if (i == y || input[i][x].Value == 0) { continue; }

			// Rix` = Rix + n*Ryx
			// 0 = input[i][x] + n * input[y][x]
			// n = -input[i][x] / input[y][x]
			Fraction n = -input[i][x] / input[y][x];
			modifiedRows.Add($"R{i+1}` = R{i+1} + {n} R{y+1}");

			for (int t = 0; t < input[i].Length; t++) { input[i][t] = input[i][t] + n * input[y][t]; }

			bool zeroes = true;
			for (int t = 0; t < input[i].Length - 1; t++) { zeroes = input[i][t].Value == 0 && zeroes; }
			if (zeroes && input[i][input[i].Length - 1].Value != 0) { return false; }
			if (zeroes && input[i][input[i].Length - 1].Value == 0) { input.RemoveAt(i--); continue; }
		}

		return true;
	}

	private static string Solve(List<Fraction[]> input, out Fraction[] results) 
	{
		string output = "";
		output += ToString(input);

		for (int i = 0; i < Math.Min(input.Count, input[0].Length - 1); i++) 
		{
			bool success = CalculateSelected(ref input, i, i, out List<string> modifiedRows);

			foreach (string r in modifiedRows) { output += r + "\n"; }
			output += ToString(input);

			if (!success) { results = new Fraction[0]; return output; }
		}

		if (input.Count != input[0].Length - 1) { results = null; return output; }

		results = new Fraction[input.Count];
		for (int i = 0; i < input.Count; i++) 
		{
			results[i] = 
				input[i][input[i].Length - 1] / input[i][i]
			;
		}

		return output;
	}

	public static string Solve(string input, out Fraction[] results) => Solve(
		input
		.Split(';', '\n', '\r', '|')
		.Where(r => !string.IsNullOrEmpty(r))
		.Select(r => r
			.Split(' ', ',')
			.Where(c => !string.IsNullOrEmpty(c))
			.Select(c => new Fraction(double.Parse(c), 1))
			.ToArray()
		)
		.ToList(),
		out results
	);

	public static string Solve(double[,] input, out Fraction[] results) 
	{
		List<Fraction[]> list = new List<Fraction[]>(input.GetLength(0));

		for (int row = 0; row < input.GetLength(0); row++) 
		{
			list.Add(new Fraction[input.GetLength(1)]);
			for (int column = 0; column < input.GetLength(1); column++) { list[row][column] = new Fraction(input[row, column], 1); }
		}

		return Solve(list, out results);
	}
}