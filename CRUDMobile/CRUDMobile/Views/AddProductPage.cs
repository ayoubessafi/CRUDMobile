using CRUDMobile.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace CRUDMobile.Views
{
    public class AddProductPage : ContentPage
    {
        private Entry _nameEntry;
        private Entry _priceEntry;
        private Button _saveButton;

        string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "DBProductMobile.db3");

        public AddProductPage()
        {
            this.Title = "Select Option";

            StackLayout stackLayout = new StackLayout();

            _nameEntry = new Entry();
            _nameEntry.Keyboard = Keyboard.Text;
            _nameEntry.Placeholder = "Product Name";
            stackLayout.Children.Add(_nameEntry);

            _priceEntry = new Entry();
            _priceEntry.Keyboard = Keyboard.Text;
            _priceEntry.Placeholder = "Price";
            stackLayout.Children.Add(_priceEntry);

            _saveButton = new Button();
            _saveButton.Text = "Add";
            _saveButton.Clicked += _saveButton_Clicked;
            stackLayout.Children.Add(_saveButton);

            Content = stackLayout;
        }

        private async void _saveButton_Clicked(object sender, EventArgs e)
        {
            var db = new SQLiteConnection(_dbPath);
            db.CreateTable<Product>();

            var maxPK = db.Table<Product>().OrderByDescending(p => p.Id).FirstOrDefault();

            Product product = new Product()
            {
                Id = (maxPK == null ? 1 : maxPK.Id + 1),
                Name = _nameEntry.Text,
                Price = float.Parse(_priceEntry.Text)
            };

            db.Insert(product);
            await DisplayAlert(null, product.Name + " saved", "ok");
            await Navigation.PopAsync();
        }
    }
}