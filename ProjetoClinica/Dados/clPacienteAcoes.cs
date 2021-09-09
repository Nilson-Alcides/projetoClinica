using MySql.Data.MySqlClient;
using ProjetoClinica.Models;
using System;
using System.Collections.Generic;
using System.Data;
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

        public List<clPaciente> BuscarPac()
        {
            List<clPaciente> Paclist = new List<clPaciente>();
            MySqlCommand cmd = new MySqlCommand("Select * from tbPaciente", con.MyConectarBD());

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            con.MyDesConectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                Paclist.Add(
                    new clPaciente
                    {
                        codPac = Convert.ToInt32(dr["codPac"]),
                        nomePac = Convert.ToString(dr["nomePac"]),
                        telPac = Convert.ToString(dr["telPac"])
                    }
                    );
            }
            return Paclist;
        }
    }
}