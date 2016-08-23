using System.Runtime.Serialization;

namespace opcode4.core.Exceptions
{
    [DataContract]
    public class ErrorDataDto
    {
        public ErrorDataDto(int errorId, string reason, string detailedInformation)
        {
            ErrorId = errorId;
            Reason = reason;
            DetailedInformation = detailedInformation;
        }

        [DataMember]
        public int ErrorId { set; get; }
        [DataMember]
        public string Reason { get; private set; }
        [DataMember]
        public string DetailedInformation { get; private set; }

    }
}
