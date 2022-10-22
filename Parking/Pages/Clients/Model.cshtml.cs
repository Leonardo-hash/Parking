using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Linq;

namespace Parking.Pages.Clients
{
    public class ModelModel : PageModel
    {
        public List<ClientInfo> listClient = new List<ClientInfo>();
        public void OnGet()
        {
            try
            {
                //conexão com o BD
                String connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Crud/parking;Integrated Security=True";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    if(conn.State == ConnectionState.Open) { Console.WriteLine("Conectado"); } else { Console.WriteLine("Não conectado"); }
                    // comando sql
                    var sql = "SELECT * FROM clients";
                    //comando para permitir a leitura do comando sql
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClientInfo clientInfo = new ClientInfo();
                                clientInfo.Id = "" + reader.GetInt32(0);
                                clientInfo.Name = reader.GetString(1);
                                clientInfo.Email = reader.GetString(2);
                                clientInfo.Phone = reader.GetString(3);
                                clientInfo.Car = reader.GetString(5);
                                clientInfo.placa = reader.GetString(6);
                                clientInfo.cor = reader.GetString(7);
                                clientInfo.created_at = reader.GetDateTime(4).ToString() ;

                                listClient.Add(clientInfo);

                                
                            }
                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:" + ex.ToString());
            }
          
        }
    }
    public class ClientInfo
    {
        public string Id;
        public string Name;
        public string Email;
        public string Phone;
        public string Car;
        public string placa;
        public string cor;
        public string created_at;
    }
}
