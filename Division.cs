using System;
using System.Collections.Generic;
using System.Linq;

public static class Division 
{
	private static string ToString(double[] coefficients) 
	{
		string output = "";

		for (int i = 0; i < coefficients.Length; i++) 
		{
			string number = (i < coefficients.Length - 1 && (coefficients[i] == 1 || coefficients[i] == -1))
				? ((coefficients[i] == -1) ? "-" : "")
				: Math.Round(coefficients[i], 6).ToString()
			;

			if (coefficients.Length - i - 1 == 0) { output += number; }
			else { output += $"{number}x^{coefficients.Length - i - 1} "; }
		}

		return output.TrimEnd(' ', ' ');
	}

	public static string Solve(double[] p, double[] g, out double[] q, out double[] r) 
	{
		List<double> quotient = new List<double>();
		string output = "";

		output += $"{ToString(p)} | g: {ToString(g)} | q: \n";

		for (int count = 1; p.Length >= g.Length; count++) 
		{
			List<double> polynomial = new List<double>();
			quotient.Add(p[0] / g[0]);

			double[] gExpanded = new double[p.Length];
			Array.Copy(g, 0, gExpanded, 0, g.Length);

			output += new string('\t', count - 1) + ToString(gExpanded.Select(x => x * quotient.Last()).ToArray()) + "\n";

			for (int i = 1; i < p.Length; i++) 
			{
				if (i < g.Length) { polynomial.Add(p[i] - (g[i] * quotient.Last())); }
				else { polynomial.Add(p[i]); }
			}

			p = polynomial.ToArray();
			output += new string('\t', count) + ToString(p) + "\n";
		}

		q = quotient.ToArray();
		r = p.ToArray();

		return output.Insert(output.IndexOf("q:") + 3, ToString(q));
	}

	public static string Solve(string p, string g, out double[] q, out double[] r) => Solve(
		p
		.Split(' ', ',')
		.Where(x => !string.IsNullOrEmpty(x))
		.Select(x => double.Parse(x))
		.ToArray(),
		g
		.Split(' ', ',')
		.Where(x => !string.IsNullOrEmpty(x))
		.Select(x => double.Parse(x))
		.ToArray(),
		out q,
		out r
	);

	public static string Solve(string p, string g) => Solve(
		p
		.Split(' ', ',')
		.Where(x => !string.IsNullOrEmpty(x))
		.Select(x => double.Parse(x))
		.ToArray(),
		g
		.Split(' ', ',')
		.Where(x => !string.IsNullOrEmpty(x))
		.Select(x => double.Parse(x))
		.ToArray(),
		out double[] q,
		out double[] r
	);

	public static string Solve(double[] p, double[] g) => Solve(p, g, out double[] q, out double[] r);
}