//path: src\Schema\OperationReport.cs

using Newtonsoft.Json;

namespace Neurocache.Schema
{
    public class OperationReport(
        Guid token,
        string author,
        string recipient,
        string payload,
        Guid agentId,
        bool final,
        string reportId,
        int status = 200,
        List<string>? errors = null
    )
    {
        public Guid Token { get; } = token;
        public string Author { get; private set; } = author;
        public string Recipient { get; private set; } = recipient;
        public string Payload { get; } = payload;
        public Guid AgentId { get; } = agentId;
        public bool Final { get; } = final;
        public string ReportId { get; } = reportId;

        public int Status { get; } = status;
        public List<string>? Errors { get; } = errors;

        public void SetRecipient(string recipient)
            => Recipient = recipient;
        public override string ToString()
            => $"OperationReport: token:{Token}, author:{Author}, recipient:{Recipient}, payload:{Payload}, agentId:{AgentId}, final:{Final}, reportId:{ReportId}, status:{Status}, errors:{Errors}";
        public static OperationReport? FromJson(string json)
            => JsonConvert.DeserializeObject<OperationReport>(json);
        public static string ToJson(OperationReport report)
            => JsonConvert.SerializeObject(report);
    }
}
