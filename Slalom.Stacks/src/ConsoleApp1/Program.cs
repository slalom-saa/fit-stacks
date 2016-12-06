using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Slalom.Stacks.Data.RavenDB;

namespace ConsoleApp1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var repository = new Repository<Item>(new EntityContext());

            //repository.ClearAsync().Wait();

            //Console.WriteLine(repository.FindAsync(new Guid("6f7330ec-037d-4343-b85f-4813637b4f11")).Result);


            //var target = new List<Item>();

            //target.Add(Item.Create(1));

            //repository.AddAsync(target.ToArray()).Wait();


            var target = repository.CreateQuery().First(e => e.Names.Contains("Item 1"));
            Console.WriteLine(target);
        }
    }
}
