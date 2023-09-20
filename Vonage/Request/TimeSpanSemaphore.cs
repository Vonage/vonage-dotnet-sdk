using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Vonage.Request;

internal class TimeSpanSemaphore : IDisposable
{
    private readonly object queueLock = new();
    private readonly Queue<DateTime> releaseTimes;
    private readonly SemaphoreSlim pool;
    private readonly TimeSpan resetSpan;

    public TimeSpanSemaphore(int maxCount, TimeSpan resetSpan)
    {
        this.pool = new SemaphoreSlim(maxCount, maxCount);
        this.resetSpan = resetSpan;
        this.releaseTimes = new Queue<DateTime>(maxCount);
        for (var i = 0; i < maxCount; i++)
        {
            this.releaseTimes.Enqueue(DateTime.MinValue);
        }
    }

    /// <summary>
    ///     Releases all resources used by the current instance
    /// </summary>
    public void Dispose()
    {
        this.pool.Dispose();
    }

    /// <summary>
    ///     Runs an action after entering the semaphore (if the CancellationToken is not canceled)
    /// </summary>
    public void Run(Action action, CancellationToken cancelToken)
    {
        // will throw if token is cancelled, but will auto-release lock
        this.Wait(cancelToken);
        try
        {
            action();
        }
        finally
        {
            this.Release();
        }
    }

    /// <summary>
    ///     Runs an action after entering the semaphore (if the CancellationToken is not canceled)
    /// </summary>
    public async Task RunAsync(Func<Task> action, CancellationToken cancelToken)
    {
        // will throw if token is cancelled, but will auto-release lock
        this.Wait(cancelToken);
        try
        {
            await action().ConfigureAwait(false);
        }
        finally
        {
            this.Release();
        }
    }

    /// <summary>
    ///     Runs an action after entering the semaphore (if the CancellationToken is not canceled)
    /// </summary>
    public async Task RunAsync<T>(Func<T, Task> action, T arg, CancellationToken cancelToken)
    {
        // will throw if token is cancelled, but will auto-release lock
        this.Wait(cancelToken);
        try
        {
            await action(arg).ConfigureAwait(false);
        }
        finally
        {
            this.Release();
        }
    }

    /// <summary>
    ///     Runs an action after entering the semaphore (if the CancellationToken is not canceled)
    /// </summary>
    public async Task<TR> RunAsync<T, TR>(Func<T, CancellationToken, Task<TR>> action, T arg,
        CancellationToken cancelToken)
    {
        // will throw if token is cancelled, but will auto-release lock
        this.Wait(cancelToken);
        try
        {
            return await action(arg, cancelToken).ConfigureAwait(false);
        }
        finally
        {
            this.Release();
        }
    }

    /// <summary>
    ///     Exits the semaphore
    /// </summary>
    private void Release()
    {
        lock (this.queueLock)
        {
            this.releaseTimes.Enqueue(DateTime.UtcNow);
        }

        this.pool.Release();
    }

    /// <summary>
    ///     Blocks the current thread until it can enter the semaphore, while observing a CancellationToken
    /// </summary>
    private void Wait(CancellationToken cancelToken)
    {
        // will throw if token is cancelled
        this.pool.Wait(cancelToken);

        // get the oldest release from the queue
        DateTime oldestRelease;
        lock (this.queueLock)
        {
            oldestRelease = this.releaseTimes.Dequeue();
        }

        // sleep until the time since the previous release equals the reset period
        var now = DateTime.UtcNow;
        var windowReset = oldestRelease.Add(this.resetSpan);
        if (windowReset <= now)
        {
            return;
        }

        var sleepMilliseconds = Math.Max(
            (int) (windowReset.Subtract(now).Ticks / TimeSpan.TicksPerMillisecond),
            1); // sleep at least 1ms to be sure next window has started
        var cancelled = cancelToken.WaitHandle.WaitOne(sleepMilliseconds);
        if (cancelled)
        {
            this.Release();
            cancelToken.ThrowIfCancellationRequested();
        }
    }
}