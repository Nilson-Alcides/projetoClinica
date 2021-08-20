using MySql.Data.MySqlClient;
using ProjetoClinica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoClinica.Dados
{
    public class clPacienteAcoes
    {
        Conexao con = new Conexao();
        public void inserirPaciente(clPaciente cm)
        {
            MySqlCommand cmd = new MySqlCommand("insert into tbPaciente (nomePac, telPac, celPac, emailPac) values (@nomePac, @telPac, @celPac, @emailPac)", con.MyConectarBD()); // @: PARAMETRO

            cmd.Parameters.Add("@nomePac", MySqlDbType.VarChar).Value = cm.nomePac;
            cmd.Parameters.Add("@telPac", MySqlDbType.VarChar).Value = cm.telPac;
            cmd.Parameters.Add("@celPac", MySqlDbType.VarChar).Value = cm.celPac;
            cmd.Parameters.Add("@emailPac", MySqlDbType.VarChar).Value = cm.emailPac;

            cmd.ExecuteNonQuery();
            con.MyDesConectarBD();
        }
    }
}