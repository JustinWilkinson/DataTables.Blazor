using System;
using System.Linq;

namespace DataTables.Blazor.Abstractions
{
    public abstract class DiscriminatedUnion
    {
        protected DiscriminatedUnion()
        {
        }

        protected DiscriminatedUnion(object value) : base()
        {
            Value = value;
        }

        protected abstract Type[] AllowedTypes { get; }

        private object _value;
        public object Value
        {
            get => _value;
            set
            {
                var typeOfValue = value.GetType();
                foreach (var type in AllowedTypes)
                {
                    if (type.IsAssignableFrom(typeOfValue))
                    {
                        _value = value;
                        return;
                    }
                }

                throw new InvalidOperationException($"Type '{typeOfValue.Name}' is not a valid type for this value. Valid values are: '{string.Join("', '", AllowedTypes.Select(x => x.Name))}'.");
            }
        }
    }

    public class DiscriminatedUnion<T1, T2> : DiscriminatedUnion
    {
        protected override Type[] AllowedTypes { get; } = { typeof(T1), typeof(T2) };

        public DiscriminatedUnion() : base()
        {
        }

        public DiscriminatedUnion(object value) : base()
        {
            Value = value;
        }

        public static implicit operator DiscriminatedUnion<T1, T2>(T1 obj) => new DiscriminatedUnion<T1, T2>(obj);

        public static implicit operator DiscriminatedUnion<T1, T2>(T2 obj) => new DiscriminatedUnion<T1, T2>(obj);
    }

    public class DiscriminatedUnion<T1, T2, T3> : DiscriminatedUnion
    {
        protected override Type[] AllowedTypes { get; } = { typeof(T1), typeof(T2), typeof(T3) };

        public DiscriminatedUnion() : base()
        {
        }

        public DiscriminatedUnion(object value) : base()
        {
            Value = value;
        }

        public static implicit operator DiscriminatedUnion<T1, T2, T3>(T1 obj) => new DiscriminatedUnion<T1, T2, T3>(obj);

        public static implicit operator DiscriminatedUnion<T1, T2, T3>(T2 obj) => new DiscriminatedUnion<T1, T2, T3>(obj);

        public static implicit operator DiscriminatedUnion<T1, T2, T3>(T3 obj) => new DiscriminatedUnion<T1, T2, T3>(obj);
    }

    public class DiscriminatedUnion<T1, T2, T3, T4> : DiscriminatedUnion
    {
        protected override Type[] AllowedTypes { get; } = { typeof(T1), typeof(T2), typeof(T3), typeof(T4) };

        public DiscriminatedUnion() : base()
        {
        }

        public DiscriminatedUnion(object value) : base()
        {
            Value = value;
        }

        public static implicit operator DiscriminatedUnion<T1, T2, T3, T4>(T1 obj) => new DiscriminatedUnion<T1, T2, T3, T4>(obj);

        public static implicit operator DiscriminatedUnion<T1, T2, T3, T4>(T2 obj) => new DiscriminatedUnion<T1, T2, T3, T4>(obj);

        public static implicit operator DiscriminatedUnion<T1, T2, T3, T4>(T3 obj) => new DiscriminatedUnion<T1, T2, T3, T4>(obj);

        public static implicit operator DiscriminatedUnion<T1, T2, T3, T4>(T4 obj) => new DiscriminatedUnion<T1, T2, T3, T4>(obj);
    }
}
