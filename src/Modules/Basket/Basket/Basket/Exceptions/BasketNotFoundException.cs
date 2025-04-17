using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Exceptions;

namespace Basket.Basket.Exceptions
{
    public class BasketNotFoundException(string userName) : NotFoundException("ShoppingCart", userName)
    {
       
    }
    
}
