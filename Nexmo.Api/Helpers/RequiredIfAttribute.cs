using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexmo.Api.Helpers
{
    public class RequiredIfAttribute : ValidationAttribute
    {
        protected RequiredAttribute _innerAttribute;
        public string DependentProperty { get; set; }
        public object TargetValue { get; set; }

        public RequiredIfAttribute(string dependentProperty, object targetValue)
        {
            _innerAttribute = new RequiredAttribute();
            DependentProperty = dependentProperty;
            TargetValue = targetValue;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var containerType = validationContext.ObjectInstance.GetType();
            var field = containerType.GetProperty(DependentProperty);

            if (field != null)
            {
                if (field != null)
                {
                    // get the value of the dependent property
                    var dependentValue = field.GetValue(validationContext.ObjectInstance, null);
                    // trim spaces of dependent value
                    if (dependentValue != null && dependentValue is string)
                    {
                        dependentValue = (dependentValue as string).Trim();

                        if ((dependentValue as string).Length == 0)
                        {
                            dependentValue = null;
                        }
                    }

                    // compare the value against the target value
                    if ((dependentValue == null && TargetValue == null) ||
                        (dependentValue != null && (TargetValue.Equals("*") || dependentValue.Equals(TargetValue))))
                    {
                        // match => means we should try validating this field
                        if (!_innerAttribute.IsValid(value))
                            // validation failed - return an error
                            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName), new[] { validationContext.MemberName });
                    }

                }
            }

             return ValidationResult.Success;
        }
    }
}
