using FI.AtividadeEntrevista.BLL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using WebAtividadeEntrevista.Models;
using FI.AtividadeEntrevista.DML;

namespace WebAtividadeEntrevista.Controllers
{
    public class BeneficiarioController : Controller
    {
        public ActionResult Index(int id)
        {
            return PartialView();
        }

        [HttpPost]
        public JsonResult Incluir(BeneficiarioModel model)
        {
            BoBeneficiario bo = new BoBeneficiario();

            if (VerificaCPFRepetido(model))
            {
                //return Json(new { Result = "ERROR", Message = "CPF Já cadastrado para este cliente." });
                return Json("CPF Já cadastrado para este cliente.");
            }

            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }
            else
            {
                model.Id = bo.Incluir(new Beneficiario()
                {
                    Nome = model.Nome,
                    CPF = model.CPF,
                    IdCliente = model.IdCliente
                });

                return Json("Cadastro efetuado com sucesso");
            }
        }

        [HttpPost]
        public JsonResult BeneficiarioList(long idCliente, int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                int qtd = 0;
                string campo = string.Empty;
                string crescente = string.Empty;
                string[] array = { "Nome", "ASC" };

                if (array.Length > 0)
                    campo = array[0];

                if (array.Length > 1)
                    crescente = array[1];

                List<Beneficiario> beneficiarios = new BoBeneficiario().Pesquisa(idCliente, jtStartIndex, jtPageSize, campo, crescente.Equals("ASC", StringComparison.InvariantCultureIgnoreCase), out qtd);
               
                return Json(new { Result = "OK", Records = beneficiarios, TotalRecordCount = qtd });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult Alterar(BeneficiarioModel model)
        {
            BoBeneficiario bo = new BoBeneficiario();

            if (VerificaCPFRepetido(model))
            {
                //return Json(new { Result = "ERROR", Message = "CPF Já cadastrado para este cliente." });
                return Json("CPF Já cadastrado para este beneficiário.");
            }

            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }
            else
            {
                bo.Alterar(new Beneficiario()
                {
                    Id = model.Id,
                    IdCliente = model.IdCliente,
                    Nome = model.Nome,
                    CPF = model.CPF
                });

                return Json("Cadastro alterado com sucesso");
            }
        }

        //[HttpPost]
        //public ActionResult Alterar(long id)
        //{
        //    BoBeneficiario bo = new BoBeneficiario();
        //    Beneficiario beneficiario = bo.Consultar(id);
        //    BeneficiarioModel model = null;

        //    if (beneficiario != null)
        //    {
        //        model = new BeneficiarioModel()
        //        {
        //            Id = beneficiario.Id,
        //            IdCliente = beneficiario.IdCliente,
        //            Nome = beneficiario.Nome,
        //            CPF = beneficiario.CPF
        //        };
        //    }

        //    return View(model);
        //}

        [HttpPost]
        public JsonResult Excluir(long id)
        {
            BoBeneficiario bo = new BoBeneficiario();
            Beneficiario beneficiario = bo.Consultar(id);

            if (beneficiario == null) return Json("Beneficiário não encontrado.");
            
            bo.Excluir(id);

            return Json("Remoção efetuada com sucesso.");
        }

        private bool VerificaCPFRepetido(BeneficiarioModel model)
        {
            BoBeneficiario bo = new BoBeneficiario();

            int qtd = 0;
            List<Beneficiario> beneficiarios = new BoBeneficiario().Pesquisa(model.IdCliente, 0, 5, "Nome", "ASC".Equals("ASC", StringComparison.InvariantCultureIgnoreCase), out qtd);

            foreach (var benef in beneficiarios)
            {
                if (benef.CPF.Equals(model.CPF) && benef.Id != model.Id) return true;
            }

            BoCliente boc = new BoCliente();
            var cliente = boc.Consultar(model.IdCliente);

            if (model.CPF == cliente.CPF) return true;

            return false;
        }
    }
}