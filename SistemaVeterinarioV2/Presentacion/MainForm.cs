using System.Data;
using SistemaVeterinarioV2.Dominio;

namespace SistemaVeterinarioV2;

internal sealed class MainForm : Form
{
    private readonly VeterinaryRepository _repository;
    private readonly DataGridView _petsGrid = CreateGrid();
    private readonly DataGridView _visitsGrid = CreateGrid();

    private readonly TextBox _petCode = new();
    private readonly TextBox _petName = new();
    private readonly TextBox _petSpecies = new();
    private readonly TextBox _petOwner = new();
    private readonly ComboBox _visitPet = new();
    private readonly TextBox _visitService = new();
    private readonly NumericUpDown _visitWeight = Numeric(0.1m, 500m, 1, 18.5m);
    private readonly NumericUpDown _visitTemperature = Numeric(35m, 43m, 1, 38.4m);
    private readonly NumericUpDown _visitCost = Numeric(0.01m, 100000m, 2, 125m);
    private readonly DateTimePicker _visitDate = new() { Format = DateTimePickerFormat.Custom, CustomFormat = "dd/MM/yyyy HH:mm", Width = 160 };
    private int? _selectedPetId;
    private int? _selectedVisitId;

    public MainForm(VeterinaryRepository repository)
    {
        _repository = repository;
        Text = "Sistema de Control Clinico Veterinario - Version 2";
        Width = 1100;
        Height = 720;
        StartPosition = FormStartPosition.CenterScreen;
        MinimumSize = new Size(900, 600);

        var tabs = new TabControl { Dock = DockStyle.Fill };
        tabs.TabPages.Add(BuildPetsTab());
        tabs.TabPages.Add(BuildVisitsTab());
        Controls.Add(tabs);

        RefreshAll();
    }

