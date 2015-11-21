﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace MUTools
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DBItems current_item;
        private DBItemCategories current_category;

        public MainWindow()
        {
            InitializeComponent();
            fill_categories();
            DBLoader.buildDBItems("item.txt");
        }

        public void fill_categories()
        {
            using (DBConnection conn = new DBConnection())
            {
                var categories = from e in conn.categories
                                 select e;
                items_list.ItemsSource = categories.ToList();
            }
        }

        public void getItemList(int category_id)
        {
            using (DBConnection conn = new DBConnection())
            {
               // Debug.WriteLine("CATEGORY: " + category_id);
                var items = from e in conn.items
                            where e.category_ID == category_id
                            select e;
                var category    =   from e in conn.categories
                                    where e.ID == category_id
                                    select e;

                items_list.ItemsSource = items.ToList();
               // current_category = category.First();
            }
        }


        private void items_list_selectChange(object sender, SelectionChangedEventArgs e)
        {
            if (items_list.SelectedItem != null)
            {
                if (current_category == null)
                {
                    current_category =  (DBItemCategories)items_list.SelectedItems[0];
                    category_label.Content = current_category.name;
                    getItemList(current_category.ID);
                    categoryBackButton.Visibility = Visibility.Visible;
                }

                else
                {
                    current_item = (DBItems)items_list.SelectedItems[0];
                    item_image_name_label.Content = current_item.name;
                    current_item_image.Source = new BitmapImage(new Uri(@"/images/items/" + current_item.image_path, UriKind.Relative));
                }
            }

            if(current_category == null) items_list.UnselectAll();
            

        }

        private void category_goBack(object sender, RoutedEventArgs e)
        {
            categoryBackButton.Visibility = Visibility.Hidden;
            current_category = null;
            fill_categories();
        }
    }
}