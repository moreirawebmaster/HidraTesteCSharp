using Flunt.Notifications;
using Flunt.Validations;

namespace Bank.Commun.Domain.Dto
{
    public abstract class Validatable : Notifiable, IValidatable
    {
        public abstract void Validate();
    }
}