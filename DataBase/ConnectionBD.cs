using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Npgsql;
using Shop.Exceptions;
using ShopDesktop;
using ShopDesktop.DBModels;
using ShopDesktop.Models;

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
       
            DBUser registeredDbUser = new DBUser(
                Helper.Database.Users.ToList().Select(c => c.UserId).Distinct().ToList().Max() + 1,
                userName,
                userPassword,
                userEmail,
                null,
                null,
                null
            );
            Helper.Database.Users.Add(registeredDbUser);
            try
            {
                Helper.Database.SaveChanges();
                SessionData.registeredUser = registeredDbUser;
            }
            catch (DbUpdateException ex)
            {
                return ex.InnerException.Message;
            }
            return null;
    }

    public static DBUser GetUser(string userEmail)
    {
        try
        {
            List<User> users = Helper.Database.Users.ToList();
            foreach (User user in users)
            {
                if (user.UserEmail == userEmail)
                {
                    return new DBUser(user);
                }
            }
            return null;
        }
        catch (DbUpdateException ex)
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
    
    public static string? UpdateUserAge(int age)
    {
        try
        {
            var user = Helper.Database.Users.FirstOrDefault(u => u.UserId == SessionData.registeredUser.UserId);
            if (user != null)
            {
                user.Age = age; // Update field, which contains photo in bytea
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
}