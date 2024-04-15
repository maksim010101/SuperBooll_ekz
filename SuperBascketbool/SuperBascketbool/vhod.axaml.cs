using Avalonia.Controls;
using Avalonia.Interactivity;

namespace SuperBascketbool;

public partial class vhod : Window
{
    public vhod()
    {
        InitializeComponent();
        RoleCBox.Items.Add("Администратор");
        RoleCBox.Items.Add("Менеджер");
    }
    private void AuthBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        if (RoleCBox.SelectedItem != null)
        {
            if (RoleCBox.SelectedItem == "Администратор")
            {
                vhod mainAdminWindow = new vhod(1);
                mainAdminWindow.Show();
                this.Close();
            }
            else
            {
                vhod mainManagerWindow = new vhod(2);
                mainManagerWindow.Show();
                this.Close();
            }
        }
        else
        {
            var error = MessageBoxManager.GetMessageBoxStandard("Ошибка", "Выберите роль для входа", ButtonEnum.Ok);
            var result = error.ShowAsync();
        }
    }
}