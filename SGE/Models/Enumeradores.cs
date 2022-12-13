using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace SGE.Models
{
    public enum Status
    {
        Inativo, Ativo
    }

    public enum TipoPessoa
    {
        Fisica, Juridica
    }
    public enum TipoEntidade
    {
        Cliente, Fornecedor
    }

    public enum TipoMovimentacao
    {
        Entrada , Saida
    }

    public enum TipoProposta
    {
        Proposta, Cliente
    }

    public enum MeioPagamento
    {
        Geral,
        Dinheiro,
        Boleto,
        [Description("Cartao de Débito")]
        CartaoDeDebito,
        CartaoDeCredito,
        Cheque,
        TransferenciaBancaria,
        PontosCartaoFidelidade
    }

    public enum SituacaoOrcamento
    {
        Novo,
        Orcado,
        Confirmado,
        [Description("Cartao de Débito")]
        NaoConfirmado,
        Substituido,
        Cancelado
    }

    public enum SituacaoEvento
    {
        Aprovado,
        Concluido,
        Cancelado
    }

    public enum Cargo
    {
        Adminisrtador,
        Financeiro,
        Gerente,
        Motorista,
        Operacional,
        Secretária,
        Supervisor,
        Técnico
    }
    public enum EstadoCivil
    {
        Solteiro,
        Casado,
        Separado,
        Divorciado,
        Viúvo
    }

   
}
