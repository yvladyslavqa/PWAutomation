using NUnit.Framework.Internal;
using PwProject.Core.Logger.Interfaces;
using ReportPortal.NUnitExtension.LogHandler.Messages;
using ReportPortal.Shared.Execution.Logging;
using System.Runtime.Serialization.Json;
using System.Text;

namespace PwProject.Core.Logger
{
    public class NunitMessageSender : IRpMessageSender
    {
        public void SendMessage(string text, LogMessageLevel level, DateTime time, byte[] attachment)
        {
            Attach attach = attachment is null ? null :
                new Attach("image/jpg", attachment);

            var message = new AddLogCommunicationMessage
            {
                Text = text,
                Level = level,
                Time = time,
                Attach = attach
            };

            TestExecutionContext.CurrentContext.SendMessage(
                    "ReportPortal-AddLogMessage",
                    Serialize<AddLogCommunicationMessage>(message));
        }

        private static string Serialize<T>(object obj)
        {
            DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(T));
            using MemoryStream memoryStream = new MemoryStream();
            dataContractJsonSerializer.WriteObject(memoryStream, obj);
            byte[] array = memoryStream.ToArray();
            return Encoding.UTF8.GetString(array, 0, array.Length);
        }
    }
}
