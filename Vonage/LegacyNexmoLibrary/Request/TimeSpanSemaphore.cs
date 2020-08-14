/*
MIT License
Copyright (C) 2011 by Joel Fillmore

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.

https://github.com/joelfillmore/JFLibrary
*/

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Nexmo.Api.Request
{
    /// <summary>
    /// Allows a limited number of acquisitions during a timespan
    /// </summary>
    internal class TimeSpanSemaphore : IDisposable
    {
        private SemaphoreSlim _pool;

        // the time span for the max number of callers
        private TimeSpan _resetSpan;

        // keep track of the release times
        private Queue<DateTime> _releaseTimes;

        // protect release time queue
        private object _queueLock = new object();

        public TimeSpanSemaphore(int maxCount, TimeSpan resetSpan)
        {
            _pool = new SemaphoreSlim(maxCount, maxCount);
            _resetSpan = resetSpan;

            // initialize queue with old timestamps
            _releaseTimes = new Queue<DateTime>(maxCount);
            for (int i = 0; i < maxCount; i++)
            {
                _releaseTimes.Enqueue(DateTime.MinValue);
            }
        }

        /// <summary>
        /// Blocks the current thread until it can enter the semaphore, while observing a CancellationToken
        /// </summary>
        private void Wait(CancellationToken cancelToken)
        {
            // will throw if token is cancelled
            _pool.Wait(cancelToken);

            // get the oldest release from the queue
            DateTime oldestRelease;
            lock (_queueLock)
            {
                oldestRelease = _releaseTimes.Dequeue();
            }

            // sleep until the time since the previous release equals the reset period
            DateTime now = DateTime.UtcNow;
            DateTime windowReset = oldestRelease.Add(_resetSpan);
            if (windowReset > now)
            {
                int sleepMilliseconds = Math.Max(
                    (int)(windowReset.Subtract(now).Ticks / TimeSpan.TicksPerMillisecond),
                    1); // sleep at least 1ms to be sure next window has started
                // TODO: log
                //_logger.LogInformation($"Waiting {sleepMilliseconds} ms for TimeSpanSemaphore limit to reset.");

                bool cancelled = cancelToken.WaitHandle.WaitOne(sleepMilliseconds);
                if (cancelled)
                {
                    Release();
                    cancelToken.ThrowIfCancellationRequested();
                }
            }
        }

        /// <summary>
        /// Exits the semaphore
        /// </summary>
        private void Release()
        {
            lock (_queueLock)
            {
                _releaseTimes.Enqueue(DateTime.UtcNow);
            }
            _pool.Release();
        }

        /// <summary>
        /// Runs an action after entering the semaphore (if the CancellationToken is not canceled)
        /// </summary>
        public void Run(Action action, CancellationToken cancelToken)
        {
            // will throw if token is cancelled, but will auto-release lock
            Wait(cancelToken);

            try
            {
                action();
            }
            finally
            {
                Release();
            }
        }

        /// <summary>
        /// Runs an action after entering the semaphore (if the CancellationToken is not canceled)
        /// </summary>
        public async Task RunAsync(Func<Task> action, CancellationToken cancelToken)
        {
            // will throw if token is cancelled, but will auto-release lock
            Wait(cancelToken);

            try
            {
                await action().ConfigureAwait(false);
            }
            finally
            {
                Release();
            }
        }

        /// <summary>
        /// Runs an action after entering the semaphore (if the CancellationToken is not canceled)
        /// </summary>
        public async Task RunAsync<T>(Func<T, Task> action, T arg, CancellationToken cancelToken)
        {
            // will throw if token is cancelled, but will auto-release lock
            Wait(cancelToken);

            try
            {
                await action(arg).ConfigureAwait(false);
            }
            finally
            {
                Release();
            }
        }

        /// <summary>
        /// Runs an action after entering the semaphore (if the CancellationToken is not canceled)
        /// </summary>
        public async Task<TR> RunAsync<T, TR>(Func<T, CancellationToken, Task<TR>> action, T arg, CancellationToken cancelToken)
        {
            // will throw if token is cancelled, but will auto-release lock
            Wait(cancelToken);

            try
            {
                return await action(arg, cancelToken).ConfigureAwait(false);
            }
            finally
            {
                Release();
            }
        }

        /// <summary>
        /// Releases all resources used by the current instance
        /// </summary>
        public void Dispose()
        {
            _pool.Dispose();
        }
    }
}