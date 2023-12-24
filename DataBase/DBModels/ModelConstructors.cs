using System;
using ShopDesktop.Models;

namespace ShopDesktop.DBModels;

public class ModelConstructors
{
    public static Notification createNotification(int UserId, string NotificationText, int NotificationAuthor, DateTime NotificationData)
    {
        Notification notification = new Notification();
        notification.UserId = UserId;
        notification.NotificationText = NotificationText;
        notification.NotificationAuthor = NotificationAuthor;
        notification.NotificationData = NotificationData;
        notification.IsNew = true;
        return notification;
    }

    public static Product createProduct(string ProductName, int ProductType, float ProductPrice, string ProductDescription, int ProductCount)
    {
        Product product = new Product();
        product.ProductName = ProductName;
        product.ProductType = ProductType;
        product.ProductPrice = ProductPrice;
        product.ProductDescription = ProductDescription;
        product.ProductCount = ProductCount;
        return product;
    }

    public static ProductImage createProductImage(int ProductId, byte[] ProductImg)
    {
        ProductImage productImage = new ProductImage();
        productImage.ProductId = ProductId;
        productImage.ProductImg = ProductImg;
        return productImage;
    }

    public static Productstype createProductstype(string TypeName, string Description)
    {
        Productstype productstype = new Productstype();
        productstype.TypeName = TypeName;
        productstype.Description = Description;
        return productstype;
    }

    public static Role createRole(string RoleName, string RoleDescription)
    {
        Role role = new Role();
        role.RoleName = RoleName;
        role.RoleDescription = RoleDescription;
        return role;
    }

    public static Transaction createTransaction(int UserId, int ProductId, DateTime? TransactionData, int TransactionProductCount)
    {
        Transaction transaction = new Transaction();
        transaction.UserId = UserId;
        transaction.ProductId = ProductId;
        if (TransactionData != null) transaction.TransactionData = TransactionData;
        transaction.TransactionProductCount = TransactionProductCount;
        return transaction;
    }

    public static User createUser(string UserName, string UserPassword, string UserEmail)
    {
        User user = new User();
        user.UserName = UserName;
        user.UserPassword = UserPassword;
        user.UserEmail = UserEmail;
        return user;
    }

    public static UserRole createUserRole(int UserId, int RoleId)
    {
        UserRole userRole = new UserRole();
        userRole.UserId = UserId;
        userRole.RoleId = RoleId;
        return userRole;
    }
    
}