using System;

namespace AppTeste.Models
{
    public class Salario
    {
        public decimal ValorHora { get; set; }
        public decimal HorasTrabalhadas { get; set; }

        // Cálculo do salário base
        public decimal CalcularSalario()
        {
            return ValorHora * HorasTrabalhadas;
        }

        // Cálculo do INSS (progressivo)
        public decimal CalcularINSS()
        {
            decimal salarioBruto = CalcularSalario();
            decimal inss = 0;

            if (salarioBruto <= 1412.00m)
            {
                inss = salarioBruto * 0.075m;
            }
            else if (salarioBruto <= 2666.68m)
            {
                inss = (1412.00m * 0.075m) +
                       ((salarioBruto - 1412.00m) * 0.09m);
            }
            else if (salarioBruto <= 4000.03m)
            {
                inss = (1412.00m * 0.075m) +
                       ((2666.68m - 1412.00m) * 0.09m) +
                       ((salarioBruto - 2666.68m) * 0.12m);
            }
            else if (salarioBruto <= 7786.02m)
            {
                inss = (1412.00m * 0.075m) +
                       ((2666.68m - 1412.00m) * 0.09m) +
                       ((4000.03m - 2666.68m) * 0.12m) +
                       ((salarioBruto - 4000.03m) * 0.14m);
            }
            else
            {
                // Teto máximo de desconto
                inss = (1412.00m * 0.075m) +
                       ((2666.68m - 1412.00m) * 0.09m) +
                       ((4000.03m - 2666.68m) * 0.12m) +
                       ((7786.02m - 4000.03m) * 0.14m);
            }

            return inss;
        }

        public decimal CalcularSalarioLiquido()
        {
            return CalcularSalario() - CalcularINSS();
        }
    }
}
