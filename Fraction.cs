using System;

public struct Fraction 
{
	private double a, b;

	public double Value { get => a / b; }
	public bool IsNegative { get => (((a < 0) ? 1 : 0) ^ ((b < 0) ? 1 : 0)) == 1; }

	public static explicit operator Fraction(double x) => new Fraction(x, 1);
	public static explicit operator double(Fraction x) => x.Value;

	public static Fraction operator + (Fraction f) => new Fraction(f.a, f.b);
	public static Fraction operator - (Fraction f) => new Fraction(-f.a, f.b);

	public static Fraction operator * (Fraction f, double x) => new Fraction(f.a * x, f.b);
	public static Fraction operator * (double x, Fraction f) => new Fraction(f.a * x, f.b);

	public static Fraction operator / (Fraction f, double x) => new Fraction(f.a, f.b * x);
	public static Fraction operator / (double x, Fraction f) => new Fraction(f.a, f.b * x);

	public static Fraction operator + (Fraction f, double x) => new Fraction(f.a + (x * f.b), f.b);
	public static Fraction operator + (double x, Fraction f) => new Fraction((x * f.b) + f.a, f.b);

	public static Fraction operator - (Fraction f, double x) => new Fraction(f.a - (x * f.b), f.b);
	public static Fraction operator - (double x, Fraction f) => new Fraction((x * f.b) - f.a, f.b);

	public static Fraction operator * (Fraction l, Fraction r) => new Fraction(
		l.a * r.a,
		l.b * r.b
	);

	public static Fraction operator / (Fraction l, Fraction r) => new Fraction(
		l.a * r.b,
		l.b * r.a
	);

	public static Fraction operator + (Fraction l, Fraction r) => new Fraction(
		(l.b != r.b) ? (l.a * r.b) + (r.a * l.b) : l.a + r.a,
		(l.b != r.b) ? l.b * r.b : l.b
	);

	public static Fraction operator - (Fraction l, Fraction r) => new Fraction(
		(l.b != r.b) ? (l.a * r.b) - (r.a * l.b) : l.a - r.a,
		(l.b != r.b) ? l.b * r.b : l.b
	);

	public override string ToString() 
	{
		if (a == 0) { return "0"; }

		string neg = this.IsNegative ? "-" : "";
		double aa = Math.Abs(a);
		double ab = Math.Abs(b);

		foreach (double x in new double[] { 2, 3, 5, 7, 11, 13, 17, 19 }) 
		{
			while (aa % x == 0 && ab % x == 0) 
			{
				aa /= x;
				ab /= x;
			}
		}

		if (aa == ab) { return $"{neg}1"; }
		if (ab == 1) { return $"{neg}{aa}"; }
		if (aa % ab == 0) { return $"{neg}{aa / ab}"; }

		return $"{neg}{aa}/{ab}";
	}

	public Fraction(double numerator, double denominator) 
	{
		this.a = numerator;
		this.b = denominator;
	}
}
