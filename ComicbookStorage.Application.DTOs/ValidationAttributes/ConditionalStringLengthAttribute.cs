
namespace ComicbookStorage.Application.DTOs.ValidationAttributes
{
    using System.ComponentModel.DataAnnotations;

    public class ConditionalStringLengthAttribute : StringLengthAttribute
    {
        public ConditionalStringLengthAttribute(int maximumLength) : base(maximumLength)
        {
        }

        public bool TolerateEmptyValues { get; set; }

        public override bool IsValid(object value)
        {
            if (TolerateEmptyValues && string.IsNullOrEmpty((string) value))
            {
                return true;
            }
            return base.IsValid(value);
        }
    }
}
