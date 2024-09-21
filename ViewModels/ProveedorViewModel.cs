using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Runtime.CompilerServices;

public class ProveedorViewModel : INotifyPropertyChanged
{
    public ObservableCollection<Proveedor> Proveedores { get; set; } = new ObservableCollection<Proveedor>();

    private Proveedor _nuevoProveedor = new Proveedor();
    public Proveedor NuevoProveedor
    {
        get => _nuevoProveedor;
        set
        {
            _nuevoProveedor = value;
            OnPropertyChanged();
        }
    }

    public ICommand AgregarProveedorCommand { get; }

    public ProveedorViewModel()
    {
        AgregarProveedorCommand = new Command(AgregarProveedor);
    }

    private void AgregarProveedor()
    {
        // Validaciones de los campos
        if (string.IsNullOrWhiteSpace(NuevoProveedor.Nombre) || string.IsNullOrWhiteSpace(NuevoProveedor.Direccion) ||
            string.IsNullOrWhiteSpace(NuevoProveedor.Telefono) || string.IsNullOrWhiteSpace(NuevoProveedor.Correo) ||
            string.IsNullOrWhiteSpace(NuevoProveedor.ProductoAsociado))
        {
            // Mostrar alerta si hay campos vacíos
            Application.Current.MainPage.DisplayAlert("Error", "Todos los campos son obligatorios.", "OK");
            return;
        }

        // Agregar el nuevo proveedor a la colección
        Proveedores.Add(new Proveedor
        {
            Id = Proveedores.Count + 1,  // Asignar un ID único
            Nombre = NuevoProveedor.Nombre,
            Direccion = NuevoProveedor.Direccion,
            Telefono = NuevoProveedor.Telefono,
            Correo = NuevoProveedor.Correo,
            ProductoAsociado = NuevoProveedor.ProductoAsociado
        });

        // Limpiar campos después de agregar
        NuevoProveedor = new Proveedor();
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

