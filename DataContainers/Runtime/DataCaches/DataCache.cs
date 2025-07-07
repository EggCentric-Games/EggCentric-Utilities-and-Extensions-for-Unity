namespace EggCentric.DataContainers
{
    public class DataCache<TValue> : ManualDataCache<TValue>
    {
        public override TValue Data => _value;
        public override bool IsValid => _hasValue;

        private bool _hasValue;
        private TValue _value;

        public DataCache()
        {

        }

        public DataCache(TValue initialValue) : this()
        {
            SetValue(initialValue);
        }

        public static implicit operator TValue(DataCache<TValue> obj)
        {
            return obj.Data;
        }

        public override void SetValue(TValue value)
        {
            _value = value;
            _hasValue = true;
        }

        public void Clear()
        {
            _hasValue = false;
            _value = default;
        }
    }
}