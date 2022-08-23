using System;
using EF_Northwind_Joins.Models;
using Microsoft.EntityFrameworkCore;

namespace EF_Northwind_Joins
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var db = new NorthwindContext();

            //Ex1
            //C# Syntax
            var productList1 = db.Products.Join(db.Products,
                x => x.ProductId,
                x => x.ProductId,
                (pName, pQuantity) => new
                {
                    pName.ProductName,
                    pQuantity.QuantityPerUnit
                }
                ).ToList();

            //Query Syntax
            var productListQuerySyntax1 = (from product in db.Products
                                           join pQuantity in db.Products
                                           on product.ProductId equals pQuantity.ProductId
                                           select new
                                           {
                                               productName = product.ProductName,
                                               quantityPerUnit = pQuantity.QuantityPerUnit
                                           }).ToList();



            //Ex2
            //C# Syntax
            var productList2 = db.Products.Where(x => x.Discontinued == true).Join(db.Products,
                x => x.ProductId,
                x => x.ProductId,
                (pID, pName) => new
                {
                    pID.ProductId,
                    pName.ProductName
                }
                ).ToList();

            //Query Syntax
            var productListQuerySyntax2 = (from productID in db.Products
                                           join productName in db.Products
                                           on productID.ProductId equals productName.ProductId
                                           where productName.Discontinued == true
                                           select new
                                           {
                                               productID.ProductId,
                                               productName.ProductName

                                           }).ToList();



            //Ex3

            //C# Syntax
            var productList3 = db.Products.Where(x => x.Discontinued == false).Join(db.Products,
                x => x.ProductId,
                x => x.ProductId,
                (pID, pName) => new
                {
                    pID.ProductId,
                    pName.ProductName
                }
                ).ToList();

            //Query Syntax
            var productListQuerySyntax3 = (from productID in db.Products
                                           join productName in db.Products
                                           on productID.ProductId equals productName.ProductId
                                           where productName.Discontinued == false
                                           select new
                                           {
                                               productID.ProductId,
                                               productName.ProductName

                                           }).ToList();


            //Ex4
            //C# Syntax
            var productList4 = db.Products.Join(db.Products,
                          x => x.ProductId,
                          x => x.ProductId,
                          (pUnitPrice, pName) => new
                          {
                              pName.ProductName,
                              pUnitPrice.UnitPrice
                          }
                          ).OrderByDescending(x => x.UnitPrice).ToList();

            //Query Syntax
            var productListQuerySyntax4 = (from productUnitPrice in db.Products
                                           join productName in db.Products
                                           on productUnitPrice.ProductId equals productName.ProductId
                                           orderby productUnitPrice.UnitPrice descending
                                           select new
                                           {
                                               productName.ProductName,
                                               productUnitPrice.UnitPrice,

                                           }).ToList();


            //Ex5
            //C# Syntax

            var productList5 = db.Products.Where(x => x.UnitPrice < 20).Join(db.Products,
                 x => x.ProductId,
                 x => x.ProductId,
                 (pUnitPrice, pName) => new
                 {
                     pName.ProductId,
                     pName.ProductName,
                     pUnitPrice.UnitPrice
                 }
                 ).OrderByDescending(x => x.UnitPrice).ToList();

            //Query Syntax
            var productListQuerySyntax5 = (from productUnitPrice in db.Products
                                           where productUnitPrice.UnitPrice < 20
                                           join productName in db.Products
                                           on productUnitPrice.ProductId equals productName.ProductId
                                           orderby productUnitPrice.UnitPrice descending
                                           select new
                                           {
                                               productName.ProductId,
                                               productName.ProductName,
                                               productUnitPrice.UnitPrice,

                                           }).ToList();


            //Ex6
            //C# Syntax

            var productList6 = db.Products.Where(x => x.UnitPrice >= 15 && x.UnitPrice <= 25).Join(db.Products,
                 x => x.ProductId,
                 x => x.ProductId,
                 (pUnitPrice, pName) => new
                 {
                     pName.ProductId,
                     pName.ProductName,
                     pUnitPrice.UnitPrice
                 }
                 ).OrderByDescending(x => x.UnitPrice).ToList();

            //Query Syntax
            var productListQuerySyntax6 = (from productUnitPrice in db.Products
                                           where productUnitPrice.UnitPrice >= 15 && productUnitPrice.UnitPrice <= 25
                                           join productName in db.Products
                                           on productUnitPrice.ProductId equals productName.ProductId
                                           orderby productUnitPrice.UnitPrice descending
                                           select new
                                           {
                                               productName.ProductId,
                                               productName.ProductName,
                                               productUnitPrice.UnitPrice,

                                           }).ToList();


            //Ex7
            //C# Syntax
            var productList7 = db.Products.Where(x => x.UnitPrice > db.Products.Average(x => x.UnitPrice)).Join(db.Products,
                 x => x.ProductId,
                 x => x.ProductId,
                 (pUnitPrice, pName) => new
                 {

                     pName.ProductName,
                     pUnitPrice.UnitPrice
                 }
                 ).OrderByDescending(x => x.UnitPrice).ToList();

            //Query Syntax
            var productListQuerySyntax7 = (from productUnitPrice in db.Products
                                           where productUnitPrice.UnitPrice > db.Products.Average(x => x.UnitPrice)
                                           join productName in db.Products
                                           on productUnitPrice.ProductId equals productName.ProductId
                                           orderby productUnitPrice.UnitPrice descending
                                           select new
                                           {

                                               productName.ProductName,
                                               productUnitPrice.UnitPrice,

                                           }).ToList();

            //Ex8
            //C# Syntax
            var productList8 = db.Products.Join(db.Products,
               x => x.ProductId,
               x => x.ProductId,
               (pUnitPrice, pName) => new
               {
                   pName.ProductName,
                   pUnitPrice.UnitPrice
               }
               ).OrderByDescending(x => x.UnitPrice).Take(10).ToList();

            //Query Syntax
            var productListQuerySyntax8 = (from productUnitPrice in db.Products
                                           where productUnitPrice.UnitPrice > db.Products.Average(x => x.UnitPrice)
                                           join productName in db.Products
                                           on productUnitPrice.ProductId equals productName.ProductId
                                           orderby productUnitPrice.UnitPrice descending
                                           select new
                                           {

                                               productName.ProductName,
                                               productUnitPrice.UnitPrice,

                                           }).Take(10).ToList();

            //Ex9
            //C# Syntax
            var productList9 = db.Products.GroupBy(x => x.Discontinued)
                .Select(g => new
                {
                    productDiscontiued = g.Key,
                    Count = g.Count()
                }).ToList();

            //Query Syntax
            var productListQuerySyntax9 = (from p in db.Products
                                           group p by p.Discontinued into g
                                           select new
                                           {
                                               productDiscontiued = g.Key,
                                               Count = g.Count()
                                           }).ToList();




            //Ex10

            //C# Syntax
            var productList10 = db.Products.Where(x => x.UnitsInStock < x.UnitsOnOrder).Join(db.Products,
               x => x.ProductId,
               x => x.ProductId,
               (pUnitPrice, pName) => new
               {
                   pName.ProductName,
                   pUnitPrice.UnitsOnOrder,
                   pUnitPrice.UnitsInStock
               }
               ).ToList();

            //Query Syntax
            var productListQuerySyntax10 = (from productUnitPrice in db.Products
                                            where productUnitPrice.UnitsInStock < productUnitPrice.UnitsOnOrder
                                            join productName in db.Products
                                            on productUnitPrice.ProductId equals productName.ProductId
                                            select new
                                            {
                                                productName.ProductName,
                                                productName.UnitsOnOrder,
                                                productUnitPrice.UnitsInStock,

                                            }).ToList();

        }
    }
}
