#if NETCOREAPP3_1
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Iterable.Immutable
{
    public static class Immutable
    {
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

    }
}
#endif