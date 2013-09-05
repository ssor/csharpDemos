namespace ZetaHtmlEditControl.Helper
{
	using System;
	using System.Diagnostics;
	using System.Runtime.InteropServices;

	[Serializable]
	[DebuggerDisplay(@"Name = {_name}, Value = {_value}")]
	[ComVisible(false)]
	internal class Pair<TK, TV>
	{
		#region Public methods.
		// ------------------------------------------------------------------

		/// <summary>
		/// Constructor.
		/// </summary>
		public Pair()
		{
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="name">The name.</param>
		public Pair(
			TK name)
		{
			Name = name;
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="val">The val.</param>
		public Pair(
			TK name,
			TV val)
		{
			Name = name;
			Value = val;
		}

		/// <summary>
		/// Returns a <see cref="T:System.String"></see> that represents the 
		/// current <see cref="T:System.Object"></see>.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String"></see> that represents the current
		/// <see cref="T:System.Object"></see>.
		/// </returns>
		public override string ToString()
		{
			return Name == null ? null : Name.ToString();
		}

		// ------------------------------------------------------------------
		#endregion

		#region Public properties.
		// ------------------------------------------------------------------

		/// <summary>
		/// Alias.
		/// </summary>
		/// <value>The one.</value>
		public TK One
		{
			get
			{
				return Name;
			}
			set
			{
				Name = value;
			}
		}

		/// <summary>
		/// Alias.
		/// </summary>
		/// <value>The two.</value>
		public TV Two
		{
			get
			{
				return Value;
			}
			set
			{
				Value = value;
			}
		}

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public TK Name
		{
			get
			{
				return _name;
			}
			set
			{
				_name = value;
			}
		}

		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		/// <value>The value.</value>
		public TV Value
		{
			get
			{
				return _value;
			}
			set
			{
				_value = value;
			}
		}

		/// <summary>
		/// Gets or sets the first.
		/// </summary>
		/// <value>The first.</value>
		public TK First
		{
			get
			{
				return _name;
			}
			set
			{
				_name = value;
			}
		}

		/// <summary>
		/// Gets or sets the second.
		/// </summary>
		/// <value>The second.</value>
		public TV Second
		{
			get
			{
				return _value;
			}
			set
			{
				_value = value;
			}
		}

		// ------------------------------------------------------------------
		#endregion

		#region Private variables.
		// ------------------------------------------------------------------

		private TK _name;
		private TV _value;

		// ------------------------------------------------------------------
		#endregion
	}
}