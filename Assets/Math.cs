namespace AssemblyCSharp
{
	public static class Math
	{
		/// <summary>
		/// returns the max value between value1 and value2. returns value 1 if equal
		/// </summary>
		/// <param name="value1">Value1.</param>
		/// <param name="value2">Value2.</param>
		public static float Max(float value1, float value2)
		{
			if (value1 >= value2) {
				return value1;
			} else {
				return value2;
			}
		}
	}
}

