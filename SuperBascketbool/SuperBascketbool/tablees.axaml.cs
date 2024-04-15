using System;
using System.Collections.ObjectModel;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MsBox.Avalonia.Enums;
using MySql.Data.MySqlClient;
using static MsBox.Avalonia.MessageBoxManager;

namespace SuperBascketbool;

public partial class tablees : Window
{
private int _role;
    private  _database = new Database();
    private ObservableCollection<Player> _players = new ObservableCollection<Player>();
    private ObservableCollection<Position> _positions = new ObservableCollection<Position>();

    private string sql =
        "select player_id, player_name, position_name, weight, height, birthday, game_start, team_name from player " +
        "join position on position_id = position " +
        "join team on team_id = team";
    public tablees(int role)
    {
        InitializeComponent();
        _role = role;
        FillPositionCBox();
        ShowTable(sql);
        if (_role == 2)
        {
            DelPlayerBtn.IsVisible = false;
        }
    }

    private void ShowTable(string sql)
    {
        try
        {
            _database.OpenConnection();
            MySqlCommand command = new MySqlCommand(sql, _database.GetConnection());
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var currentPlayer = new Player()
                {
                    PlayerId = reader.GetInt32("player_id"),
                    PlayerName = reader.GetString("player_name"),
                    Position = reader.GetString("position_name"),
                    Weight = reader.GetDouble("weight"),
                    Height = reader.GetDouble("height"),
                    Birthday = reader.GetDateTime("birthday"),
                    GameStart = reader.GetDateTime("game_start"),
                    Team = reader.GetString("team_name")
                };
                _players.Add(currentPlayer);
            }
            _database.CloseConnection();
            PlayerDGrid.ItemsSource = _players;
        }
        catch (Exception e)
        {
            var error = GetMessageBoxStandard("Ошибка", "Ошибка" + e, ButtonEnum.Ok);
            var result = error.ShowAsync();
        }
    }

    private async void DelPlayerBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        Player selectedPlayer = PlayerDGrid.SelectedItem as Player;
        if (selectedPlayer != null)
        {
            var warning = GetMessageBoxStandard("Предупреждение",
                "Вы уверены что хотите удалить выбранный элемент?", ButtonEnum.YesNo,
                MsBox.Avalonia.Enums.Icon.Question);
            var result = await warning.ShowAsync();
            if (result == ButtonResult.Yes)
            {
                try
                {
                    _database.OpenConnection();
                    string sql = "delete from player where player_id = " + selectedPlayer.PlayerId;
                    MySqlCommand command = new MySqlCommand(sql, _database.GetConnection());
                    command.ExecuteNonQuery();
                    _database.CloseConnection();
                    var success = GetMessageBoxStandard("Успешно", "Элемент успешно удален",
                        ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Success);
                    var resultSuc = success.ShowAsync();
                }
                catch (Exception exception)
                {
                    var err = GetMessageBoxStandard("Ошибка", "Ошибка " + exception, ButtonEnum.Ok);
                    var resErr = err.ShowAsync();
                }
            }
            else
            {
                var cancel =
                    GetMessageBoxStandard("Отмена", "Операция удаления отменена", ButtonEnum.Ok);
                var resultCancel = cancel.ShowAsync();
            }
        }
        else
        {
            var error = GetMessageBoxStandard("Ошибка", "Выберите элемент для удаления",
                ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Error);
            var result = error.ShowAsync();
        }
    }

    private void SearchTBox_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        ObservableCollection<Player> _search =
            new ObservableCollection<Player>(_players.Where(x => x.PlayerName.ToLower().Contains(SearchTBox.Text.ToLower())));
        PlayerDGrid.ItemsSource = _search;
    }
    
    private void FillPositionCBox()
    {
        try
        {
            _database.OpenConnection();
            string sql = "select position_name from position";
            MySqlCommand command = new MySqlCommand(sql, _database.GetConnection());
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var currentPosition = new Position()
                {
                    PositionName = reader.GetString("position_name")
                };
                _positions.Add(currentPosition);
            }
            _database.CloseConnection();
            _positions.Insert(0, new Position{PositionName = "Все позиции"});
            FilterCBox.ItemsSource = _positions;
        }
        catch (Exception e)
        {
            var error = GetMessageBoxStandard("Ошибка", "Ошибка" + e, ButtonEnum.Ok);
            var result = error.ShowAsync();
        }
    }

    private void FilterCBox_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        Position selectedPosition = FilterCBox.SelectedItem as Position;

        if (selectedPosition.PositionName == "Все позиции")
        {
            PlayerDGrid.ItemsSource = _players;
        }
        else
        {
            ObservableCollection<Player> filterResults = new ObservableCollection<Player>(_players.Where(x => x.Position.Contains(selectedPosition.PositionName)));
            PlayerDGrid.ItemsSource = filterResults;
        }
    }

    private void DelPlayerBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }
}