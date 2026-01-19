using System;

namespace PebriBox.Application.Wrappers;

public interface IResponseWrapper
{
    public List<string> Messages { get; set; }
    public bool IsSuccess { get; set; }
}

public interface IResponseWrapper<T> : IResponseWrapper
{
    public T Data { get; set; }
}
