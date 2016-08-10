using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Principal;
using opcode4.core.Data;
using opcode4.core.Model.Identity;
using opcode4.core.Model.Log;
using opcode4.utilities;

namespace opcode4.core.Helpers
{
    public static class Extensions
    {
        public static void Default<T>(this T entity)
        {
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(entity))
            {
                var attr = (DefaultValueAttribute) property.Attributes[typeof (DefaultValueAttribute)];
                if (attr != null)
                {
                    property.SetValue(entity, attr.Value);
                }
            }
        }

        public static void Using<T>(this T client, Action<T> work, string connectionString = null) where T : IDAOBase
        {
            using (client)
            {
                client.OpenConnection(connectionString?? ConfigUtils.ConnectionString);
                work(client);
            }
        }

        public static TResult Using<T, TResult>(this T client, Func<T, TResult> work, string connectionString = null) where T : IDAOBase
        {
            using (client)
            {
                client.OpenConnection(connectionString ?? ConfigUtils.ConnectionString);
                return work(client);
            }
        }

        public static void UsingTransaction<T>(this T client, Action<T> work, string connectionString = null) where T : IDAOBase
        {
            using (client)
            {
                client.OpenConnection(connectionString ?? ConfigUtils.ConnectionString);
                client.StartTransaction();
                try
                {
                    work(client);
                    client.CommitTransaction();
                }
                catch (Exception)
                {
                    client.RollbackTransaction();
                    throw;
                }

            }
        }
        
        public static bool EqualsTo<T>(this T? left, T? right)
            where T : struct, IComparable<T>
        {
            return left.GetValueOrDefault().Equals(right.GetValueOrDefault());
        }

        public static bool ShouldLog(this IPrincipal principal, LogEventType logLevel)
        {
            var identity = principal.Identity as CustomIdentity;
            if (identity == null)
                return true;

            return identity.LogLevel <= logLevel;
        }

        public static bool IsEnumerable<T>(this T obj)
        {
            return typeof (IEnumerable).IsAssignableFrom(typeof (T));
        }


        public static T[] Slice<T>(this T[] source, int start, int end)
        {
            // Handles negative ends.
            if (end < 0)
            {
                end = source.Length + end;
            }
            int len = end - start;

            // Return new array.
            T[] res = new T[len];
            for (int i = 0; i < len; i++)
            {
                res[i] = source[i + start];
            }
            return res;
        }

        public static T[] Slice<T>(this T[] source, int start)
        {
            return Slice(source, start, source.Length);
        }

        public static int IndexOfPattern(this byte[] bytes, byte[] pattern)
        {
            if (bytes == null || pattern == null || bytes.Length < pattern.Length)
                return -1;

            // precomputing this shaves some seconds from the loop execution
            int maxloop = bytes.Length - pattern.Length;
            for (int i = 0; i < maxloop; i++)
            {
                if (bytes[i] == pattern[0])
                {
                    bool ismatch = true;
                    for (int j = 1; j < pattern.Length; j++)
                    {
                        if (pattern[j] != bytes[i + j])
                        {
                            ismatch = false;
                            break;
                        }
                    }
                    if (ismatch)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        public static int IndexOfPattern<T>(this T[] bytes, T[] pattern)
        {
            if (bytes == null || pattern == null || bytes.Length < pattern.Length)
                return -1;

            // precomputing this shaves some seconds from the loop execution
            int maxloop = bytes.Length - pattern.Length;
            for (int i = 0; i < maxloop; i++)
            {
                if (bytes[i].Equals(pattern[0]))
                {
                    bool ismatch = true;
                    for (var j = 1; j < pattern.Length; j++)
                    {
                        if (!pattern[j].Equals(bytes[i + j]))
                        {
                            ismatch = false;
                            break;
                        }
                    }
                    if (ismatch)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        public static T[] SubArray<T>(this T[] data, int index, int length)
        {
            T[] result = new T[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }

        public static bool IsSimilaryTo(this byte[] original, byte[] byteArray, int tolerancePercent)
        {
            if (original == null && byteArray == null)
                return true;
            if (original == null || byteArray == null)
                return false;

            if (original.Length != byteArray.Length)
                return false;

            var diffCount = original.Where((t, i) => t != byteArray[i]).Count();

            var difPercent = diffCount * 100 / original.Length;
            return difPercent <= tolerancePercent;


        }

        public static IEnumerable<TSource> Duplicates<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector)
        {
            return source.GroupBy(selector).Where(i => i.Count() > 1).SelectMany(i => i);
        }

        public static IEnumerable<TSource> Duplicates<TSource>(this IEnumerable<TSource> source)
        {
            return source.Duplicates(i => i);
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector)
        {
            return source.GroupBy(selector).Select(g => g.First());
        }

        public static void Clear(this DirectoryInfo directory, string skipName = "")
        {
            directory.EnumerateFiles("*", SearchOption.AllDirectories).ToList().ForEach(f =>
            {
                if(f.Name.Equals(skipName, StringComparison.InvariantCultureIgnoreCase))
                if (f.IsReadOnly)
                    f.IsReadOnly = false;
                f.Delete();
            });

            // d.Attributes = d.Attributes & ~FileAttributes.ReadOnly;
            directory.EnumerateDirectories().ToList().ForEach(d => d.Delete(true));
            
        }

        public static string TrimEnd(this string source, string suffixToRemove)
        {
            if (source.EndsWith(suffixToRemove))
            {
                source = source.Substring(0, source.Length - suffixToRemove.Length);
            }

            return source;
        }

        public static object EntireToJS<T>(this Enum enumValue) where T : Attribute
        {
            if (typeof (T).Name.Equals("EnumMemberAttribute"))
                return (from e in enumValue.GetType().GetFields()
                    let attribute = e.GetCustomAttributes(typeof (T), false).FirstOrDefault() as EnumMemberAttribute
                    where attribute != null
                    select new {Id = (int) e.GetValue(null), e.Name, Descr = attribute.Value,}).ToList();

            return enumValue.EntireToJS();
        }

        public static object EntireToJS(this Enum enumValue)
        {
            return
                (from object e in Enum.GetValues(enumValue.GetType()) select new { Id = (int)e, Name = e.ToString() })
                    .ToList();
        }

    }
}
