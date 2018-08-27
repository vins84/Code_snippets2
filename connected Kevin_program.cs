using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;


namespace ADO_Walkthrough_Connected
{
    public class Program
    {
        static void Main(string[] args)
        {

                            // ===== This is unnecessary as we have implemented it into a App.Config!!! and added following 7 lines ======
            //string connectionString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=oracle.fdmgroup.com)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xe)));Pooling=false;User Id=miroslawkaczor;Password=Oracle101;";  //Remember to use correct credentials!

            string host = ConfigurationManager.AppSettings["host"].ToString();
            string port = ConfigurationManager.AppSettings["port"].ToString();
            string oracleSID = ConfigurationManager.AppSettings["oracleSID"].ToString();
            string username = ConfigurationManager.AppSettings["username"].ToString();
            string password = ConfigurationManager.AppSettings["password"].ToString();

            string connectionString = ConfigurationManager.ConnectionStrings["FDMOracle"].ToString();

            connectionString = String.Format(connectionString, host, port, oracleSID, username, password);


                            // ===== Create an instance of the repository, passing in the connection string, and call GetAllBrokers(). =====
            IBrokerRepository repository = new OracleSqlBrokerRepository(connectionString);

                            // =====  Adding a new Broker to the database  =====
            //repository.AddBroker(new Broker() { Id = 20, FirstName = "Donald", LastName = "Duck" });
                            // =====  Trying to make it interactive  =====
            bool running = true;
            while (running)
            {
                Console.WriteLine("====   Choose from following options :   ====");
                Console.WriteLine("\n\t[1]. Add Broker \n\t[2]. View List Brokers  \n\t[3]. Quit");

                string featureChoice = Console.ReadLine();
                if (featureChoice.Equals("1"))
                {
                    break;
                }
                else if (featureChoice.Equals("2"))
                {
                    List<Broker> brokers = repository.GetAllBrokers();
                }
                else if (featureChoice.Equals("3"))
                {
                    Console.WriteLine("Good Bye");
                    System.Threading.Thread.Sleep(2000);            //  two second delay!
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("ALARME ALARME");
                    System.Threading.Thread.Sleep(1000);
                }
                //Environment.Exit(0);
            }


            bool running2 = true;
            while (running2)
            {
                Console.WriteLine("====   Would you like to add a new broker to the database?   ====");
                Console.WriteLine("\n\t[1]. Yes \n\t[2]. No  \n\t[3]. Quit");

                string featureChoice2 = Console.ReadLine();
                if (featureChoice2.Equals("1"))
                {
                    Console.WriteLine("====   Specify brokers ID   ====");
                    string chooseBrokersID = Console.ReadLine();
                    int chooseBrokersIDInt = int.Parse(chooseBrokersID);

                    Console.WriteLine("====   Choose brokers first name   ====");
                    string chooseBrokersFirstName = Console.ReadLine();
        
                    Console.WriteLine("====   Choose brokers last name   ====");
                    string chooseBrokersLastName = Console.ReadLine();

                    repository.AddBroker(new Broker() { Id = chooseBrokersIDInt, FirstName = chooseBrokersFirstName, LastName = chooseBrokersLastName });
                }
                else if (featureChoice2.Equals("2"))
                {
                    Console.WriteLine("Displaying the list of all brokers");
                }
                else if (featureChoice2.Equals("3"))
                {
                    Environment.Exit(0);
                }


                // =====  Updating Broker in the database  =====
                //repository.UpdateBroker(new Broker() { Id = 20, FirstName = "Mikey", LastName = "Mouse" });

                // =====  Removing Broker from the database  =====
                //repository.RemoveBroker(new Broker() { Id = 20, FirstName = "Donald", LastName = "Duck" });


                List<Broker> brokers = repository.GetAllBrokers();
                Console.WriteLine("\nBroker Details\n==============");
                Console.WriteLine("{0,-4} {1,-12} {2,-15}", "ID", "First Name", "Last Name");
                Console.WriteLine("{0,-4} {1,-12} {2,-15}", "--", "----------", "---------");
                foreach (Broker broker in brokers)
                {
                    Console.WriteLine("{0,-4} {1,-12} {2,-15}", broker.Id, broker.FirstName, broker.LastName);
                }
                Console.WriteLine("\nEND");
                Console.ReadLine();
            }
        }
    }
}


