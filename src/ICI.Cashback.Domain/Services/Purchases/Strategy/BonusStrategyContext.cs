using System;

namespace ICI.Cashback.Domain.Services.Purchases.Strategy
{
	public class BonusStrategyContext
	{
		private BonusStrategy _bonusStrategy;

		public Tuple<string, float> GetBonus(float value)
		{
			if (value < 1000)
				_bonusStrategy = new Bonus10Strategy();
			else if (value > 1500)
				_bonusStrategy = new Bonus20Strategy();
			else
				_bonusStrategy = new Bonus15Strategy();

			return _bonusStrategy.GetBonusValue(value);
		}
	}
}
