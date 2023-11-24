using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Shop.DBModels;
using Shop.Exceptions;
using ShopDesktop;
using ShopDesktop.Models;

namespace Shop;

public class ConnectionBD
{
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
}