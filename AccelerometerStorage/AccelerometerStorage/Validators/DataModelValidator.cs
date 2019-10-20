using System.Collections.Generic;
using System.Linq;
using FluentValidation;

namespace AccelerometerStorage.WebApi.Validators
{
    public class DataModelValidator : AbstractValidator<DataModel>
    {
        public static readonly IEnumerable<string> AcceptedContentTypes = new List<string> { "text/csv", "application/vnd.ms-excel" };

        public DataModelValidator()
        {
            RuleFor(d => d.CsvFile)
                .Must(file => AcceptedContentTypes.Contains(file.ContentType))
                .WithMessage("Only csv files are accepted");
        }
    }
}
