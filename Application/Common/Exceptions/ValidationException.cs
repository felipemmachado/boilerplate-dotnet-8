using FluentValidation.Results;

namespace Application.Common.Exceptions;
public class ValidationException : Exception
{
    public ValidationException()
        : base("One or more validation failures have occurred.")
    {
        Errors = [];
    }

    public ValidationException(string property, string message) : this()
    {
        Errors ??= new List<ErrorModel>();
        Errors.Add(new ErrorModel(property, message));
    }

    public ValidationException(IEnumerable<ValidationFailure> notifications)
        : this()
    {
        var errors = notifications.ToList()
          .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
          .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToList())
          .Select(p => new ErrorModel(p.Key, p.Value)).ToList();

        Errors = errors;
    }

    public List<ErrorModel> Errors { get; }
}


public class ErrorModel
{
    public string Field { get; set; }
    public List<string> Errors { get; set; }

    public ErrorModel(string field, string error)
    {
        Field = field;
        Errors = [error];
    }

    public ErrorModel(string field, List<string> errors)
    {
        Field = field;
        Errors = errors;
    }
}

