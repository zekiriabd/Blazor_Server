using DapperBlazorApp.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Radzen.Blazor;
using System.Reflection;
using System.Resources;

namespace DapperBlazorApp.Pages.Components
{
    
    public class RzDataGrid<TItem> : RadzenDataGrid<TItem>
    {
        //[Inject] private IStringLocalizer<Resource> _StringLocalizer { get; set; }
        public RzDataGrid() : base()
        {
            var stringLocalizer = new ResourceManager("DapperBlazorApp.Resources.Resource", Assembly.GetExecutingAssembly());

            base.AndOperatorText = "Y";
            base.EqualsText = "Igual a";
            base.NotEqualsText = "No es igual a";
            base.LessThanText = "Menor qué";
            base.LessThanOrEqualsText = "Menor que o igual";
            base.GreaterThanText = "Mayor qué";
            base.GreaterThanOrEqualsText = "Mayor que o Igual";
            base.IsNullText = "Es nulo";
            base.IsNotNullText = "No es nulo";
            base.AndOperatorText = "Y";
            base.OrOperatorText = "O";
            base.ContainsText = "Contiene";
            base.DoesNotContainText = "No Contiene";
            base.StartsWithText = "Inicia Con";
            base.EndsWithText = "Termina Con";
            base.ClearFilterText = "Limpiar";
            base.ApplyFilterText = stringLocalizer.GetString("ApplyFilterText");
            base.FilterText = "Filter";
        }
    }
}
