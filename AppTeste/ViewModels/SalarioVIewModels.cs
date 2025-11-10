using System;
using System.Globalization;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using AppTeste.Models;

namespace AppTeste.ViewModels
{
    public class SalarioViewModel : ObservableObject
    {
        private readonly Salario _salario = new Salario();
        private decimal _salarioCalculado;

        public IRelayCommand CalcularSalarioCommand { get; }

        public SalarioViewModel()
        {
            CalcularSalarioCommand = new RelayCommand(
                execute: () => CalcularSalario(),
                canExecute: () => PodeCalcular()
            );

            ValorHoraString = string.Empty;
            HorasTrabalhadasString = string.Empty;
        }

        private string _valorHoraString;
        public string ValorHoraString
        {
            get => _valorHoraString;
            set
            {
                if (SetProperty(ref _valorHoraString, value))
                {
                    if (TryParseDecimal(value, out var parsed))
                        _salario.ValorHora = parsed;
                    else
                        _salario.ValorHora = 0m;

                    CalcularSalarioCommand.NotifyCanExecuteChanged();
                }
            }
        }

        private string _horasTrabalhadasString;
        public string HorasTrabalhadasString
        {
            get => _horasTrabalhadasString;
            set
            {
                if (SetProperty(ref _horasTrabalhadasString, value))
                {
                    if (TryParseDecimal(value, out var parsed))
                        _salario.HorasTrabalhadas = parsed;
                    else
                        _salario.HorasTrabalhadas = 0m;

                    CalcularSalarioCommand.NotifyCanExecuteChanged();
                }
            }
        }

        public decimal SalarioCalculado
        {
            get => _salarioCalculado;
            private set => SetProperty(ref _salarioCalculado, value);
        }

        private bool PodeCalcular()
        {
            return _salario.ValorHora > 0m && _salario.HorasTrabalhadas > 0m;
        }

        private void CalcularSalario()
        {
            SalarioCalculado = _salario.CalcularSalarioLiquido();
        }

        private bool TryParseDecimal(string s, out decimal result)
        {
            result = 0m;
            if (string.IsNullOrWhiteSpace(s)) return false;

            return decimal.TryParse(s, NumberStyles.Number, CultureInfo.CurrentCulture, out result)
                || decimal.TryParse(s, NumberStyles.Number, CultureInfo.InvariantCulture, out result);
        }
    }
}
