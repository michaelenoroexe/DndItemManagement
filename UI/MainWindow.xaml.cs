using DataAccess;
using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IDataAccessor _accessor;
        private IList<Item> _items;

        public MainWindow()
        {
            InitializeComponent();

            _accessor = DataProviderFactory.GetDataAccessor();
            _items = _accessor.GetItems();
        }

        private void AddNew(object sender, RoutedEventArgs e)
        {
            _items.Add(new Item { ItemId = TextBox_NewItem.Text.GetHashCode(), Name = TextBox_NewItem.Text, Number = int.Parse(TextBox_ItemOwned.Text)});
        }

        private void LoadItems(object sender, RoutedEventArgs e)
        {
            ItemsStorage.Items.Clear();
            ItemsStorage.Items.Add("| ID |  Name  |  Number ");
            foreach (var item in _items)
            {
                ItemsStorage.Items.Add($"{item.ItemId} | {item.Name}: {item.Number}");
            }
        }

        private void SaveChanges(object sender, RoutedEventArgs e) 
        {
            _accessor.SaveItems();
        }
    }
}
