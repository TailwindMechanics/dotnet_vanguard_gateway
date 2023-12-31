//path: src\Operations\OperationService.cs

using System.Collections.Concurrent;
using System.Net.WebSockets;

using Neurocache.ShipsInfo;
using Neurocache.Schema;
using System.Reactive;

namespace Neurocache.Operations
{
    public static class OperationService
    {
        public static ConcurrentDictionary<Guid, Operation> Operations { get; } = new();

        public static Operation CreateOperation(
            WebSocket webSocket,
            Guid agentId,
            OperationOutline outline
        )
        {
            Ships.Log($"Creating operation for agent {agentId}");
            var operation = new Operation(webSocket, agentId, outline);
            Operations.TryAdd(operation.OperationToken, operation);
            return operation;
        }

        public static void Stop(Guid token)
        {
            Operations.TryGetValue(token, out var operation);
            if (operation == null)
            {
                Ships.Warning($"Operation with token {token} not found");
                return;
            }

            operation.StopSubject.OnNext(Unit.Default);
            Operations.TryRemove(token, out _);
        }

        public static void KillAll()
        {
            foreach (var operation in Operations.Values)
            {
                operation.StopSubject.OnNext(Unit.Default);
            }

            Operations.Clear();
        }
    }
}
