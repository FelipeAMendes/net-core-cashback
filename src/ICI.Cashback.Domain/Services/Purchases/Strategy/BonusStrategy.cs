using System;

namespace ICI.Cashback.Domain.Services.Purchases.Strategy
{
	public abstract class BonusStrategy
	{
		public abstract Tuple<string, float> GetBonusValue(float value);
	}
}
