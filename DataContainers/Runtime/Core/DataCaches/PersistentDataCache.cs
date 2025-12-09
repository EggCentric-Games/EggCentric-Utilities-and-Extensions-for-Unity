namespace EggCentric.DataContainers
{
    public class PersistentDataCache<TValue> : ManualDataCache<TValue>
    {
        public override TValue Data => _value;
        public override bool IsValid => _hasValue;

        private bool _hasValue;
        private TValue _value;

        public PersistentDataCache(TValue initialValue = default) => SetValue(initialValue);

        public static implicit operator TValue(PersistentDataCache<TValue> obj) => obj.Data;

        public override void SetValue(TValue value)
        {
            _value = value;
            _hasValue = true;
        }

        public override void Invalidate()
        {
            _hasValue = false;
            _value = default;
        }
    }
}