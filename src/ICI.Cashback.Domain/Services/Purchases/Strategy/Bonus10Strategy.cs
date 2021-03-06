using System;

namespace ICI.Cashback.Domain.Services.Purchases.Strategy
{
	public class Bonus10Strategy : BonusStrategy
	{
		public override Tuple<string, float> GetBonusValue(float value)
		{
			return Tuple.Create("10%", value * 10 / 100);
		}
	}
}
