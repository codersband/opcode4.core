using System;
using System.Runtime.Serialization;

namespace opcode4.core.Exceptions
{
    [DataContract]
    public class EVayoCustomException : ApplicationException
    {
        [DataMember]
        public int ErrorCode { get; set; }
        [DataMember]
        public string Tag { get; set; }

        public EVayoCustomException(string message)
            : base(message) { Tag = "UNDEFINED"; }

        public EVayoCustomException(string message, string tag)
            : base(message) { Tag = tag; }

        public EVayoCustomException(int errorCode, string message)
            : base(message) { ErrorCode = errorCode; Tag = "CUSTOM_TAG"; }

        public EVayoCustomException(int errorCode, string message, string tag)
            : base(message) { ErrorCode = errorCode; Tag = tag; }

        protected EVayoCustomException(SerializationInfo info, StreamingContext context)
            : base(info, context){ }
    }

    [DataContract]
    public class CustomException : EVayoCustomException
    {
        [DataMember]
        public ExceptionCode Code 
        {
            set{ }
            get { return (ExceptionCode)Enum.ToObject(typeof(ExceptionCode), ErrorCode); }
        }

        public CustomException(ExceptionCode code, string message)
            : base((int)code, message, code.ToString()) { }

        public CustomException(ExceptionCode code, string message, string tag)
            : base((int)code, message, tag) { }
    }

    [DataContract]
    public abstract class EVayoException<T> : EVayoCustomException where T : struct , IComparable, IFormattable, IConvertible
    {
        [DataMember]
        public T Protocol
        {
            set { }
            get { return (T)Enum.ToObject(typeof(T), ErrorCode); }
        }

        protected EVayoException(T protocol, string message)
            : base(Convert.ToInt32(protocol), message, protocol.ToString()) { }

        protected EVayoException(T protocol, string message, string tag)
            : base(Convert.ToInt32(protocol), message, tag) { }
    }
}
