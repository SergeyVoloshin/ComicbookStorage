
namespace ComicbookStorage.Application.Validation
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;
    using System.Resources;
    using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

    public class CustomValidationMetadataProvider : IValidationMetadataProvider
    {
        private readonly ResourceManager resourceManager;
        private readonly Type resourceType;

        public CustomValidationMetadataProvider(Type resourceType)
        {
            this.resourceType = resourceType;
            resourceManager = new ResourceManager(resourceType.FullName, resourceType.Assembly);
        }

        public void CreateValidationMetadata(ValidationMetadataProviderContext context)
        {
            if (context.Key.ModelType.GetTypeInfo().IsValueType &&
                context.ValidationMetadata.ValidatorMetadata.Count(m => m.GetType() == typeof(RequiredAttribute)) == 0)
            {
                context.ValidationMetadata.ValidatorMetadata.Add(new RequiredAttribute());
            }

            foreach (var attribute in context.ValidationMetadata.ValidatorMetadata)
            {
                if (attribute is ValidationAttribute validationAttribute && 
                    validationAttribute.ErrorMessage == null && 
                    validationAttribute.ErrorMessageResourceName == null)
                {
                    var name = validationAttribute.GetType().Name;
                    if (resourceManager.GetString(name) != null)
                    {
                        validationAttribute.ErrorMessageResourceType = resourceType;
                        validationAttribute.ErrorMessageResourceName = name;
                        validationAttribute.ErrorMessage = null;
                    }
                }
            }
        }
    }
}
