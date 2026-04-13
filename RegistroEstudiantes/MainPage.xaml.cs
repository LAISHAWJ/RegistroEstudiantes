namespace RegistroEstudiantes;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    // Botón: Seleccionar Fotografía
    private async void OnSeleccionarFotoClicked(object sender, EventArgs e)
    {
        try
        {
            var foto = await MediaPicker.Default.PickPhotoAsync();

            if (foto != null)
            {
                var stream = await foto.OpenReadAsync();
                FotoEstudiante.Source = ImageSource.FromStream(() => stream);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "No se pudo cargar la foto: " + ex.Message, "OK");
        }
    }

    // Botón: Guardar
    private async void OnGuardarClicked(object sender, EventArgs e)
    {
        // Validar campos vacíos
        if (string.IsNullOrWhiteSpace(EntryMatricula.Text) ||
            string.IsNullOrWhiteSpace(EntryNombre.Text) ||
            string.IsNullOrWhiteSpace(EntryApellidos.Text) ||
            PickerSexo.SelectedIndex == -1)
        {
            await DisplayAlert("⚠️ Campos incompletos",
                               "Por favor, llena todos los campos.", "Entendido");
            return;
        }

        // Mostrar datos guardados
        LabelResultado.Text =
            $"🎓 Matrícula: {EntryMatricula.Text}\n" +
            $"👤 Nombre: {EntryNombre.Text} {EntryApellidos.Text}\n" +
            $"⚧ Sexo: {PickerSexo.SelectedItem}";

        FrameResultado.IsVisible = true;

        await DisplayAlert("Éxito", "Datos guardados correctamente.", "OK");
    }

    // Botón: Limpiar
    private void OnLimpiarClicked(object sender, EventArgs e)
    {
        EntryMatricula.Text = string.Empty;
        EntryNombre.Text = string.Empty;
        EntryApellidos.Text = string.Empty;
        PickerSexo.SelectedIndex = -1;
        FotoEstudiante.Source = "student_placeholder.png";
        FrameResultado.IsVisible = false;
    }
}