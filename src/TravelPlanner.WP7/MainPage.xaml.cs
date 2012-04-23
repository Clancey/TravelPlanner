using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using TravelPlanner.HotelSearch;

namespace TravelPlanner.WP7
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
           // DataContext = App.ViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            ProgressBar.Visibility  = Visibility.Collapsed;
            hotelSearchResultsListBox.Visibility = Visibility.Collapsed;
            SearchExpander.IsExpanded = true;
            hotelCheckIn.Value = DateTime.Today;
            hotelCheckOut.Value = DateTime.Today.AddDays(1);
            hotelDestiantion.AcceptsReturn = false;


            //   if (!App.ViewModel.IsDataLoaded)
            // {
            //   App.ViewModel.LoadData();
            // }
        }
         private void Results_SelectionChanged(object sender, SelectionChangedEventArgs e)
         {

         }
         private void Go_Click(object sender, RoutedEventArgs e)
         {
             hotelSearchResultsListBox.Visibility = Visibility.Collapsed;
             GoButton.IsEnabled = false;
             ProgressBar.Visibility = System.Windows.Visibility.Visible;
             var searchString = String.Format (Constants.HotelSearchUrl,
												new object[]{hotelDestiantion.Text
														,  ((DateTime)hotelCheckIn.Value).ToString("MM/dd/yyyy")
														, ((DateTime)hotelCheckOut.Value).ToString ("MM/dd/yyyy")
														, (hotelRooms.SelectedItem as ListPickerItem).Content
														, (hotelAdults.SelectedItem as ListPickerItem).Content
														, (hotelChildren.SelectedItem as ListPickerItem).Content});
             ThreadPool.QueueUserWorkItem(delegate
                                              {

                                                  var results = DataAccess.FetchHotelSearchResults(searchString);
                                                  Dispatcher.BeginInvoke(delegate
                                                                             {
                                                                                 reloadData(results);
                                                                             });
                                              });
         }

        private void reloadData(HotelSearchResults result)
        {

            hotelSearchResultsListBox.Visibility = Visibility.Visible;
            hotelSearchResultsListBox.Items.Clear();
            SearchExpander.IsExpanded = false;
            foreach (var deal in result.Results)
            {
                hotelSearchResultsListBox.Items.Add(deal);
            }
            GoButton.IsEnabled = true;
            ProgressBar.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void toggleHotelSearch_Click(object sender, RoutedEventArgs e)
        {
            SearchExpander.IsExpanded = !SearchExpander.IsExpanded;
        }
    }
}