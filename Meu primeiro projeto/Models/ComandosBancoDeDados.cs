using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Meu_primeiro_projeto.Models
{
    public class ComandosBancoDeDados
    {
        private string ConnectionString
        {
            get { return @"Data Source=senacturmati.database.windows.net;
                          Initial Catalog=Alysson;
                          User id=senac;
                          Password=Teste123#"; }
        }
        public List<Clientes> BuscarClientes()
        {
            List<Clientes> listaClientes = new List<Clientes>();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Clientes", con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Clientes Clientes = new Clientes();
                            Clientes.ClientesId = (Guid)reader["ClientesId"];
                            Clientes.Nome = (string)reader["Nome"];
                            Clientes.Endereco = (string)reader["Endereco"];
                            Clientes.Telefone = (int)reader["Telefone"];
                            Clientes.Observacao = (string)reader["Observacao"];
                            listaClientes.Add(Clientes);
                        }
                    }
                }
            }
            return listaClientes;
        }
        public Clientes BuscarClientesPorId(Guid ClientesId)
        {
            Clientes Clientes = new Clientes();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Clientes where ClientesId = @ClientesId", con))
                {
                    command.Parameters.Add("@ClientesId", SqlDbType.UniqueIdentifier).Value = ClientesId;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Clientes.ClientesId = (Guid)reader["ClientesId"];
                            Clientes.Nome = (string)reader["Nome"];
                            Clientes.Endereco = (string)reader["Endereco"];
                            Clientes.Telefone = (int)reader["Telefone"];
                            Clientes.Observacao = (string)reader["Observacao"];
                        }
                    }
                }
            }
            return Clientes;
        }
        public void SalvarCliente(Clientes Clientes)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (SqlCommand command =
                    new SqlCommand(@"UPDATE Clientes SET Quantidade = @Quantidade,
                                                            Nome = @Nome,
                                                            Endereco = @Endereco,
                                                            Telefone = @Telefone,
                                                            Observacao = @Observacao
                                                        where ClientesId = @ClientesId", con))
                {
                    command.Parameters.Add("@ClientesId", SqlDbType.UniqueIdentifier).Value = Clientes.ClientesId;
                    command.Parameters.Add("@Nome", SqlDbType.VarChar).Value = Clientes.Nome;
                    command.Parameters.Add("@Endereco", SqlDbType.VarChar).Value = Clientes.Endereco;
                    command.Parameters.Add("@Telefone", SqlDbType.Int).Value = Clientes.Telefone;
                    command.Parameters.Add("@Observacao", SqlDbType.VarChar).Value = Clientes.Observacao;
                    command.ExecuteReader();
                }
            }
        }
        public void InserirCliente(Clientes Clientes)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (SqlCommand command =
                    new SqlCommand(@"INSERT INTO Clientes values (@ClientesId,
                                                                @Quantidade,
                                                                @Nome,
                                                                @Telefone,
                                                                @Observacao,
                                                                @Censurado)", con))
                {
                    command.Parameters.Add("@ClientesId", SqlDbType.UniqueIdentifier).Value = Clientes.ClientesId;
                    command.Parameters.Add("@Nome", SqlDbType.VarChar).Value = Clientes.Nome;
                    command.Parameters.Add("@Endereco", SqlDbType.VarChar).Value = Clientes.Endereco;
                    command.Parameters.Add("@Telefone", SqlDbType.Int).Value = Clientes.Telefone;
                    command.Parameters.Add("@Observacao", SqlDbType.VarChar).Value = Clientes.Observacao;
                    command.ExecuteReader();
                }
            }
        }
        public void ExcluirCliente(Guid ClientesId)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (SqlCommand command =
                    new SqlCommand(@"DELETE Clientes where ClientesId = @ClientesId", con))
                {
                    command.Parameters.Add("@ClientesId", SqlDbType.UniqueIdentifier).Value = ClientesId;
                    command.ExecuteReader();
                }
            }
        }
        public Login BuscarUsuario(Login login)
        {
            login.UsuarioSenha = Criptografar(login.UsuarioSenha);
            Login usuario = null;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(
                    "SELECT top 1 * FROM Usuarios where Login = @UsuarioLogin and Senha = @UsurioSenha", con))
                {
                    command.Parameters.Add("@UsuarioLogin", SqlDbType.VarChar).Value = login.UsuarioLogin;
                    command.Parameters.Add("@UsurioSenha", SqlDbType.VarChar).Value = login.UsuarioSenha;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            usuario = new Login();
                            usuario.UsuarioLogin = (string)reader["Login"];
                            usuario.UsuarioSenha = (string)reader["Senha"];
                        }
                    }
                }
            }
            return usuario;
        }

        public static string Criptografar(string valor)
        {
            string chaveCripto = "Alysson";
            Byte[] cript = System.Text.ASCIIEncoding.ASCII.GetBytes(valor);
            chaveCripto = Convert.ToBase64String(cript);
            return chaveCripto;
        }
        public static string Descriptografar(string valor)
        {
            string chaveCripto = "Alysson";
            Byte[] cript = Convert.FromBase64String(valor);
            chaveCripto = System.Text.ASCIIEncoding.ASCII.GetString(cript);
            return chaveCripto;
        }
        public void InserirCliente(Login login)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (SqlCommand command =
                    new SqlCommand(@"INSERT INTO Usuarios values (@UsuarioId,
                                                                @Login,
                                                                @Senha,)", con))
                {
                    command.Parameters.Add("@UsuarioId", SqlDbType.UniqueIdentifier).Value = login.LoginId;
                    command.Parameters.Add("@Login", SqlDbType.VarChar).Value = login.UsuarioLogin;
                    command.Parameters.Add("@Senha", SqlDbType.VarChar).Value = login.UsuarioSenha;
                    command.ExecuteReader();
                }
            }
        }

    }
}