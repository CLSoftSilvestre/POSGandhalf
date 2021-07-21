using System;
namespace POSClasses
{
    public class Stock
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public float Quantity { get; set; }
        public DateTime LastUpdate { get; set; }

        public Stock()
        {
        }

        public void UpdateQuantity(float qtd)
        {
            if( Quantity != qtd)
            {
                Quantity = qtd;
                LastUpdate = DateTime.Now;
            }  
        }

        public void AddToStock(float qtd)
        {
            if (qtd > 0)
            {
                Quantity += qtd;
                LastUpdate = DateTime.Now;
            }
        }

        public bool RemoveFromStock(float qtd)
        {
            if( Quantity - qtd >= 0)
            {
                Quantity -= qtd;
                LastUpdate = DateTime.Now;
                return true;
            } else
            {
                return false;
            }
        }
    }
}
