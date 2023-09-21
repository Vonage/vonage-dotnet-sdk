﻿using System;

namespace Vonage.Common;

/// <summary>
/// Represents a time provider.
/// </summary>
public interface ITimeProvider
{
    /// <summary>
    /// Returns the current date and time.
    /// </summary>
    DateTime Now { get; }
    
    /// <summary>
    /// Returns the current epoch value.
    /// </summary>
    int Epoch { get; }
}