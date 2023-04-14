// Copyright (c) Microsoft. All rights reserved.

namespace Backend.Services;

public sealed class ConsoleLogger : ILogger
{
    private readonly string _name;

    public ConsoleLogger(
        string name) =>
        (_name) = (name);

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull => default!;

    public bool IsEnabled(LogLevel logLevel) => false;

    public void Log<TState>(
        LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception? exception,
        Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel))
        {
            return;
        }

        Console.WriteLine($"[{eventId.Id,2}: {logLevel,-12}]");
        Console.Write($"     {_name} - ");
        Console.Write($"{formatter(state, exception)}");
        Console.WriteLine();
    }
}
