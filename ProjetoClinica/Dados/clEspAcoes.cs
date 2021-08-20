using MySql.Data.MySqlClient;
using ProjetoClinica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoClinica.Dados
{
    public class clEspAcoes
    {
        Conexao con = new Conexao();

        public void inserirEsp(clEsp cm)
        {
            MySqlCommand cmd = new MySqlCommand("insert into tbEsp (especialidade) values (@especialidade)", con.MyConectarBD()); // @: PARAMETRO

            cmd.Parameters.Add("@especialidade", MySqlDbType.VarChar).Value = cm.especialidade;

            cmd.ExecuteNonQuery();
            con.MyDesConectarBD();
        }
    }
}