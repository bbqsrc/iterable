using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace Iterable
{
    public static class Iterable
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> Filter<T>(this IEnumerable<T> enumerable, Func<T, uint, bool> predicate) {
            uint index = 0;
            
            foreach (var element in enumerable) {
                if (predicate(element, index)) {
                    yield return element;
                }
                
                checked {
                    index += 1;
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> Filter<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate) {
            foreach (var element in enumerable) {
                if (predicate(element)) {
                    yield return element;
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<U> Map<T, U>(this IEnumerable<T> enumerable, Func<T, uint, U> selector) {
            uint index = 0;
            
            foreach (var element in enumerable) {
                yield return selector(element, index);
                
                checked {
                    index += 1;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<U> Map<T, U>(this IEnumerable<T> enumerable, Func<T, U> selector) {
            foreach (var element in enumerable) {
                yield return selector(element);
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<U> FlatMap<T, U>(this IEnumerable<IEnumerable<T>> enumerable, Func<T, U> selector) {
            foreach (var innerEnum in enumerable) {
                foreach (var element in innerEnum) {
                    yield return selector(element);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<U> FlatMap<T, U>(this IEnumerable<T> enumerable, Func<T, IEnumerable<U>> selector)
        {
            foreach (var value in enumerable)
            {
                var innerEnum = selector(value);
                foreach (var element in innerEnum)
                {
                    yield return element;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> Flatten<T>(this IEnumerable<IEnumerable<T>> enumerable) {
            foreach (var innerEnum in enumerable) {
                foreach (var element in innerEnum) {
                    yield return element;
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<U> FilterMap<T, U>(this IEnumerable<T> enumerable, Func<T, U?> selector)
            where U : class {
            foreach (var element in enumerable) {
                var result = selector(element);
        
                if (result != null) {
                    yield return result;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<U> FilterMap<T, U>(this IEnumerable<T> enumerable, Func<T, U?> selector)
            where U: struct
        {
            foreach (var element in enumerable) {
                var result = selector(element);

                if (result.HasValue) {
                    yield return result!.Value;
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<U> FilterMap<T, U>(this IEnumerable<T> enumerable, Func<T, uint, U?> selector) where U: class {
            uint index = 0;
            
            foreach (var element in enumerable) {
                var result = selector(element, index);
                
                if (result != null) {
                    yield return result;
                }
                
                checked {
                    index += 1;
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<U> FilterMap<T, U>(this IEnumerable<T> enumerable, Func<T, uint, U?> selector)
            where U: struct 
        {
            uint index = 0;
            
            foreach (var element in enumerable) {
                var result = selector(element, index);

                if (result.HasValue) {
                    yield return result!.Value;
                }
                
                checked {
                    index += 1;
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<T> ToList<T>(this IEnumerable<T> enumerable) where T: notnull {
            var output = new List<T>();

            foreach (var result in enumerable) {
                output.Add(result);
            }

            return output;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] ToArray<T>(this IEnumerable<T> enumerable) where T: notnull {
            return ToList(enumerable).ToArray();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HashSet<T> ToSet<T>(this IEnumerable<T> enumerable) where T: notnull {
            var output = new HashSet<T>();

            foreach (var result in enumerable) {
                output.Add(result);
            }

            return output;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SortedSet<T> ToSortedSet<T>(this IEnumerable<T> enumerable) where T: notnull {
            var output = new SortedSet<T>();

            foreach (var result in enumerable) {
                output.Add(result);
            }

            return output;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> enumerable) where T: notnull {
            var output = new ObservableCollection<T>();

            foreach (var result in enumerable) {
                output.Add(result);
            }

            return output;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static C ToCollection<T, C>(this IEnumerable<T> enumerable)
            where T: notnull
            where C: ICollection<T>, new()
        {
            var output = new C();

            foreach (var result in enumerable) {
                output.Add(result);
            }

            return output;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<K, V> ToDict<K, V>(this IEnumerable<Tuple<K, V>> enumerable) where K: notnull {
            var output = new Dictionary<K, V>();
            
            foreach (var (key, value) in enumerable) {
                output.Add(key, value);
            }

            return output;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<K, V> ToDict<K, V>(this IEnumerable<(K, V)> enumerable) where K: notnull {
            var output = new Dictionary<K, V>();
            
            foreach (var (key, value) in enumerable) {
                output.Add(key, value);
            }

            return output;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SortedDictionary<K, V> ToSortedDict<K, V>(this IEnumerable<Tuple<K, V>> enumerable) where K: notnull {
            var output = new SortedDictionary<K, V>();
            
            foreach (var (key, value) in enumerable) {
                output.Add(key, value);
            }

            return output;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SortedDictionary<K, V> ToSortedDict<K, V>(this IEnumerable<(K, V)> enumerable) where K: notnull {
            var output = new SortedDictionary<K, V>();
            
            foreach (var (key, value) in enumerable) {
                output.Add(key, value);
            }

            return output;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableDictionary<K, V> ToImmutableDict<K, V>(this IEnumerable<(K, V)> enumerable) where K: notnull {
            var builder = ImmutableDictionary.CreateBuilder<K, V>();
            
            foreach (var (key, value) in enumerable) {
                builder.Add(key, value);
            }

            return builder.ToImmutable();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableList<T> ToImmutableList<T>(this IEnumerable<T> enumerable) where T: notnull {
            var builder = ImmutableList.CreateBuilder<T>();
            
            foreach (var value in enumerable) {
                builder.Add(value);
            }

            return builder.ToImmutable();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableHashSet<T> ToImmutableSet<T>(this IEnumerable<T> enumerable) where T: notnull {
            var builder = ImmutableHashSet.CreateBuilder<T>();
            
            foreach (var value in enumerable) {
                builder.Add(value);
            }

            return builder.ToImmutable();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ImmutableArray<T> ToImmutableArray<T>(this IEnumerable<T> enumerable) where T: notnull {
            var builder = ImmutableArray.CreateBuilder<T>();
            
            foreach (var value in enumerable) {
                builder.Add(value);
            }

            return builder.ToImmutable();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? First<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate) where T: class {
            foreach (var item in enumerable) {
                if (predicate(item)) {
                    return item;
                }
            }

            return null;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? First<T>(this IEnumerable<T> enumerable) where T: class {
            foreach (var item in enumerable) {
                return item;
            }

            return null;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T FirstOrDefault<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate) where T: struct {
            foreach (var item in enumerable) {
                if (predicate(item)) {
                    return item;
                }
            }

            return default;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T FirstOrDefault<T>(this IEnumerable<T> enumerable) where T: struct {
            foreach (var item in enumerable) {
                return item;
            }

            return default;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? First<T>(this IEnumerable<T?> enumerable, Func<T?, bool> predicate) where T: struct {
            foreach (var item in enumerable) {
                if (predicate(item)) {
                    return item;
                }
            }

            return null;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? First<T>(this IEnumerable<T?> enumerable) where T: struct {
            foreach (var item in enumerable) {
                return item;
            }

            return null;
        }
    }
}