using System.ComponentModel.DataAnnotations;

namespace Funcionario_AtvPds
{
        public class FuncionarioDTO
        {
            [Required]
            public string Nome { get; set; }
            [Required]
            public string CPF { get; set; }
            [Required]
            public string CTPS { get; set; }
            [Required]
            public string RG { get; set; }
            [Required]
            public string Funcao { get; set; }
            [Required]
            public string Setor { get; set; }
            [Required]
            public string Sala { get; set; }
            [Required]
            public string NumeroTelefone { get; set; }
            [Required]
            public string Endereco { get; set; }
            public string Uf { get; set; }
            public string Cidade { get; set; }
            public string Bairro { get; set; }
            public string Numero { get; set; }
            public string Cep { get; set; }

        }
}
