namespace ZetaHtmlEditControl
{
	public interface IExternalInformationProvider
	{
		#region Persistance.
		// ------------------------------------------------------------------

		/// <summary>
		/// Saves a per user per workstation value.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="value">The value.</param>
		void SavePerUserPerWorkstationValue(
			string name,
			string value );

		/// <summary>
		/// Restores a per user per workstation value.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="fallBackTo">The fall back to.</param>
		/// <returns></returns>
		string RestorePerUserPerWorkstationValue(
			string name,
			string fallBackTo );

		// ------------------------------------------------------------------
		#endregion
	}
}