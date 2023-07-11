using System;

namespace KeyboardKata.Tests.Abstractions
{
    public interface IApp : IDisposable
    {
        bool IsVisible { get; }
    }
}
