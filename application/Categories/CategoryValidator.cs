using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using domain;
using FluentValidation;

namespace application.Categories
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}