#region License and Terms
// MoreLINQ - Extensions to LINQ to Objects
// Copyright (c) 2009 Atif Aziz. All rights reserved.
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
#endregion

#if !NO_OBSERVABLES

namespace MoreLinq
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// TODO
    /// </summary>

    public interface IConnected<out T>
    {
        /// <summary>
        /// TODO
        /// </summary>

        Func<TResult> Connect<TState, TResult>(
            TState seed, Func<TState, T, TState> accumulator,
            Func<TState, TResult> resultSelector);
    }

    sealed class Connected<T> : IConnected<T>
    {
        readonly IObservable<T> _source;

        public Connected(IObservable<T> source) => _source = source;

        public Func<TResult> Connect<TState, TResult>(
            TState seed, Func<TState, T, TState> accumulator,
            Func<TState, TResult> resultSelector)
        {
            var state = seed;
            var a = accumulator;
            IDisposable subscription = null;
            void DisposeSubscription()
            {
                if (subscription == null)
                    return;
                subscription.Dispose();
                subscription = null;
            }
            subscription = _source.Subscribe(Observer.Create((T item) => state = a(state, item), _ => DisposeSubscription(), DisposeSubscription));
            return () => resultSelector(state);
        }
    }

    /// <summary>
    /// TODO
    /// </summary>

    public static class Connected
    {
        /// <summary>
        /// TODO
        /// </summary>

        public static IConnected<T> Connect<T>(this IObservable<T> source) =>
            new Connected<T>(source);

        /// <summary>
        /// Subscribes an accumulator function to the source and returns a
        /// function for retrieving the accumulated state.
        /// </summary>

        public static Func<TState> Connect<TState, T>(
            this IConnected<T> source,
            TState seed, Func<TState, T, TState> accumulator) =>
            source.Connect(seed, accumulator, s => s);
    }

    /// <summary>
    /// Aggregators for use in concurrent aggregations.
    /// </summary>

    public static class Aggregator
    {
        /// <summary>
        /// Subscribes to the source and returns a function that can be used
        /// to retrieve the sum of integers observed on the source.
        /// </summary>

        public static Func<int> Sum(this IConnected<int> source) =>
            source.Connect(0, (s, x) => s + x);

        /// <summary>
        /// Subscribes to the source and returns a function that can be used
        /// to retrieve the count of elements observed on the source.
        /// </summary>

        public static Func<int> Count<T>(this IConnected<T> source) =>
            source.Connect(0, (s, _) => s + 1);

        /// <summary>
        /// Subscribes an accumulator function to the source and returns a
        /// function that can be used to retrieve the result of the accumulation
        /// for one or more elements observed on the source.
        /// </summary>
        /// <remarks>
        /// The returned function throws <see cref="InvalidOperationException"/>
        /// if no elements were observed from the source.
        /// </remarks>

        public static Func<T> Some<T>(this IConnected<T> source, Func<T, T, T> accumulator) =>
            source.Connect(
                (Count: 0, State: default(T)),
                (s, e) => (s.Count + 1, s.Count > 0 ? accumulator(s.State, e) : e),
                s => s.Count > 0 ? s.State : throw new InvalidOperationException());

        /// <summary>
        /// Subscribes to the source and returns a function that can be used
        /// to retrieve the smallest element observed on the source.
        /// </summary>

        public static Func<T> Min<T>(this IConnected<T> source)
            where T : IComparable<T> =>
            source.Some((x, y) => x.CompareTo(y) < 0 ? x : y);

        /// <summary>
        /// Subscribes to the source and returns a function that can be used
        /// to retrieve the largest element observed on the source.
        /// </summary>

        public static Func<T> Max<T>(this IConnected<T> source)
            where T : IComparable<T> =>
            source.Some((x, y) => x.CompareTo(y) > 0 ? x : y);

        /// <summary>
        /// Subscribes to the source and returns a function that can be used
        /// to retrieve a list containing all the elements observed on the
        /// source.
        /// </summary>

        public static Func<List<T>> List<T>(this IConnected<T> source) =>
            source.Collect(new List<T>());

        /// <summary>
        /// Subscribes to the source and returns a function that can be used
        /// to retrieve a set containing all the unique elements observed on the
        /// source.
        /// </summary>

        public static Func<ISet<T>> Distinct<T>(this IConnected<T> source, IEqualityComparer<T> comparer = null) =>
            source.Collect(new HashSet<T>(comparer));

        /// <summary>
        /// Subscribes to the source and returns a function that can be used
        /// to retrieve the collection with all tge elements observed on the
        /// source added to it.
        /// </summary>

        public static Func<TCollection> Collect<T, TCollection>(this IConnected<T> source, TCollection collection)
            where TCollection : ICollection<T> =>
            source.Connect(collection, (s, t) => { s.Add(t); return s; }, s => s);
    }
}

#endif