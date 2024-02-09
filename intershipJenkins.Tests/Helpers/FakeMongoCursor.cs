using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace intershipJenkins.Tests.Helpers
{
    public class FakeMongoCursor<T> : IAsyncCursor<T>
    {
        private readonly IEnumerable<T> _items;
        private IEnumerator<T>? _enumerator;

        public FakeMongoCursor(IEnumerable<T> items)
        {
            _items = items;
            _enumerator = _items.GetEnumerator();
        }

        public IEnumerable<T> Current => _enumerator != null ? new[] { _enumerator.Current } : throw new InvalidOperationException();

        public bool MoveNext(CancellationToken cancellationToken = default)
        {
            return _enumerator?.MoveNext() ?? false;
        }

        public Task<bool> MoveNextAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(MoveNext());
        }

        public void Dispose()
        {
            _enumerator?.Dispose();
        }
    }
}
