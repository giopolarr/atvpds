namespace Funcionario_AtvPds.ajuda
{
        public class CpfValidator
        {
            public static bool ValidaCpf(string cpf)
            {
                cpf = cpf.Replace(".", "").Replace("-", "");

                if (cpf.Length != 11)
                {
                    return false;
                }

                int[] m1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                int[] m2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };


                int soma = 0;
                int resto = 0;

                for (int i = 0; i < cpf.Length - 2; i++)
                {
                    soma += int.Parse(cpf[i].ToString()) * m1[i];
                }

                resto = soma % 11;

                if (resto < 2)
                {
                    resto = 0;
                }
                if (resto >= 2)
                {
                    resto = 11 - resto;
                }
                int soma2 = 0;
                int resto2 = 0;

                for (int i = 0; i < cpf.Length - 1; i++)
                {
                    soma2 += int.Parse(cpf[i].ToString()) * m2[i];
                }

                resto2 = soma2 % 11;

                if (resto2 < 2)
                {
                    resto2 = 0;
                }
                if (resto2 >= 2)
                {
                    resto2 = 11 - resto2;
                }
                if (resto == int.Parse(cpf[9].ToString()) && resto2 == int.Parse(cpf[10].ToString()))
                {
                    return true;
                }
                else
                {
                    return false;
                }


            }
        }
 }

