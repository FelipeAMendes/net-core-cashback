using System;

namespace ICI.Cashback.Domain.Services.Purchases.Strategy
{
	public class Bonus15Strategy : BonusStrategy
	{
		public override Tuple<string, float> GetBonusValue(float value)
		{
			return Tuple.Create("15%", value * 15 / 100);
		}
	}
}
