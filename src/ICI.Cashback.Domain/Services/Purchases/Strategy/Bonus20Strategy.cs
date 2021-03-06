using System;

namespace ICI.Cashback.Domain.Services.Purchases.Strategy
{
	public class Bonus20Strategy : BonusStrategy
	{
		public override Tuple<string, float> GetBonusValue(float value)
		{
			return Tuple.Create("20%", value * 20 / 100);
		}
	}
}
