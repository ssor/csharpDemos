namespace ZetaHtmlEditControl.Helper
{
	using System;
	using System.Globalization;

	internal static class ConvertHelper
	{
		/// <summary>
		/// Convert to a string.
		/// </summary>
		/// <param name="o">The o.</param>
		/// <returns></returns>
		public static string ToString(
			object o)
		{
			return ToString(o, (string)null);
		}

		/// <summary>
		/// Convert to a string.
		/// </summary>
		/// <param name="o">The o.</param>
		/// <param name="provider">The provider.</param>
		/// <returns></returns>
		public static string ToString(
			object o,
			IFormatProvider provider)
		{
			return ToString(o, null, provider);
		}

		/// <summary>
		/// Convert to a string.
		/// </summary>
		/// <param name="o">The o.</param>
		/// <param name="fallBackTo">The fall back to.</param>
		/// <returns></returns>
		public static string ToString(
			object o,
			string fallBackTo)
		{
			return ToString(o, fallBackTo, CultureInfo.CurrentCulture);
		}

		/// <summary>
		/// Convert to a string.
		/// </summary>
		/// <param name="o">The o.</param>
		/// <param name="fallBackTo">The fall back to.</param>
		/// <param name="provider">The provider.</param>
		/// <returns></returns>
		public static string ToString(
			object o,
			string fallBackTo,
			IFormatProvider provider)
		{
			if (o == null)
			{
				return fallBackTo;
			}
			// This is the fastest way, see
			// http://www.google.de/url?sa=t&ct=res&cd=4&url=http%3A%2F%2Fblogs.msdn.com%2Fvancem%2Farchive%2F2006%2F10%2F01%2F779503.aspx&ei=nOuTRY7TAoXe2QLi7qX3Dg&usg=__GUu0brYrkgjJl63ZZ3JBOzJCVH8=&sig2=1wvt78Kof6Bw7Drs3LL_ng
			else if (o.GetType() == typeof(string))
			{
				return (string)o;
			}
			else
			{
				return Convert.ToString(o, provider);
			}
		}

		/// <summary>
		/// Convert a string to an integer, returns 0 if fails.
		/// </summary>
		/// <param name="o">The o.</param>
		/// <returns></returns>
		public static int ToInt32(
			object o)
		{
			return ToInt32(o, CultureInfo.CurrentCulture);
		}

		/// <summary>
		/// Convert a string to an integer, returns 0 if fails.
		/// </summary>
		/// <param name="o">The o.</param>
		/// <param name="provider">The provider.</param>
		/// <returns></returns>
		public static int ToInt32(
			object o,
			IFormatProvider provider)
		{
			return ToInt32(o, 0, provider);
		}

		/// <summary>
		/// Convert a string to an integer, returns 0 if fails.
		/// </summary>
		/// <param name="o">The o.</param>
		/// <param name="fallBackTo">The fall back to.</param>
		/// <returns></returns>
		public static int ToInt32(
			object o,
			int fallBackTo)
		{
			return ToInt32(o, fallBackTo, CultureInfo.CurrentCulture);
		}

		/// <summary>
		/// Convert a string to an integer, returns 0 if fails.
		/// </summary>
		/// <param name="o">The o.</param>
		/// <param name="fallBackTo">The fall back to.</param>
		/// <param name="provider">The provider.</param>
		/// <returns></returns>
		public static int ToInt32(
			object o,
			int fallBackTo,
			IFormatProvider provider)
		{
			if (o == null)
			{
				return fallBackTo;
			}
			// This is the fastest way, see
			// http://www.google.de/url?sa=t&ct=res&cd=4&url=http%3A%2F%2Fblogs.msdn.com%2Fvancem%2Farchive%2F2006%2F10%2F01%2F779503.aspx&ei=nOuTRY7TAoXe2QLi7qX3Dg&usg=__GUu0brYrkgjJl63ZZ3JBOzJCVH8=&sig2=1wvt78Kof6Bw7Drs3LL_ng
			else if (o.GetType() == typeof(int))
			{
				return (int)o;
			}
			else if (IsInteger(o, provider))
			{
				return Convert.ToInt32(o, provider);
			}
			else if (IsDouble(o, provider))
			{
				return (int)Convert.ToDouble(o, provider);
			}
			else if (o is Enum)
			{
				return (int)o;
			}
			else
			{
				return fallBackTo;
			}
		}

		/// <summary>
		/// Checks whether a string contains a valid double.
		/// </summary>
		/// <param name="o">The string to check.</param>
		/// <returns>
		/// Returns TRUE if the string contains a double,
		/// FALSE if not.
		/// </returns>
		public static bool IsDouble(
			object o)
		{
			return IsDouble(o, CultureInfo.CurrentCulture);
		}

		/// <summary>
		/// Checks whether a string contains a valid double.
		/// </summary>
		/// <param name="o">The string to check.</param>
		/// <param name="provider">The provider.</param>
		/// <returns>
		/// Returns TRUE if the string contains a double,
		/// FALSE if not.
		/// </returns>
		public static bool IsDouble(
			object o,
			IFormatProvider provider)
		{
			if (o == null)
			{
				return false;
			}
			// This is the fastest way, see
			// http://www.google.de/url?sa=t&ct=res&cd=4&url=http%3A%2F%2Fblogs.msdn.com%2Fvancem%2Farchive%2F2006%2F10%2F01%2F779503.aspx&ei=nOuTRY7TAoXe2QLi7qX3Dg&usg=__GUu0brYrkgjJl63ZZ3JBOzJCVH8=&sig2=1wvt78Kof6Bw7Drs3LL_ng
			else if (o.GetType() == typeof(double))
			{
				return true;
			}
			// This is the fastest way, see
			// http://www.google.de/url?sa=t&ct=res&cd=4&url=http%3A%2F%2Fblogs.msdn.com%2Fvancem%2Farchive%2F2006%2F10%2F01%2F779503.aspx&ei=nOuTRY7TAoXe2QLi7qX3Dg&usg=__GUu0brYrkgjJl63ZZ3JBOzJCVH8=&sig2=1wvt78Kof6Bw7Drs3LL_ng
			else if (o.GetType() == typeof(float))
			{
				return true;
			}
			else
			{
				return doIsNumeric(o,
					FloatNumberStyle,
					provider);
			}
		}

		/// <summary>
		/// Checks whether a string contains a valid integer.
		/// </summary>
		/// <param name="o">The string to check.</param>
		/// <returns>
		/// Returns TRUE if the string contains an integer,
		/// FALSE if not.
		/// </returns>
		public static bool IsInteger(
			object o)
		{
			return IsInteger(o, CultureInfo.CurrentCulture);
		}

		/// <summary>
		/// Checks whether a string contains a valid integer.
		/// </summary>
		/// <param name="o">The string to check.</param>
		/// <param name="provider">The provider.</param>
		/// <returns>
		/// Returns TRUE if the string contains an integer,
		/// FALSE if not.
		/// </returns>
		public static bool IsInteger(
			object o,
			IFormatProvider provider)
		{
			if (o == null)
			{
				return false;
			}
			// This is the fastest way, see
			// http://www.google.de/url?sa=t&ct=res&cd=4&url=http%3A%2F%2Fblogs.msdn.com%2Fvancem%2Farchive%2F2006%2F10%2F01%2F779503.aspx&ei=nOuTRY7TAoXe2QLi7qX3Dg&usg=__GUu0brYrkgjJl63ZZ3JBOzJCVH8=&sig2=1wvt78Kof6Bw7Drs3LL_ng
			else if (o.GetType() == typeof(int))
			{
				return true;
			}
			// This is the fastest way, see
			// http://www.google.de/url?sa=t&ct=res&cd=4&url=http%3A%2F%2Fblogs.msdn.com%2Fvancem%2Farchive%2F2006%2F10%2F01%2F779503.aspx&ei=nOuTRY7TAoXe2QLi7qX3Dg&usg=__GUu0brYrkgjJl63ZZ3JBOzJCVH8=&sig2=1wvt78Kof6Bw7Drs3LL_ng
			else if (o.GetType() == typeof(long))
			{
				return true;
			}
			else if (o is Enum)
			{
				return true;
			}
			else
			{
				return doIsNumeric(o, NumberStyles.Integer, provider);
			}
		}

		/// <summary>
		/// Checks whether a string contains a valid integer.
		/// </summary>
		/// <param name="o">The string to check.</param>
		/// <returns>
		/// Returns TRUE if the string contains an integer,
		/// FALSE if not.
		/// </returns>
		public static bool IsInt32(
			object o)
		{
			return IsInt32(o, CultureInfo.CurrentCulture);
		}

		/// <summary>
		/// Checks whether a string contains a valid integer.
		/// </summary>
		/// <param name="o">The string to check.</param>
		/// <param name="provider">The provider.</param>
		/// <returns>
		/// Returns TRUE if the string contains an integer,
		/// FALSE if not.
		/// </returns>
		public static bool IsInt32(
			object o,
			IFormatProvider provider)
		{
			if (o == null)
			{
				return false;
			}
			// This is the fastest way, see
			// http://www.google.de/url?sa=t&ct=res&cd=4&url=http%3A%2F%2Fblogs.msdn.com%2Fvancem%2Farchive%2F2006%2F10%2F01%2F779503.aspx&ei=nOuTRY7TAoXe2QLi7qX3Dg&usg=__GUu0brYrkgjJl63ZZ3JBOzJCVH8=&sig2=1wvt78Kof6Bw7Drs3LL_ng
			else if (o.GetType() == typeof(Int32))
			{
				return true;
			}
			else
			{
				return doIsNumeric(o, NumberStyles.Integer, provider);
			}
		}

		/// <summary>
		/// Convert a string to a decimal, returns zero if fails.
		/// </summary>
		/// <param name="o">The o.</param>
		/// <returns></returns>
		public static decimal ToDecimal(
			object o)
		{
			return ToDecimal(o, CultureInfo.CurrentCulture);
		}

		/// <summary>
		/// Convert a string to a decimal, returns zero if fails.
		/// </summary>
		/// <param name="o">The o.</param>
		/// <param name="provider">The provider.</param>
		/// <returns></returns>
		public static decimal ToDecimal(
			object o,
			IFormatProvider provider)
		{
			return ToDecimal(o, decimal.Zero, provider);
		}

		/// <summary>
		/// Convert a string to a decimal, returns zero if fails.
		/// </summary>
		/// <param name="o">The o.</param>
		/// <param name="fallBackTo">The fall back to.</param>
		/// <returns></returns>
		public static decimal ToDecimal(
			object o,
			decimal fallBackTo)
		{
			return ToDecimal(o, fallBackTo, CultureInfo.CurrentCulture);
		}

		/// <summary>
		/// Convert a string to a decimal, returns zero if fails.
		/// </summary>
		/// <param name="o">The o.</param>
		/// <param name="fallBackTo">The fall back to.</param>
		/// <param name="provider">The provider.</param>
		/// <returns></returns>
		public static decimal ToDecimal(
			object o,
			decimal fallBackTo,
			IFormatProvider provider)
		{
			if (o == null)
			{
				return fallBackTo;
			}
			// This is the fastest way, see
			// http://www.google.de/url?sa=t&ct=res&cd=4&url=http%3A%2F%2Fblogs.msdn.com%2Fvancem%2Farchive%2F2006%2F10%2F01%2F779503.aspx&ei=nOuTRY7TAoXe2QLi7qX3Dg&usg=__GUu0brYrkgjJl63ZZ3JBOzJCVH8=&sig2=1wvt78Kof6Bw7Drs3LL_ng
			else if (o.GetType() == typeof(decimal))
			{
				return (decimal)o;
			}
			else if (IsDecimal(o, provider))
			{
				return Convert.ToDecimal(o, provider);
			}
			else
			{
				return fallBackTo;
			}
		}

		/// <summary>
		/// Checks whether a string contains a valid decimal.
		/// </summary>
		/// <param name="o">The string to check.</param>
		/// <returns>
		/// Returns TRUE if the string contains a decimal,
		/// FALSE if not.
		/// </returns>
		public static bool IsDecimal(
			object o)
		{
			return IsDecimal(o, CultureInfo.CurrentCulture);
		}

		/// <summary>
		/// Checks whether a string contains a valid decimal.
		/// </summary>
		/// <param name="o">The string to check.</param>
		/// <param name="provider">The provider.</param>
		/// <returns>
		/// Returns TRUE if the string contains a decimal,
		/// FALSE if not.
		/// </returns>
		public static bool IsDecimal(
			object o,
			IFormatProvider provider)
		{
			if (o == null)
			{
				return false;
			}
			// This is the fastest way, see
			// http://www.google.de/url?sa=t&ct=res&cd=4&url=http%3A%2F%2Fblogs.msdn.com%2Fvancem%2Farchive%2F2006%2F10%2F01%2F779503.aspx&ei=nOuTRY7TAoXe2QLi7qX3Dg&usg=__GUu0brYrkgjJl63ZZ3JBOzJCVH8=&sig2=1wvt78Kof6Bw7Drs3LL_ng
			else if (o.GetType() == typeof(decimal))
			{
				return true;
			}
			else
			{
				return doIsNumeric(
					o,
					FloatNumberStyle,
					provider);
			}
		}

		/// <summary>
		/// Does the is numeric.
		/// </summary>
		/// <param name="o">The o.</param>
		/// <param name="styles">The styles.</param>
		/// <param name="provider">The provider.</param>
		/// <returns></returns>
		private static bool doIsNumeric(
			object o,
			NumberStyles styles,
			IFormatProvider provider)
		{
			if (o == null)
			{
				return false;
			}
			else if (Convert.ToString(o, provider).Length <= 0)
			{
				return false;
			}
			else
			{
				double result;
				return double.TryParse(
					o.ToString(),
					styles,
					provider,
					out result);
			}
		}

		private const NumberStyles FloatNumberStyle =
			NumberStyles.Float |
			NumberStyles.Number |
			NumberStyles.AllowThousands |
			NumberStyles.AllowDecimalPoint |
			NumberStyles.AllowLeadingSign |
			NumberStyles.AllowLeadingWhite |
			NumberStyles.AllowTrailingWhite;
	}
}