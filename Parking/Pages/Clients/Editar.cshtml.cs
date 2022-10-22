using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Data;

namespace Parking.Pages.Clients
{
    public class EditarModel : PageModel
    {

        public ClientInfo clientInfo = new ClientInfo();
        public string errorMessage = "";
        public string sucessMessage = "";
        public void OnGet()
        {
            string id = Request.Query["id"];
            try
            {
                //conexão com o BD
                String connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Crud/parking;Integrated Security=True";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    if (conn.State == ConnectionState.Open) { Console.WriteLine("Conectado"); } else { Console.WriteLine("Não conectado"); }
                    // comando sql
                    var sql = "SELECT * FROM clients WHERE id=@id";
                    //comando para permitir a leitura do comando sql
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {

                                clientInfo.Id = "" + reader.GetInt32(0);
                                clientInfo.Name = reader.GetString(1);
                                clientInfo.Email = reader.GetString(2);
                                clientInfo.Phone = reader.GetString(3); 
                                clientInfo.Car = reader.GetString(5);
                                clientInfo.placa = reader.GetString(6);
                                clientInfo.cor = reader.GetString(7);



                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERRO" + ex.ToString());
            }
        }
       public void Onpost() {
            clientInfo.Id = Request.Form["id"];
            clientInfo.Name = Request.Form["name"];
            clientInfo.Email = Request.Form["email"];
            clientInfo.Phone = Request.Form["phone"];
            clientInfo.Car = Request.Form["car"];
            clientInfo.placa = Request.Form["placa"];
            clientInfo.cor = Request.Form["cor"];

            if (clientInfo.Name.Length == 0 || clientInfo.Email.Length == 0 ||
                clientInfo.Phone.Length == 0 ||
                clientInfo.Car.Length == 0 || clientInfo.placa.Length == 0 || clientInfo.cor.Length == 0) { errorMessage = "Todos os campos são necessários"; return; }

            try
            {
                String connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Crud/parking;Integrated Security=True";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    var sql = "UPDATE  clients " +
                        
                        "SET name = @name, email = @email, phone = @phone, car=@car, placa= @placa, cor = @cor " +
                        "WHERE id=@id";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                       
                        cmd.Parameters.AddWithValue("@name", clientInfo.Name);
                        cmd.Parameters.AddWithValue("@email", clientInfo.Email);
                        cmd.Parameters.AddWithValue("@Phone", clientInfo.Phone);
                        cmd.Parameters.AddWithValue("@car", clientInfo.Car);
                        cmd.Parameters.AddWithValue("@Placa", clientInfo.placa);
                        cmd.Parameters.AddWithValue("@cor", clientInfo.cor);
                        cmd.Parameters.AddWithValue("@id", clientInfo.Id);
                        cmd.ExecuteNonQuery();

                    }
                }
            }


            catch (Exception ex)
            {
                Console.WriteLine("ERRO" + ex.ToString());
            }

            Response.Redirect("/Clients/Model");
        }
    }
}
