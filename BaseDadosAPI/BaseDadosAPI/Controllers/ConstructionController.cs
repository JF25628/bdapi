using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;


namespace BaseDadosAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConstructionController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ConstructionController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult GetClientes()
        {
            List<string> clientes = new List<string>();

            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "Select Nome FROM CLientes";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            string nome = $"{reader["Nome"]}";
                            clientes.Add(nome);
                        }

                        reader.Close();

                        return Ok(clientes);
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500, $"Erro ao buscar clientes: {ex.Message}");
                    }
                }
            }
        }


        ////[HttpGet]
        ////public IActionResult GetDepartamento()
        ////{
        ////    List<string> departamentos = new List<string>();

        ////    string connectionString = _configuration.GetConnectionString("DefaultConnection");

        ////    using (SqlConnection connection = new SqlConnection(connectionString))
        ////    {
        ////        string sqlQuery = "Select Nome FROM Departamento";

        ////        using (SqlCommand command = new SqlCommand(sqlQuery, connection))
        ////        {
        ////            try
        ////            {
        ////                connection.Open();
        ////                SqlDataReader reader = command.ExecuteReader();

        ////                while (reader.Read())
        ////                {
        ////                    string nome = $"{reader["Nome"]}";
        ////                    departamentos.Add(nome);
        ////                }

        ////                reader.Close();

        ////                return Ok(departamentos);
        ////            }
        ////            catch (Exception ex)
        ////            {
        ////                return StatusCode(500, $"Erro ao buscar departamentos: {ex.Message}");
        ////            }
        ////        }
        ////    }
        ////}
    }
}