    private TabPage BuildPetsTab()
    {
        var page = new TabPage("Mascotas");
        var layout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2, Padding = new Padding(12) };
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 150));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        var editor = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoScroll = true, WrapContents = true };
        AddField(editor, "Codigo", _petCode);
        AddField(editor, "Nombre", _petName);
        AddField(editor, "Especie", _petSpecies);
        AddField(editor, "Propietario", _petOwner);
        editor.Controls.Add(Button("Guardar", SavePet));
        editor.Controls.Add(Button("Actualizar", UpdatePet));
        editor.Controls.Add(Button("Eliminar", DeletePet));
        editor.Controls.Add(Button("Limpiar", (_, _) => ClearPetForm()));
        layout.Controls.Add(editor, 0, 0);

        _petsGrid.SelectionChanged += (_, _) => SelectPet();
        layout.Controls.Add(_petsGrid, 0, 1);
        page.Controls.Add(layout);
        return page;
    }

    private TabPage BuildVisitsTab()
    {
        var page = new TabPage("Consultas");
        var layout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 1, RowCount = 2, Padding = new Padding(12) };
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 185));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        var editor = new FlowLayoutPanel { Dock = DockStyle.Fill, AutoScroll = true, WrapContents = true };
        _visitPet.DropDownStyle = ComboBoxStyle.DropDownList;
        _visitPet.Width = 210;
        AddField(editor, "Mascota", _visitPet);
        AddField(editor, "Servicio", _visitService);
        AddField(editor, "Peso (kg)", _visitWeight);
        AddField(editor, "Temperatura (C)", _visitTemperature);
        AddField(editor, "Costo (Q)", _visitCost);
        AddField(editor, "Fecha", _visitDate);
        editor.Controls.Add(Button("Guardar", SaveVisit));
        editor.Controls.Add(Button("Actualizar", UpdateVisit));
        editor.Controls.Add(Button("Eliminar", DeleteVisit));
        editor.Controls.Add(Button("Limpiar", (_, _) => ClearVisitForm()));
        layout.Controls.Add(editor, 0, 0);

        _visitsGrid.SelectionChanged += (_, _) => SelectVisit();
        layout.Controls.Add(_visitsGrid, 0, 1);
        page.Controls.Add(layout);
        return page;
    }

    private void SavePet(object sender, EventArgs e) => RunAction(() =>
    {
        _repository.CrearMascota(ReadPet());
        RefreshAll();
        ClearPetForm();
    }, "Mascota guardada.");

    private void UpdatePet(object sender, EventArgs e) => RunAction(() =>
    {
        if (_selectedPetId is null) throw new InvalidOperationException("Seleccione una mascota para actualizar.");
        _repository.ActualizarMascota(_selectedPetId.Value, ReadPet());
        RefreshAll();
        ClearPetForm();
    }, "Mascota actualizada.");

    private void DeletePet(object sender, EventArgs e) => RunAction(() =>
    {
        if (_selectedPetId is null) throw new InvalidOperationException("Seleccione una mascota para eliminar.");
        if (MessageBox.Show("Al eliminar la mascota tambien se eliminaran sus consultas. ¿Continuar?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            return;
        _repository.EliminarMascota(_selectedPetId.Value);
        RefreshAll();
        ClearPetForm();
    }, "Mascota eliminada.");

    private void SaveVisit(object sender, EventArgs e) => RunAction(() =>
    {
        _repository.CrearConsulta(ReadVisit());
        RefreshAll();
        ClearVisitForm();
    }, "Consulta guardada.");

    private void UpdateVisit(object sender, EventArgs e) => RunAction(() =>
    {
        if (_selectedVisitId is null) throw new InvalidOperationException("Seleccione una consulta para actualizar.");
        _repository.ActualizarConsulta(_selectedVisitId.Value, ReadVisit());
        RefreshAll();
        ClearVisitForm();
    }, "Consulta actualizada.");

    private void DeleteVisit(object sender, EventArgs e) => RunAction(() =>
    {
        if (_selectedVisitId is null) throw new InvalidOperationException("Seleccione una consulta para eliminar.");
        if (MessageBox.Show("¿Eliminar la consulta seleccionada?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            return;
        _repository.EliminarConsulta(_selectedVisitId.Value);
        RefreshAll();
        ClearVisitForm();
    }, "Consulta eliminada.");

    private Mascota ReadPet()
    {
        var code = Required(_petCode.Text, "El codigo de la mascota es obligatorio.");
        var name = Required(_petName.Text, "El nombre de la mascota es obligatorio.");
        var species = Required(_petSpecies.Text, "La especie es obligatoria.");
        var owner = Required(_petOwner.Text, "El propietario es obligatorio.");
        return new Mascota(code, name, species, owner);
    }

    private ConsultaVeterinaria ReadVisit()
    {
        if (_visitPet.SelectedValue is null)
            throw new ArgumentException("Registre y seleccione una mascota antes de guardar la consulta.");
        return new ConsultaVeterinaria(
            Convert.ToInt32(_visitPet.SelectedValue),
            _visitService.Text,
            (double)_visitWeight.Value,
            (double)_visitTemperature.Value,
            _visitCost.Value,
            _visitDate.Value);
    }

    private void RefreshAll()
    {
        _petsGrid.DataSource = _repository.ObtenerMascotas();
        _visitsGrid.DataSource = _repository.ObtenerConsultas();
        _visitPet.DataSource = _repository.ObtenerMascotasParaSeleccion();
        _visitPet.DisplayMember = "Mascota";
        _visitPet.ValueMember = "Id";
        ConfigureGrid(_petsGrid, "Id");
        ConfigureGrid(_visitsGrid, "Id", "MascotaId");
        if (_visitPet.Items.Count == 0) _visitPet.SelectedIndex = -1;
    }

    private void SelectPet()
    {
        if (_petsGrid.CurrentRow?.DataBoundItem is not DataRowView row) return;
        _selectedPetId = Convert.ToInt32(row["Id"]);
        _petCode.Text = Convert.ToString(row["Codigo"]);
        _petName.Text = Convert.ToString(row["Nombre"]);
        _petSpecies.Text = Convert.ToString(row["Especie"]);
        _petOwner.Text = Convert.ToString(row["Propietario"]);
    }

    private void SelectVisit()
    {
        if (_visitsGrid.CurrentRow?.DataBoundItem is not DataRowView row) return;
        _selectedVisitId = Convert.ToInt32(row["Id"]);
        _visitPet.SelectedValue = Convert.ToInt32(row["MascotaId"]);
        _visitService.Text = Convert.ToString(row["Servicio"]);
        _visitWeight.Value = Clamp(Convert.ToDecimal(row["Peso"]), _visitWeight.Minimum, _visitWeight.Maximum);
        _visitTemperature.Value = Clamp(Convert.ToDecimal(row["Temperatura"]), _visitTemperature.Minimum, _visitTemperature.Maximum);
        _visitCost.Value = Clamp(Convert.ToDecimal(row["Costo"]), _visitCost.Minimum, _visitCost.Maximum);
        if (DateTime.TryParse(Convert.ToString(row["Fecha"]), out var date)) _visitDate.Value = date;
    }

    private void ClearPetForm()
    {
        _selectedPetId = null;
        _petCode.Clear(); _petName.Clear(); _petSpecies.Clear(); _petOwner.Clear();
        _petsGrid.ClearSelection();
    }

    private void ClearVisitForm()
    {
        _selectedVisitId = null;
        _visitService.Clear();
        _visitWeight.Value = 18.5m;
        _visitTemperature.Value = 38.4m;
        _visitCost.Value = 125m;
        _visitDate.Value = DateTime.Now;
        _visitsGrid.ClearSelection();
    }

    private void RunAction(Action action, string successMessage)
    {
        try
        {
            action();
            MessageBox.Show(successMessage, Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception error)
        {
            MessageBox.Show(error.Message, "No se pudo completar la operacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private static string Required(string value, string message) =>
        string.IsNullOrWhiteSpace(value) ? throw new ArgumentException(message) : value.Trim();

    private static decimal Clamp(decimal value, decimal min, decimal max) => Math.Min(max, Math.Max(min, value));

    private static DataGridView CreateGrid() => new()
    {
        Dock = DockStyle.Fill,
        ReadOnly = true,
        AllowUserToAddRows = false,
        AllowUserToDeleteRows = false,
        AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
        MultiSelect = false,
        SelectionMode = DataGridViewSelectionMode.FullRowSelect,
        RowHeadersVisible = false
    };

    private static void ConfigureGrid(DataGridView grid, params string[] hiddenColumns)
    {
        foreach (var column in hiddenColumns)
            if (grid.Columns.Contains(column)) grid.Columns[column].Visible = false;
        grid.ClearSelection();
    }

    private static NumericUpDown Numeric(decimal min, decimal max, int decimals, decimal value) => new()
    {
        Minimum = min,
        Maximum = max,
        DecimalPlaces = decimals,
        Value = value,
        Width = 120,
        Increment = decimals == 0 ? 1 : 0.1m
    };

    private static void AddField(FlowLayoutPanel panel, string label, Control control)
    {
        var container = new Panel { Width = control.Width + 12, Height = 62, Margin = new Padding(4) };
        container.Controls.Add(new Label { Text = label, Dock = DockStyle.Top, Height = 22 });
        control.Location = new Point(0, 25);
        control.Width = Math.Max(control.Width, 150);
        container.Controls.Add(control);
        panel.Controls.Add(container);
    }

    private static Button Button(string text, EventHandler click)
    {
        var button = new Button { Text = text, Width = 110, Height = 32, Margin = new Padding(6, 28, 6, 0) };
        button.Click += click;
        return button;
    }
}
