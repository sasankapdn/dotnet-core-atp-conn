using System;
using Oracle.ManagedDataAccess.Client;

namespace adb7
{
    class Program
    {
        static void Main(string[] args)
        {

            //Demo: ODP.NET Core application that connects to Oracle Autonomous DB

            //Enter user id and password, such as ADMIN user	
            string conString = "User Id=admin;Password=AlphaOffice1@;" +

            //Enter net service name for data source value
            "Data Source=atpd_high; Connection Timeout=500;";

            using (OracleConnection con = new OracleConnection(conString))
            {
                using (OracleCommand cmd = con.CreateCommand())
                {
                    try
                    {
                        //Enter directory where the tnsnames.ora and sqlnet.ora files are located
                        OracleConfiguration.TnsAdmin = @"/Users/sasanka/Downloads/Wallet_ATPD";

                        //Alternatively, connect descriptor and net service name entries can be placed in app itself
                        //To use, uncomment below and enter the DB machine port, hostname/IP, service name, and distinguished name
                        //Lastly, set the Data Source value to "autonomous"
                        //OracleConfiguration.OracleDataSources.Add("autonomous", "(description=(address=(protocol=tcps)(port=<PORT>)(host=<HOSTNAME/IP>))(connect_data=(service_name=<SERVICE NAME>))(security=(ssl_server_cert_dn=<DISTINGUISHED NAME>)))");                       

                        //Enter directory where wallet is stored locally
                        OracleConfiguration.WalletLocation = @"/Users/sasanka/Downloads//Wallet_ATPD";

                        con.Open();

                        Console.WriteLine("Successfully connected to Oracle Autonomous Database");

                        //Retrieve database version info
                        cmd.CommandText = "SELECT BANNER FROM V$VERSION";
                        OracleDataReader reader = cmd.ExecuteReader();
                        reader.Read();
                        Console.WriteLine("Connected to " + reader.GetString(0));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        //Console.WriteLine(Convert.ToString(ex.InnerException.Message.ToString()));
                        Console.WriteLine(ex.StackTrace);
                    }

                    Console.ReadLine();
                }
            }
        }
    }
}
