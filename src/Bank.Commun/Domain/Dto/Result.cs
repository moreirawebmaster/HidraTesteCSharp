using System.Collections.Generic;
using System.Linq;

namespace Bank.Commun.Domain.Dto
{
    public class Result
    {
        public static Result Ok = new Result();

        private readonly List<string> _validations = new List<string>();
        public bool HasValidation => _validations.Any();
        public IReadOnlyList<string> Validations => _validations;

        public Result AddValidation(string validation)
        {
            _validations.Add(validation);
            return this;
        }
    }

    public class Result<T> : Result
    {
        public T Data { get; }
        public Result() { }

        public Result(T data) => Data = data;
    }
}