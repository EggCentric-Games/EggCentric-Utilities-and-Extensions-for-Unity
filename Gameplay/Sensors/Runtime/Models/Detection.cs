using System.Collections.Generic;

namespace System.Runtime.CompilerServices
{
    internal static class IsExternalInit { }
}

namespace EggCentric.Sensors
{
    public interface IDetection<out TComponent>
    {
        public TComponent Component { get; }
    }

    public class Detection<TComponent, TContext> : IDetection<TComponent>
    {
        public TComponent Component => _component;
        public TContext Context => _context;

        public readonly TComponent _component;
        public readonly TContext _context;

        public Detection(TComponent component, TContext source)
        {
            _component = component;
            _context = source;
        }

        public bool Equals(Detection<TComponent, TContext> other) => EqualityComparer<TComponent>.Default.Equals(Component, other.Component);
        public override bool Equals(object obj) => obj is Detection<TComponent, TContext> other && Equals(other);
        public override int GetHashCode() => EqualityComparer<TComponent>.Default.GetHashCode(Component);

        public static bool operator ==(Detection<TComponent, TContext> left, Detection<TComponent, TContext> right) => left.Equals(right);
        public static bool operator !=(Detection<TComponent, TContext> left, Detection<TComponent, TContext> right) => !left.Equals(right);
    }
}