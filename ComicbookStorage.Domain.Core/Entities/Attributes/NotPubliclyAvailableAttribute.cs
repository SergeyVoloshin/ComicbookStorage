
namespace ComicbookStorage.Domain.Core.Entities.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.Property)]
    public class NotPubliclyAvailableAttribute : Attribute
    {
    }
}
