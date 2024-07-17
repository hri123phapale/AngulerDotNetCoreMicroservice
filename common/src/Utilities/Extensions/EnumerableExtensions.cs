using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Blogposts.Common.Utilities.Extensions
{
    public static class EnumerableExtensions
    {
        #region Public Methods

        /// <summary>
        /// Extension method to create a concurrentDictionary from an IEnumerable
        /// </summary>
        /// <typeparam name="TSource">Source type</typeparam>
        /// <typeparam name="TKey">Key type</typeparam>
        /// <typeparam name="TElement">Element type</typeparam>
        /// <param name="source">An IEnumerable source</param>
        /// <param name="keySelector">selector function to retrieve key for the element</param>
        /// <param name="elementSelector">selector function to retrieve element</param>
        /// <returns></returns>
        public static ConcurrentDictionary<TKey, TElement> ToConcurrentDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
        {
            return new ConcurrentDictionary<TKey, TElement>(source.ToDictionary(keySelector, elementSelector));
        }

        /// <summary>
        /// Finds the index of the first item matching the expression in an IEnumerable
        /// </summary>
        /// <param name="items">IEnumerable to search</param>
        /// <param name="predicate">The expression to test against</param>
        /// <returns>The first index matching the item, or -1 if no items match</returns>
        public static int FindIndex<T>(this IEnumerable<T> items, Func<T, bool> predicate)
        {
            if (items == null)
            {
                throw new ArgumentNullException("items");
            }
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            int returnVal = 0;
            foreach (var item in items)
            {
                if (predicate(item))
                {
                    return returnVal;
                }
                returnVal++;
            }
            return -1;
        }

        /// <summary>
        /// Finds the index of the first occurrence of an item in IEnumberable
        /// </summary>
        /// <param name="items">The IEnumerable to search</param>
        /// <param name="item">The item to find</param>
        /// <returns></returns>
        public static int IndexOf<T>(this IEnumerable<T> items, T item)
        {
            return items.FindIndex(i => EqualityComparer<T>.Default.Equals(item, i));
        }

        /// <summary>
        /// Checks if the enumerable a contains all items of enumerable b
        /// </summary>
        /// <typeparam name="T">The type of the items</typeparam>
        /// <param name="a">The lists</param>
        /// <param name="b">The sublist</param>
        /// <returns>True if enumerable a contains all items of enumerable b</returns>
        public static bool ContainsAllItems<T>(this IEnumerable<T> a, IEnumerable<T> b)
        {
            return !b.Except(a).Any();
        }

        /// <summary>
        /// Checks if the enumerable a contains any of the items in enumerable b
        /// </summary>
        /// <typeparam name="T">The type of the items</typeparam>
        /// <param name="a">The lists</param>
        /// <param name="b">The sublist</param>
        /// <returns>True if enumerable a contains all items of enumerable b</returns>
        public static bool ContainsAnyItems<T>(this IEnumerable<T> a, IEnumerable<T> b)
        {
            return a.Intersect(b).Any();
        }

        /// <summary>
        /// Runs an action on each item in the list and does so recursively
        /// us the children function to retrieve any children
        /// </summary>
        /// <typeparam name="T">The list item type</typeparam>
        /// <param name="source">Source list</param>
        /// <param name="children">Function that returns a list of children if any</param>
        /// <param name="action">The action to perform on each item</param>
        public static void ForEachRecursive<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> children, Action<T> action)
        {
            if (source == null)
            {
                return;
            }
            foreach (T item in source)
            {
                action(item);
                IEnumerable<T> itemChildren = children(item);
                if (itemChildren != null)
                {
                    ForEachRecursive(itemChildren, children, action);
                }
            }
        }

        /// <summary>
        /// A parallel ForEach extensions utilizing TPL Dataflow's <see cref="ActionBlock{TInput}"/>
        /// </summary>
        /// <typeparam name="T">The collection object type</typeparam>
        /// <param name="source">The source collection</param>
        /// <param name="body">The function to execute</param>
        /// <param name="maxDegreeOfParallelism">The maximum degree of parallelism</param>
        /// <param name="scheduler">The task scheduler</param>
        /// <returns></returns>
        public static Task ParallelForEachAsync<T>(this IEnumerable<T> source, Func<T, Task> body, int maxDegreeOfParallelism = DataflowBlockOptions.Unbounded, TaskScheduler scheduler = null)
        {
            var options = new ExecutionDataflowBlockOptions
            {
                MaxDegreeOfParallelism = maxDegreeOfParallelism
            };
            if (scheduler != null)
            {
                options.TaskScheduler = scheduler;
            }

            var block = new ActionBlock<T>(body, options);

            foreach (var item in source)
            {
                block.Post(item);
            }

            block.Complete();
            return block.Completion;
        }

        /// <summary>
        /// Checks if the collection is not null and has items
        /// </summary>
        public static bool NotNullAny<T>(this IEnumerable<T> collection)
        {
            return collection != null && collection.Any();
        }

        public static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> collection, int batchSize)
        {
            using (var enumerator = collection.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    yield return enumerator.EnumerateSome(batchSize);
                }
            }
        }

        /// <summary>
        /// Returns the set of elements in the first sequence which aren't
        /// in the second sequence, according to a given key selector.
        /// </summary>
        /// <remarks>
        /// Source from MoreLinq library. ExceptBy is natively supported in .NET6
        /// </remarks>
        public static IEnumerable<TSource> ExceptBy<TSource, TKey>(this IEnumerable<TSource> first,
            IEnumerable<TSource> second,
            Func<TSource, TKey> keySelector,
            IEqualityComparer<TKey>? keyComparer = default)
        {
            if (first == null)
            {
                throw new ArgumentNullException(nameof(first));
            }
            if (second == null)
            {
                throw new ArgumentNullException(nameof(second));
            }
            if (keySelector == null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }

            return _();
            IEnumerable<TSource> _()
            {
                var keys = new HashSet<TKey>(second.Select(keySelector), keyComparer);
                foreach (var element in first)
                {
                    var key = keySelector(element);
                    if (keys.Contains(key))
                    {
                        continue;
                    }
                    yield return element;
                    keys.Add(key);
                }
            }
        }

        /// <summary>
        /// Returns an empty <see cref="IEnumerable{T}"/> when the <paramref name="source"/> is <c>null</c>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> source)
        {
            return source ?? Enumerable.Empty<T>();
        }

        /// <summary>
        /// Returns a flattened collection of <see cref="T"/> from recursively retrieving nested collections
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IEnumerable<T> SelectManyRecursive<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> selector)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector == null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return !source.Any() ? source :
                source.Concat(
                    source
                    .SelectMany(i => selector(i).EmptyIfNull())
                    .SelectManyRecursive(selector)
                );
        }

        public static bool NotAny<T>(this IEnumerable<T> source)
        {
            return !source.Any();
        }

        public static bool NotAny<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            return !source.Any(predicate);
        }

        #endregion

        #region Private Methods

        private static IEnumerable<T> EnumerateSome<T>(this IEnumerator<T> enumerator, int count)
        {
            var list = new List<T>(count);
            int i = 0;
            while (true)
            {
                list.Add(enumerator.Current);

                i++;
                if (i == count)
                {
                    break;
                }

                if (!enumerator.MoveNext())
                {
                    break;
                }
            }
            return list;
        }

        #endregion
    }
}