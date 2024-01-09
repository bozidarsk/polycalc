using System;
using System.Collections.Generic;
using System.Linq;

public static class Division 
{
	private static string ToString(Fraction[] coefficients) 
	{
		string output = "";

		for (int i = 0; i < coefficients.Length; i++) 
		{
			string number = (i < coefficients.Length - 1 && (coefficients[i] == (Fraction)1 || coefficients[i] == (Fraction)(-1)))
				? ((coefficients[i] == (Fraction)(-1)) ? "-" : "")
				: coefficients[i].ToString()
			;

			if (coefficients.Length - i - 1 == 0) { output += number; }
			else { output += $"{number}x^{coefficients.Length - i - 1} "; }
		}

		return output.TrimEnd(' ', ' ');
	}

	public static string Solve(Fraction[] p, Fraction[] g, out Fraction[] q, out Fraction[] r) 
	{
		List<Fraction> quotient = new List<Fraction>();
		string output = "";

		output += $"{ToString(p)} | g: {ToString(g)} | q: \n";

		for (int count = 1; p.Length >= g.Length; count++) 
		{
			List<Fraction> polynomial = new List<Fraction>();
			quotient.Add(p[0] / g[0]);

			Fraction[] gExpanded = new Fraction[p.Length];
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

	public static string Solve(string p, string g, out Fraction[] q, out Fraction[] r) => Solve(
		p
		.Split(' ', ',')
		.Where(x => !string.IsNullOrEmpty(x))
		.Select(x => (Fraction)double.Parse(x))
		.ToArray(),
		g
		.Split(' ', ',')
		.Where(x => !string.IsNullOrEmpty(x))
		.Select(x => (Fraction)double.Parse(x))
		.ToArray(),
		out q,
		out r
	);

	public static string Solve(string p, string g) => Solve(p, g, out Fraction[] q, out Fraction[] r);
	public static string Solve(Fraction[] p, Fraction[] g) => Solve(p, g, out Fraction[] q, out Fraction[] r);
}
