using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Microsoft.EntityFrameworkCore;
using ShopDesktop.DBModels;
using ShopDesktop.Models;
using Exception = System.Exception;

namespace ShopDesktop;

// TODO now after update any in DB, and then select this we have old value, need to take new value with out reloading app

public class ConnectionBD
{
    public static void FirstDBPickConnection()
    {
        try { Helper.Database.Roles.ToList(); }
        catch {}
    }
    
    public static string? CreateUser(string userName, string userEmail, string userPassword)
    { 
        User user = ModelConstructors.createUser(userName, userPassword, userEmail);
        Helper.Database.Users.Add(user);
        try { Helper.Database.SaveChanges(); }
        catch (DbUpdateException ex) { return ex.InnerException.Message; }

        return null;
    }

    public static string? createUserRole(UserRole userRole)
    {
        Helper.Database.UserRoles.Add(userRole);
        try { Helper.Database.SaveChanges(); }
        catch (DbUpdateException ex) { return ex.InnerException.Message; }
        return null;
    }

    public static User GetUser(string userEmail)
    {
        try
        {
            List<User> users = Helper.Database.Users.ToList();
            foreach (User user in users)
            {
                if (user.UserEmail == userEmail)
                {
                    return user;
                }
            }
            return null;
        }
        catch (DbUpdateException ex)
        {
            return null;
        }
    }

    public static string[] GetUserRoles(int user_id)
    {
        try
        {
            IEnumerable<int?> roles_from_user = Helper.Database.UserRoles.
                Where(role => role.UserId == user_id).ToList().Select(x => x.RoleId);
            string[] roles = (
                from role in Helper.Database.Roles
                where roles_from_user.Contains(role.RoleId)
                select role.RoleName
            ).ToArray();
            return roles;
        }
        catch
        {
            return null;
        }
    }

    public static bool ChangeProfilePhoto(string photoPath)
    {
        try
        {
            var user = Helper.Database.Users.FirstOrDefault(u => u.UserId == SessionData.registeredUser.UserId);
            if (user != null)
            {
                byte[] photoBytes = File.ReadAllBytes(photoPath); // Read file from bytea

                user.Photo = photoBytes; // Update field, which contains photo in bytea
                Helper.Database.SaveChanges();
            }
            else
            {
                return false; // This should never happens
            }
        }
        catch
        {
            return false;
        }
        return true;
    }
    
    public static Image GetUserProfilePhoto()
    {
        try
        {
            if (SessionData.registeredUser.Photo != null)
            {
                // Преобразуем массив байтов в изображение
                var image = new Image();
                using (var stream = new MemoryStream(SessionData.registeredUser.Photo))
                {
                    var bitmap = new Bitmap(stream);
                    image.Source = bitmap;
                }
                return image;
            }
        }
        catch
        {
            return null;
            // Обработка ошибок при получении изображения из базы данных
        }
        return null;
    }

    public static string? UpdateUserName(string nickName)
    {
        try
        {
            var user = Helper.Database.Users.FirstOrDefault(u => u.UserId == SessionData.registeredUser.UserId);
            if (user != null)
            {
                user.UserName = nickName; // Update field, which contains photo in bytea
                Helper.Database.SaveChanges();
                return null;
            }
        }
        catch (DbUpdateException ex)
        {
            return ex.InnerException.Message;
        }
        return null;
    }
    
    public static string? UpdateUserBirthdate(DateOnly birthdate)
    {
        try
        {
            var user = Helper.Database.Users.FirstOrDefault(u => u.UserId == SessionData.registeredUser.UserId);
            if (user != null)
            {
                user.Birthdate = birthdate; 
                Helper.Database.SaveChanges();
            }
        }
        catch (DbUpdateException ex)
        {
            return ex.InnerException.Message;
        }
        return null;
    }
    
    public static string? UpdateUserPassword(string password)
    {
        try
        {
            var user = Helper.Database.Users.FirstOrDefault(u => u.UserId == SessionData.registeredUser.UserId);
            if (user != null)
            {
                user.UserPassword = password;
                Helper.Database.SaveChanges();
            }
        }
        catch (DbUpdateException ex)
        {
            return ex.InnerException.Message;
        }
        return null;
    }

    public static List<Product> GetSellerProducts(int userId)
    {
        List<Product> products = Helper.Database.Products
            .Include(p => p.ProductImages)
            .Where(p => p.ProductAuthor == userId)
            .ToList();

        return products;
    }
    
    public static ObservableCollection<Productstype> GetAllProductsTypes()
    {
        List<Productstype> products = Helper.Database.Productstypes.ToList();
        ObservableCollection<Productstype> observableProducts = new ObservableCollection<Productstype>(products);

        return observableProducts;
    }

    public static Product? CreateNewProduct(Product product)
    {
        try
        {
            Product createdProduct = Helper.Database.Products.Add(product).Entity;
            Helper.Database.SaveChanges();
            return createdProduct;
        }
        catch (DbUpdateException ex)
        {
            Console.WriteLine(ex.InnerException.Message);
            return null;
        }
    }

    public static void AddNewProductImage(int productAddingImageId, byte[] productSavePhotoPhoto)
    {
        try
        {
            ProductImage productImage = new ProductImage();
            productImage.ProductId = productAddingImageId;
            productImage.ProductImg = productSavePhotoPhoto;
            Helper.Database.ProductImages.Add(productImage);
            Helper.Database.SaveChanges();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Something went wrong by loading product photo! - " + ex.Message);
        }
    }

    public static void DeleteProduct(Product product)
    {
        try
        {
            Helper.Database.Products.Remove(product);
            Helper.Database.SaveChanges();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Something went wrong by loading product photo! - " + ex.Message);
        }
    }
}