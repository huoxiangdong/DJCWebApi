namespace DJCWebApi.Areas.HelpPage
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class ObjectGenerator
    {
        internal const int DefaultCollectionSize = 2;
        private readonly SimpleTypeObjectGenerator SimpleObjectGenerator = new SimpleTypeObjectGenerator();

        private static object GenerateArray(Type arrayType, int size, Dictionary<Type, object> createdObjectReferences)
        {
            int num2;
            Type elementType = arrayType.GetElementType();
            Array array = Array.CreateInstance(elementType, size);
            bool flag = true;
            ObjectGenerator generator = new ObjectGenerator();
            for (int i = 0; i < size; i = num2 + 1)
            {
                object obj2 = generator.GenerateObject(elementType, createdObjectReferences);
                array.SetValue(obj2, i);
                flag &= obj2 == null;
                num2 = i;
            }
            if (flag)
            {
                return null;
            }
            return array;
        }

        private static object GenerateCollection(Type collectionType, int size, Dictionary<Type, object> createdObjectReferences)
        {
            int num2;
            Type type = collectionType.IsGenericType ? collectionType.GetGenericArguments()[0] : typeof(object);
            object obj2 = Activator.CreateInstance(collectionType);
            MethodInfo method = collectionType.GetMethod("Add");
            bool flag = true;
            ObjectGenerator generator = new ObjectGenerator();
            for (int i = 0; i < size; i = num2 + 1)
            {
                object obj3 = generator.GenerateObject(type, createdObjectReferences);
                object[] parameters = new object[] { obj3 };
                method.Invoke(obj2, parameters);
                flag &= obj3 == null;
                num2 = i;
            }
            if (flag)
            {
                return null;
            }
            return obj2;
        }

        private static object GenerateComplexObject(Type type, Dictionary<Type, object> createdObjectReferences)
        {
            object obj2 = null;
            if (!createdObjectReferences.TryGetValue(type, out obj2))
            {
                if (type.IsValueType)
                {
                    obj2 = Activator.CreateInstance(type);
                }
                else
                {
                    ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);
                    if (constructor == null)
                    {
                        return null;
                    }
                    obj2 = constructor.Invoke(new object[0]);
                }
                createdObjectReferences.Add(type, obj2);
                SetPublicProperties(type, obj2, createdObjectReferences);
                SetPublicFields(type, obj2, createdObjectReferences);
            }
            return obj2;
        }

        private static object GenerateDictionary(Type dictionaryType, int size, Dictionary<Type, object> createdObjectReferences)
        {
            int num2;
            Type type = typeof(object);
            Type type2 = typeof(object);
            if (dictionaryType.IsGenericType)
            {
                Type[] genericArguments = dictionaryType.GetGenericArguments();
                type = genericArguments[0];
                type2 = genericArguments[1];
            }
            object obj2 = Activator.CreateInstance(dictionaryType);
            MethodInfo info = dictionaryType.GetMethod("Add") ?? dictionaryType.GetMethod("TryAdd");
            MethodInfo info2 = dictionaryType.GetMethod("Contains") ?? dictionaryType.GetMethod("ContainsKey");
            ObjectGenerator generator = new ObjectGenerator();
            for (int i = 0; i < size; i = num2 + 1)
            {
                object obj3 = generator.GenerateObject(type, createdObjectReferences);
                if (obj3 == null)
                {
                    return null;
                }
                object[] parameters = new object[] { obj3 };
                if (!((bool) info2.Invoke(obj2, parameters)))
                {
                    object obj5 = generator.GenerateObject(type2, createdObjectReferences);
                    object[] objArray2 = new object[] { obj3, obj5 };
                    info.Invoke(obj2, objArray2);
                }
                num2 = i;
            }
            return obj2;
        }

        private static object GenerateEnum(Type enumType)
        {
            Array values = Enum.GetValues(enumType);
            if (values.Length > 0)
            {
                return values.GetValue(0);
            }
            return null;
        }

        private static object GenerateGenericType(Type type, int collectionSize, Dictionary<Type, object> createdObjectReferences)
        {
            Type genericTypeDefinition = type.GetGenericTypeDefinition();
            if (genericTypeDefinition == typeof(Nullable<>))
            {
                return GenerateNullable(type, createdObjectReferences);
            }
            if (genericTypeDefinition == typeof(KeyValuePair<,>))
            {
                return GenerateKeyValuePair(type, createdObjectReferences);
            }
            if (IsTuple(genericTypeDefinition))
            {
                return GenerateTuple(type, createdObjectReferences);
            }
            Type[] genericArguments = type.GetGenericArguments();
            if (genericArguments.Length == 1)
            {
                if (((genericTypeDefinition == typeof(IList<>)) || (genericTypeDefinition == typeof(IEnumerable<>))) || (genericTypeDefinition == typeof(ICollection<>)))
                {
                    return GenerateCollection(typeof(List<>).MakeGenericType(genericArguments), collectionSize, createdObjectReferences);
                }
                if (genericTypeDefinition == typeof(IQueryable<>))
                {
                    return GenerateQueryable(type, collectionSize, createdObjectReferences);
                }
                Type[] typeArguments = new Type[] { genericArguments[0] };
                if (typeof(ICollection<>).MakeGenericType(typeArguments).IsAssignableFrom(type))
                {
                    return GenerateCollection(type, collectionSize, createdObjectReferences);
                }
            }
            if (genericArguments.Length == 2)
            {
                if (genericTypeDefinition == typeof(IDictionary<,>))
                {
                    return GenerateDictionary(typeof(Dictionary<,>).MakeGenericType(genericArguments), collectionSize, createdObjectReferences);
                }
                Type[] typeArguments = new Type[] { genericArguments[0], genericArguments[1] };
                if (typeof(IDictionary<,>).MakeGenericType(typeArguments).IsAssignableFrom(type))
                {
                    return GenerateDictionary(type, collectionSize, createdObjectReferences);
                }
            }
            if (type.IsPublic || type.IsNestedPublic)
            {
                return GenerateComplexObject(type, createdObjectReferences);
            }
            return null;
        }

        private static object GenerateKeyValuePair(Type keyValuePairType, Dictionary<Type, object> createdObjectReferences)
        {
            Type[] genericArguments = keyValuePairType.GetGenericArguments();
            Type type = genericArguments[0];
            Type type2 = genericArguments[1];
            ObjectGenerator generator = new ObjectGenerator();
            object obj2 = generator.GenerateObject(type, createdObjectReferences);
            object obj3 = generator.GenerateObject(type2, createdObjectReferences);
            if ((obj2 == null) && (obj3 == null))
            {
                return null;
            }
            object[] args = new object[] { obj2, obj3 };
            return Activator.CreateInstance(keyValuePairType, args);
        }

        private static object GenerateNullable(Type nullableType, Dictionary<Type, object> createdObjectReferences)
        {
            Type type = nullableType.GetGenericArguments()[0];
            ObjectGenerator generator = new ObjectGenerator();
            return generator.GenerateObject(type, createdObjectReferences);
        }

        public object GenerateObject(Type type) => 
            this.GenerateObject(type, new Dictionary<Type, object>());

        private object GenerateObject(Type type, Dictionary<Type, object> createdObjectReferences)
        {
            try
            {
                if (SimpleTypeObjectGenerator.CanGenerateObject(type))
                {
                    return this.SimpleObjectGenerator.GenerateObject(type);
                }
                if (type.IsArray)
                {
                    return GenerateArray(type, 2, createdObjectReferences);
                }
                if (type.IsGenericType)
                {
                    return GenerateGenericType(type, 2, createdObjectReferences);
                }
                if (type == typeof(IDictionary))
                {
                    return GenerateDictionary(typeof(Hashtable), 2, createdObjectReferences);
                }
                if (typeof(IDictionary).IsAssignableFrom(type))
                {
                    return GenerateDictionary(type, 2, createdObjectReferences);
                }
                if (((type == typeof(IList)) || (type == typeof(IEnumerable))) || (type == typeof(ICollection)))
                {
                    return GenerateCollection(typeof(ArrayList), 2, createdObjectReferences);
                }
                if (typeof(IList).IsAssignableFrom(type))
                {
                    return GenerateCollection(type, 2, createdObjectReferences);
                }
                if (type == typeof(IQueryable))
                {
                    return GenerateQueryable(type, 2, createdObjectReferences);
                }
                if (type.IsEnum)
                {
                    return GenerateEnum(type);
                }
                if (type.IsPublic || type.IsNestedPublic)
                {
                    return GenerateComplexObject(type, createdObjectReferences);
                }
            }
            catch
            {
                return null;
            }
            return null;
        }

        private static object GenerateQueryable(Type queryableType, int size, Dictionary<Type, object> createdObjectReferences)
        {
            object obj2;
            bool isGenericType = queryableType.IsGenericType;
            if (isGenericType)
            {
                obj2 = GenerateCollection(typeof(List<>).MakeGenericType(queryableType.GetGenericArguments()), size, createdObjectReferences);
            }
            else
            {
                obj2 = GenerateArray(typeof(object[]), size, createdObjectReferences);
            }
            if (obj2 == null)
            {
                return null;
            }
            if (isGenericType)
            {
                Type type2 = typeof(IEnumerable<>).MakeGenericType(queryableType.GetGenericArguments());
                Type[] types = new Type[] { type2 };
                object[] parameters = new object[] { obj2 };
                return typeof(Queryable).GetMethod("AsQueryable", types).Invoke(null, parameters);
            }
            return ((IEnumerable) obj2).AsQueryable();
        }

        private static object GenerateTuple(Type type, Dictionary<Type, object> createdObjectReferences)
        {
            int num2;
            Type[] genericArguments = type.GetGenericArguments();
            object[] args = new object[genericArguments.Length];
            bool flag = true;
            ObjectGenerator generator = new ObjectGenerator();
            for (int i = 0; i < genericArguments.Length; i = num2 + 1)
            {
                args[i] = generator.GenerateObject(genericArguments[i], createdObjectReferences);
                flag &= args[i] == null;
                num2 = i;
            }
            if (flag)
            {
                return null;
            }
            return Activator.CreateInstance(type, args);
        }

        private static bool IsTuple(Type genericTypeDefinition) => 
            (((((genericTypeDefinition == typeof(Tuple<>)) || (genericTypeDefinition == typeof(Tuple<,>))) || ((genericTypeDefinition == typeof(Tuple<,,>)) || (genericTypeDefinition == typeof(Tuple<,,,>)))) || (((genericTypeDefinition == typeof(Tuple<,,,,>)) || (genericTypeDefinition == typeof(Tuple<,,,,,>))) || (genericTypeDefinition == typeof(Tuple<,,,,,,>)))) || (genericTypeDefinition == typeof(Tuple<,,,,,,,>)));

        private static void SetPublicFields(Type type, object obj, Dictionary<Type, object> createdObjectReferences)
        {
            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);
            ObjectGenerator generator = new ObjectGenerator();
            foreach (FieldInfo info in fields)
            {
                object obj2 = generator.GenerateObject(info.FieldType, createdObjectReferences);
                info.SetValue(obj, obj2);
            }
        }

        private static void SetPublicProperties(Type type, object obj, Dictionary<Type, object> createdObjectReferences)
        {
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            ObjectGenerator generator = new ObjectGenerator();
            foreach (PropertyInfo info in properties)
            {
                if (info.CanWrite)
                {
                    object obj2 = generator.GenerateObject(info.PropertyType, createdObjectReferences);
                    info.SetValue(obj, obj2, null);
                }
            }
        }

        private class SimpleTypeObjectGenerator
        {
            private long _index = 0L;
            private static readonly Dictionary<Type, Func<long, object>> DefaultGenerators = InitializeGenerators();

            public static bool CanGenerateObject(Type type) => 
                DefaultGenerators.ContainsKey(type);

            public object GenerateObject(Type type)
            {
                long arg = this._index + 1L;
                this._index = arg;
                return DefaultGenerators[type](arg);
            }

            private static Dictionary<Type, Func<long, object>> InitializeGenerators() => 
                new Dictionary<Type, Func<long, object>> { 
                    { 
                        typeof(bool),
                        index => true
                    },
                    { 
                        typeof(byte),
                        index => ((byte) 0x40)
                    },
                    { 
                        typeof(char),
                        index => 'A'
                    },
                    { 
                        typeof(DateTime),
                        index => DateTime.Now
                    },
                    { 
                        typeof(DateTimeOffset),
                        index => new DateTimeOffset(DateTime.Now)
                    },
                    { 
                        typeof(DBNull),
                        index => DBNull.Value
                    },
                    { 
                        typeof(decimal),
                        index => ((decimal) index)
                    },
                    { 
                        typeof(double),
                        index => (index + 0.1)
                    },
                    { 
                        typeof(Guid),
                        index => Guid.NewGuid()
                    },
                    { 
                        typeof(short),
                        index => ((short) (index % 0x7fffL))
                    },
                    { 
                        typeof(int),
                        index => ((int) (index % 0x7fffffffL))
                    },
                    { 
                        typeof(long),
                        index => index
                    },
                    { 
                        typeof(object),
                        index => new object()
                    },
                    { 
                        typeof(sbyte),
                        index => ((sbyte) 0x40)
                    },
                    { 
                        typeof(float),
                        index => ((float) (index + 0.1))
                    },
                    { 
                        typeof(string),
                        index => string.Format(CultureInfo.CurrentCulture, "sample string {0}", new object[] { index })
                    },
                    { 
                        typeof(TimeSpan),
                        index => TimeSpan.FromTicks(0x12d687L)
                    },
                    { 
                        typeof(ushort),
                        index => ((ushort) (index % 0xffffL))
                    },
                    { 
                        typeof(uint),
                        index => ((uint) (((ulong) index) % 0xffffffffUL))
                    },
                    { 
                        typeof(ulong),
                        index => ((ulong) index)
                    },
                    { 
                        typeof(Uri),
                        index => new Uri(string.Format(CultureInfo.CurrentCulture, "http://webapihelppage{0}.com", new object[] { index }))
                    }
                };

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly ObjectGenerator.SimpleTypeObjectGenerator.<>c <>9 = new ObjectGenerator.SimpleTypeObjectGenerator.<>c();
                public static Func<long, object> <>9__2_0;
                public static Func<long, object> <>9__2_1;
                public static Func<long, object> <>9__2_2;
                public static Func<long, object> <>9__2_3;
                public static Func<long, object> <>9__2_4;
                public static Func<long, object> <>9__2_5;
                public static Func<long, object> <>9__2_6;
                public static Func<long, object> <>9__2_7;
                public static Func<long, object> <>9__2_8;
                public static Func<long, object> <>9__2_9;
                public static Func<long, object> <>9__2_10;
                public static Func<long, object> <>9__2_11;
                public static Func<long, object> <>9__2_12;
                public static Func<long, object> <>9__2_13;
                public static Func<long, object> <>9__2_14;
                public static Func<long, object> <>9__2_15;
                public static Func<long, object> <>9__2_16;
                public static Func<long, object> <>9__2_17;
                public static Func<long, object> <>9__2_18;
                public static Func<long, object> <>9__2_19;
                public static Func<long, object> <>9__2_20;

                internal object <InitializeGenerators>b__2_0(long index) => 
                    true;

                internal object <InitializeGenerators>b__2_1(long index) => 
                    ((byte) 0x40);

                internal object <InitializeGenerators>b__2_10(long index) => 
                    ((int) (index % 0x7fffffffL));

                internal object <InitializeGenerators>b__2_11(long index) => 
                    index;

                internal object <InitializeGenerators>b__2_12(long index) => 
                    new object();

                internal object <InitializeGenerators>b__2_13(long index) => 
                    ((sbyte) 0x40);

                internal object <InitializeGenerators>b__2_14(long index) => 
                    ((float) (index + 0.1));

                internal object <InitializeGenerators>b__2_15(long index)
                {
                    object[] args = new object[] { index };
                    return string.Format(CultureInfo.CurrentCulture, "sample string {0}", args);
                }

                internal object <InitializeGenerators>b__2_16(long index) => 
                    TimeSpan.FromTicks(0x12d687L);

                internal object <InitializeGenerators>b__2_17(long index) => 
                    ((ushort) (index % 0xffffL));

                internal object <InitializeGenerators>b__2_18(long index) => 
                    ((uint) (((ulong) index) % 0xffffffffUL));

                internal object <InitializeGenerators>b__2_19(long index) => 
                    ((ulong) index);

                internal object <InitializeGenerators>b__2_2(long index) => 
                    'A';

                internal object <InitializeGenerators>b__2_20(long index)
                {
                    object[] args = new object[] { index };
                    return new Uri(string.Format(CultureInfo.CurrentCulture, "http://webapihelppage{0}.com", args));
                }

                internal object <InitializeGenerators>b__2_3(long index) => 
                    DateTime.Now;

                internal object <InitializeGenerators>b__2_4(long index) => 
                    new DateTimeOffset(DateTime.Now);

                internal object <InitializeGenerators>b__2_5(long index) => 
                    DBNull.Value;

                internal object <InitializeGenerators>b__2_6(long index) => 
                    ((decimal) index);

                internal object <InitializeGenerators>b__2_7(long index) => 
                    (index + 0.1);

                internal object <InitializeGenerators>b__2_8(long index) => 
                    Guid.NewGuid();

                internal object <InitializeGenerators>b__2_9(long index) => 
                    ((short) (index % 0x7fffL));
            }
        }
    }
}

