﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PmsEteck.Helpers
{
    public abstract class ConditionalValidationAttribute : ValidationAttribute, IClientValidatable
    {
        protected readonly ValidationAttribute InnerAttribute;
        public string DependentProperty { get; set; }
        public object TargetValue { get; set; }
        protected abstract string ValidationName { get; }

        protected virtual IDictionary<string, object> GetExtraValidationParameters()
        {
            return new Dictionary<string, object>();
        }

        protected ConditionalValidationAttribute(ValidationAttribute innerAttribute, string dependentProperty, object targetValue)
        {
            InnerAttribute = innerAttribute;
            DependentProperty = dependentProperty;
            TargetValue = targetValue;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var containerType = validationContext.ObjectInstance.GetType();
            var field = containerType.GetProperty(DependentProperty);
            if (field != null)
            {
                // get the value of the dependent property
                var dependentvalue = field.GetValue(validationContext.ObjectInstance, null);

                // compare the value against the target value
                if ((dependentvalue == null && TargetValue == null) || (dependentvalue != null && dependentvalue.Equals(TargetValue)))
                {
                    // match => means we should try validating this field
                    if (!InnerAttribute.IsValid(value))
                    {
                        // validation failed - return an error
                        return new ValidationResult(ErrorMessage, new[] { validationContext.MemberName });
                    }
                }

            }
            return ValidationResult.Success;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule()
            {
                ErrorMessage = FormatErrorMessage(metadata.GetDisplayName()),
                ValidationType = ValidationName,
            };
            string depProp = BuildDependentPropertyId(metadata, context as ViewContext);
            // find the value on the control we depend on; if it's a bool, format it javascript style
            string targetValue = (TargetValue ?? "").ToString();
            if (TargetValue.GetType() == typeof(bool))
            {
                targetValue = targetValue.ToLower();
            }
            rule.ValidationParameters.Add("dependentproperty", depProp);
            rule.ValidationParameters.Add("targetvalue", targetValue);
            // Add the extra params, if any
            foreach (var param in GetExtraValidationParameters())
            {
                rule.ValidationParameters.Add(param);
            }
            yield return rule;
        }

        private string BuildDependentPropertyId(ModelMetadata metadata, ViewContext viewContext)
        {
            string depProp = viewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(DependentProperty);
            // This will have the name of the current field appended to the beginning, because the TemplateInfo's context has had this fieldname appended to it.
            var thisField = metadata.PropertyName + "_";
            if (depProp.StartsWith(thisField))
            {
                depProp = depProp.Substring(thisField.Length);
            }
            return depProp;
        }
    }

    public class RequiredIfAttribute : ConditionalValidationAttribute
    {
        protected override string ValidationName
        {
            get { return "requiredif"; }
        }
        public RequiredIfAttribute(string dependentProperty, object targetValue) : base(new RequiredAttribute(), dependentProperty, targetValue) { }
        protected override IDictionary<string, object> GetExtraValidationParameters()
        {
            return new Dictionary<string, object>
        {
            { "rule", "required" }
        };
        }
    }

    public class RegularExpressionIfAttribute : ConditionalValidationAttribute
    {
        private readonly string pattern;
        protected override string ValidationName
        {
            get { return "regularexpressionif"; }
        }
        public RegularExpressionIfAttribute(string pattern, string dependentProperty, object targetValue)
            : base(new RegularExpressionAttribute(pattern), dependentProperty, targetValue)
        {
            this.pattern = pattern;
        }
        protected override IDictionary<string, object> GetExtraValidationParameters()
        {
            // Set the rule RegEx and the rule param pattern
            return new Dictionary<string, object>
        {
            {"rule", "regex"},
            { "ruleparam", pattern }
        };
        }
    }

    public class RangeIfAttribute : ConditionalValidationAttribute
    {
        private readonly int minimum;
        private readonly int maximum;
        protected override string ValidationName
        {
            get { return "rangeif"; }
        }
        public RangeIfAttribute(int minimum, int maximum, string dependentProperty, object targetValue)
            : base(new RangeAttribute(minimum, maximum), dependentProperty, targetValue)
        {
            this.minimum = minimum;
            this.maximum = maximum;
        }
        protected override IDictionary<string, object> GetExtraValidationParameters()
        {
            // Set the rule Range and the rule param [minumum,maximum]
            return new Dictionary<string, object>
        {
            {"rule", "range"},
            { "ruleparam", string.Format("[{0},{1}]", minimum, maximum) }
        };
        }
    }
}