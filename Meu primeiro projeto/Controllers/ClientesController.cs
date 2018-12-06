using Meu_primeiro_projeto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Clientes.Controllers
{
    public class ClientesController : Controller
    {
        public bool ClienteLogado
        {
            get
            {
                if (TempData["ClienteLogado"] != null)
                {
                    TempData.Keep("ClienteLogado");
                    return (bool)TempData["ClienteLogado"];
                }
                else
                    return false;
            }
            set
            {
                TempData["ClienteLogado"] = value;
            }
        }
        public ActionResult Index()
        {
            if (ClienteLogado)
            {
                ComandosBancoDeDados comandos = new ComandosBancoDeDados();
                var lista = comandos.BuscarClientes();
                return View(lista);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public ActionResult Editar(Guid FilmeId)
        {
            if (ClienteLogado)
            {
                ComandosBancoDeDados comandos = new ComandosBancoDeDados();
                var Clientes = comandos.BuscarClientesPorId(FilmeId);
                return View(Clientes);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public ActionResult Deletar(Guid FilmeId)
        {
            if (ClienteLogado)
            {
                ComandosBancoDeDados comandos = new ComandosBancoDeDados();
                comandos.ExcluirCliente(FilmeId);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public ActionResult SalvarFilme(Meu_primeiro_projeto.Models.Clientes cliente)
        {
            if (ClienteLogado)
            {
                ComandosBancoDeDados comandos = new ComandosBancoDeDados();
                comandos.SalvarCliente(cliente);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public ActionResult CriarFilme()
        {
            if (ClienteLogado)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public ActionResult SalvarFilmeCriado(Meu_primeiro_projeto.Models.Clientes Clientes)
        {
            if (ClienteLogado)
            {
                ComandosBancoDeDados comandos = new ComandosBancoDeDados();
                comandos.InserirCliente(Clientes);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }


        public ActionResult Login()
        {
            if (ClienteLogado)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Logar(Login login)
        {
            ComandosBancoDeDados comandos = new ComandosBancoDeDados();
            var usuario = comandos.BuscarUsuario(login);

            if (usuario != null)
            {
                ClienteLogado = true;
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult CadastrarCliente()
        {
            if (ClienteLogado)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult SalvarClienteCriado(Login login)
        {
            if (ClienteLogado)
            {
                ComandosBancoDeDados comandos = new ComandosBancoDeDados();
                comandos.InserirCliente(login);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
    }
}