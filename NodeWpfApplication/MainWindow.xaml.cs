using NodeManagement;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
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

namespace NodeWpfApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private uint NodeId;
        private string City;
        private NodeManager nodeManager;

        private ObservableCollection<INode> NodeListViewItems;

        public MainWindow()
        {
            InitializeComponent();
            //Initialize NodeManager
            nodeManager = new NodeManager();
            //Initialize NodeListViewItems
            NodeListViewItems = new ObservableCollection<INode>();
            //assign NodeListViewItems to NodeListView
            NodeListView.ItemsSource = NodeListViewItems;
            
        }

        //move window around
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            // Begin dragging the window
            this.DragMove();
        }

        //click to show add node area
        private void ShowAddNodeArea_Click(object sender, RoutedEventArgs e)
        {
            AddNodeAreaGrid.Visibility = Visibility.Visible;
            EditNodeAreaStackPanel.Visibility = Visibility.Collapsed;
            RemoveNodeAreaStackPanel.Visibility = Visibility.Collapsed;
            //give the button border color when is actived
            AddButtonBorder.BorderBrush = new SolidColorBrush(Colors.White);
            AddButtonBorder.BorderThickness = new Thickness(0, 0, 0, 3);
            RemoveButtonBorder.BorderBrush = new SolidColorBrush(Colors.White);
            RemoveButtonBorder.BorderThickness = new Thickness(0, 0, 0, 0);
            SetOnlineButtonBorder.BorderBrush = new SolidColorBrush(Colors.White);
            SetOnlineButtonBorder.BorderThickness = new Thickness(0, 0, 0, 0);
        }
        //click to show remove node area
        private void ShowRemoveNodeArea_Click(object sender, RoutedEventArgs e)
        {
            AddNodeAreaGrid.Visibility = Visibility.Collapsed;
            EditNodeAreaStackPanel.Visibility = Visibility.Collapsed;
            RemoveNodeAreaStackPanel.Visibility = Visibility.Visible;
            //get all nodes to combobox
            foreach (var item in NodeListViewItems)
            {
                RemoveComboBox.Items.Add(item.NodeId);
            }
            //give the button border color when is actived
            AddButtonBorder.BorderBrush = new SolidColorBrush(Colors.White);
            AddButtonBorder.BorderThickness = new Thickness(0, 0, 0, 0);
            RemoveButtonBorder.BorderBrush = new SolidColorBrush(Colors.White);
            RemoveButtonBorder.BorderThickness = new Thickness(0, 0, 0, 3);
            SetOnlineButtonBorder.BorderBrush = new SolidColorBrush(Colors.White);
            SetOnlineButtonBorder.BorderThickness = new Thickness(0,0,0,0);
        }
        //click to show edit node area
        private void ShowEditNodeArea_Click(object sender, RoutedEventArgs e)
        {
            AddNodeAreaGrid.Visibility = Visibility.Collapsed;
            EditNodeAreaStackPanel.Visibility = Visibility.Visible;
            RemoveNodeAreaStackPanel.Visibility = Visibility.Collapsed;
            //get all nodes to combobox
            foreach (var item in NodeListViewItems)
            {
                EditComboBox.Items.Add(item.NodeId);
            }
            //give the button border color when is actived
            AddButtonBorder.BorderBrush = new SolidColorBrush(Colors.White);
            AddButtonBorder.BorderThickness = new Thickness(0, 0, 0, 0);
            RemoveButtonBorder.BorderBrush = new SolidColorBrush(Colors.White);
            RemoveButtonBorder.BorderThickness = new Thickness(0, 0, 0, 0);
            SetOnlineButtonBorder.BorderBrush = new SolidColorBrush(Colors.White);
            SetOnlineButtonBorder.BorderThickness = new Thickness(0, 0, 0, 3);
        }
        //add node button
        private void AddNodeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //local variables
                var rnd = new Random();
                //set NodeId auto increase
                NodeId++;
                //get city from user input
                City = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(CityTextBox.Text); //capitalize the first letter 
                
                //add a new node
                nodeManager.AddNode(new Node(NodeId, City, rnd));
                var node = nodeManager.GetNode(NodeId);
                //assign node to NodeListViewItems
                NodeListViewItems.Add(node);

                //celar CityTextBox after noded added
                CityTextBox.Text = "";
            }
            catch (Exception er)
            {
                MessageBox.Show("Error:\n" + er);
            }
            
        }
        //cancel addnode button
        private void CancelNodeButton_Click(object sender, RoutedEventArgs e)
        {
            //celar CityTextBox
            CityTextBox.Text = "";
            //hide add node area
            AddNodeAreaGrid.Visibility = Visibility.Collapsed;
        }

        //exit the application
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        //remove node button
        private void RemoveNodeButton_Click(object sender, RoutedEventArgs e)
        {
            //get id from combobox
            var id = Convert.ToUInt32( RemoveComboBox.SelectedValue);
            Debug.WriteLine(id);
            //remove id from combobox
            RemoveComboBox.Items.Remove(RemoveComboBox.SelectedValue);
            //defince a node object by id
            var node = nodeManager.GetNode(id);

            //remove node from nodeManager
            nodeManager.RemoveNode(id);

            //remove node from NodeListViewItems
            NodeListViewItems.Remove(node);
        }
        //cancel removenode button
        private void CancelRemoveNodeButton_Click(object sender, RoutedEventArgs e)
        {
            RemoveNodeAreaStackPanel.Visibility = Visibility.Collapsed;
        }

        // edit node button
        private void CancelEditNodeButton_Click(object sender, RoutedEventArgs e)
        {
            EditNodeAreaStackPanel.Visibility = Visibility.Collapsed;
        }
        //cancel editnode button
        private void EditNodeButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
