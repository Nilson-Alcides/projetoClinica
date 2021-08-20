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