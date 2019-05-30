using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFrameworkFinal222;

namespace EntityFrameworkFinal222
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to our small database utility");
            System.Threading.Thread.Sleep(1000);            //  one second delay!
            EntityDataModel222 context = new EntityDataModel222();


            bool running = true;
            while (running)
            {
                Console.WriteLine("Would you like to add broker?");
                Console.WriteLine("[1]. Yes");
                Console.WriteLine("[2]. No");
                Console.WriteLine("[3]. Quit");
                int decision = Convert.ToInt32(Console.ReadLine());
                if (decision == 1)
                {
                    RetrievingMethod Retrieve = new RetrievingMethod();
                    Console.WriteLine("Choose brokers ID");
                    string opt1 = Console.ReadLine();
                    int option1 = int.Parse(opt1);
                    Retrieve.ReadFromDatabase(option1);
                }
                else if (decision == 2)
                {
                    Console.WriteLine("\nSo....What would you like to do?");
                    Console.WriteLine();
                }
                else
                {
                    Environment.Exit(0);
                }
            }


            //RetrievingMethod writingCompany = new RetrievingMethod();
            //var company = new Company() { id = 2, name = "Dell" };
            //Console.WriteLine("Choose Company ID");
            //int option2 = Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine("Define Company Name");
            //string option22 = Console.ReadLine();
            //context.companies.Add(company);
            //context.SaveChanges();
 


            // ===========================  Writing Company to Database  ========================================================
            //    var company = new Company() { id = 2, name = "Dell", unitNumber = 152, street = "Bredbury Park Way Bredbury", city = "Stockport", postcode = "Sk6 2SX", country = "England"  };
            //    EntityDataModel222 context = new EntityDataModel222();
            //    context.companies.Add(company);
            //    context.SaveChanges();
            //    
            //}

            // ===========================  Writing Broker to Database  =========================================================
            //    var broker = new Broker() { id = 11, companyId = 11, first_name = "Chuck", last_name = "Norris", houseNo = 147, street = "Seedley Park Road", city = "Salford", postcode = 44241, country = "England" };
            //    EntityDataModel222 context = new EntityDataModel222();
            //    context.brokers.Add(broker);
            //    context.SaveChanges();
            //}

            // ===========================  Updating Brokers Database  ==================================================================
            //    EntityDataModel222 context = new EntityDataModel222();
            //    var broker = context.brokers.Find(8);       //Select correct borkers ID in Find()
            //    broker.first_name = "Miroslaw";             //Select new name 
            //    broker.companyId = 11;                      //Select new company Id for the broker  -   REMEMBER that you cannot udate primary key you fool
            //    context.SaveChanges();
            //}

            //===========================  Updating Company Database  ==================================================================
            //    EntityDataModel222 context = new EntityDataModel222();
            //    var company = context.companies.Find(11);       //Select correct Company ID in Find()
            //    company.name = "FDM";                           //Select new name for the company               
            //    context.SaveChanges();                          //Persist changes
            //}

            // ===========================  Deleting Broker from Database  =======================================================
            //    EntityDataModel222 context = new EntityDataModel222();
            //    var brokers = context.brokers.Where(b => b.first_name == "Wladimir");
            //    foreach (var broker in brokers)
            //    {
            //        context.brokers.Remove(broker);
            //    }
            //    context.SaveChanges();
            //}

            // ===========================  Deleting Company from Database  =======================================================
            //    EntityDataModel222 context = new EntityDataModel222();
            //    var companies = context.companies.Where(c => c.name == "New Shop");
            //    foreach (var company in companies)
            //    {
            //        context.companies.Remove(company);
            //    }
            //    context.SaveChanges();
            //}

            // ===========================  Reading with Related Data - More Complex query  =====================================
            //EntityDataModel222 context = new EntityDataModel222();
            //var query = (from b in context.brokers
            //             where b.company.name == "FDM"
            //             select b);

            //var queryName = (from c in context.companies        // The following is to display company name. As the name is stored as an object it had to be converted to the string
            //             where c.name == "FDM"
            //             select c.name);
            //var companyName="";
            //foreach (var ss in queryName)
            //{
            //    companyName = ss;
            //}

            //foreach (var broker in query)
            //{
            //    Console.WriteLine("Broker " + broker.first_name + " " + broker.last_name + " works for " + companyName + "- hihih");
            //}

        }
    }
}


