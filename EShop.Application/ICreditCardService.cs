using EShop.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application;

public interface ICreditCardService
{
    void ValidateCard(string cardNumber);

    CreditCardProvider? GetCardProvider(string cardNumber);
}
