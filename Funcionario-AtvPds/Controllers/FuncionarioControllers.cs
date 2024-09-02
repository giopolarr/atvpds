using Funcionario_AtvPds.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace Funcionario_AtvPds.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {
        private const string FuncionarioRepositorio = "funcionarios.txt";

        private bool ValidarCpf(string cpf)
        {
            cpf = Regex.Replace(cpf, @"\D", "");
            return cpf.Length == 11;
        }

        private List<Funcionario> ListarFuncionarios()
        {
            var funcionarios = new List<Funcionario>();

            if (System.IO.File.Exists(FuncionarioRepositorio))
            {
                var linhas = System.IO.File.ReadAllLines(FuncionarioRepositorio);
                foreach (var linha in linhas)
                {
                    var dados = linha.Split('|');
                    if (dados.Length == 13)
                    {
                        funcionarios.Add(new Funcionario
                        {
                            Nome = dados[0],
                            CPF = dados[1],
                            CTPS = dados[2],
                            RG = dados[3],
                            Funcao = dados[4],
                            Setor = dados[5],
                            Sala = dados[6],
                            NumeroTelefone = dados[7],
                            UF = dados[8],
                            Cidade = dados[9],
                            Bairro = dados[10],
                            Numero = dados[11],
                            CEP = dados[12]

                        });
                    }
                    else
                    {
                        Console.WriteLine("Organize conforme as linhas: " + linha);
                    }
                }
            }
            else
            {
                Console.WriteLine("Arquivo não encontrado: " + FuncionarioRepositorio);
            }

            return funcionarios;
        }

        private void SalvarFuncionarios(List<Funcionario> funcionarios)
        {
            var linhas = funcionarios.Select(f => $"{f.Nome}|{f.CPF}|{f.CTPS}|{f.RG}|{f.Funcao}|{f.Setor}|{f.Sala}|{f.NumeroTelefone}|{f.UF}|{f.Cidade}|{f.Bairro}|{f.Numero}|{f.CEP}");
            System.IO.File.WriteAllLines(FuncionarioRepositorio, linhas);
        }

        [HttpGet]
        public IActionResult ObterTodos()
        {
            var funcionarios = ListarFuncionarios();
            return Ok(funcionarios);
        }

        [HttpGet("{cpf}")]
        public IActionResult GetByCPF(string cpf)
        {
            if (!ValidarCpf(cpf))
            {
                return BadRequest("CPF inválido.");
            }

            var funcionarios = ListarFuncionarios();
            var funcionario = funcionarios.FirstOrDefault(f => f.CPF == cpf);

            if (funcionario == null)
            {
                return NotFound();
            }

            if (funcionarios.Any(f => f.CPF == funcionario.CPF))
            {
                return Conflict("Já existe um funcionário com este CPF.");
            }

            return Ok(funcionario);
        }
        [HttpPost]
        public IActionResult NovoFuncionario([FromBody] FuncionarioDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Os dados fornecidos são inválidos.");
            }

            if (!ValidarCpf(dto.CPF))
            {
                return BadRequest("O CPF fornecido é inválido.");
            }

            var funcionarios = ListarFuncionarios();
            if (funcionarios.Any(f => f.CPF == dto.CPF))
            {
                return Conflict("Já existe um funcionário com este CPF.");
            }

            var funcionario = new Funcionario
            {
                Nome = dto.Nome,
                CPF = dto.CPF,
                CTPS = dto.CTPS,
                RG = dto.RG,
                Funcao = dto.Funcao,
                Setor = dto.Setor,
                Sala = dto.Sala,
                NumeroTelefone = dto.NumeroTelefone,
            };

            funcionarios.Add(funcionario);
            SalvarFuncionarios(funcionarios);

            return CreatedAtAction(nameof(ValidarCpf), new { cpf = funcionario.CPF }, funcionario);
        }
        [HttpDelete("{cpf}")]
        public IActionResult Deletar(string cpf)
        {
            if (!ValidarCpf(cpf))
            {
                return BadRequest("CPF inválido.");
            }

            var funcionarios = ListarFuncionarios();
            var funcionario = funcionarios.FirstOrDefault(f => f.CPF == cpf);

            if (funcionario == null)
            {
                return NotFound();
            }

            funcionarios.Remove(funcionario);
            SalvarFuncionarios(funcionarios);

            return Ok(funcionario);
        }

        [HttpPut("{cpf}")]
        public IActionResult AtualizarFuncionario(string cpf, [FromBody] FuncionarioDTO funcionarioAtualizado)
        {
            if (funcionarioAtualizado == null)
            {
                return BadRequest("Os dados fornecidos são inválidos.");
            }

            if (!ValidarCpf(funcionarioAtualizado.CPF))
            {
                return BadRequest("O CPF fornecido é inválido.");
            }

            var funcionarios = ListarFuncionarios();
            if (funcionarios.Any(f => f.CPF == funcionarioAtualizado.CPF))
            {
                return Conflict("Já existe um funcionário com este CPF.");
            }

             SalvarFuncionarios(funcionarios);
            return Ok(funcionarios);
            
        }
        [HttpDelete("{cpf}")]
        public IActionResult ExcluirFuncionario(string cpf)
        {

            if (ValidarCpf(cpf))
            {
                return BadRequest("CPF inválido.");
            }

            var funcionarios = ListarFuncionarios();
            var funcionario = funcionarios.FirstOrDefault(f => f.CPF == cpf);

            if (funcionario == null)
            {
                return NotFound();
            }

            funcionarios.Remove(funcionario);
            SalvarFuncionarios(funcionarios);

            return Ok(funcionario);

        }
    }
}   
        
    

