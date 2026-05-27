using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CalculadoraPropinas.ViewModels;

public partial class CalculadoraViewModel : ObservableObject
{
    [ObservableProperty]
    private decimal _totalConsumo;

    [ObservableProperty]
    private int _numeroPersonas = 1;

    [ObservableProperty]
    private int _porcentajePropina = 10;

    [ObservableProperty]
    private decimal _subTotalPorPersona;

    [ObservableProperty]
    private decimal _propinaPorPersona;

    [ObservableProperty]
    private decimal _totalPorPersona;

    public CalculadoraViewModel()
    {
        CalcularValores();
    }

    partial void OnTotalConsumoChanged(decimal value) => CalcularValores();
    partial void OnNumeroPersonasChanged(int value) => CalcularValores();
    partial void OnPorcentajePropinaChanged(int value) => CalcularValores();

    [RelayCommand]
    private void SeleccionarPropina(string porcentaje)
    {
        if (int.TryParse(porcentaje, out int p))
        {
            PorcentajePropina = p;
        }
    }

    [RelayCommand]
    private void AumentarPersonas()
    {
        NumeroPersonas++;
    }

    [RelayCommand]
    private void DisminuirPersonas()
    {
        if (NumeroPersonas > 1) 
            NumeroPersonas--;
    }

    private void CalcularValores()
    {
        if (NumeroPersonas <= 0) return;

        SubTotalPorPersona = TotalConsumo / NumeroPersonas;
        PropinaPorPersona = (TotalConsumo * (PorcentajePropina / 100m)) / NumeroPersonas;
        TotalPorPersona = SubTotalPorPersona + PropinaPorPersona;
    }
}