using System;
using System.Collections.Generic;
using System.Text;

namespace Word2NumberConverterTests.Functions.Tests
{
    public class NullScope : IDisposable
    {
        public static NullScope Instance { get; } = new NullScope();

        private NullScope() { }

        public void Dispose() { }
    }
}
