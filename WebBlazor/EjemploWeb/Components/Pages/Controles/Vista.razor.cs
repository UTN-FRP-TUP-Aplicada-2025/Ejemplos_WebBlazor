namespace EjemploWeb.Components.Pages.Controles
{
    public partial class Vista
    {
        List<ItemViewModel> ListaItems = new List<ItemViewModel>() {
            new ItemViewModel{Nombre="fernando"},
            new ItemViewModel{Nombre="rafael"}
        };

        string filtro;
        string detalle;

        protected override Task OnInitializedAsync()
        {
            //
            StateHasChanged();
            return Task.CompletedTask;
        }

        protected override void OnParametersSet()
        {
            
        }

        async Task OnVer(ItemViewModel item)
        {
            detalle = item.Nombre;
          //  return Task.CompletedTask;

        }

        async Task OnCambioFiltro()
        {
           // Task.CompletedTask;
        }
    }

    class ItemViewModel
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public int Cantidad { get; set; }
    }
}