namespace ZetaHtmlEditControl.Helper
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;

	internal class StringHelper
	{
		/// <summary>
		/// Reads the description value from an enumeration.
		/// </summary>
		/// <param name="value">The description of the enum value to
		/// read.</param>
		/// <returns>
		/// Returns the description string or the enum value
		/// as a string, if no description was found.
		/// </returns>
		/// <seealso cref="http://www.codeproject.com/csharp/EnumDescConverter.asp."/>
		public static string GetEnumDescription(
			Enum value)
		{
			return GetEnumDescription(value, true);
		}

		/// <summary>
		/// Reads the description value from an enumeration.
		/// </summary>
		/// <param name="value">The description of the enum value to
		/// read.</param>
		/// <param name="allowCaching"></param>
		/// <returns>
		/// Returns the description string or the enum value
		/// as a string, if no description was found.
		/// </returns>
		/// <seealso cref="http://www.codeproject.com/csharp/EnumDescConverter.asp."/>
		public static string GetEnumDescription(
			Enum value,
			bool allowCaching)
		{
			string result;
			if (allowCaching && RecentEnumDescriptions.TryGetValue(value, out result))
			{
				return result;
			}
			else
			{
				var fi = value.GetType().GetField(value.ToString());

				var attributes =
					(DescriptionAttribute[])fi.GetCustomAttributes(
						typeof(DescriptionAttribute),
						false);

				result = attributes.Length > 0 ? attributes[0].Description : value.ToString();

				RecentEnumDescriptions[value] = result;
				return result;
			}
		}

		/// <summary>
		/// Much faster access by storing previously read values.
		/// </summary>
		private static readonly Dictionary<Enum, string> RecentEnumDescriptions =
			new Dictionary<Enum, string>();
	}
}