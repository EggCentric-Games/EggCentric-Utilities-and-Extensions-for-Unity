namespace EggCentric.ValueProviders.DataContainers
{
    public class PersistentDataCache<TValue> : ManualDataCache<TValue>
    {
        public override TValue Value => _value;
        public override bool IsValid => _hasValue;

        private bool _hasValue;
        private TValue _value;

        public PersistentDataCache() => _value = default;
        public PersistentDataCache(TValue initialValue) => SetValue(initialValue);

        public static implicit operator TValue(PersistentDataCache<TValue> obj) => obj.Value;

        public override void SetValue(TValue value)
        {
            _value = value;
            _hasValue = true;

            base.SetValue(value);
        }

        public override void Invalidate()
        {
            _hasValue = false;
            _value = default;

            base.Invalidate();
        }
    }
}