using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirstDBTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string brokers_first_name;
            string brokers_last_name;
            string brokers_city;

            Console.WriteLine("Enter broker first name: ");
            brokers_first_name = Console.ReadLine();
            
            Console.WriteLine("Enter broker last name: ");
            brokers_last_name = Console.ReadLine();
            
            Console.WriteLine("Enter broker company id: ");
            string brokers_company_id = Console.ReadLine();
            int brokers_comp_id = Int16.Parse(brokers_company_id);

            Console.WriteLine("Enter broker city: ");
            brokers_city = Console.ReadLine();

            DataBaseControls dataBaseControls = new DataBaseControls();

            dataBaseControls.AddToTheDatabase( brokers_first_name, brokers_last_name, brokers_comp_id, brokers_city );

           


            ////Reading the database
            //ModelCodeFirst context = new ModelCodeFirst();
            //foreach (var broker in context.brokers)
            //{
            //Console.WriteLine(broker.name);
            //}
            //context.brokers.Find(1);



            ////Writing to Database
            //var company = new Company() { id = 4, name = "FDM" };
            //ModelCodeFirst context = new ModelCodeFirst();
            //context.companies.Add(company);
            //context.SaveChanges();



            ////Writing to Database
            //var broker = new Broker() { first_name = "John", last_name = "Hurst", companyId = 1, city = "Londons" };
            //ModelCodeFirst context = new ModelCodeFirst();
            //context.brokers.Add(broker);
            //context.SaveChanges(); //Persists changes to database



            ////Updating the database
            //ModelCodeFirst context = new ModelCodeFirst();
            //var broker = context.brokers.Find(10);
            //broker.city = "London";
            //context.SaveChanges();



            ////Read from the database
            //ModelCodeFirst context = new ModelCodeFirst();
            //Console.WriteLine(context.brokers.Find(5).first_name);

            ////Delete from database
            //ModelCodeFirst context = new ModelCodeFirst();
            //var brokers = context.brokers.Where(b => b.companyId == 1);

            //foreach (var broker in brokers)
            //{
            //context.brokers.Remove(broker);
            //}
            //context.SaveChanges();



            //ModelCodeFirst context = new ModelCodeFirst();
            //var query = (from b in context.brokers
            //             where b.city == "London"
            //             select b);

            //foreach (var broker in query)
            //{
            //    Console.WriteLine("{0} {1}", broker.first_name, broker.last_name);
            //}


        }
    }
}
