﻿namespace Dizignit.Core.Interfaces
{
    public interface ILogger<T> where T : ILoggable, new()
    {
        bool Log();
    }
}
