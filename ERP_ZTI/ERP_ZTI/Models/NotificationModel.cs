using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_ZTI.Models
{
    public class NotificationModel
    {
        public List<Models.Products> smallAmount { get; set; }
        public List<Models.Products> zeroAmount { get; set; }

        //Constructor
        public NotificationModel()
        {
            //Instantiate the lists
            this.smallAmount = new List<Products>();
            this.zeroAmount = new List<Products>();
            //Get amounts of all products
            Models.ERP_DBEntities db = new Models.ERP_DBEntities();
            var products = from p in db.Products
                          select p;
            //Fulfill the propeties of class  
            foreach(Products product in products)
            {
                int amount;
                int.TryParse(product.Amount,out amount);
                if (amount == 0)
                {
                    this.zeroAmount.Add(product);
                    
                }
                else if(amount < 5)
                {
                    this.smallAmount.Add(product);
                }
            }
        }
    }
}