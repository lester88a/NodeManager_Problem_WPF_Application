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
        private uint _NodeId;
        private string _City;
        private uint _ConnectedClients;

        private NodeManager _NodeManager;

        private ObservableCollection<INode> _NodeListViewItems;

        public MainWindow()
        {
            InitializeComponent();
            //Initialize NodeManager
            _NodeManager = new NodeManager();
            //Initialize NodeListViewItems
            _NodeListViewItems = new ObservableCollection<INode>();
            //assign NodeListViewItems to NodeListView
            NodeListView.ItemsSource = _NodeListViewItems;
            
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
            SetMetricsAreaStackPanel.Visibility = Visibility.Collapsed;
            //give the button border color when is actived
            AddButtonBorder.BorderBrush = new SolidColorBrush(Colors.White);
            AddButtonBorder.BorderThickness = new Thickness(0, 0, 0, 3);
            RemoveButtonBorder.BorderBrush = new SolidColorBrush(Colors.White);
            RemoveButtonBorder.BorderThickness = new Thickness(0, 0, 0, 0);
            SetOnlineButtonBorder.BorderBrush = new SolidColorBrush(Colors.White);
            SetOnlineButtonBorder.BorderThickness = new Thickness(0, 0, 0, 0);
            SetMetricsButtonBorder.BorderBrush = new SolidColorBrush(Colors.White);
            SetMetricsButtonBorder.BorderThickness = new Thickness(0, 0, 0, 0);
        }
        //click to show remove node area
        private void ShowRemoveNodeArea_Click(object sender, RoutedEventArgs e)
        {
            AddNodeAreaGrid.Visibility = Visibility.Collapsed;
            EditNodeAreaStackPanel.Visibility = Visibility.Collapsed;
            RemoveNodeAreaStackPanel.Visibility = Visibility.Visible;
            SetMetricsAreaStackPanel.Visibility = Visibility.Collapsed;
            //get all nodes to combobox
            foreach (var item in _NodeListViewItems)
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
            SetMetricsButtonBorder.BorderBrush = new SolidColorBrush(Colors.White);
            SetMetricsButtonBorder.BorderThickness = new Thickness(0, 0, 0, 0);
        }
        //click to show edit node area
        private void ShowEditNodeArea_Click(object sender, RoutedEventArgs e)
        {
            AddNodeAreaGrid.Visibility = Visibility.Collapsed;
            EditNodeAreaStackPanel.Visibility = Visibility.Visible;
            RemoveNodeAreaStackPanel.Visibility = Visibility.Collapsed;
            SetMetricsAreaStackPanel.Visibility = Visibility.Collapsed;
            //get all nodes to combobox
            foreach (var item in _NodeListViewItems)
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
            SetMetricsButtonBorder.BorderBrush = new SolidColorBrush(Colors.White);
            SetMetricsButtonBorder.BorderThickness = new Thickness(0, 0, 0, 0);
        }
        //click to show metrics area
        private void ShowMetricsArea_Click(object sender, RoutedEventArgs e)
        {
            AddNodeAreaGrid.Visibility = Visibility.Collapsed;
            EditNodeAreaStackPanel.Visibility = Visibility.Collapsed;
            RemoveNodeAreaStackPanel.Visibility = Visibility.Collapsed;
            SetMetricsAreaStackPanel.Visibility = Visibility.Visible;
            //get all nodes to combobox
            foreach (var item in _NodeListViewItems)
            {
                SetMetricsComboBox.Items.Add(item.NodeId);
            }
            //give the button border color when is actived
            AddButtonBorder.BorderBrush = new SolidColorBrush(Colors.White);
            AddButtonBorder.BorderThickness = new Thickness(0, 0, 0, 0);
            RemoveButtonBorder.BorderBrush = new SolidColorBrush(Colors.White);
            RemoveButtonBorder.BorderThickness = new Thickness(0, 0, 0, 0);
            SetOnlineButtonBorder.BorderBrush = new SolidColorBrush(Colors.White);
            SetOnlineButtonBorder.BorderThickness = new Thickness(0, 0, 0, 0);
            SetMetricsButtonBorder.BorderBrush = new SolidColorBrush(Colors.White);
            SetMetricsButtonBorder.BorderThickness = new Thickness(0, 0, 0, 3);
        }


        //add node button
        private void AddNodeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //local variables
                var rnd = new Random();
                //set NodeId auto increase
                _NodeId++;
                //get city from user input
                _City = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(CityTextBox.Text); //capitalize the first letter 
                
                //add a new node
                _NodeManager.AddNode(new Node(_NodeId, _City, rnd));
                var node = _NodeManager.GetNode(_NodeId);
                //assign node to NodeListViewItems
                _NodeListViewItems.Add(node);

                //celar CityTextBox after noded added
                CityTextBox.Text = "";

                //show status info
                StatusInfoTextBlock.Text += "Node: "+ node.NodeId + " add successful!\n";
            }
            catch (Exception er)
            {
                //show status info
                StatusInfoTextBlock.Text += "Error:\n" + er + "\n";
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
            //remove id from combobox
            RemoveComboBox.Items.Remove(RemoveComboBox.SelectedValue);
            //defince a node object by id
            var node = _NodeManager.GetNode(id);

            //remove node from nodeManager
            _NodeManager.RemoveNode(id);

            //remove node from NodeListViewItems
            _NodeListViewItems.Remove(node);

            //show status info
            StatusInfoTextBlock.Text += "Node: " + node.NodeId + " removed successful!\n";
        }
        //cancel removenode button
        private void CancelRemoveNodeButton_Click(object sender, RoutedEventArgs e)
        {
            RemoveNodeAreaStackPanel.Visibility = Visibility.Collapsed;
        }

        //cancel editnode button
        private void CancelEditNodeButton_Click(object sender, RoutedEventArgs e)
        {
            EditNodeAreaStackPanel.Visibility = Visibility.Collapsed;
        }
        // edit node button
        private void EditNodeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //get id from combobox
                var id = Convert.ToUInt32(EditComboBox.SelectedValue);

                if (EditComboBox.SelectedValue != null)
                {
                    // defince a node object by id
                    var node = _NodeManager.GetNode(id);

                    if (OnlineRadioButton.IsChecked == true)
                    {
                        //set the node online
                        node.SetOnline();

                        //refresh the list view after changed
                        NodeListView.Items.Refresh();

                        //alarm 
                        //nodeManager.AlarmUser(id,);

                    }
                    if (OfflineRadioButton.IsChecked == true)
                    {
                        //set the node offline
                        node.SetOffline();
                        //refresh the list view after changed
                        NodeListView.Items.Refresh();
                    }

                    //show status info
                    StatusInfoTextBlock.Text += "Node: " + node.NodeId + " update successful!\n";
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("Error:\n" + er);
                //show status info
                StatusInfoTextBlock.Text += "Error:\n" + er + "\n";
            }
            
        }
        //Set Metrics Button
        private void SetMetricsButton_Click(object sender, RoutedEventArgs e)
        {
            //get id from combobox
            var id = Convert.ToUInt32(SetMetricsComboBox.SelectedValue);
            var maxClients = Convert.ToUInt32(MaxConnectedClientsTextBox.Text);
            var maxUpload = (float)Convert.ToDecimal(MaxUploadTextBox.Text);
            var maxDownload = (float)Convert.ToDecimal(MaxDownloadTextBox.Text);
            var maxErrorRate = (float)Convert.ToDecimal(MaxErrorRateTextBox.Text);
            if (SetMetricsComboBox.SelectedValue != null)
            {
                // defince a node object by id
                var node = _NodeManager.GetNode(id);

                //Set MaxMetricsValuee
                node.SetMaxMetricsValue(maxClients, maxUpload, maxDownload, maxErrorRate);

                //refresh the list view after changed
                NodeListView.Items.Refresh();

                //show status info
                StatusInfoTextBlock.Text += "Node: " + node.NodeId + " set max value successful!\n";
            }
        }
        //cancel SetMetrics button
        private void CancelSetMetricsButton_Click(object sender, RoutedEventArgs e)
        {
            SetMetricsAreaStackPanel.Visibility = Visibility.Collapsed;
        }
    }
}
