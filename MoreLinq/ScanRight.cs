#region License and Terms
// MoreLINQ - Extensions to LINQ to Objects
// Copyright (c) 2017 Leandro F. Vieira (leandromoh). All rights reserved.
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

namespace MoreLinq
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    static partial class MoreEnumerable
    {
        /// <summary>
        /// Peforms a right-associative scan (inclusive prefix) on a sequence of elements.
        /// This operator is the right-associative version of the 
        /// <see cref="MoreEnumerable.Scan{TSource}(IEnumerable{TSource}, Func{TSource, TSource, TSource})"/> LINQ operator.
        /// </summary>
        /// <typeparam name="TSource">Type of elements in source sequence.</typeparam>
        /// <param name="source">Source sequence.</param>
        /// <param name="func">
        /// A right-associative accumulator function to be invoked on each element.
        /// Its first argument is the current value in the sequence; second argument is the previous accumulator value.
        /// </param>
        /// <returns>The scanned sequence.</returns>
        /// <example>
        /// <code>
        /// var result = Enumerable.Range(1, 5).Select(i => i.ToString()).ScanRight((a, b) => string.Format("({0}/{1})", a, b));
        /// </code>
        /// The <c>result</c> variable will contain <c>[ "(1+(2+(3+(4+5))))", "(2+(3+(4+5)))", "(3+(4+5))", "(4+5)", "5" ]</c>.
        /// </example>
        /// <remarks>
        /// This operator uses deferred execution and streams its results.
        /// </remarks>

        public static IEnumerable<TSource> ScanRight<TSource>(this IEnumerable<TSource> source, Func<TSource, TSource, TSource> func)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (func == null) throw new ArgumentNullException(nameof(func));

            return ScanRightImpl(source,
                                 list => list.Count > 0
                                       ? ScanRightSeedCount.Some(list.Last(), list.Count - 1)
                                       : ScanRightSeedCount<TSource>.None,
                                 func);
        }

        /// <summary>
        /// Peforms a right-associative scan (inclusive prefix) on a sequence of elements.
        /// The specified seed value is used as the initial accumulator value.
        /// This operator is the right-associative version of the 
        /// <see cref="MoreEnumerable.Scan{TSource, TState}(IEnumerable{TSource}, TState, Func{TState, TSource, TState})"/> LINQ operator.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
        /// <param name="source">Source sequence.</param>
        /// <param name="seed">The initial accumulator value.</param>
        /// <param name="func">A right-associative accumulator function to be invoked on each element.</param>
        /// <returns>The scanned sequence.</returns>
        /// <example>
        /// <code>
        /// var result = Enumerable.Range(1, 4).ScanRight("5", (a, b) => string.Format("({0}/{1})", a, b));
        /// </code>
        /// The <c>result</c> variable will contain <c>[ "(1+(2+(3+(4+5))))", "(2+(3+(4+5)))", "(3+(4+5))", "(4+5)", "5" ]</c>.
        /// </example>
        /// <remarks>
        /// This operator uses deferred execution and streams its results.
        /// </remarks>

        public static IEnumerable<TAccumulate> ScanRight<TSource, TAccumulate>(this IEnumerable<TSource> source, TAccumulate seed, Func<TSource, TAccumulate, TAccumulate> func)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (func == null) throw new ArgumentNullException(nameof(func));

            return ScanRightImpl(source, list => ScanRightSeedCount.Some(seed, list.Count), func);
        }

        static class ScanRightSeedCount
        {
            public static ScanRightSeedCount<T> Some<T>(T seed, int count) =>
                new ScanRightSeedCount<T>(true, seed, count);
        }

        struct ScanRightSeedCount<T>
        {
            public readonly bool Seeded;
            public readonly T Seed;
            public readonly int Count;

            public static readonly ScanRightSeedCount<T> None = new ScanRightSeedCount<T>(false, default(T), 0);

            public ScanRightSeedCount(bool seeded, T seed, int count)
            {
                Seeded = seeded;
                Seed = seed;
                Count = count;
            }
        }

        private static IEnumerable<TResult> ScanRightImpl<TSource, TResult>(IEnumerable<TSource> source, Func<IList<TSource>, ScanRightSeedCount<TResult>> seeder, Func<TSource, TResult, TResult> func)
        {
            var list = (source as IList<TSource>) ?? source.ToList();

            var r = seeder(list);
            var i = r.Count;
            if (!r.Seeded)
                yield break;
            var accumulator = r.Seed;
            var stack = new Stack<TResult>(i + 1);
            stack.Push(accumulator);

            while (i-- > 0)
            {
                accumulator = func(list[i], accumulator);
                stack.Push(accumulator);
            }

            foreach (var item in stack)
                yield return item;
        }
    }
}