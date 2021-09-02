using MySql.Data.MySqlClient;
using ProjetoClinica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoClinica.Dados
{
    public class clAtendimentoAcoes
    {
        Conexao con = new Conexao();

        public void TestarAgenda(clAtendimento agenda)
        {
            MySqlCommand cmd = new MySqlCommand("select * from tbAtendimento where dataAtend = @data and horaAtend = @hora", con.MyConectarBD());

            cmd.Parameters.Add("@data", MySqlDbType.VarChar).Value = agenda.dataAtend;
            cmd.Parameters.Add("@hora", MySqlDbType.VarChar).Value = agenda.horaAtend;

            MySqlDataReader leitor;

            leitor = cmd.ExecuteReader();

            if (leitor.HasRows)
            {
               // while (leitor.Read())
              ///  {
                    agenda.confAgendamento = "0";
             //   }

            }

            else
            {
                agenda.confAgendamento = "1";
            }

            con.MyDesConectarBD();
        }


        public void inserirAgenda(clAtendimento cm)
        {

            MySqlCommand cmd = new MySqlCommand("insert into tbAtendimento(dataAtend, horaAtend, codMedico, codPac) values (@data, @hora,@codMedico,@codPac)", con.MyConectarBD());
            cmd.Parameters.Add("@data", MySqlDbType.VarChar).Value = cm.dataAtend;
            cmd.Parameters.Add("@hora", MySqlDbType.VarChar).Value = cm.horaAtend;
            cmd.Parameters.Add("@codMedico", MySqlDbType.VarChar).Value = cm.codMedico;
            cmd.Parameters.Add("@codPac", MySqlDbType.VarChar).Value = cm.codPac;

            cmd.ExecuteNonQuery();
            con.MyDesConectarBD();
        }
    }
}