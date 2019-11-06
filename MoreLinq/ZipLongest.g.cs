#region License and Terms
// MoreLINQ - Extensions to LINQ to Objects
// Copyright (c) 2019 Pierre Lando. All rights reserved.
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

    static partial class MoreEnumerable
    {
        /// <summary>
        /// Returns a projection of tuples, where each tuple contains the N-th
        /// element from each of the input sequences. The resulting sequence
        /// will always be as long as the longest of input sequences where the
        /// default value of each of the shorter sequence element types is used
        /// for padding.
        /// </summary>
        /// <typeparam name="T1">Type of elements in first input sequence.</typeparam>
        /// <typeparam name="T2">Type of elements in second input sequence.</typeparam>
        /// <typeparam name="TResult">Type of elements in result sequence.</typeparam>
        /// <param name="first">The first source sequence.</param>
        /// <param name="second">The second source sequence.</param>
        /// <param name="resultSelector">
        /// Function to apply to each tuple of elements.</param>
        /// <returns>
        /// A projection of tuples, where each tuple contains the N-th element
        /// from each of the argument sequences.</returns>
        /// <remarks>
        /// This operator uses deferred execution and streams its results.
        /// </remarks>

        public static IEnumerable<TResult> ZipLongest<T1, T2, TResult>(
            this IEnumerable<T1> first,
            IEnumerable<T2> second,
            Func<T1, T2, TResult> resultSelector)
        {
            if (first == null) throw new ArgumentNullException(nameof(first));
            if (second == null) throw new ArgumentNullException(nameof(second));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return _(); IEnumerable<TResult> _()
            {
                IEnumerator<T1> e1 = null;
                IEnumerator<T2> e2 = null;

                try
                {
                    e1 = first.GetEnumerator();
                    e2 = second.GetEnumerator();

                    var v1 = default(T1);
                    var v2 = default(T2);

                    // | is used instead of || in purpose. All operands have to be evaluated.
                    while (
                        ZipLongestHelper.MoveNextOrDispose<T1>(ref e1, ref v1) |
                        ZipLongestHelper.MoveNextOrDispose<T2>(ref e2, ref v2))
                    {
                        yield return resultSelector(v1, v2);
                    }
                }
                finally
                {
                    e1?.Dispose();
                    e2?.Dispose();
                }
            }
        }

        /// <summary>
        /// Returns a sequence of tuples, where each tuple contains the N-th
        /// element from each of the input sequences. The resulting sequence
        /// will always be as long as the longest of input sequences where the
        /// default value of each of the shorter sequence element types is used
        /// for padding.
        /// </summary>
        /// <typeparam name="T1">Type of elements in first input sequence.</typeparam>
        /// <typeparam name="T2">Type of elements in second input sequence.</typeparam>
        /// <param name="first">The first source sequence.</param>
        /// <param name="second">The second source sequence.</param>
        /// <returns>
        /// A sequence of tuples, where each tuple contains the N-th element
        /// from each of the argument sequences.</returns>
        /// <remarks>
        /// This operator uses deferred execution and streams its results.
        /// </remarks>

        public static IEnumerable<(T1, T2)> ZipLongest<T1, T2>(
            this IEnumerable<T1> first,
            IEnumerable<T2> second)
        {
            return ZipLongest(
                first,
                second,
                ValueTuple.Create);
        }

        /// <summary>
        /// Returns a projection of tuples, where each tuple contains the N-th
        /// element from each of the input sequences. The resulting sequence
        /// will always be as long as the longest of input sequences where the
        /// default value of each of the shorter sequence element types is used
        /// for padding.
        /// </summary>
        /// <typeparam name="T1">Type of elements in first input sequence.</typeparam>
        /// <typeparam name="T2">Type of elements in second input sequence.</typeparam>
        /// <typeparam name="T3">Type of elements in third input sequence.</typeparam>
        /// <typeparam name="TResult">Type of elements in result sequence.</typeparam>
        /// <param name="first">The first source sequence.</param>
        /// <param name="second">The second source sequence.</param>
        /// <param name="third">The third source sequence.</param>
        /// <param name="resultSelector">
        /// Function to apply to each tuple of elements.</param>
        /// <returns>
        /// A projection of tuples, where each tuple contains the N-th element
        /// from each of the argument sequences.</returns>
        /// <remarks>
        /// This operator uses deferred execution and streams its results.
        /// </remarks>

        public static IEnumerable<TResult> ZipLongest<T1, T2, T3, TResult>(
            this IEnumerable<T1> first,
            IEnumerable<T2> second,
            IEnumerable<T3> third,
            Func<T1, T2, T3, TResult> resultSelector)
        {
            if (first == null) throw new ArgumentNullException(nameof(first));
            if (second == null) throw new ArgumentNullException(nameof(second));
            if (third == null) throw new ArgumentNullException(nameof(third));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return _(); IEnumerable<TResult> _()
            {
                IEnumerator<T1> e1 = null;
                IEnumerator<T2> e2 = null;
                IEnumerator<T3> e3 = null;

                try
                {
                    e1 = first.GetEnumerator();
                    e2 = second.GetEnumerator();
                    e3 = third.GetEnumerator();

                    var v1 = default(T1);
                    var v2 = default(T2);
                    var v3 = default(T3);

                    // | is used instead of || in purpose. All operands have to be evaluated.
                    while (
                        ZipLongestHelper.MoveNextOrDispose<T1>(ref e1, ref v1) |
                        ZipLongestHelper.MoveNextOrDispose<T2>(ref e2, ref v2) |
                        ZipLongestHelper.MoveNextOrDispose<T3>(ref e3, ref v3))
                    {
                        yield return resultSelector(v1, v2, v3);
                    }
                }
                finally
                {
                    e1?.Dispose();
                    e2?.Dispose();
                    e3?.Dispose();
                }
            }
        }

        /// <summary>
        /// Returns a sequence of tuples, where each tuple contains the N-th
        /// element from each of the input sequences. The resulting sequence
        /// will always be as long as the longest of input sequences where the
        /// default value of each of the shorter sequence element types is used
        /// for padding.
        /// </summary>
        /// <typeparam name="T1">Type of elements in first input sequence.</typeparam>
        /// <typeparam name="T2">Type of elements in second input sequence.</typeparam>
        /// <typeparam name="T3">Type of elements in third input sequence.</typeparam>
        /// <param name="first">The first source sequence.</param>
        /// <param name="second">The second source sequence.</param>
        /// <param name="third">The third source sequence.</param>
        /// <returns>
        /// A sequence of tuples, where each tuple contains the N-th element
        /// from each of the argument sequences.</returns>
        /// <remarks>
        /// This operator uses deferred execution and streams its results.
        /// </remarks>

        public static IEnumerable<(T1, T2, T3)> ZipLongest<T1, T2, T3>(
            this IEnumerable<T1> first,
            IEnumerable<T2> second,
            IEnumerable<T3> third)
        {
            return ZipLongest(
                first,
                second,
                third,
                ValueTuple.Create);
        }

        /// <summary>
        /// Returns a projection of tuples, where each tuple contains the N-th
        /// element from each of the input sequences. The resulting sequence
        /// will always be as long as the longest of input sequences where the
        /// default value of each of the shorter sequence element types is used
        /// for padding.
        /// </summary>
        /// <typeparam name="T1">Type of elements in first input sequence.</typeparam>
        /// <typeparam name="T2">Type of elements in second input sequence.</typeparam>
        /// <typeparam name="T3">Type of elements in third input sequence.</typeparam>
        /// <typeparam name="T4">Type of elements in fourth input sequence.</typeparam>
        /// <typeparam name="TResult">Type of elements in result sequence.</typeparam>
        /// <param name="first">The first source sequence.</param>
        /// <param name="second">The second source sequence.</param>
        /// <param name="third">The third source sequence.</param>
        /// <param name="fourth">The fourth source sequence.</param>
        /// <param name="resultSelector">
        /// Function to apply to each tuple of elements.</param>
        /// <returns>
        /// A projection of tuples, where each tuple contains the N-th element
        /// from each of the argument sequences.</returns>
        /// <remarks>
        /// This operator uses deferred execution and streams its results.
        /// </remarks>

        public static IEnumerable<TResult> ZipLongest<T1, T2, T3, T4, TResult>(
            this IEnumerable<T1> first,
            IEnumerable<T2> second,
            IEnumerable<T3> third,
            IEnumerable<T4> fourth,
            Func<T1, T2, T3, T4, TResult> resultSelector)
        {
            if (first == null) throw new ArgumentNullException(nameof(first));
            if (second == null) throw new ArgumentNullException(nameof(second));
            if (third == null) throw new ArgumentNullException(nameof(third));
            if (fourth == null) throw new ArgumentNullException(nameof(fourth));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return _(); IEnumerable<TResult> _()
            {
                IEnumerator<T1> e1 = null;
                IEnumerator<T2> e2 = null;
                IEnumerator<T3> e3 = null;
                IEnumerator<T4> e4 = null;

                try
                {
                    e1 = first.GetEnumerator();
                    e2 = second.GetEnumerator();
                    e3 = third.GetEnumerator();
                    e4 = fourth.GetEnumerator();

                    var v1 = default(T1);
                    var v2 = default(T2);
                    var v3 = default(T3);
                    var v4 = default(T4);

                    // | is used instead of || in purpose. All operands have to be evaluated.
                    while (
                        ZipLongestHelper.MoveNextOrDispose<T1>(ref e1, ref v1) |
                        ZipLongestHelper.MoveNextOrDispose<T2>(ref e2, ref v2) |
                        ZipLongestHelper.MoveNextOrDispose<T3>(ref e3, ref v3) |
                        ZipLongestHelper.MoveNextOrDispose<T4>(ref e4, ref v4))
                    {
                        yield return resultSelector(v1, v2, v3, v4);
                    }
                }
                finally
                {
                    e1?.Dispose();
                    e2?.Dispose();
                    e3?.Dispose();
                    e4?.Dispose();
                }
            }
        }

        /// <summary>
        /// Returns a sequence of tuples, where each tuple contains the N-th
        /// element from each of the input sequences. The resulting sequence
        /// will always be as long as the longest of input sequences where the
        /// default value of each of the shorter sequence element types is used
        /// for padding.
        /// </summary>
        /// <typeparam name="T1">Type of elements in first input sequence.</typeparam>
        /// <typeparam name="T2">Type of elements in second input sequence.</typeparam>
        /// <typeparam name="T3">Type of elements in third input sequence.</typeparam>
        /// <typeparam name="T4">Type of elements in fourth input sequence.</typeparam>
        /// <param name="first">The first source sequence.</param>
        /// <param name="second">The second source sequence.</param>
        /// <param name="third">The third source sequence.</param>
        /// <param name="fourth">The fourth source sequence.</param>
        /// <returns>
        /// A sequence of tuples, where each tuple contains the N-th element
        /// from each of the argument sequences.</returns>
        /// <remarks>
        /// This operator uses deferred execution and streams its results.
        /// </remarks>

        public static IEnumerable<(T1, T2, T3, T4)> ZipLongest<T1, T2, T3, T4>(
            this IEnumerable<T1> first,
            IEnumerable<T2> second,
            IEnumerable<T3> third,
            IEnumerable<T4> fourth)
        {
            return ZipLongest(
                first,
                second,
                third,
                fourth,
                ValueTuple.Create);
        }

        /// <summary>
        /// Returns a projection of tuples, where each tuple contains the N-th
        /// element from each of the input sequences. The resulting sequence
        /// will always be as long as the longest of input sequences where the
        /// default value of each of the shorter sequence element types is used
        /// for padding.
        /// </summary>
        /// <typeparam name="T1">Type of elements in first input sequence.</typeparam>
        /// <typeparam name="T2">Type of elements in second input sequence.</typeparam>
        /// <typeparam name="T3">Type of elements in third input sequence.</typeparam>
        /// <typeparam name="T4">Type of elements in fourth input sequence.</typeparam>
        /// <typeparam name="T5">Type of elements in fifth input sequence.</typeparam>
        /// <typeparam name="TResult">Type of elements in result sequence.</typeparam>
        /// <param name="first">The first source sequence.</param>
        /// <param name="second">The second source sequence.</param>
        /// <param name="third">The third source sequence.</param>
        /// <param name="fourth">The fourth source sequence.</param>
        /// <param name="fifth">The fifth source sequence.</param>
        /// <param name="resultSelector">
        /// Function to apply to each tuple of elements.</param>
        /// <returns>
        /// A projection of tuples, where each tuple contains the N-th element
        /// from each of the argument sequences.</returns>
        /// <remarks>
        /// This operator uses deferred execution and streams its results.
        /// </remarks>

        public static IEnumerable<TResult> ZipLongest<T1, T2, T3, T4, T5, TResult>(
            this IEnumerable<T1> first,
            IEnumerable<T2> second,
            IEnumerable<T3> third,
            IEnumerable<T4> fourth,
            IEnumerable<T5> fifth,
            Func<T1, T2, T3, T4, T5, TResult> resultSelector)
        {
            if (first == null) throw new ArgumentNullException(nameof(first));
            if (second == null) throw new ArgumentNullException(nameof(second));
            if (third == null) throw new ArgumentNullException(nameof(third));
            if (fourth == null) throw new ArgumentNullException(nameof(fourth));
            if (fifth == null) throw new ArgumentNullException(nameof(fifth));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return _(); IEnumerable<TResult> _()
            {
                IEnumerator<T1> e1 = null;
                IEnumerator<T2> e2 = null;
                IEnumerator<T3> e3 = null;
                IEnumerator<T4> e4 = null;
                IEnumerator<T5> e5 = null;

                try
                {
                    e1 = first.GetEnumerator();
                    e2 = second.GetEnumerator();
                    e3 = third.GetEnumerator();
                    e4 = fourth.GetEnumerator();
                    e5 = fifth.GetEnumerator();

                    var v1 = default(T1);
                    var v2 = default(T2);
                    var v3 = default(T3);
                    var v4 = default(T4);
                    var v5 = default(T5);

                    // | is used instead of || in purpose. All operands have to be evaluated.
                    while (
                        ZipLongestHelper.MoveNextOrDispose<T1>(ref e1, ref v1) |
                        ZipLongestHelper.MoveNextOrDispose<T2>(ref e2, ref v2) |
                        ZipLongestHelper.MoveNextOrDispose<T3>(ref e3, ref v3) |
                        ZipLongestHelper.MoveNextOrDispose<T4>(ref e4, ref v4) |
                        ZipLongestHelper.MoveNextOrDispose<T5>(ref e5, ref v5))
                    {
                        yield return resultSelector(v1, v2, v3, v4, v5);
                    }
                }
                finally
                {
                    e1?.Dispose();
                    e2?.Dispose();
                    e3?.Dispose();
                    e4?.Dispose();
                    e5?.Dispose();
                }
            }
        }

        /// <summary>
        /// Returns a sequence of tuples, where each tuple contains the N-th
        /// element from each of the input sequences. The resulting sequence
        /// will always be as long as the longest of input sequences where the
        /// default value of each of the shorter sequence element types is used
        /// for padding.
        /// </summary>
        /// <typeparam name="T1">Type of elements in first input sequence.</typeparam>
        /// <typeparam name="T2">Type of elements in second input sequence.</typeparam>
        /// <typeparam name="T3">Type of elements in third input sequence.</typeparam>
        /// <typeparam name="T4">Type of elements in fourth input sequence.</typeparam>
        /// <typeparam name="T5">Type of elements in fifth input sequence.</typeparam>
        /// <param name="first">The first source sequence.</param>
        /// <param name="second">The second source sequence.</param>
        /// <param name="third">The third source sequence.</param>
        /// <param name="fourth">The fourth source sequence.</param>
        /// <param name="fifth">The fifth source sequence.</param>
        /// <returns>
        /// A sequence of tuples, where each tuple contains the N-th element
        /// from each of the argument sequences.</returns>
        /// <remarks>
        /// This operator uses deferred execution and streams its results.
        /// </remarks>

        public static IEnumerable<(T1, T2, T3, T4, T5)> ZipLongest<T1, T2, T3, T4, T5>(
            this IEnumerable<T1> first,
            IEnumerable<T2> second,
            IEnumerable<T3> third,
            IEnumerable<T4> fourth,
            IEnumerable<T5> fifth)
        {
            return ZipLongest(
                first,
                second,
                third,
                fourth,
                fifth,
                ValueTuple.Create);
        }

        /// <summary>
        /// Returns a projection of tuples, where each tuple contains the N-th
        /// element from each of the input sequences. The resulting sequence
        /// will always be as long as the longest of input sequences where the
        /// default value of each of the shorter sequence element types is used
        /// for padding.
        /// </summary>
        /// <typeparam name="T1">Type of elements in first input sequence.</typeparam>
        /// <typeparam name="T2">Type of elements in second input sequence.</typeparam>
        /// <typeparam name="T3">Type of elements in third input sequence.</typeparam>
        /// <typeparam name="T4">Type of elements in fourth input sequence.</typeparam>
        /// <typeparam name="T5">Type of elements in fifth input sequence.</typeparam>
        /// <typeparam name="T6">Type of elements in sixth input sequence.</typeparam>
        /// <typeparam name="TResult">Type of elements in result sequence.</typeparam>
        /// <param name="first">The first source sequence.</param>
        /// <param name="second">The second source sequence.</param>
        /// <param name="third">The third source sequence.</param>
        /// <param name="fourth">The fourth source sequence.</param>
        /// <param name="fifth">The fifth source sequence.</param>
        /// <param name="sixth">The sixth source sequence.</param>
        /// <param name="resultSelector">
        /// Function to apply to each tuple of elements.</param>
        /// <returns>
        /// A projection of tuples, where each tuple contains the N-th element
        /// from each of the argument sequences.</returns>
        /// <remarks>
        /// This operator uses deferred execution and streams its results.
        /// </remarks>

        public static IEnumerable<TResult> ZipLongest<T1, T2, T3, T4, T5, T6, TResult>(
            this IEnumerable<T1> first,
            IEnumerable<T2> second,
            IEnumerable<T3> third,
            IEnumerable<T4> fourth,
            IEnumerable<T5> fifth,
            IEnumerable<T6> sixth,
            Func<T1, T2, T3, T4, T5, T6, TResult> resultSelector)
        {
            if (first == null) throw new ArgumentNullException(nameof(first));
            if (second == null) throw new ArgumentNullException(nameof(second));
            if (third == null) throw new ArgumentNullException(nameof(third));
            if (fourth == null) throw new ArgumentNullException(nameof(fourth));
            if (fifth == null) throw new ArgumentNullException(nameof(fifth));
            if (sixth == null) throw new ArgumentNullException(nameof(sixth));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return _(); IEnumerable<TResult> _()
            {
                IEnumerator<T1> e1 = null;
                IEnumerator<T2> e2 = null;
                IEnumerator<T3> e3 = null;
                IEnumerator<T4> e4 = null;
                IEnumerator<T5> e5 = null;
                IEnumerator<T6> e6 = null;

                try
                {
                    e1 = first.GetEnumerator();
                    e2 = second.GetEnumerator();
                    e3 = third.GetEnumerator();
                    e4 = fourth.GetEnumerator();
                    e5 = fifth.GetEnumerator();
                    e6 = sixth.GetEnumerator();

                    var v1 = default(T1);
                    var v2 = default(T2);
                    var v3 = default(T3);
                    var v4 = default(T4);
                    var v5 = default(T5);
                    var v6 = default(T6);

                    // | is used instead of || in purpose. All operands have to be evaluated.
                    while (
                        ZipLongestHelper.MoveNextOrDispose<T1>(ref e1, ref v1) |
                        ZipLongestHelper.MoveNextOrDispose<T2>(ref e2, ref v2) |
                        ZipLongestHelper.MoveNextOrDispose<T3>(ref e3, ref v3) |
                        ZipLongestHelper.MoveNextOrDispose<T4>(ref e4, ref v4) |
                        ZipLongestHelper.MoveNextOrDispose<T5>(ref e5, ref v5) |
                        ZipLongestHelper.MoveNextOrDispose<T6>(ref e6, ref v6))
                    {
                        yield return resultSelector(v1, v2, v3, v4, v5, v6);
                    }
                }
                finally
                {
                    e1?.Dispose();
                    e2?.Dispose();
                    e3?.Dispose();
                    e4?.Dispose();
                    e5?.Dispose();
                    e6?.Dispose();
                }
            }
        }

        /// <summary>
        /// Returns a sequence of tuples, where each tuple contains the N-th
        /// element from each of the input sequences. The resulting sequence
        /// will always be as long as the longest of input sequences where the
        /// default value of each of the shorter sequence element types is used
        /// for padding.
        /// </summary>
        /// <typeparam name="T1">Type of elements in first input sequence.</typeparam>
        /// <typeparam name="T2">Type of elements in second input sequence.</typeparam>
        /// <typeparam name="T3">Type of elements in third input sequence.</typeparam>
        /// <typeparam name="T4">Type of elements in fourth input sequence.</typeparam>
        /// <typeparam name="T5">Type of elements in fifth input sequence.</typeparam>
        /// <typeparam name="T6">Type of elements in sixth input sequence.</typeparam>
        /// <param name="first">The first source sequence.</param>
        /// <param name="second">The second source sequence.</param>
        /// <param name="third">The third source sequence.</param>
        /// <param name="fourth">The fourth source sequence.</param>
        /// <param name="fifth">The fifth source sequence.</param>
        /// <param name="sixth">The sixth source sequence.</param>
        /// <returns>
        /// A sequence of tuples, where each tuple contains the N-th element
        /// from each of the argument sequences.</returns>
        /// <remarks>
        /// This operator uses deferred execution and streams its results.
        /// </remarks>

        public static IEnumerable<(T1, T2, T3, T4, T5, T6)> ZipLongest<T1, T2, T3, T4, T5, T6>(
            this IEnumerable<T1> first,
            IEnumerable<T2> second,
            IEnumerable<T3> third,
            IEnumerable<T4> fourth,
            IEnumerable<T5> fifth,
            IEnumerable<T6> sixth)
        {
            return ZipLongest(
                first,
                second,
                third,
                fourth,
                fifth,
                sixth,
                ValueTuple.Create);
        }

        /// <summary>
        /// Returns a projection of tuples, where each tuple contains the N-th
        /// element from each of the input sequences. The resulting sequence
        /// will always be as long as the longest of input sequences where the
        /// default value of each of the shorter sequence element types is used
        /// for padding.
        /// </summary>
        /// <typeparam name="T1">Type of elements in first input sequence.</typeparam>
        /// <typeparam name="T2">Type of elements in second input sequence.</typeparam>
        /// <typeparam name="T3">Type of elements in third input sequence.</typeparam>
        /// <typeparam name="T4">Type of elements in fourth input sequence.</typeparam>
        /// <typeparam name="T5">Type of elements in fifth input sequence.</typeparam>
        /// <typeparam name="T6">Type of elements in sixth input sequence.</typeparam>
        /// <typeparam name="T7">Type of elements in seventh input sequence.</typeparam>
        /// <typeparam name="TResult">Type of elements in result sequence.</typeparam>
        /// <param name="first">The first source sequence.</param>
        /// <param name="second">The second source sequence.</param>
        /// <param name="third">The third source sequence.</param>
        /// <param name="fourth">The fourth source sequence.</param>
        /// <param name="fifth">The fifth source sequence.</param>
        /// <param name="sixth">The sixth source sequence.</param>
        /// <param name="seventh">The seventh source sequence.</param>
        /// <param name="resultSelector">
        /// Function to apply to each tuple of elements.</param>
        /// <returns>
        /// A projection of tuples, where each tuple contains the N-th element
        /// from each of the argument sequences.</returns>
        /// <remarks>
        /// This operator uses deferred execution and streams its results.
        /// </remarks>

        public static IEnumerable<TResult> ZipLongest<T1, T2, T3, T4, T5, T6, T7, TResult>(
            this IEnumerable<T1> first,
            IEnumerable<T2> second,
            IEnumerable<T3> third,
            IEnumerable<T4> fourth,
            IEnumerable<T5> fifth,
            IEnumerable<T6> sixth,
            IEnumerable<T7> seventh,
            Func<T1, T2, T3, T4, T5, T6, T7, TResult> resultSelector)
        {
            if (first == null) throw new ArgumentNullException(nameof(first));
            if (second == null) throw new ArgumentNullException(nameof(second));
            if (third == null) throw new ArgumentNullException(nameof(third));
            if (fourth == null) throw new ArgumentNullException(nameof(fourth));
            if (fifth == null) throw new ArgumentNullException(nameof(fifth));
            if (sixth == null) throw new ArgumentNullException(nameof(sixth));
            if (seventh == null) throw new ArgumentNullException(nameof(seventh));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return _(); IEnumerable<TResult> _()
            {
                IEnumerator<T1> e1 = null;
                IEnumerator<T2> e2 = null;
                IEnumerator<T3> e3 = null;
                IEnumerator<T4> e4 = null;
                IEnumerator<T5> e5 = null;
                IEnumerator<T6> e6 = null;
                IEnumerator<T7> e7 = null;

                try
                {
                    e1 = first.GetEnumerator();
                    e2 = second.GetEnumerator();
                    e3 = third.GetEnumerator();
                    e4 = fourth.GetEnumerator();
                    e5 = fifth.GetEnumerator();
                    e6 = sixth.GetEnumerator();
                    e7 = seventh.GetEnumerator();

                    var v1 = default(T1);
                    var v2 = default(T2);
                    var v3 = default(T3);
                    var v4 = default(T4);
                    var v5 = default(T5);
                    var v6 = default(T6);
                    var v7 = default(T7);

                    // | is used instead of || in purpose. All operands have to be evaluated.
                    while (
                        ZipLongestHelper.MoveNextOrDispose<T1>(ref e1, ref v1) |
                        ZipLongestHelper.MoveNextOrDispose<T2>(ref e2, ref v2) |
                        ZipLongestHelper.MoveNextOrDispose<T3>(ref e3, ref v3) |
                        ZipLongestHelper.MoveNextOrDispose<T4>(ref e4, ref v4) |
                        ZipLongestHelper.MoveNextOrDispose<T5>(ref e5, ref v5) |
                        ZipLongestHelper.MoveNextOrDispose<T6>(ref e6, ref v6) |
                        ZipLongestHelper.MoveNextOrDispose<T7>(ref e7, ref v7))
                    {
                        yield return resultSelector(v1, v2, v3, v4, v5, v6, v7);
                    }
                }
                finally
                {
                    e1?.Dispose();
                    e2?.Dispose();
                    e3?.Dispose();
                    e4?.Dispose();
                    e5?.Dispose();
                    e6?.Dispose();
                    e7?.Dispose();
                }
            }
        }

        /// <summary>
        /// Returns a sequence of tuples, where each tuple contains the N-th
        /// element from each of the input sequences. The resulting sequence
        /// will always be as long as the longest of input sequences where the
        /// default value of each of the shorter sequence element types is used
        /// for padding.
        /// </summary>
        /// <typeparam name="T1">Type of elements in first input sequence.</typeparam>
        /// <typeparam name="T2">Type of elements in second input sequence.</typeparam>
        /// <typeparam name="T3">Type of elements in third input sequence.</typeparam>
        /// <typeparam name="T4">Type of elements in fourth input sequence.</typeparam>
        /// <typeparam name="T5">Type of elements in fifth input sequence.</typeparam>
        /// <typeparam name="T6">Type of elements in sixth input sequence.</typeparam>
        /// <typeparam name="T7">Type of elements in seventh input sequence.</typeparam>
        /// <param name="first">The first source sequence.</param>
        /// <param name="second">The second source sequence.</param>
        /// <param name="third">The third source sequence.</param>
        /// <param name="fourth">The fourth source sequence.</param>
        /// <param name="fifth">The fifth source sequence.</param>
        /// <param name="sixth">The sixth source sequence.</param>
        /// <param name="seventh">The seventh source sequence.</param>
        /// <returns>
        /// A sequence of tuples, where each tuple contains the N-th element
        /// from each of the argument sequences.</returns>
        /// <remarks>
        /// This operator uses deferred execution and streams its results.
        /// </remarks>

        public static IEnumerable<(T1, T2, T3, T4, T5, T6, T7)> ZipLongest<T1, T2, T3, T4, T5, T6, T7>(
            this IEnumerable<T1> first,
            IEnumerable<T2> second,
            IEnumerable<T3> third,
            IEnumerable<T4> fourth,
            IEnumerable<T5> fifth,
            IEnumerable<T6> sixth,
            IEnumerable<T7> seventh)
        {
            return ZipLongest(
                first,
                second,
                third,
                fourth,
                fifth,
                sixth,
                seventh,
                ValueTuple.Create);
        }

        /// <summary>
        /// Returns a projection of tuples, where each tuple contains the N-th
        /// element from each of the input sequences. The resulting sequence
        /// will always be as long as the longest of input sequences where the
        /// default value of each of the shorter sequence element types is used
        /// for padding.
        /// </summary>
        /// <typeparam name="T1">Type of elements in first input sequence.</typeparam>
        /// <typeparam name="T2">Type of elements in second input sequence.</typeparam>
        /// <typeparam name="T3">Type of elements in third input sequence.</typeparam>
        /// <typeparam name="T4">Type of elements in fourth input sequence.</typeparam>
        /// <typeparam name="T5">Type of elements in fifth input sequence.</typeparam>
        /// <typeparam name="T6">Type of elements in sixth input sequence.</typeparam>
        /// <typeparam name="T7">Type of elements in seventh input sequence.</typeparam>
        /// <typeparam name="T8">Type of elements in eighth input sequence.</typeparam>
        /// <typeparam name="TResult">Type of elements in result sequence.</typeparam>
        /// <param name="first">The first source sequence.</param>
        /// <param name="second">The second source sequence.</param>
        /// <param name="third">The third source sequence.</param>
        /// <param name="fourth">The fourth source sequence.</param>
        /// <param name="fifth">The fifth source sequence.</param>
        /// <param name="sixth">The sixth source sequence.</param>
        /// <param name="seventh">The seventh source sequence.</param>
        /// <param name="eighth">The eighth source sequence.</param>
        /// <param name="resultSelector">
        /// Function to apply to each tuple of elements.</param>
        /// <returns>
        /// A projection of tuples, where each tuple contains the N-th element
        /// from each of the argument sequences.</returns>
        /// <remarks>
        /// This operator uses deferred execution and streams its results.
        /// </remarks>

        public static IEnumerable<TResult> ZipLongest<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(
            this IEnumerable<T1> first,
            IEnumerable<T2> second,
            IEnumerable<T3> third,
            IEnumerable<T4> fourth,
            IEnumerable<T5> fifth,
            IEnumerable<T6> sixth,
            IEnumerable<T7> seventh,
            IEnumerable<T8> eighth,
            Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> resultSelector)
        {
            if (first == null) throw new ArgumentNullException(nameof(first));
            if (second == null) throw new ArgumentNullException(nameof(second));
            if (third == null) throw new ArgumentNullException(nameof(third));
            if (fourth == null) throw new ArgumentNullException(nameof(fourth));
            if (fifth == null) throw new ArgumentNullException(nameof(fifth));
            if (sixth == null) throw new ArgumentNullException(nameof(sixth));
            if (seventh == null) throw new ArgumentNullException(nameof(seventh));
            if (eighth == null) throw new ArgumentNullException(nameof(eighth));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));

            return _(); IEnumerable<TResult> _()
            {
                IEnumerator<T1> e1 = null;
                IEnumerator<T2> e2 = null;
                IEnumerator<T3> e3 = null;
                IEnumerator<T4> e4 = null;
                IEnumerator<T5> e5 = null;
                IEnumerator<T6> e6 = null;
                IEnumerator<T7> e7 = null;
                IEnumerator<T8> e8 = null;

                try
                {
                    e1 = first.GetEnumerator();
                    e2 = second.GetEnumerator();
                    e3 = third.GetEnumerator();
                    e4 = fourth.GetEnumerator();
                    e5 = fifth.GetEnumerator();
                    e6 = sixth.GetEnumerator();
                    e7 = seventh.GetEnumerator();
                    e8 = eighth.GetEnumerator();

                    var v1 = default(T1);
                    var v2 = default(T2);
                    var v3 = default(T3);
                    var v4 = default(T4);
                    var v5 = default(T5);
                    var v6 = default(T6);
                    var v7 = default(T7);
                    var v8 = default(T8);

                    // | is used instead of || in purpose. All operands have to be evaluated.
                    while (
                        ZipLongestHelper.MoveNextOrDispose<T1>(ref e1, ref v1) |
                        ZipLongestHelper.MoveNextOrDispose<T2>(ref e2, ref v2) |
                        ZipLongestHelper.MoveNextOrDispose<T3>(ref e3, ref v3) |
                        ZipLongestHelper.MoveNextOrDispose<T4>(ref e4, ref v4) |
                        ZipLongestHelper.MoveNextOrDispose<T5>(ref e5, ref v5) |
                        ZipLongestHelper.MoveNextOrDispose<T6>(ref e6, ref v6) |
                        ZipLongestHelper.MoveNextOrDispose<T7>(ref e7, ref v7) |
                        ZipLongestHelper.MoveNextOrDispose<T8>(ref e8, ref v8))
                    {
                        yield return resultSelector(v1, v2, v3, v4, v5, v6, v7, v8);
                    }
                }
                finally
                {
                    e1?.Dispose();
                    e2?.Dispose();
                    e3?.Dispose();
                    e4?.Dispose();
                    e5?.Dispose();
                    e6?.Dispose();
                    e7?.Dispose();
                    e8?.Dispose();
                }
            }
        }

        /// <summary>
        /// Returns a sequence of tuples, where each tuple contains the N-th
        /// element from each of the input sequences. The resulting sequence
        /// will always be as long as the longest of input sequences where the
        /// default value of each of the shorter sequence element types is used
        /// for padding.
        /// </summary>
        /// <typeparam name="T1">Type of elements in first input sequence.</typeparam>
        /// <typeparam name="T2">Type of elements in second input sequence.</typeparam>
        /// <typeparam name="T3">Type of elements in third input sequence.</typeparam>
        /// <typeparam name="T4">Type of elements in fourth input sequence.</typeparam>
        /// <typeparam name="T5">Type of elements in fifth input sequence.</typeparam>
        /// <typeparam name="T6">Type of elements in sixth input sequence.</typeparam>
        /// <typeparam name="T7">Type of elements in seventh input sequence.</typeparam>
        /// <typeparam name="T8">Type of elements in eighth input sequence.</typeparam>
        /// <param name="first">The first source sequence.</param>
        /// <param name="second">The second source sequence.</param>
        /// <param name="third">The third source sequence.</param>
        /// <param name="fourth">The fourth source sequence.</param>
        /// <param name="fifth">The fifth source sequence.</param>
        /// <param name="sixth">The sixth source sequence.</param>
        /// <param name="seventh">The seventh source sequence.</param>
        /// <param name="eighth">The eighth source sequence.</param>
        /// <returns>
        /// A sequence of tuples, where each tuple contains the N-th element
        /// from each of the argument sequences.</returns>
        /// <remarks>
        /// This operator uses deferred execution and streams its results.
        /// </remarks>

        public static IEnumerable<(T1, T2, T3, T4, T5, T6, T7, T8)> ZipLongest<T1, T2, T3, T4, T5, T6, T7, T8>(
            this IEnumerable<T1> first,
            IEnumerable<T2> second,
            IEnumerable<T3> third,
            IEnumerable<T4> fourth,
            IEnumerable<T5> fifth,
            IEnumerable<T6> sixth,
            IEnumerable<T7> seventh,
            IEnumerable<T8> eighth)
        {
            return ZipLongest(
                first,
                second,
                third,
                fourth,
                fifth,
                sixth,
                seventh,
                eighth,
                ValueTuple.Create);
        }

    }
}