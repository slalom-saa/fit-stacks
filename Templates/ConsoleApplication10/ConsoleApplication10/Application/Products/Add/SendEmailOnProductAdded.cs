using System;
using Slalom.Stacks.Messaging;

namespace ConsoleApplication10.Application.Products.Add
{
    public class SendEmailOnProductAdded : EventActor<AddProductEvent>
    {
        public override void Execute(AddProductEvent message)
        {
            Console.WriteLine("Sending mail.");
        }
    }
}