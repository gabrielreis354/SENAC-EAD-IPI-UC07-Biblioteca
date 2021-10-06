using MySqlConnector;
namespace uc7_ativ2.Models
{
    public class UsuarioRepositorio
    {
        protected const string DadosConexao = "database=Biblioteca; Data source = localhost; User Id = root;";
        protected MySqlConnection conn = new MySqlConnection(DadosConexao);
    }
}