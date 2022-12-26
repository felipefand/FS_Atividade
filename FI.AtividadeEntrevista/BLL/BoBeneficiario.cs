using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoBeneficiario
    {
        /// <summary>
        /// Inclui um novo cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        public long Incluir(DML.Beneficiario beneficiario)
        {
            DAL.DaoBeneficiario benef = new DAL.DaoBeneficiario();
            return benef.Incluir(beneficiario);
        }

        public List<DML.Beneficiario> Pesquisa(long idCliente, int iniciarEm, int quantidade, string campoOrdenacao, bool crescente, out int qtd)
        {
            DAL.DaoBeneficiario benef = new DAL.DaoBeneficiario();
            return benef.Pesquisa(idCliente, iniciarEm, quantidade, campoOrdenacao, crescente, out qtd);
        }
    }
}
