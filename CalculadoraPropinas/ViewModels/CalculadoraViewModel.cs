using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace CalculadoraPropinas.ViewModels
{
    public class CalculadoraViewModel : INotifyPropertyChanged
    {
        // Variables privadas
        private decimal _totalConsumo;
        private int _numeroPersonas = 1;
        private int _porcentajePropina = 10; // Inicia en 10%

        private decimal _subTotalPorPersona;
        private decimal _propinaPorPersona;
        private decimal _totalPorPersona;

        // Evento requerido por MVVM para avisar a la pantalla de los cambios
        public event PropertyChangedEventHandler? PropertyChanged;

        // Propiedades públicas (Las que se conectan con el XAML mediante {Binding})
        public decimal TotalConsumo
        {
            get => _totalConsumo;
            set { if (_totalConsumo != value) { _totalConsumo = value; OnPropertyChanged(); CalcularValores(); } }
        }

        public int NumeroPersonas
        {
            get => _numeroPersonas;
            set { if (_numeroPersonas != value) { _numeroPersonas = value; OnPropertyChanged(); CalcularValores(); } }
        }

        public int PorcentajePropina
        {
            get => _porcentajePropina;
            set { if (_porcentajePropina != value) { _porcentajePropina = value; OnPropertyChanged(); CalcularValores(); } }
        }

        // Resultados calculados
        public decimal SubTotalPorPersona
        {
            get => _subTotalPorPersona;
            private set { _subTotalPorPersona = value; OnPropertyChanged(); }
        }

        public decimal PropinaPorPersona
        {
            get => _propinaPorPersona;
            private set { _propinaPorPersona = value; OnPropertyChanged(); }
        }

        public decimal TotalPorPersona
        {
            get => _totalPorPersona;
            private set { _totalPorPersona = value; OnPropertyChanged(); }
        }

        // Comandos (Reemplazan al evento "Clicked" tradicional)
        public ICommand SeleccionarPropinaCommand { get; }
        public ICommand AumentarPersonasCommand { get; }
        public ICommand DisminuirPersonasCommand { get; }

        public CalculadoraViewModel()
        {
            // Inicialización de comandos
            SeleccionarPropinaCommand = new Command<string>(porcentaje =>
            {
                if (int.TryParse(porcentaje, out int p))
                {
                    PorcentajePropina = p; // Esto actualiza el Slider automáticamente
                }
            });

            AumentarPersonasCommand = new Command(() => NumeroPersonas++);
            DisminuirPersonasCommand = new Command(() => { if (NumeroPersonas > 1) NumeroPersonas--; });

            CalcularValores(); // Cálculo inicial
        }

        // Lógica de negocio solicitada en la rúbrica
        private void CalcularValores()
        {
            if (NumeroPersonas <= 0) return;

            CalcularSubTotal();
            CalcularPropina();
            CalcularTotal();
        }

        private void CalcularSubTotal()
        {
            SubTotalPorPersona = TotalConsumo / NumeroPersonas;
        }

        private void CalcularPropina()
        {
            decimal propinaTotal = TotalConsumo * (PorcentajePropina / 100m);
            PropinaPorPersona = propinaTotal / NumeroPersonas;
        }

        private void CalcularTotal()
        {
            TotalPorPersona = SubTotalPorPersona + PropinaPorPersona;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}