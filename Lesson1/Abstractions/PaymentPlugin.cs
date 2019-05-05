using System;

namespace Abstractions
{
    public interface PaymentPlugin
    {   
        PaymentProcessor Pay();
        string GetName();
    }

}