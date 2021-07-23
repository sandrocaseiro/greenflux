using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using static Greenflux.Models.VResponseError;

namespace Greenflux.Models
{
    public record VResponse<T>
    {
        public T Data { get; init; }
        public IEnumerable<VResponseError> Errors { get; init; }
        public bool ISuccess { get => !Errors.Any() || !Errors.Any(e => e.Type != ErrorType.SUCCESS); }
    }

    public record VResponseError
    {
        public int Code { get; init; }
        public ErrorType Type { get; init; }
        public string Description { get; init; }

        public static VResponseError Error(int code, string description) => new() { Code = code, Type = ErrorType.ERROR, Description = description };

        public static VResponseError Success(int code, string description) => new() { Code = code, Type = ErrorType.SUCCESS, Description = description };

        public enum ErrorType
        {
            [EnumMember(Value = "E")]
            ERROR,
            [EnumMember(Value = "W")]
            WARNING,
            [EnumMember(Value = "I")]
            INFORMATION,
            [EnumMember(Value = "S")]
            SUCCESS
        }
    }
}