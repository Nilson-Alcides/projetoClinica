using MySql.Data.MySqlClient;
using ProjetoClinica.Dados;
using ProjetoClinica.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

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
        clAtendimentoAcoes acAtend = new clAtendimentoAcoes();

        //Carrega medico do medico
        public void carregarMedicos()
        {
            List<SelectListItem> medicos = new List<SelectListItem>();

            using (MySqlConnection con = new MySqlConnection("Server=localhost;port=3307; DataBase=bdClin0408; user id=root;password=361190"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbMedico", con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    medicos.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()
                    });
                }
                con.Close();

            }

            ViewBag.med = new SelectList(medicos, "Value", "Text");
        }
        //Carrega Paciente do medico
        public void carregarPacientes()
        {
            List<SelectListItem> pacientes = new List<SelectListItem>();

            using (MySqlConnection con = new MySqlConnection("Server=localhost;port=3307; DataBase=bdClin0408; user id=root;password=361190"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbPaciente", con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    pacientes.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()
                    });
                }
                con.Close();

            }

            ViewBag.pac = new SelectList(pacientes, "Value", "Text");
        }

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

        public ActionResult consAgenda()
        {
            GridView gvAtend = new GridView();
            gvAtend.DataSource = acAtend.selecionaAgenda();
            gvAtend.DataBind();
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gvAtend.RenderControl(htw);
            ViewBag.GridViewString = sw.ToString();
            return View();
        }
        public ActionResult consAgendaBusca()
        {
            return View();
        }
        [HttpPost]
        public ActionResult consAgendaBusca(clAtendimento modeloAtend)
        {
            GridView gvAtend = new GridView();
            gvAtend.DataSource = acAtend.selecionaAgendaData(modeloAtend);
            gvAtend.DataBind();
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gvAtend.RenderControl(htw);
            ViewBag.GridViewString = sw.ToString();
            return View();
            
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult cadAtendimento()
        {
            carregarMedicos();
            carregarPacientes();
            return View();
        }
        [HttpPost]
        public ActionResult cadAtendimento(clAtendimento modeloAtend)
        {
            
            carregarMedicos();
            carregarPacientes();
            acAtend.TestarAgenda(modeloAtend);

            if (modeloAtend.confAgendamento=="1")
            {
                modeloAtend.codPac = Request["pac"];
                modeloAtend.codMedico = Request["med"];
                acAtend.inserirAgenda(modeloAtend);
                ViewBag.msg = "Agendamento realizado com sucesso";
            }
            else
            {
                ViewBag.msg = "Horário indisponível, por favor escolaher outra data/hora";
            }
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

        public ActionResult ConsultaEspecialidade()
        {
            clEspAcoes ac = new clEspAcoes();
            GridView dgv = new GridView(); // Instância para a tabela
            dgv.DataSource = ac.consultaEspeciadade(); //Atribuir ao grid o resultado da consulta
            dgv.DataBind(); //Confirmação do Grid
            StringWriter sw = new StringWriter(); //Comando para construção do Grid na tela
            HtmlTextWriter htw = new HtmlTextWriter(sw); //Comando para construção do Grid na tela
            dgv.RenderControl(htw); //Comando para construção do Grid na tela
            ViewBag.GridViewString = sw.ToString(); //Comando para construção do Grid na tela
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