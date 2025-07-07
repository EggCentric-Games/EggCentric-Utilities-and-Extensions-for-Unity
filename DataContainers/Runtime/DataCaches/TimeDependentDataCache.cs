namespace EggCentric.DataContainers
{
    public class TimeDependentDataCache<TValue> : ManualDataCache<TValue>
    {
        // to-do: fix null-ref that happens if cache was created without initial value
        // decided to move on, because it is unlikely to happen, since IsValid presumes external validation before usage
        public override TValue Data => _lastRecord;

        public override bool IsValid => _lastRecord != null && _lastRecord.IsValid;

        private DataRecord<TValue> _lastRecord;
        private float _defaultRecordLifetime;

        public TimeDependentDataCache(float timeToLive = 0f)
        {
            _defaultRecordLifetime = timeToLive;
        }

        public TimeDependentDataCache(TValue initialValue, float timeToLive = 0f) : this(timeToLive)
        {
            SetValue(initialValue);
        }

        public static implicit operator TValue(TimeDependentDataCache<TValue> obj)
        {
            return obj.Data;
        }

        public override void SetValue(TValue value)
        {
            _lastRecord = new DataRecord<TValue>(value, _defaultRecordLifetime);
        }
    }
}