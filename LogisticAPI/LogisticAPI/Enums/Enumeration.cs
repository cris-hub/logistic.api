using System.Reflection;

namespace LogisticAPI.Enums
{
    public abstract class Enumeration : IComparable
    {
        private const int UNDEFINED_INT_VALUE = -1;

        private int _numberOfZeros;

        private string _IdString;

        public string Name { get; set; }

        public int Id { get; set; }

        public string IdString
        {
            get
            {
                if ((int.TryParse(_IdString ?? string.Empty, out var _) || Id != -1) && _numberOfZeros > 0)
                {
                    _IdString = ConcatenateWithZeros();
                }

                return _IdString;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _IdString = value;
                }
            }
        }

        protected Enumeration(int id, string name, int numberOfZeros = 2)
        {
            Id = id;
            Name = name;
            _numberOfZeros = numberOfZeros;
        }

        protected Enumeration(string idString, string name)
        {
            IdString = idString;
            Name = name;
            Id = -1;
            _numberOfZeros = 0;
        }

        protected Enumeration()
        {
            _numberOfZeros = 0;
            IdString = (-1).ToString();
            Name = string.Empty;
        }

        public override string ToString()
        {
            return Name;
        }

        public static IEnumerable<T> GetAll<T>() where T : Enumeration
        {
            return (from f in typeof(T).GetFields(BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public)
                    select f.GetValue(null)).Cast<T>();
        }

        public override bool Equals(object obj)
        {
            Enumeration enumeration = obj as Enumeration;
            if (enumeration == null)
            {
                return false;
            }

            bool num = GetType().Equals(obj.GetType());
            bool flag = ((!string.IsNullOrEmpty(IdString) && !string.IsNullOrEmpty((obj as Enumeration).IdString)) ? IdString.Equals(enumeration.IdString) : Id.Equals(enumeration.Id));
            return num && flag;
        }

        public override int GetHashCode()
        {
            if (string.IsNullOrEmpty(IdString))
            {
                return Id.GetHashCode();
            }

            return IdString.GetHashCode();
        }

        public static int AbsoluteDifference(Enumeration firstValue, Enumeration secondValue)
        {
            return Math.Abs(firstValue.Id - secondValue.Id);
        }

        public static T FromValue<T>(int value) where T : Enumeration
        {
            return Parse(value, "value", (T item) => item.Id == value);
        }

        public static T FromValue<T>(string value) where T : Enumeration
        {
            return Parse(value, "value", (T item) => item.IdString == value);
        }

        public static T FromDisplayName<T>(string displayName) where T : Enumeration
        {
            return Parse(displayName, "displayName", (T item) => item.Name == displayName);
        }

        public static bool IsValidValue<T>(int value) where T : Enumeration
        {
            try
            {
                return FromValue<T>(value).Id.Equals(value);
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }

        public static bool IsValidValue<T>(string value) where T : Enumeration
        {
            try
            {
                return FromValue<T>(value).IdString.Equals(value);
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }

        private static T Parse<T, K>(K value, string description, Func<T, bool> predicate) where T : Enumeration
        {
            return GetAll<T>().FirstOrDefault(predicate) ?? throw new InvalidOperationException($"'{value}' is not a valid {description} in {typeof(T)}");
        }

        public int CompareTo(object other)
        {
            if (string.IsNullOrEmpty(IdString) || string.IsNullOrEmpty((other as Enumeration).IdString))
            {
                return Id.CompareTo((other as Enumeration).Id);
            }

            return IdString.CompareTo(((Enumeration)other).IdString);
        }

        private string ConcatenateWithZeros()
        {
            return Id.ToString().PadLeft(_numberOfZeros, '0');
        }
    }
}
