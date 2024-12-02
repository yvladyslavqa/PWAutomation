using FluentAssertions.Execution;
using NUnit.Framework;
using PwProject.Core.Logger.Interfaces;
using System.Text;

namespace PwProject.Core.Assertion
{
    public class SoftAssertStrategy : IAssertionStrategy
    {
        private readonly ILogger _log;

        private readonly List<string> _failureMessages = new();

        public IEnumerable<string> FailureMessages => _failureMessages;

        public SoftAssertStrategy(ILogger log)
        {
            _log = log;
        }

        public IEnumerable<string> DiscardFailures()
        {
            var discardedFailures = _failureMessages.ToArray();
            _failureMessages.Clear();
            return discardedFailures;
        }

        public void ThrowIfAny(IDictionary<string, object> context)
        {
            if (!_failureMessages.Any()) return;

            var builder = new StringBuilder();
            builder.AppendLine(string.Join(Environment.NewLine, _failureMessages));

            throw new AssertionException(builder.ToString());
        }

        public void HandleFailure(string message)
        {
            _log.Error(message);

            if (string.IsNullOrEmpty(message)) return;
            message = $"{Environment.NewLine}{_failureMessages.Count + 1}.{message}";

            _failureMessages.Add(message);

            throw new AssertionException(message);
        }
    }
}
