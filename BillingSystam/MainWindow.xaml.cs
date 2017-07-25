using System;
using System.Collections.Generic;
using System.IO;
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

namespace BillingSystam
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Dictionary<string, string> childWindowDictionary = new Dictionary<string, string>();
        
        //List<object> objList = new List<object>();
        //Purchase objPurchase = new Purchase();
        //Sale objSale = new Sale();
        //Search objSearch = new Search();
        //Add objAdd = new Add();
        //TabItem tabPurchase = new TabItem();
        //TabItem tabSale = new TabItem();
        //TabItem tabSearch = new TabItem();
        //TabItem tabAdd = new TabItem();

        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
            //bImage.ImageSource = new BitmapImage(new Uri(@"BillingSystam\Images\logo PTS new.jpg", UriKind.Relative));
        }

        public MainWindow(string username)
        {
            InitializeComponent();
            //bImage.ImageSource = new BitmapImage(new Uri(@"logo PTS new.jpg", UriKind.Relative));

        }
        #endregion

        #region Window Events
        private void MenuPurchase_Click_1(object sender, RoutedEventArgs e)
        {
            Purchase objPurchase = new Purchase("Purchase", "Purchase");
            AddChildWindow(objPurchase);
        }

        private void MenuSale_Click_1(object sender, RoutedEventArgs e)
        {
            Sale objSale = new Sale("Sale", "Sale");
            AddChildWindow(objSale);

        }

        private void MenuSearch_Click_1(object sender, RoutedEventArgs e)
        {
            Search objSearch = new Search("Search", "Search");
            AddChildWindow(objSearch);
        }

       
        #endregion

        #region private methods

        private void AddChildWindow(ITabbedWindow childWindow)
        {
            if (childWindowDictionary.ContainsKey(childWindow.TabName))
            {
                foreach (TabItem  tabItem in tabChildContainer.Items)
	            {
                    if(tabItem.Name==childWindow.TabName)
                    {
                        tabItem.Focus();
                        break;
                    }
		 
	            }
            }
            else
            {
                TabItem tabItam=new TabItem();
                tabItam.Name=childWindow.TabName;
                tabItam.Header = childWindow.TabName.Substring(0, 1);
                
                tabItam.Content=childWindow;
                tabChildContainer.Items.Add(tabItam);
                tabChildContainer.SelectedItem=tabItam;

                //attach the event handler
                childWindow.CloseInitiated +=new CloseTab(childWindow_CloseInitiated);

                //add to tab item name and title to dictionary
                childWindowDictionary.Add(childWindow.TabName,childWindow.TabTitle);
            }

            //to show and hide 'PTS' logo when no tab is open
            if (tabChildContainer.Items.Count > 0)
            {
                tabChildContainer.ClearValue(TabControl.BackgroundProperty);
            }
        }

        private void childWindow_CloseInitiated(ITabbedWindow childWindow, EventArgs e)
        {
            foreach (TabItem item in tabChildContainer.Items)
            {
                if (item.Name == childWindow.TabName)
                {
                    tabChildContainer.Items.Remove(item);
                    childWindowDictionary.Remove(childWindow.TabName);
                    break;
                }

            }

            if (tabChildContainer.Items.Count == 0)
            {
                ImageBrush img=new ImageBrush(new BitmapImage(new Uri(@"Images\logo PTS new.jpg",UriKind.Relative)));
                
                tabChildContainer.Background = img;
            }
           
        }

        #endregion

        private void MenuLogOut_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
