using System;
using EggCentric.Evaluators;

namespace EggCentric.Stats
{
    public class StagedStat : ValueStat
    {
        protected uint stageLimit;
        protected IGrowth growthCalculator;

        protected float accumulatedValue;
        protected float valueLimit;

        protected uint currentStage;
        protected float stageValueLimit;

        public new float TotalValue => accumulatedValue + currentValue;
        public float Percentage => currentValue / stageValueLimit;
        public float Stage => currentStage;

        public event Action OnStageChanged;
        public event Action OnMaxStageReached;

        public StagedStat(IGrowth growthCalculator, uint stageLimit = 0) : base()
        {
            this.growthCalculator = growthCalculator;
            this.stageLimit = stageLimit;

            EvaluateStageLimits(currentStage);
        }

        protected override void IncreaseValue(float amount)
        {
            float capacityToLimit = stageValueLimit - currentValue;
            float limitedChange = Math.Min(amount, capacityToLimit);

            if (limitedChange == 0)
                return;

            if (capacityToLimit == limitedChange)
            {
                float remainValue = amount - limitedChange;
                IncreaseStage();
                ChangeValue(remainValue);
            }
            else
            {
                base.IncreaseValue(amount);
            }
        }

        protected override void DecreaseValue(float amount)
        {
            float capacityToLimit = currentValue;
            float limitedChange = Math.Min(amount, capacityToLimit);

            if (limitedChange == 0)
                return;

            if (capacityToLimit == limitedChange)
            {
                float remainValue = amount - limitedChange;
                DecreaseStage();
                ChangeValue(-remainValue);
            }
            else
            {
                base.DecreaseValue(amount);
            }

        }

        private void IncreaseStage()
        {
            if (currentStage == stageLimit)
                return;

            SetStage(currentStage + 1);
        }

        private void DecreaseStage()
        {
            if (currentStage == 0)
                return;

            SetStage(currentStage - 1);
        }

        private void SetStage(uint stage)
        {
            if (currentStage - stage == 0)
                return;

            currentValue = 0f;
            currentStage = stage;
            EvaluateStageLimits(stage);

            OnStageChanged?.Invoke();

            if (currentStage == stageLimit)
                OnMaxStageReached?.Invoke();
        }

        private void EvaluateStageLimits(uint stage)
        {
            stageValueLimit = growthCalculator.Evaluate(stage + 1);
        }
    }
}
