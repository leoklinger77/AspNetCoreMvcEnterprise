using Enterprise.Business.Interfaces;
using Enterprise.Business.Interfaces.Service;
using Enterprise.Business.Models;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enterprise.Business.Services
{
    public abstract class BaseService
    {
        private readonly INotification _notification;

        protected BaseService(INotification notification)
        {
            _notification = notification;
        }

        protected void Notifier(ValidationResult validationResult)
        {
            foreach (var item in validationResult.Errors)
            {
                Notifier(item.ErrorMessage);
            }
        }
        protected void Notifier(string message)
        {
            _notification.Handle(message);
        }

        protected bool ExecutedValidation<TV, TE>(TV validation, TE entity) where TV : AbstractValidator<TE> where TE : Entity
        {
            var validator = validation.Validate(entity);
            if (!validator.IsValid)
            {
                Notifier(validator);
                return false;
            }
            return true;
        }
    }
}
