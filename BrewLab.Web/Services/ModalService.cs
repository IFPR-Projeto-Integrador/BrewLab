using BrewLab.Models.Models;
using BrewLab.Web.Components;
using BrewLab.Web.Pages.ExperimentalPlanning;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BrewLab.Web.Services;

public class ModalService(IDialogService dialogService)
{
    private readonly IDialogService _dialogService = dialogService;
    private bool _canEditDisplayDialog = false;
    public readonly DialogOptions DialogOptions = new();

    public ModalService Required()
    {
        this.DialogOptions.BackdropClick = false;
        this.DialogOptions.CloseButton = false;

        return this;
    }

    public ModalService Optional()
    {
        this.DialogOptions.BackdropClick = true;
        this.DialogOptions.CloseButton = true;

        return this;
    }

    public ModalService CanEdit()
    {
        this._canEditDisplayDialog = true;

        return this;
    }

    public ModalService ViewOnly()
    {
        this._canEditDisplayDialog = false;

        return this;
    }

    public ModalService ScreenSize()
    {
        this.DialogOptions.FullWidth = true;
        this.DialogOptions.MaxWidth = MaxWidth.Large;

        return this;
    }

    public async Task<string?> DisplayViewAsync(string toDisplay, string title)
    {
        var parameters = new DialogParameters<DisplayDialog>
        {
            { x => x.ToDisplay, toDisplay },
            { x => x.CanEdit, this._canEditDisplayDialog }
        };

        var dialog = await dialogService.ShowAsync<DisplayDialog>(title, parameters, this.DialogOptions);
        var result = await dialog.Result;

        if (result is not null && !result.Canceled)
            return result.Data as string ?? "";
        else
            return "";
    }


}
