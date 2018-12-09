# Hangfire.ConsoleHost

[![Build Status](https://benjaminabt.visualstudio.com/BenjaminAbt.Hangfire.ConsoleHost/_apis/build/status/BenjaminAbt.Hangfire.ConsoleHost-CI)](https://benjaminabt.visualstudio.com/BenjaminAbt.Hangfire.ConsoleHost/_build/latest?definitionId=12) ![Nuget](https://img.shields.io/nuget/v/Hangfire.ConsoleHost.svg)

## Overview

Run Hangfire with Microsoft.Extensions.DependencyInjection in your .NET Core Console.

## Install

```
PM> Install-Package Hangfire.ConsoleHost
```

## Usage

Register transient `HangfireHost` with

```csharp
    // Register Hangfire Host
    services.AddHangfireHost();
```

or add manually by

```csharp
    // Register Hangfire Host
    services.AddTransient<IHangfireHost, HangfireHost>();
```

Use dependency injection in your impementation (and dont forget Dispose!)

```csharp

    public class MyHangfireApp : IDisposable
    {
        private readonly IHangfireHost _hangfireHost;

        public MyHangfireApp(IHangfireHost hangfireHost)
        {
            _hangfireHost = hangfireHost;
        }

        public void Dispose()
        {
            _hangfireHost?.Dispose();
        }
    }
```

You can find a full .NET Core console example based on `IHostedService` in a [sample](sample/Server/Program.cs)

## License

```
MIT License

Copyright (c) 2018 Benjamin Abt

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
```
