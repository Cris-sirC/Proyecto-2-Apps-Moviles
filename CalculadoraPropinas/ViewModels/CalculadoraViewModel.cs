using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CalculadoraPropinas.ViewModels;

// 1. Heredamos de ObservableObject y la clase DEBE ser "partial"
public partial class CalculadoraViewModel : ObservableObject
{
    // 2. Usamos [ObservableProperty] en variables privadas (minúsculas). 
    // El Toolkit automáticamente creará las propiedades públicas (Mayúsculas) por detrás.
    
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

    // 3. Estos métodos "On...Changed" los detecta el Toolkit automáticamente.
    // Se ejecutan solos cada vez que cambian las variables de arriba.
    partial void OnTotalConsumoChanged(decimal value) => CalcularValores();
    partial void OnNumeroPersonasChanged(int value) => CalcularValores();
    partial void OnPorcentajePropinaChanged(int value) => CalcularValores();

    // 4. Usamos [RelayCommand] para convertir métodos normales en Comandos
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

    // Método privado normal con la lógica de negocio
    private void CalcularValores()
    {
        if (NumeroPersonas <= 0) return;

        SubTotalPorPersona = TotalConsumo / NumeroPersonas;
        PropinaPorPersona = (TotalConsumo * (PorcentajePropina / 100m)) / NumeroPersonas;
        TotalPorPersona = SubTotalPorPersona + PropinaPorPersona;
    }
}