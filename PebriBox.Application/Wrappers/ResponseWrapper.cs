using System;

namespace PebriBox.Application.Wrappers;

public class ResponseWrapper : IResponseWrapper
{
    public List<string> Messages { get; set; } = [];
    public bool IsSuccess { get; set; }

    #region Failure Static Methods
    public static ResponseWrapper Fail()
    {
        return new ResponseWrapper { IsSuccess = false };
    }

    public static ResponseWrapper Fail(string message)
    {
        return new ResponseWrapper { IsSuccess = false, Messages = [message] };
    }

    public static ResponseWrapper Fail(List<string> messages)
    {
        return new ResponseWrapper { IsSuccess = false, Messages = messages };
    }
    #endregion  


    #region Success Static Methods
    public static ResponseWrapper Success()
    {
        return new ResponseWrapper { IsSuccess = true };
    }

    public static ResponseWrapper Success(string message)
    {
        return new ResponseWrapper { IsSuccess = true, Messages = [message] };
    }

    public static ResponseWrapper Success(List<string> messages)
    {
        return new ResponseWrapper { IsSuccess = true, Messages = messages };
    }
    #endregion


}

public class ResponseWrapper<T> : ResponseWrapper, IResponseWrapper<T>
{
    public T Data { get; set; }

    #region Failure Static Methods
    public new static ResponseWrapper<T> Fail()
    {
        return new ResponseWrapper<T> { IsSuccess = false };
    }

    public new static ResponseWrapper<T> Fail(string message)
    {
        return new ResponseWrapper<T> { IsSuccess = false, Messages = [message] };
    }

    public new static ResponseWrapper<T> Fail(List<string> messages)
    {
        return new ResponseWrapper<T> { IsSuccess = false, Messages = messages };
    }
    #endregion

    #region Success Static Methods
    public new static ResponseWrapper<T> Success()
    {
        return new ResponseWrapper<T> { IsSuccess = true };
    }
    public new static ResponseWrapper<T> Success(string message)
    {
        return new ResponseWrapper<T> { IsSuccess = true, Messages = [message] };
    }
    public new static ResponseWrapper<T> Success(List<string> messages)
    {
        return new ResponseWrapper<T> { IsSuccess = true, Messages = messages };
    }
    public static ResponseWrapper<T> Success(T data)
    {
        return new ResponseWrapper<T> { IsSuccess = true, Data = data };
    }
    public static ResponseWrapper<T> Success(T data, string message)
    {
        return new ResponseWrapper<T> { IsSuccess = true, Messages = [message], Data = data };
    }
    public static ResponseWrapper<T> Success(T data, List<string> messages)
    {
        return new ResponseWrapper<T> { IsSuccess = true, Messages = messages, Data = data };
    }
    #endregion
}