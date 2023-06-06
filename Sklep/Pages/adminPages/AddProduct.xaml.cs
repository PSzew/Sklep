using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;
using Sklep.entity;
using Sklep.Repsoitory;
using Sklep.Entity;

namespace Sklep.Pages.adminPages
{
    /// <summary>
    /// Logika interakcji dla klasy AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Page
    {
        string imgPath = "";
        bool isImageUploaded;
        bool isInEditMode = false;
        Product toEdit;
        public AddProduct()
        {
            InitializeComponent();
            CategoryRepository cr = new CategoryRepository();
            cat.ItemsSource = cr.GetAllCategory();
        }
        public AddProduct(Product p)
        {
            InitializeComponent();
            CategoryRepository cr = new CategoryRepository();
            cat.ItemsSource = cr.GetAllCategory();
            isInEditMode = true;
            title.Text = p.Title;
            descrition.Text = p.Description;
            price.Text = p.Price.ToString();
            quantity.Text = p.Quantity.ToString();
            cat.SelectedItem = p.Category;
            toEdit = p;
        }

        private void uploadIMG(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Pliki obrazów (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png|Wszystkie pliki (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                string selectedImagePath = openFileDialog.FileName;

                string projectPath = System.IO.Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
                string finalpath = System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(projectPath)));
                string destinationFolderPath = finalpath + @"\Images\\ProductImages";
                string destinationFilePath = System.IO.Path.Combine(destinationFolderPath, System.IO.Path.GetFileName(selectedImagePath)); 
                if(!File.Exists(destinationFilePath)) 
                {
                    File.Copy(selectedImagePath, destinationFilePath, true);
                    imgPath = destinationFilePath;
                    Uploadbtn.Content = "Dodano";
                    Uploadbtn.IsHitTestVisible = false;
                    isImageUploaded = true;
                    imgPath = @"\Images\ProductImages\"+ System.IO.Path.GetFileName(destinationFilePath);  
                }
                else
                {
                    MessageBox.Show("Taki plik już istnieje!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    imgPath = destinationFilePath;
                    Uploadbtn.Content = "Dodano";
                    Uploadbtn.IsHitTestVisible = false;
                    isImageUploaded = true;
                    imgPath = @"\Images\ProductImages\" + System.IO.Path.GetFileName(destinationFilePath);
                }
                    
                
            }
        }

        private void addProductClick(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(title.Text) && !string.IsNullOrEmpty(descrition.Text) && !string.IsNullOrEmpty(price.Text) && !string.IsNullOrEmpty(quantity.Text) && cat.SelectedItem != null && Int32.TryParse(quantity.Text,out int quan)&& Decimal.TryParse(price.Text,out decimal p))
            {
                if(isInEditMode)
                {
                    int categ;
                    category CategoryTmp = (category)cat.SelectedItem;
                    categ = (int)CategoryTmp.ID;
                    Product product = toEdit;
                    if (isImageUploaded)
                    {
                        product = new Product(title.Text, decimal.Parse(price.Text), int.Parse(quantity.Text), descrition.Text, categ, imgPath);
                    }
                    else
                    {
                        string fileName = System.IO.Path.GetFileName(toEdit.Image.UriSource.LocalPath);
                        string finalImage = @"\Images\ProductImages\" + fileName;
                        Trace.WriteLine(finalImage);
                        product = new Product(title.Text, decimal.Parse(price.Text), int.Parse(quantity.Text), descrition.Text, categ, finalImage);
                    }               
                    ProductRepository pr = new ProductRepository();
                    pr.Update(toEdit,product);
                    mainFrame.Content = new ProductManagment();
                }
                else if(isImageUploaded)
                {
                    int categ;
                    category CategoryTmp = (category)cat.SelectedItem;
                    categ = (int)CategoryTmp.ID;
                    Product product = new Product(title.Text, decimal.Parse(price.Text), int.Parse(quantity.Text), descrition.Text, categ, imgPath);
                    ProductRepository pr = new ProductRepository();
                    pr.Insert(product);
                    mainFrame.Content = new AdminPage();
                }
                else
                {
                    MessageBox.Show("Wgraj zdjęcie!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Podaj prawidłowe dane", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
