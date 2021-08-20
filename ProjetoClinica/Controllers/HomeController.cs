using MySql.Data.MySqlClient;
using ProjetoClinica.Dados;
using ProjetoClinica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjetoClinica.Controllers
{
    public class HomeController : Controller
    {
        clEsp modEsp = new clEsp();
        clEspAcoes acEsp = new clEspAcoes();
        clMedico modMedico = new clMedico();
        clMedicoAcoes acMedico = new clMedicoAcoes();
        clPaciente modPaciente = new clPaciente();
        clPacienteAcoes acPaciente = new clPacienteAcoes();

        //Carrega especialidades do medico
        public void carregarEsp()
        {
            List<SelectListItem> especialidades = new List<SelectListItem>();

            using (MySqlConnection con = new MySqlConnection("Server=localhost;port=3307; DataBase=bdClin0408; user id=root;password=361190"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbEsp", con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    especialidades.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()
                    });
                }
                con.Close();

            }

            ViewBag.esp = new SelectList(especialidades, "Value", "Text");
        }
        
        //_______________________________________///___________________________
        public ActionResult Index()
        {
            return View();
        }

        //Cadastro de Especialidades
        public ActionResult cadEsp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult cadEsp(FormCollection frm)
        {

            modEsp.especialidade = frm["txtEsp"];
            acEsp.inserirEsp(modEsp);
            ViewBag.msg = "Cadastro Realizado com sucesso!";
            return View();
        }

        //Cadastro do Médico
        public ActionResult cadMedico()
        {
            carregarEsp();
            return View();
        }
        [HttpPost]
        public ActionResult cadMedico(FormCollection frm)
        {
            carregarEsp();

            modMedico.nomeMedico = frm["txtNmMedico"];
            modMedico.codEspecialidade = Request["esp"];

            acMedico.inserirMedico(modMedico);

            ViewBag.msg = "Cadastro Realizado com sucesso!";
            return View();
        }
        //Cadastro do paciente
        public ActionResult cadPaciente()
        {

            return View();
        }
        [HttpPost]
        public ActionResult cadPaciente(FormCollection frm)
        {

            modPaciente.nomePac = frm["txtNmPaciente"];
            modPaciente.telPac = frm["txtTelefone"];
            modPaciente.celPac = frm["txtCelular"];
            modPaciente.emailPac = frm["txtEmail"];

            acPaciente.inserirPaciente(modPaciente);

            ViewBag.msg = "Cadastro Realizado com sucesso!";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}