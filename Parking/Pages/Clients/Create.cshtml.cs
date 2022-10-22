using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Parking.Pages.Clients
{
    public class CreateModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public string errorMessage = "";
        public string sucessMessage = "";
        public void OnGet()
        {

        }
        public void OnPost () {
            clientInfo.Name = Request.Form["name"];
            clientInfo.Email = Request.Form["email"];
            clientInfo.Phone = Request.Form["phone"];
            clientInfo.Car = Request.Form["car"];
            clientInfo.placa = Request.Form["placa"];
            clientInfo.cor = Request.Form["cor"];

            if(clientInfo.Name.Length == 0 || clientInfo.Email.Length==0|| 
                clientInfo.Phone.Length==0||
                clientInfo.Car.Length==0 || clientInfo.placa.Length==0 ||clientInfo.cor.Length == 0) { errorMessage = "Todos os campos são necessários";return; }





            //salvar novos cliente na Base de dados

            try
            {
                String connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Crud/parking;Integrated Security=True";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    var sql = "INSERT INTO clients " +
                        "(" +
                        "name, email, phone, car, placa, cor) VALUES" +
                        "(@name, @email, @phone, @car, @placa, @cor)";

                    using (SqlCommand cmd = new SqlCommand(sql, conn)) 
                    {
                        cmd.Parameters.AddWithValue("@name", clientInfo.Name);
                        cmd.Parameters.AddWithValue("@email", clientInfo.Email);
                        cmd.Parameters.AddWithValue("@Phone", clientInfo.Phone);
                        cmd.Parameters.AddWithValue("@car", clientInfo.Car);
                        cmd.Parameters.AddWithValue("@Placa", clientInfo.placa);
                        cmd.Parameters.AddWithValue("@cor", clientInfo.cor);

                        cmd.ExecuteNonQuery();
                       
                    }
                }
            }


            catch (Exception ex)
            {
                Console.WriteLine("ERRO" + ex.ToString());
            }
            clientInfo.Name = "";
            clientInfo.Email = "";
            clientInfo.Phone = "";
            clientInfo.Car = "";
            clientInfo.placa = "";
            clientInfo.cor = ""; sucessMessage = "Parabéns, você é o nosso novo cliente";

            Response.Redirect("Clients/Model");
        }
    }
}
